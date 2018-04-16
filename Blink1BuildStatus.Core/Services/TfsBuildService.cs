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
        private readonly IEnumerable<string> _buildDefinitionIDs;

        public TfsBuildService(ITfsAccess tfsAccess, string projectName, IEnumerable<string> buildDefinitionIDs = null)
        {
            _tfsAccess = tfsAccess;
            _projectName = projectName;
            _buildDefinitionIDs = buildDefinitionIDs;
        }

        public IEnumerable<string> Info => new [] 
        {
            $"Instance: {_tfsAccess.TfsInstance}",
            $"ProjectName: {_projectName}",
            $"BuildDefinitionIDs: {string.Join(";", _buildDefinitionIDs)}"
        };

        public List<BuildStatus> GetLatestBuildStatuses()
        {
            var buildStatuses = _tfsAccess.GetLatestBuildStatuses(_projectName, _buildDefinitionIDs);

            return buildStatuses.ToList();
        }
    }
}
