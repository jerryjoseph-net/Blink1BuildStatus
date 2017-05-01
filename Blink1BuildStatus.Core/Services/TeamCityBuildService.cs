﻿using System.Collections.Generic;
using System.Linq;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess;

namespace Blink1BuildStatus.Core.Services
{
    public class TeamCityBuildService : IBuildService
    {
        private readonly ITeamCityAccess _teamCityAccess;
        private readonly List<string> _buildConfigurationIDs;

        public TeamCityBuildService(ITeamCityAccess teamCityAccess, List<string> buildConfigurationIDs)
        {
            _teamCityAccess = teamCityAccess;
            _buildConfigurationIDs = buildConfigurationIDs;
        }

        public List<BuildStatus> GetLatestBuildStatuses()
        {
            return _buildConfigurationIDs.Select(bci => _teamCityAccess.GetBuildStatus(bci)).ToList();
        }
    }
}
