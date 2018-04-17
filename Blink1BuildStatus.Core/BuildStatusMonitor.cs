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
        private readonly ILog _log;

        public BuildStatusMonitor(IBlink1Factory blink1Factory, IBuildService buildService, ILog log)
        {
            _blink1Factory = blink1Factory;
            _buildService = buildService;
            _log = log;
        }

        public void Monitor()
        {
            _log.Heading("Starting build monitoring");

            foreach (var info in _buildService.Info)
            {
                _log.Info(info);
            }

            using (var blink1 = _blink1Factory.Create())
            {
                while (true)
                {
                    try
                    {
                        var latestBuildStatuses = _buildService.GetLatestBuildStatuses();

                        var consolidatedStatus = new ConsolidatedStatus(latestBuildStatuses);

                        SetColour(blink1, consolidatedStatus);
                    }
                    catch (Exception)
                    {
                        _log.Error("Unable to connect");
                    }

                    Thread.Sleep(2000);
                }
            }
        }

        private void SetColour(IBlink1 blink1, ConsolidatedStatus consolidatedStatus)
        {
            switch (consolidatedStatus.BuildStatus)
            {
                case BuildStatus.Failure:
                    _log.Error($"Setting {consolidatedStatus.Color} since {consolidatedStatus.Reason}");
                    blink1.SetRed();
                    break;

                case BuildStatus.Success:
                    _log.Success($"Setting {consolidatedStatus.Color} since {consolidatedStatus.Reason}");
                    blink1.SetGreen();
                    break;

                case BuildStatus.Running:
                    _log.Warning($"Setting {consolidatedStatus.Color} since {consolidatedStatus.Reason}");
                    blink1.SetOrange();
                    break;

                default:
                    _log.Info($"Setting {consolidatedStatus.Color} since  {consolidatedStatus.Reason}");
                    blink1.SetGrey();
                    break;
            }
        }
    }
}
