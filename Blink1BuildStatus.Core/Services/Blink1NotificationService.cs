using Blink1BuildStatus.Core.Interfaces;
using Blink1BuildStatus.Core.Interfaces.Core.Services;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control;

namespace Blink1BuildStatus.Core.Services
{
    public class Blink1NotificationService : IBlink1NotificationService
    {
        private readonly ILog _log;

        public Blink1NotificationService(ILog log)
        {
            _log = log;
        }

        public void Notify(IBlink1 blink1, ConsolidatedStatus consolidatedStatus)
        {
            var message = $"Setting {consolidatedStatus.Color} since {consolidatedStatus.Reason}";

            switch (consolidatedStatus.BuildStatus)
            {
                case BuildStatus.Failure:
                    _log.Error(message);
                    blink1.SetRed();
                    break;

                case BuildStatus.Success:
                    _log.Success(message);
                    blink1.SetGreen();
                    break;

                case BuildStatus.Running:
                    _log.Warning(message);
                    blink1.SetOrange();
                    break;

                default:
                    _log.Info(message);
                    blink1.SetGrey();
                    break;
            }
        }
    }
}
