using AttendanceBot.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceBot.Tests.Models
{
    public class AttendanceEntryTests
    {
        private static void TestAttendanceEntryStateChange(AttendanceStatus initial, AttendanceStatus expected, Action<AttendanceEntry> action)
        {
            var entry = new AttendanceEntry("some id", "some name", initial);

            action(entry);

            Assert.AreEqual(expected, entry.Status);
        }

        [TestFixture]
        public class InMethod
        {
            [Test]
            public void WhenInitialStatusIsYes_StatusShouldRemainTheSame()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.Yes, AttendanceStatus.Yes, entry => entry.In());
            }

            [Test]
            public void WhenInitialStatusIsNo_StatusShouldBeYes()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.No, AttendanceStatus.Yes, entry => entry.In());
            }
        }

        [TestFixture]
        public class OutMethod
        {
            [Test]
            public void WhenInitialStatusIsNo_StatusShouldRemainTheSame()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.No, AttendanceStatus.No, entry => entry.Out());
            }

            [Test]
            public void WhenInitialStatusIsYes_StatusShouldBeNo()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.Yes, AttendanceStatus.No, entry => entry.Out());
            }

            [Test]
            public void WhenMessageIsProvided_EntryShouldStoreTheMessage()
            {
                var entry = new AttendanceEntry("some id", "some name", AttendanceStatus.Yes);
                var message = "some message";

                entry.Out(message);

                Assert.AreEqual(message, entry.Message);
            }
        }

        [TestFixture]
        public class MaybeMethod
        {
            [Test]
            public void WhenInitialStatusIsMaybe_StatusShouldRemainTheSame()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.Maybe, AttendanceStatus.Maybe, entry => entry.Maybe());
            }

            [Test]
            public void WhenInitialStatusIsYes_StatusShouldBeMaybe()
            {
                TestAttendanceEntryStateChange(AttendanceStatus.Yes, AttendanceStatus.Maybe, entry => entry.Maybe());
            }

            [Test]
            public void WhenMessageIsProvided_EntryShouldStoreTheMessage()
            {
                var entry = new AttendanceEntry("some id", "some name", AttendanceStatus.Yes);
                var message = "some message";

                entry.Maybe(message);

                Assert.AreEqual(message, entry.Message);
            }
        }
    }
}
