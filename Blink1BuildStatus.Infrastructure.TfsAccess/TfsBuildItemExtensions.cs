using Blink1BuildStatus.Core;
using System;

namespace Blink1BuildStatus.Infrastructure.TfsAccess
{
    public static class TfsBuildItemExtensions
    {
        public static BuildStatus MapToBuildStatus(this TfsBuildItem tfsBuildItem)
        {
            var buildStatus = BuildStatus.Unknown;

            if (tfsBuildItem != null)
            {
                switch (tfsBuildItem.Status)
                {
                    case TfsBuildStatus.InProgress:
                        buildStatus = BuildStatus.Running;
                        break;

                    case TfsBuildStatus.Completed:

                        switch (tfsBuildItem.Result)
                        {
                            case TfsBuildResult.Succeeded:
                                buildStatus = BuildStatus.Success;
                                break;

                            case TfsBuildResult.Failed:
                            case TfsBuildResult.PartiallySucceeded:
                                buildStatus = BuildStatus.Failure;
                                break;

                            case TfsBuildResult.None:
                            case TfsBuildResult.Canceled:
                                buildStatus = BuildStatus.Unknown;
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(TfsBuildResult));
                        }
                        break;

                    case TfsBuildStatus.None:
                    case TfsBuildStatus.Cancelling:
                    case TfsBuildStatus.Postponed:
                    case TfsBuildStatus.NotStarted:
                    case TfsBuildStatus.All:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(TfsBuildStatus));
                }
            }

            return buildStatus;
        }
    }
}
