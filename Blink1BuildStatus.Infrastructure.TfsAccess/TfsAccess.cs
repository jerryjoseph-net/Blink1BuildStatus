using System.Collections.Generic;
using System.Linq;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;
using Blink1BuildStatus.Infrastructure.TfsAccess.Extensions;

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

        public string TfsInstance => _tfsApiClient.Instance;

        public IEnumerable<BuildStatus> GetLatestBuildStatuses(string projectName, IEnumerable<string> definitionIDs = null)
        {
            var tfsBuildsResponse = _tfsApiClient.GetBuildsAsync(projectName, definitionIDs).Result;

            var buildStatuses = new List<BuildStatus>();

            var relevantBuilds = tfsBuildsResponse.ExtractRelevantBuilds();

            // Select latest build for each definition ID in the result

            var definitionIdsInResult = relevantBuilds.Select(rb => rb.Definition.Id).Distinct();
            var latestBuilds = definitionIdsInResult.Select(di => relevantBuilds.First(rb => rb.Definition.Id == di)).ToList();

            foreach (var latestBuild in latestBuilds)
            {
                var buildStatus = latestBuild.MapToBuildStatus();

                buildStatuses.Add(buildStatus);
            }

            return buildStatuses;
        }
    }
}
