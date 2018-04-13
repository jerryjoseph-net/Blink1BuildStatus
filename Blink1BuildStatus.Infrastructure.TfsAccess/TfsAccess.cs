using System.Collections.Generic;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    // TODO Fix bad programming by removing .Result using async await

    public class TfsAccess : ITfsAccess
    {
        private readonly TfsApiClient _tfsApiClient;
        private readonly string _projectName;
        private readonly List<string> _definitionIDs;

        public TfsAccess(TfsApiClient tfsApiClient, string projectName, List<string> definitionIDs)
        {
            _tfsApiClient = tfsApiClient;
            _projectName = projectName;
            _definitionIDs = definitionIDs;
        }

        public BuildStatus GetBuildStatus()
        {
            var tfsBuildsResponse = _tfsApiClient.GetBuildsAsync(_projectName, _definitionIDs).Result;

            var latestBuild = tfsBuildsResponse.Value[0];

            if (latestBuild.Status == "inProgress")
            {
                return BuildStatus.Running;
            }
            if (latestBuild.Result == "succeeded")
            {
                return BuildStatus.Success;
            }
            if (latestBuild.Result == "failed" || latestBuild.Result == "partiallySucceeded")
            {
                return BuildStatus.Failure;
            }

            return BuildStatus.Unknown;
        }
    }
}
