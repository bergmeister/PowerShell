// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if !CORECLR
using System.Collections.Generic;

namespace System.Management.Automation
{
    /// <summary>
    /// A set of extension methods that a adapt fullclr methods to coreclr methods to avoid unnecessary pragmas.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Adapts String.StartsWith(char) method from CoreClr to FullClr.
        /// </summary>
        /// <param name="_string"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StartsWith(this String _string, char value)
        {
            return _string.StartsWith(value.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtensionMethods
    {
        /// <summary>
        /// Adapts IDictionary.TryAdd(TKey, TValue) method from CoreClr to FullClr.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            dictionary.Add(key, value);
            return true;
        }
    }
}
#endif