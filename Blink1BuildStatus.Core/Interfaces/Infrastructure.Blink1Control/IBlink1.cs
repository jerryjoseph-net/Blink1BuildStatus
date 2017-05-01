using System;

namespace Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control
{
    public interface IBlink1 : IDisposable
    {
        void SetRed();
        void SetGreen();
        void SetOrange();
        void SetGrey();
    }
}
