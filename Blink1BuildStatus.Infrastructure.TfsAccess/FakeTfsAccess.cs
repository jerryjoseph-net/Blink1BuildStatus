using System;
using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Interfaces.Infrastructure.TfsAccess;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public class FakeTfsAccess : ITfsAccess
    {
        public BuildStatus GetBuildStatus()
        {
            var s = DateTime.Now.Second % 10;

            if (s < 5)
            {
                return BuildStatus.Success;
            }
            if (s < 7)
            {
                return BuildStatus.Running;
            }
            return BuildStatus.Failure;
        }
    }
}
