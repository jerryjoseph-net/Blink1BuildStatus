using System;
using System.Drawing;
using System.Linq;
using Sleddog.Blink1;
using IBlink1 = Blink1BuildStatus.Core.Interfaces.Infrastructure.Blink1Control.IBlink1;

namespace Blink1BuildStatus.Infrastructure.Blink1Control
{
    public class Blink1Controller : IBlink1
    {
        protected readonly Blink1 Blink1;

        public Blink1Controller()
        {
            var blink1 = Blink1Connector.Scan()?.FirstOrDefault();

            if (blink1 == null) { throw new InvalidOperationException("No Blink(1) device connected."); }

            Blink1 = (Blink1)blink1;
        }

        public virtual void SetRed()
        {
            Blink1.Set(Color.Red);
        }

        public virtual void SetGreen()
        {
            Blink1.Set(Color.Green);
        }

        public virtual void SetOrange()
        {
            Blink1.Blink(Color.Orange, TimeSpan.FromMilliseconds(10), 200);

            //_blink1.Blink(Color.Orange, TimeSpan.FromMilliseconds(100), 5);
            //Thread.Sleep(500);
            //_blink1.FadeToColor(Color.Orange, TimeSpan.FromMilliseconds(10));
        }

        public void SetGrey()
        {
            Blink1.Fade(Color.FromArgb(5, 5, 5), TimeSpan.FromSeconds(1));
        }

        #region IDisposable 

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    Blink1.Set(Color.Black);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                Blink1.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~Blink1Controller()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
