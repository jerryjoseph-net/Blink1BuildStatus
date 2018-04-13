using System.Collections.Generic;

namespace Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess
{
    public interface ITfsAccess
    {
        IEnumerable<BuildStatus> GetLatestBuildStatuses(string projectName, IEnumerable<string> definitionIDs = null);
    }
}
