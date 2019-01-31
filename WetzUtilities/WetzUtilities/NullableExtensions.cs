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

namespace WetzUtilities
{
    /// <summary>
    /// Helper methods for not having to explicitly do null checks for comparisons or hash gets
    /// </summary>
    public static class NullableExtensions
    {
        public static bool SafeEquals<T>(this T? source, T? other) where T : struct
        {
            if (source.HasValue)
            {
                return source.Equals(other);
            }
            return !other.HasValue;
        }

        public static int SafeHashCode<T>(this T? source) where T : struct
        {
            return source.HasValue ? source.GetHashCode() : 0;
        }
    }
}