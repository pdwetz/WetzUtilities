namespace WetzUtilities
{
    public static class LongExtensions
    {
        /// <summary>
        /// Parse a rough estimate of bytes in "english", using decimal size (not binary).
        /// Meant for simple display purposes.
        /// </summary>
        public static string ParseByteSize(this long byteSize, string format = "#,###.#")
        {
            const decimal kBSize = 1_000;
            const decimal MBSize = 1_000_000;
            const decimal GBSize = 1_000_000_000;
            const decimal TBSize = 1_000_000_000_000;
            const decimal PBSize = 1_000_000_000_000_000;
            decimal v = byteSize;
            if (byteSize < kBSize)
            {
                return $"{v.ToString(format)} bytes";
            }
            if (byteSize < MBSize)
            {
                return $"{(v / kBSize).ToString(format)} kB";
            }
            if (byteSize < GBSize)
            {
                return $"{(v / MBSize).ToString(format)} MB";
            }
            if (byteSize < TBSize)
            {
                return $"{(v / GBSize).ToString(format)} GB";
            }
            if (byteSize < PBSize)
            {
                return $"{(v / TBSize).ToString(format)} TB";
            }
            return $"{(v / PBSize).ToString(format)} PB";
        }
    }
}