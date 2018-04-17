using Blink1BuildStatus.Core;
using Blink1BuildStatus.Core.Extensions;
using NUnit.Framework;

namespace Blink1BuildStatus.Test.Unit.Core.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        [Test]
        public void ConvertTo_Int_ParsesToInt()
        {
            var sut = "341";

            var result = sut.ConvertTo<int>();

            Assert.AreEqual(341, result);
        }

        [Test]
        public void ConvertTo_NullableInt_ParsesToInt()
        {
            var sut = "341";

            var result = sut.ConvertTo<int?>();

            Assert.AreEqual(341, result);
        }

        [Test]
        public void ConvertTo_NullToNullableInt_ParsesToNull()
        {
            var sut = (string)null;

            var result = sut.ConvertTo<int?>();

            Assert.AreEqual(null, result);
        }

        [Test]
        public void ConvertTo_Bool_ParsesToBool()
        {
            var sut = "True";

            var result = sut.ConvertTo<bool>();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ConvertTo_NullableBool_ParsesToBool()
        {
            var sut = "True";

            var result = sut.ConvertTo<bool?>();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ConvertTo_NullToNullableBool_ParsesToNull()
        {
            var sut = (string)null;

            var result = sut.ConvertTo<bool?>();

            Assert.AreEqual(null, result);
        }

        [Test]
        public void ConvertTo_Enum_ParsesToEnum()
        {
            var sut = "TFS";

            var result = sut.ConvertTo<BuildServer>();

            Assert.AreEqual(BuildServer.TFS, result);
        }
    }
}
