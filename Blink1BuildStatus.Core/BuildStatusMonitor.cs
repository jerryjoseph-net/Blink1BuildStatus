using System;
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
        private readonly IBlink1NotificationService _blink1NotificationService;
        private readonly ILog _log;

        public BuildStatusMonitor(
            IBlink1Factory blink1Factory, 
            IBuildService buildService, 
            IBlink1NotificationService blink1NotificationService, 
            ILog log)
        {
            _blink1Factory = blink1Factory;
            _buildService = buildService;
            _blink1NotificationService = blink1NotificationService;
            _log = log;
        }

        public void Monitor()
        {
            _log.Heading("Starting build monitoring");

            foreach (var info in _buildService.Info)
            {
                _log.Info(info);
            }

            try
            {
                using (var blink1 = _blink1Factory.Create())
                {
                    while (true)
                    {
                        try
                        {
                            var latestBuildStatuses = _buildService.GetLatestBuildStatuses();

                            var consolidatedStatus = new ConsolidatedStatus(latestBuildStatuses);

                            _blink1NotificationService.Notify(blink1, consolidatedStatus);
                        }
                        catch (Exception)
                        {
                            _log.Error("Unable to connect to build server");
                        }

                        Thread.Sleep(2000);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                _log.Error(ex.Message);
                _log.Info("Connect blink(1) and restart the program");

                while (true)
                { }
            }
        }
    }
}
