using Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control;

namespace Blink1BuildStatus.Infrastructure.Blink1Control
{
    public class Blink1Factory : IBlink1Factory
    {
        private readonly bool _fadeInsteadOfBlink;

        public Blink1Factory(bool fadeInsteadOfBlink)
        {
            _fadeInsteadOfBlink = fadeInsteadOfBlink;
        }

        public IBlink1 Create()
        {
            return _fadeInsteadOfBlink 
                ? new Blink1FadeController() 
                : new Blink1Controller();
        }
    }
}
