/*
Copyright 2019 Peter Wetzel

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
using System;

namespace WetzUtilities
{
    /// <summary>
    /// Extension methods for DateTimeOffsets, normal as well as nullable variations
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Return year from nullable date if possible
        /// </summary>
        public static int? SafeYear(this DateTimeOffset? date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return date.Value.Year;
        }

        /// <summary>
        /// Return ISO 8601 formatted date from nullable date if possible
        /// </summary>
        public static string SafeShortDate(this DateTimeOffset? date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return date.Value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Check if given date has no value or is MinValue
        /// </summary>
        public static bool IsEmpty(this DateTimeOffset? date)
        {
            return !date.HasValue || date.Value == DateTimeOffset.MinValue;
        }

        /// <summary>
        /// Check if given date has a value and is not MinValue
        /// </summary>
        public static bool IsNotEmpty(this DateTimeOffset? date)
        {
            return date.HasValue && date.Value > DateTimeOffset.MinValue;
        }

        /// <summary>
        /// Return year from date if possible
        /// </summary>
        public static int? SafeYear(this DateTimeOffset date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return date.Year;
        }

        /// <summary>
        /// Return ISO 8601 formatted date from date if possible
        /// </summary>
        public static string SafeShortDate(this DateTimeOffset date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Check if given date is MinValue
        /// </summary>
        public static bool IsEmpty(this DateTimeOffset date)
        {
            return date == DateTimeOffset.MinValue;
        }

        /// <summary>
        /// Check if given date is not MinValue
        /// </summary>
        public static bool IsNotEmpty(this DateTimeOffset date)
        {
            return date > DateTimeOffset.MinValue;
        }
    }
}