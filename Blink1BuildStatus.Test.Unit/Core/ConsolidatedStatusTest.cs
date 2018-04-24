using Blink1BuildStatus.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace Blink1BuildStatus.Test.Unit.Core
{
    [TestFixture]
    public class ConsolidatedStatusTest
    {
        [TestCase(BuildStatus.Failure, BuildStatus.Failure, BuildStatus.Failure, BuildStatus.Failure)]
        [TestCase(BuildStatus.Failure, BuildStatus.Success, BuildStatus.Failure, BuildStatus.Failure)]
        [TestCase(BuildStatus.Failure, BuildStatus.Failure, BuildStatus.Running, BuildStatus.Failure)]
        [TestCase(BuildStatus.Success, BuildStatus.Success, BuildStatus.Failure, BuildStatus.Failure)]
        [TestCase(BuildStatus.Success, BuildStatus.Running, BuildStatus.Failure, BuildStatus.Failure)]
        public void Constructor_AtleastOneFailure_ConsolidatesToFailure(BuildStatus buildStatus1, BuildStatus buildStatus2, BuildStatus buildStatus3, BuildStatus expected)
        {
            var sut = new ConsolidatedStatus(new List<BuildStatus> { buildStatus1, buildStatus2, buildStatus3 });

            var result = sut.BuildStatus;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Constructor_AllSuccess_ConsolidatesToSuccess()
        {
            var sut = new ConsolidatedStatus(new List<BuildStatus> { BuildStatus.Success, BuildStatus.Success, BuildStatus.Success });

            var result = sut.BuildStatus;

            Assert.AreEqual(BuildStatus.Success, result);
        }

        [TestCase(BuildStatus.Running, BuildStatus.Running, BuildStatus.Success, BuildStatus.Running)]
        [TestCase(BuildStatus.Success, BuildStatus.Success, BuildStatus.Running, BuildStatus.Running)]
        [TestCase(BuildStatus.Running, BuildStatus.Success, BuildStatus.Success, BuildStatus.Running)]
        [TestCase(BuildStatus.Success, BuildStatus.Running, BuildStatus.Success, BuildStatus.Running)]
        public void Constructor_NoneFailingButAtleastOneRunning_ConsolidatesToRunning(BuildStatus buildStatus1, BuildStatus buildStatus2, BuildStatus buildStatus3, BuildStatus expected)
        {
            var sut = new ConsolidatedStatus(new List<BuildStatus> { buildStatus1, buildStatus2, buildStatus3 });

            var result = sut.BuildStatus;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Constructor_AtLeastOneUnknown_ConsolidatesToUnknown()
        {
            var sut = new ConsolidatedStatus(new List<BuildStatus> { BuildStatus.Unknown, BuildStatus.Success });

            var result = sut.BuildStatus;

            Assert.AreEqual(BuildStatus.Unknown, result);
        }
    }
}
