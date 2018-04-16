namespace Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess
{
    public interface ITeamCityAccess
    {
        string TeamCityInstance { get; }

        BuildStatus GetBuildStatus(string buildConfigurationId);
    }
}
