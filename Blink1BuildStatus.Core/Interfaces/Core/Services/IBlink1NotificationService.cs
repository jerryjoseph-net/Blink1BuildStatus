using Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control;

namespace Blink1BuildStatus.Core.Interfaces.Core.Services
{
    public interface IBlink1NotificationService
    {
        void Notify(IBlink1 blink1, ConsolidatedStatus consolidatedStatus);
    }
}
