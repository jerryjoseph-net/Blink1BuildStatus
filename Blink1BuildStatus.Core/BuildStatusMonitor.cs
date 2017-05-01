﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Blink1BuildStatus.Core.Interfaces;
using Blink1BuildStatus.Core.Interfaces.Core;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control;

namespace Blink1BuildStatus.Core
{
    public class BuildStatusMonitor : IBuildStatusMonitor
    {
        private readonly IBlink1Factory _blink1Factory;
        private readonly IBuildService _buildService;
        private readonly ILogger _logger;

        public BuildStatusMonitor(IBlink1Factory blink1Factory, IBuildService buildService, ILogger logger)
        {
            _blink1Factory = blink1Factory;
            _buildService = buildService;
            _logger = logger;
        }

        public void Monitor()
        {
            _logger.LogInfo("Starting build monitoring");

            using (var blink1 = _blink1Factory.Create())
            {
                while (true)
                {
                    try
                    {
                        var latestBuildStatuses = _buildService.GetLatestBuildStatuses();

                        SetColour(blink1, latestBuildStatuses);
                    }
                    catch (Exception)
                    {
                        _logger.LogInfo("Unable to connect");
                    }

                    Thread.Sleep(2000);
                }
            }
        }

        private void SetColour(IBlink1 blink1, List<BuildStatus> latestBuildStatuses)
        {
            if (latestBuildStatuses.Any(lb => lb == BuildStatus.Failure))
            {
                _logger.LogInfo("Setting RED due to one or more failures");

                blink1.SetRed();
            }
            else if (latestBuildStatuses.All(lb => lb == BuildStatus.Success))
            {
                _logger.LogInfo("Setting GREEN since all succeeded");

                blink1.SetGreen();
            }
            else if (latestBuildStatuses.Any(lb => lb == BuildStatus.Running))
            {
                _logger.LogInfo("Setting ORANGE since one or more builds is running");

                blink1.SetOrange();
            }
            else
            {
                _logger.LogInfo("Setting GREY due to unknown state");

                blink1.SetGrey();
            }
        }
    }
}
