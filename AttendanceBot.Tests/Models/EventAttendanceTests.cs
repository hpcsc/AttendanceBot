using AttendanceBot.Helpers;
using AttendanceBot.Models;
using NUnit.Framework;
using System;

namespace AttendanceBot.Tests.Models
{
    public class EventAttendanceTests
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WhenCreated_CreatedDateShouldBeSetToCurrentTime()
            {
                var now = DateTime.Now;
                var e = new EventAttendance("some conversation id", "some name");
                var then = DateTime.Now;

                Assert.IsTrue(e.CreatedDate >= now);
                Assert.IsTrue(e.CreatedDate <= then);
            }
        }

        [TestFixture]
        public class InMethod
        {
            [Test]
            public void WhenUserInForTheFirstTime_UserStatusShouldBeDisplayedAsYes()
            {
                var eventName = "some event name";
                var userName = "User 1";
                var e = new EventAttendance("some conversation id", eventName);

                e.In("user1", userName);

                var expected = $"## {eventName}" +
                    2.Lines() + $"Yes ( 1 )" +
                    1.Lines() + $"- {userName}" +                    
                    2.Lines() + $"No ( 0 )" +
                    1.Lines() +
                    2.Lines() + $"Maybe ( 0 )" +
                    1.Lines();

                Assert.AreEqual(expected, e.ToString());
            }
        }

        [TestFixture]
        public class OutMethod
        {
            [Test]
            public void WhenUserOutForTheFirstTime_UserStatusShouldBeDisplayedAsNo()
            {
                var eventName = "some event name";
                var userName = "User 1";
                var reason = "sick";
                var e = new EventAttendance("some conversation id", eventName);

                e.Out("user1", userName, reason);

                var expected = $"## {eventName}" +
                    2.Lines() + $"Yes ( 0 )" +
                    1.Lines() +
                    2.Lines() + $"No ( 1 )" +
                    1.Lines() + $"- {userName} (sick)" +
                    2.Lines() + $"Maybe ( 0 )" +
                    1.Lines();

                Assert.AreEqual(expected, e.ToString());
            }
        }

        [TestFixture]
        public class MaybeMethod
        {
            [Test]
            public void WhenUserMaybeForTheFirstTime_UserStatusShouldBeDisplayedAsMaybe()
            {
                var eventName = "some event name";
                var userName = "User 1";
                var reason = "sick";
                var e = new EventAttendance("some conversation id", eventName);

                e.Maybe("user1", userName, reason);

                var expected = $"## {eventName}" +
                    2.Lines() + $"Yes ( 0 )" +
                    1.Lines() +
                    2.Lines() + $"No ( 0 )" +
                    1.Lines() +
                    2.Lines() + $"Maybe ( 1 )" +
                    1.Lines() + $"- {userName} (sick)";

                Assert.AreEqual(expected, e.ToString());
            }
        }
    }
}
