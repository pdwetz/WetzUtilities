/*
Copyright 2020 Peter Wetzel

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
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