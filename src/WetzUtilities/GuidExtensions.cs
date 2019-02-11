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
    /// Extension methods for Guids, normal as well as nullable variations
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Check if given Guid has no value or is Empty
        /// </summary>
        public static bool IsEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }

        /// <summary>
        /// Check if given Guid has a value and is not Empty
        /// </summary>
        public static bool IsNotEmpty(this Guid? guid)
        {
            return guid.HasValue && guid.Value != Guid.Empty;
        }

        /// <summary>
        /// Check if given Guid has is Empty
        /// </summary>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// Check if given Guid is not Empty
        /// </summary>
        public static bool IsNotEmpty(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        /*
        Source from https://github.com/tmsmith/Dapper-Extensions/blob/master/DapperExtensions/DapperExtensionsConfiguration.cs
        Copyright 2011 Thad Smith, Page Brooks and contributors
        Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
        http://www.apache.org/licenses/LICENSE-2.0
        Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
        */
        // Note: Changed usage of DateTime.Now to DateTime.UtcNow
        /// <summary>
        /// Retrieve a sequential Guid to allow for ordering by the field
        /// </summary>
        /// <returns>Guid</returns>
        public static Guid GetNextGuid()
        {
            byte[] b = Guid.NewGuid().ToByteArray();
            var dateTime = new DateTime(1900, 1, 1);
            DateTime now = DateTime.UtcNow;
            var timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes1 = BitConverter.GetBytes(timeSpan.Days);
            byte[] bytes2 = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes1);
            Array.Reverse(bytes2);
            Array.Copy(bytes1, bytes1.Length - 2, b, b.Length - 6, 2);
            Array.Copy(bytes2, bytes2.Length - 4, b, b.Length - 4, 4);
            return new Guid(b);
        }
    }
}