using System;
using Xunit;
using WetzUtilities;

namespace WetzUtilities.Test
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsEmptyTest()
        {
            Assert.True("".IsEmpty());
            Assert.False("foo".IsEmpty());
        }

        [Fact]
        public void ContainsTest()
        {
            Assert.True("the Contains test".Contains("con", StringComparison.OrdinalIgnoreCase));
            Assert.False("the Contains test".Contains("con", StringComparison.Ordinal));
        }

        [Fact]
        public void SafeEqualsTest()
        {
            string source = null;
            string target = null;
            Assert.True(source.SafeEquals(target));
        }

        [Fact]
        public void SafeHashCodeTest()
        {
            string source = null;
            Assert.Equal(0, source.SafeHashCode());
        }

        [Fact]
        public void ShaveTest()
        {
            Assert.Equal("foo..", "foo bar".Shave(3, ".."));
        }

        [Fact]
        public void ParsePrefixTest()
        {
            Assert.Equal("Phrase, The", "The Phrase".ParsePrefix("The "));
        }

        [Fact]
        public void IsUnorderedSubstringTest()
        {
            Assert.True("abc".IsUnorderedSubstring("decba"));
        }

        [Fact]
        public void TryParseDateTest()
        {
            DateTime? target = null;
            Assert.Equal(target, "".TryParseDate());
        }

        [Fact]
        public void TryParseDateOffsetTest()
        {
            DateTimeOffset? target = null;
            Assert.Equal(target, "".TryParseDateOffset());
        }

        [Fact]
        public void URLFriendlyTest()
        {
            Assert.Equal("test-phrase", "Test Phrase".URLFriendly()); 
        }

        [Fact]
        public void ParseTimeSpanTest()
        {
            TimeSpan target = new TimeSpan(1, 5, 20);
            Assert.Equal(target, "01:05:20".ParseTimeSpan());
        }
    }
}