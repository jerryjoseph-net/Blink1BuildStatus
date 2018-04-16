using Blink1BuildStatus.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
// ReSharper disable InconsistentNaming

namespace Blink1BuildStatus.UI
{
    public static class AppSettings
    {
        public static class Blink1
        {
            public static bool FadeInsteadOfBlink => bool.Parse(ConfigurationManager.AppSettings[$"{nameof(Blink1)}.{nameof(FadeInsteadOfBlink)}"]);
        }

        public static class Monitoring
        {
            public static BuildServer BuildServer => (BuildServer)Enum.Parse(typeof(BuildServer), ConfigurationManager.AppSettings[$"{nameof(Monitoring)}.{nameof(BuildServer)}"]);
        }

        public static class TFS
        {
            public static bool UseFake => bool.Parse(ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(UseFake)}"]);

            public static string Instance => ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(Instance)}"];

            public static string Username => ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(Username)}"];
            public static string Password => ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(Password)}"];

            public static string ProjectID => ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(ProjectID)}"];

            public static IEnumerable<string> BuildDefinitionIDs => ConfigurationManager.AppSettings[$"{nameof(TFS)}.{nameof(BuildDefinitionIDs)}"].Split(';').ToList();
        }

        public static class TeamCity
        {
            public static bool UseFake => bool.Parse(ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(UseFake)}"]);

            public static string Instance => ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(Instance)}"];

            public static bool UseGuestLogin => bool.Parse(ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(UseGuestLogin)}"]);

            public static string Username => ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(Username)}"];
            public static string Password => ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(Password)}"];

            public static string BuildConfigurationID => ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(BuildConfigurationID)}"];

            public static IEnumerable<string> BuildConfigurationIDs => ConfigurationManager.AppSettings[$"{nameof(TeamCity)}.{nameof(BuildConfigurationIDs)}"].Split(';').ToList();
        }
    }
}
