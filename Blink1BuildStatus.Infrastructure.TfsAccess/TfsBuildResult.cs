namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public enum TfsBuildResult
    {
        None = 0,
        Succeeded = 2,
        PartiallySucceeded = 4,
        Failed = 8,
        Canceled = 32
    }
}
