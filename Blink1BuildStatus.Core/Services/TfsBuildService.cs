using System.Collections.Generic;
using System.Linq;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Core.Services
{
    public class TfsBuildService : IBuildService
    {
        private readonly ITfsAccess _tfsAccess;
        private readonly string _projectName;
        private readonly IEnumerable<string> _definitionIDs;

        public TfsBuildService(ITfsAccess tfsAccess, string projectName, IEnumerable<string> definitionIDs = null)
        {
            _tfsAccess = tfsAccess;
            _projectName = projectName;
            _definitionIDs = definitionIDs;
        }

        public List<BuildStatus> GetLatestBuildStatuses()
        {
            var buildStatuses = _tfsAccess.GetLatestBuildStatuses(_projectName, _definitionIDs);

            return buildStatuses.ToList();
        }
    }
}
