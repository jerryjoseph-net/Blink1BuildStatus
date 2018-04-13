namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public enum TfsBuildStatus
    {
        None = 0,
        InProgress = 1,
        Completed = 2,
        Cancelling = 4,
        Postponed = 8,
        NotStarted = 32,
        All = 47
    }
}
