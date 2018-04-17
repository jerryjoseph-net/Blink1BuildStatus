using System.Collections.Generic;
using System.Linq;

namespace Blink1BuildStatus.Core
{
    public class ConsolidatedStatus
    {
        public ConsolidatedStatus(List<BuildStatus> buildStatuses)
        {
            if (buildStatuses.Any(lb => lb == BuildStatus.Failure))
            {
                BuildStatus = BuildStatus.Failure;
                Color = "RED";
                Reason = "there are one or more build failures";
            }
            else if (buildStatuses.All(lb => lb == BuildStatus.Success))
            {
                BuildStatus = BuildStatus.Success;
                Color = "GREEN";
                Reason = "all builds succeeded";
            }
            else if (buildStatuses.Any(lb => lb == BuildStatus.Running))
            {
                BuildStatus = BuildStatus.Running;
                Color = "ORANGE";
                Reason = "one or more builds are running";
            }
            else
            {
                BuildStatus = BuildStatus.Unknown;
                Color = "GREY";
                Reason = "the state is unknown";
            }
        }

        public BuildStatus BuildStatus { get; }
        public string Color { get; }
        public string Reason { get; }
    }
}
