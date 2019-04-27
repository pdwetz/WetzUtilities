using Xunit;

namespace WetzUtilities.Test
{
    public class LongExtensionsTests
    {
        [Fact]
        public void ParseByteSizeTest_whole()
        {
            long v = 15_000;
            Assert.Equal("15 kB", v.ParseByteSize());
        }

        [Fact]
        public void ParseByteSize_fraction()
        {
            long v = 425_720;
            Assert.Equal("425.7 kB", v.ParseByteSize());
        }

        [Fact]
        public void ParseByteSizeTest_format()
        {
            long v = 15_430_800;
            Assert.Equal("15.43 MB", v.ParseByteSize("#,##0.00"));
        }
        
        [Fact]
        public void ParseByteSizeTest_large()
        {
            long v = 9_500_000_000_000_000;
            Assert.Equal("9.5 PB", v.ParseByteSize());
        }
    }
}