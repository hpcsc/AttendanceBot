using AttendanceBot.Helpers;
using NUnit.Framework;

namespace AttendanceBot.Tests.Helpers
{
    public class StringExtensionsTests
    {
        [TestFixture]
        public class EqualsIgnoreCaseMethod
        {
            [Test]
            public void CompareTwoSameStringsSameCase_ShouldReturnTrue()
            {
                var first = "Test String";
                var second = "Test String";

                Assert.IsTrue(StringExtensions.EqualsIgnoreCase(first, second));
            }

            [Test]
            public void CompareTwoSameStringsDifferentCase_ShouldReturnTrue()
            {
                var first = "Test String";
                var second = "test string";

                Assert.IsTrue(StringExtensions.EqualsIgnoreCase(first, second));
            }

            [Test]
            public void CompareTwoDifferentStrings_ShouldReturnFalse()
            {
                var first = "First String";
                var second = "Second String";

                Assert.IsFalse(StringExtensions.EqualsIgnoreCase(first, second));
            }
        }
    }
}
