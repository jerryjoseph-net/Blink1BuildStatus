using System.Collections.Generic;
using System.Linq;

namespace Blink1BuildStatus.Infrastructure.TfsAccess.Extensions
{
    public static class TfsBuildsResponseExtensions
    {
        public static List<TfsBuildItem> ExtractRelevantBuilds(this TfsBuildsResponse tfsBuildsResponse)
        {
            return tfsBuildsResponse?.Value
                       .Where(b => b.Status == TfsBuildStatus.InProgress || b.Status == TfsBuildStatus.Completed)
                       .Where(b => b.Result != TfsBuildResult.Canceled)
                       .ToList()
                   ?? new List<TfsBuildItem>();
        }
    }
}
