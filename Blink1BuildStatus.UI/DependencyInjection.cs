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
using System;

namespace Blink1BuildStatus.UI
{
    internal class DependencyInjection
    {
        public static ITfsAccess TfsAccess;
        public static ITeamCityAccess TeamCityAccess;
        public static IBuildService BuildService;
        public static IBlink1Factory Blink1Factory;
        public static IBlink1NotificationService Blink1NotificationService;
        public static IBuildStatusMonitor BuildStatusMonitor;

        public static void Setup()
        {
            var fadeInsteadOfBlink = AppSettings.Blink1.FadeInsteadOfBlink;

            var log = new ConsoleLog(); 

            TfsAccess = AppSettings.TFS.UseFake
                ? (ITfsAccess)new FakeTfsAccess()
                : new TfsAccess(new TfsApiClient(AppSettings.TFS.Instance, AppSettings.TFS.Username, AppSettings.TFS.Password));

            TeamCityAccess = AppSettings.TeamCity.UseFake
                ? (ITeamCityAccess)new FakeTeamCityAccess()
                : new TeamCityAccess(
                    AppSettings.TeamCity.Instance,
                    AppSettings.TeamCity.UseGuestLogin,
                    AppSettings.TeamCity.Username,
                    AppSettings.TeamCity.Password);

            BuildService = AppSettings.Monitoring.BuildServer == BuildServer.TFS || AppSettings.Monitoring.BuildServer == BuildServer.VSTS
                ? (IBuildService)new TfsBuildService(TfsAccess, AppSettings.TFS.ProjectID, AppSettings.TFS.BuildDefinitionIDs)
                : AppSettings.Monitoring.BuildServer == BuildServer.TeamCity
                    ? new TeamCityBuildService(TeamCityAccess, AppSettings.TeamCity.BuildConfigurationIDs)
                    : throw new ArgumentOutOfRangeException(nameof(AppSettings.Monitoring.BuildServer));

            Blink1Factory = new Blink1Factory(fadeInsteadOfBlink);

            Blink1NotificationService = new Blink1NotificationService(log);

            BuildStatusMonitor = new BuildStatusMonitor(Blink1Factory, BuildService, Blink1NotificationService, log);
        }
    }
}
