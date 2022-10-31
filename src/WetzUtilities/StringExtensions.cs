/*
Copyright 2022 Peter Wetzel

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
using System.Linq;
using System.Text;

namespace WetzUtilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if given string is IsNullOrWhiteSpace
        /// </summary>
        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrWhiteSpace(val);
        }

        /// <summary>
        /// Check if given string is not IsNullOrWhiteSpace
        /// </summary>
        public static bool IsNotEmpty(this string val)
        {
            return !string.IsNullOrWhiteSpace(val);
        }

        /// <summary>
        /// Any empty string returns null, any non-empty string is trimmed.
        /// </summary>
        public static string Clean(this string val)
        {
            return string.IsNullOrWhiteSpace(val) ? null : val.Trim();
        }

        /// <summary>
        /// Check if source is a substring or not. Actual order of characters is ignored (e.g. "abc" would be valid substring of "decba")
        /// </summary>
        public static bool IsUnorderedSubstring(this string source, string target)
        {
            if (source == null)
            {
                return target == null;
            }
            if (source.Equals(target))
            {
                return true;
            }
            int excepted = source.Except(target).Count();
            int intersected = source.Intersect(target).Count();
            return excepted == 0 && intersected == source.Length;
        }

        /// <summary>
        /// Allows chaining equality method regardless of whether source is null or not
        /// </summary>
        public static bool SafeEquals(this string source, string other, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null)
            {
                return other == null;
            }
            return source.Equals(other, comp);
        }

        /// <summary>
        /// Allows chaining hash method regardless of whether source is null or not
        /// </summary>
        public static int SafeHashCode(this string source)
        {
            return source?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Shortens any string over a set amount, appending a string if it does so
        /// </summary>
        public static string Shave(this string source, int maxLength, string concat = "...")
        {
            if (string.IsNullOrEmpty(source) || source.Length <= maxLength)
            {
                return source;
            }
            return string.Concat(source.Substring(0, maxLength), concat);
        }

        /// <summary>
        /// Parse prefix within phrase, re-ordering result so that it is comma separated with prefix trailing
        /// Example "The Phrase", "The " => "Phrase, The"
        /// </summary>
        public static string ParsePrefix(this string phrase, string prefix, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (phrase.IsEmpty())
            {
                return null;
            }
            if (prefix.IsEmpty())
            {
                return phrase;
            }
            int i = phrase.IndexOf(prefix, comp);
            if (i != 0)
            {
                return phrase;
            }
            return $"{phrase.Substring(prefix.Length).Trim()}, {phrase.Substring(0, prefix.Length).Trim()}";
        }

        /// <summary>
        /// Helper method for parsing a date string into a nullable DateTime.
        /// If invalid, empty, etc., result will be null.
        /// </summary>
        public static DateTime? TryParseDate(this string date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return DateTime.TryParse(date, out DateTime dt) ? dt : (DateTime?)null;
        }

        /// <summary>
        /// Helper method for parsing a date string into a nullable DateTimeOffset.
        /// If invalid, empty, etc., result will be null.
        /// </summary>
        public static DateTimeOffset? TryParseDateOffset(this string date)
        {
            if (date.IsEmpty())
            {
                return null;
            }
            return DateTimeOffset.TryParse(date, out DateTimeOffset dt) ? dt : (DateTimeOffset?)null;
        }

        /// <summary>
        /// Helper method for parsing a time with a given date and time zone.
        /// If time or date are empty, result will be null.
        /// </summary>
        public static DateTimeOffset? ParseTime(this string time, DateTime date, string timeZoneId)
        {
            if (time.IsEmpty() || date.IsEmpty())
            {
                return null;
            }
            var dt = DateTime.Parse($"{date.SafeShortDate()} {time}");
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var offset = tz.GetUtcOffset(dt);
            return new DateTimeOffset(dt, offset);
        }

        /// <summary>
        /// Parses given string into a TimeSpan.
        /// Empty strings result in new TimeSpan.
        /// </summary>
        public static TimeSpan ParseTimeSpan(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return new TimeSpan();
            }
            var split = source.Split(new char[] { ':' });
            return split.Length switch
            {
                3 => TimeSpan.ParseExact(source, @"h\:mm\:ss", null),
                2 => TimeSpan.ParseExact(source, @"m\:ss", null),
                _ => TimeSpan.ParseExact(source, @"ss", null)
            };
        }

        /// <summary>
        /// Case-insensitive override for contains
        /// Source:
        /// http://stackoverflow.com/a/444818/21865
        /// (Creative Commons Attribution Share Alike)
        /// </summary>
        public static bool Contains(this string source, string target, StringComparison comp)
        {
            if (source == null || target == null)
            {
                return false;
            }
            return source.IndexOf(target, comp) >= 0;
        }

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n)
        /// Source:
        /// http://stackoverflow.com/a/25486/21865
        /// (Creative Commons Attribution Share Alike)
        /// </summary>
        public static string URLFriendly(this string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                         c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        /// <summary>
        /// Source:
        /// http://meta.stackoverflow.com/a/7696
        /// (Creative Commons Attribution Share Alike)
        /// </summary>
        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
    }
}