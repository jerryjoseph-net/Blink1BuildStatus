using System;
using System.Drawing;

namespace Blink1BuildStatus.Infrastructure.Blink1Control
{
    public class Blink1FadeController : Blink1Controller
    {
        public override void SetRed()
        {
            Blink1.Fade(Color.Red, TimeSpan.FromSeconds(1));
        }

        public override void SetGreen()
        {
            Blink1.Fade(Color.Green, TimeSpan.FromSeconds(1));
        }

        public override void SetOrange()
        {
            Blink1.Fade(Color.Orange, TimeSpan.FromSeconds(1));
        }
    }
}
