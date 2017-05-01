using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Core;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;
using Blink1BuildStatus.Core.Services;
using Blink1BuildStatus.Infrastructure.Blink1Control;
using Blink1BuildStatus.Infrastructure.TeamCityAccess;
using Blink1BuildStatus.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.UI
{
    internal class DependencyInjection
    {
        public static ITfsAccess TfsAccess;
        public static ITeamCityAccess TeamCityAccess;
        public static IBuildService BuildService;
        public static IBlink1Factory Blink1Factory;
        public static IBuildStatusMonitor BuildStatusMonitor;

        public static void Setup()
        {
            //var buildConfigurationIDs = AppSettings.TeamCity.BuildConfigurationIDs;
            var fadeInsteadOfBlink = AppSettings.Blink1.FadeInsteadOfBlink;

            TfsAccess = AppSettings.TFS.UseFake
                ? (ITfsAccess)new FakeTfsAccess()
                : new TfsAccess(
                    new TfsApiClient(AppSettings.TFS.Host, AppSettings.TFS.Username, AppSettings.TFS.Password),
                    AppSettings.TFS.ProjectID, 
                    AppSettings.TFS.DefinitionIDs);

            TeamCityAccess = AppSettings.TeamCity.UseFake
                ? (ITeamCityAccess)new FakeTeamCityAccess()
                : new TeamCityAccess(
                    AppSettings.TeamCity.Host,
                    AppSettings.TeamCity.UseGuestLogin,
                    AppSettings.TeamCity.Username,
                    AppSettings.TeamCity.Password);

            BuildService = new TfsBuildService(TfsAccess);
            //BuildService = new TeamCityBuildService(TeamCityAccess, buildConfigurationIDs);

            Blink1Factory = new Blink1Factory(fadeInsteadOfBlink);

            BuildStatusMonitor = new BuildStatusMonitor(Blink1Factory, BuildService, new ConsoleLogger());
        }
    }
}
