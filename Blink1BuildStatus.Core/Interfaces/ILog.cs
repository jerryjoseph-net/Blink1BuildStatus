namespace Blink1BuildStatus.Core.Interfaces
{
    public interface ILog
    {
        void Info(string message);

        void Heading(string message);

        void Success(string message);
        void Warning(string message);
        void Error(string message);
    }
}
