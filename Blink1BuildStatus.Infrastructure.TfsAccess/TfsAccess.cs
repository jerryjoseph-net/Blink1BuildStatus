using System.Collections.Generic;
using System.Linq;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    // TODO Fix bad programming by removing .Result using async await

    public class TfsAccess : ITfsAccess
    {
        private readonly TfsApiClient _tfsApiClient;

        public TfsAccess(TfsApiClient tfsApiClient)
        {
            _tfsApiClient = tfsApiClient;
        }

        public IEnumerable<BuildStatus> GetLatestBuildStatuses(string projectName, IEnumerable<string> definitionIDs = null)
        {
            var tfsBuildsResponse = _tfsApiClient.GetBuildsAsync(projectName, definitionIDs).Result;

            var buildStatuses = new List<BuildStatus>();

            var relevantBuilds = tfsBuildsResponse?.Value
                .Where(b => b.Status == TfsBuildStatus.InProgress || b.Status == TfsBuildStatus.Completed)
                .Where(b => b.Result != TfsBuildResult.Canceled);

            var definitionIds = relevantBuilds.Select(rb => rb.Definition.Id).Distinct();

            var latestBuilds = new List<TfsBuildItem>();

            foreach (var definitionId in definitionIds)
            {
                latestBuilds.Add(relevantBuilds.Where(rb => rb.Definition.Id == definitionId).First());
            }

            foreach (var latestBuild in latestBuilds)
            {
                var buildStatus = latestBuild.MapToBuildStatus();

                buildStatuses.Add(buildStatus);
            }

            return buildStatuses;
        }
    }
}
