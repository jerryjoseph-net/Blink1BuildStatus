namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class TfsBuildItem
    {
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
}