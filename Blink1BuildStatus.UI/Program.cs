using System.Runtime.InteropServices;

namespace Blink1BuildStatus.UI
{
    internal class Program
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        private static EventHandler _handler;

        private enum CtrlType
        {
            // ReSharper disable InconsistentNaming
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
            // ReSharper restore InconsistentNaming
        }

        public static void Main(string[] args)
        {
            // Some biolerplate to react to close window event
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            DependencyInjection.Setup();

            var buildStatusMonitor = DependencyInjection.BuildStatusMonitor;

            buildStatusMonitor.Monitor();
        }


        private static bool Handler(CtrlType sig)
        {
            using (var blink1 = DependencyInjection.Blink1Factory.Create())
            { }

            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                default:
                    return false;
            }
        }
    }
}
