namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class TfsBuildItem
    {
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public TfsBuildDefinition Definition { get; set; }
        public TfsBuildStatus Status { get; set; }
        public TfsBuildResult Result { get; set; }
    }
}