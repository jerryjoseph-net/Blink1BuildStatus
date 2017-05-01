namespace Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess
{
    public interface ITeamCityAccess
    {
        BuildStatus GetBuildStatus(string buildConfigurationId);
    }
}
