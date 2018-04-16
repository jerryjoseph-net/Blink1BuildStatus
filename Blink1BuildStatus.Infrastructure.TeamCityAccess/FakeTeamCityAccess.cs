using System;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess;

namespace Blink1BuildStatus.Infrastructure.TeamCityAccess
{
    public class FakeTeamCityAccess : ITeamCityAccess
    {
        public string TeamCityInstance => "";

        public BuildStatus GetBuildStatus(string buildConfigurationId)
        {
            var s = DateTime.Now.Second % 10;

            if (s < 5)
            {
                return BuildStatus.Success;
            }
            if (s < 7)
            {
                return BuildStatus.Running;
            }
            return BuildStatus.Failure;
        }
    }
}
