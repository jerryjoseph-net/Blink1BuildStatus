using System;
using System.Collections.Generic;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class FakeTfsAccess : ITfsAccess
    {
        public string TfsInstance => "";

        public IEnumerable<BuildStatus> GetLatestBuildStatuses(string projectName, IEnumerable<string> definitionIDs = null)
        {
            var s = DateTime.Now.Second % 10;

            if (s < 5)
            {
                return new List<BuildStatus> { BuildStatus.Success };
            }
            if (s < 7)
            {
                return new List<BuildStatus> { BuildStatus.Running };
            }
            return new List<BuildStatus> { BuildStatus.Failure };
        }
    }
}
