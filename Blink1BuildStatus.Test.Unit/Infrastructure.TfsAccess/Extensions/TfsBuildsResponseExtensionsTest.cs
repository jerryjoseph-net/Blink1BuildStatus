using System.Collections.Generic;
using Blink1BuildStatus.Infrastructure.TfsAccess;
using Blink1BuildStatus.Infrastructure.TfsAccess.Extensions;
using NUnit.Framework;

namespace Blink1BuildStatus.Test.Unit.Infrastructure.TfsAccess.Extensions
{
    [TestFixture]
    public class TfsBuildsResponseExtensionsTest
    {
        [Test]
        public void ExtractRelevantBuilds_Null_ReturnsEmptyList()
        {
            var sut = (TfsBuildsResponse)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = sut.ExtractRelevantBuilds();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void ExtractRelevantBuilds_ContainsCancelled_ReturnsWithoutCancelled()
        {
            var sut = new TfsBuildsResponse
            {
                Count = 2,
                Value = new List<TfsBuildItem>
                {
                    new TfsBuildItem{ Result = TfsBuildResult.Canceled},
                    new TfsBuildItem{ Status = TfsBuildStatus.InProgress}
                }
            };

            var result = sut.ExtractRelevantBuilds();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(TfsBuildStatus.InProgress, result[0].Status);
        }
    }
}
