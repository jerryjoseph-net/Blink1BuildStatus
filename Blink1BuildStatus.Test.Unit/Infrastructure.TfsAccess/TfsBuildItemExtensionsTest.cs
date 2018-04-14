using Blink1BuildStatus.Core;
using Blink1BuildStatus.Infrastructure.TfsAccess;
using NUnit.Framework;

namespace Blink1BuildStatus.Test.Unit.Infrastructure.TfsAccess
{
    [TestFixture]
    public class TfsBuildItemExtensionsTest
    {
        [Test]
        public void MapToBuildStatus_Null_ReturnsUnknownBuildStatus()
        {
            var sut = (TfsBuildItem)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = sut.MapToBuildStatus();

            Assert.AreEqual(BuildStatus.Unknown, result);
        }

        [Test]
        public void MapToBuildStatus_SingleInProgressBuildItem_ReturnsRunningBuildStatus()
        {
            var sut = new TfsBuildItem { Status = TfsBuildStatus.InProgress };

            var result = sut.MapToBuildStatus();

            Assert.AreEqual(BuildStatus.Running, result);
        }

        [TestCase(TfsBuildStatus.Completed, TfsBuildResult.Canceled, BuildStatus.Unknown)]
        [TestCase(TfsBuildStatus.Completed, TfsBuildResult.Failed, BuildStatus.Failure)]
        [TestCase(TfsBuildStatus.Completed, TfsBuildResult.PartiallySucceeded, BuildStatus.Failure)]
        [TestCase(TfsBuildStatus.Completed, TfsBuildResult.Succeeded, BuildStatus.Success)]
        public void MapToBuildStatus_SingleCompletedBuildItem_ReturnsCorrectBuildStatus(TfsBuildStatus tfsBuildStatus, TfsBuildResult tfsBuildResult, BuildStatus expected)
        {
            var sut = new TfsBuildItem { Status = tfsBuildStatus, Result = tfsBuildResult };

            var result = sut.MapToBuildStatus();

            Assert.AreEqual(expected, result);
        }
    }
}
