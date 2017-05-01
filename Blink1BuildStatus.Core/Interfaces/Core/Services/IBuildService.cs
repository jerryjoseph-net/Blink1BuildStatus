using System.Collections.Generic;

namespace Blink1BuildStatus.Core.Interfaces.Core.Services
{
    public interface IBuildService
    {
        List<BuildStatus> GetLatestBuildStatuses();
    }
}
