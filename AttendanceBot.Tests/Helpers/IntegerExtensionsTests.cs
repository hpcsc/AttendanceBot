using System;
using System.Linq;
using AttendanceBot.Helpers;
using NUnit.Framework;

namespace AttendanceBot.Tests.Helpers
{
    public class IntegerExtensionsTests
    {
        [TestFixture]
        public class LinesMethod
        {
            [Test]
            public void WhenInputIsNegative_ThrowArgumentException()
            {
                Assert.Throws<ArgumentException>(() => (-1).Lines());
            }

            [Test]
            public void WhenInputIsPositive_ReturnCorrespondingNumberOfNewLines()
            {
                var result = 3.Lines();

                Assert.AreEqual(string.Join("", Enumerable.Repeat(Environment.NewLine, 3)), result);
            }
        }
    }
}