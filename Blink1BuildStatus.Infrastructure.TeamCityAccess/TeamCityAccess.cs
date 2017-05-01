using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TeamCityAccess;
using FluentTc;
using FluentTc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamCityBuildStatus = FluentTc.Locators.BuildStatus;

namespace Blink1BuildStatus.Infrastructure.TeamCityAccess
{
    public class TeamCityAccess : ITeamCityAccess
    {
        private readonly string _teamCityHost;
        private readonly bool _useGuestLogin;
        private readonly string _username;
        private readonly string _password;

        public TeamCityAccess(string teamCityHost, bool useGuestLogin, string username, string password)
        {
            _teamCityHost = teamCityHost;
            _useGuestLogin = useGuestLogin;
            _username = username;
            _password = password;
        }

        public BuildStatus GetBuildStatus(string buildConfigurationId)
        {
            try
            {
                IList<IBuild> finishedBuilds;
                IList<IBuild> runningBuilds;

                if (_useGuestLogin)
                {
                    finishedBuilds = new RemoteTc()
                        .Connect(_ => _.ToHost(_teamCityHost).AsGuest())
                        .GetBuilds(_ => _.BuildConfiguration(__ => __.Id(buildConfigurationId)));
                    //.GetBuilds(_ => _.BuildConfiguration(__ => __.Project(___ => ___.Id(ConfigurationManager.AppSettings["TeamCity.ProjectID"]))));


                    runningBuilds = new RemoteTc()
                        .Connect(_ => _.ToHost(_teamCityHost).AsGuest())
                        .GetBuilds(_ => _.BuildConfiguration(__ => __.Id(buildConfigurationId)).Running());
                }
                else
                {
                    finishedBuilds = new RemoteTc()
                        .Connect(_ => _.ToHost(_teamCityHost).AsUser(_username, _password))
                        .GetBuilds(_ => _.BuildConfiguration(__ => __.Id(buildConfigurationId)));

                    runningBuilds = new RemoteTc()
                        .Connect(_ => _.ToHost(_teamCityHost).AsUser(_username, _password))
                        .GetBuilds(_ => _.BuildConfiguration(__ => __.Id(buildConfigurationId)).Running());
                }

                if (!finishedBuilds.Any())
                    return BuildStatus.Unknown;

                if (runningBuilds.Any())
                    return BuildStatus.Running;

                //var isRunning = finishedBuilds[0].State == BuildState.Running;

                //var buildStatus = finishedBuilds[isRunning ? 1 : 0].Status;

                var buildStatus = finishedBuilds[0].Status;

                //if (isRunning)
                //    return Core.BuildStatus.Running;

                if (buildStatus == TeamCityBuildStatus.Success)
                    return BuildStatus.Success;

                if (buildStatus == TeamCityBuildStatus.Failure || buildStatus == TeamCityBuildStatus.Error)
                    return BuildStatus.Failure;
                
                return BuildStatus.Unknown;
            }
            catch (Exception)
            {
                return BuildStatus.Unknown;
            }
        }
    }
}
