using System;
using Xunit;

namespace WetzUtilities.Test
{
    public class DateTimeOffsetExtensionsTests
    {
        [Fact]
        public void SafeDateTimeZone_same()
        {
            var dt = new DateTimeOffset(2000, 12, 15, 17, 30, 0, 0, new TimeSpan(-5, 0, 0));
            Assert.Equal("2000-12-15 5:30 PM", dt.SafeDateTimeZone("Eastern Standard Time"));
        }

        [Fact]
        public void SafeDateTimeZone_different()
        {
            var dt = new DateTimeOffset(new DateTime(2000, 9, 15, 5, 30, 0, DateTimeKind.Utc));
            Assert.Equal("2000-09-14 10:30 PM", dt.SafeDateTimeZone("Pacific Standard Time"));
        }

        [Fact]
        public void SafeDateTimeZone_format()
        {
            var dt = new DateTimeOffset(2000, 12, 15, 17, 30, 0, 0, new TimeSpan(-5, 0, 0));
            Assert.Equal("15 Dec 2000 04:30 PM CST", dt.SafeDateTimeZone("Central Standard Time", "dd MMM yyyy hh:mm tt \"CST\""));
        }
    }
}