using System.Collections.Generic;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class TfsBuildsResponse
    {
        public int Count { get; set; }

        public List<TfsBuildItem> Value { get; set; }
    }
}
