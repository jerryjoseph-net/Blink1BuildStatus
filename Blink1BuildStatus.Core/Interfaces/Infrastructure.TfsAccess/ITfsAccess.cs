using System.Collections.Generic;

namespace Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess
{
    public interface ITfsAccess
    {
        string TfsInstance { get; }

        IEnumerable<BuildStatus> GetLatestBuildStatuses(string projectName, IEnumerable<string> buildDefinitionIDs = null);
    }
}
