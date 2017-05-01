using System;
using System.Collections.Generic;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Core.Services
{
    public class TfsBuildService : IBuildService
    {
        private readonly ITfsAccess _tfsAccess;

        public TfsBuildService(ITfsAccess tfsAccess)
        {
            _tfsAccess = tfsAccess;
        }

        public List<BuildStatus> GetLatestBuildStatuses()
        {
            var buildStatus = _tfsAccess.GetBuildStatus();

            return new List<BuildStatus> { buildStatus };
        }
    }
}
