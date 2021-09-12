using System;
using System.Collections.Generic;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    internal static class CryptoRandomStreamExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="crs"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        /// <remarks>This algorithm is based on <see href="https://stackoverflow.com/a/6299708/5038805" />.</remarks>
        internal static ulong NextRandom(this CryptoRandomStream crs, int minValue, int maxValue)
        {
            if (minValue < 0)
                throw new ArgumentOutOfRangeException("minValue", minValue, "minValue must be positive or zero.");

            if (maxValue < 1)
                throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue must be positive.");

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue", minValue, "minValue cannot exceed maxLength");

            if (minValue == maxValue)
                throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue must be higher than minValue");

            var min = (ulong)minValue;

            var diff = (ulong)maxValue - min;
            byte[] buffer;
            while (true)
            {
                buffer = crs.GetRandomBytes(4);
                var rand = BitConverter.ToUInt32(buffer, 0);

                var max = 1 + (ulong)uint.MaxValue;
                var remainder = max % diff;
                if (rand < max - remainder) return min + (rand % diff);
            }
        }
        
        /// <summary>
        /// Generates a random index value for the provided <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="crs"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int NextRandomIndex<T>(this CryptoRandomStream crs, IList<T> list)
        {
            return (int)NextRandom(crs, 0, list.Count - 1);
        }
        
        /// <summary>
        /// Generates a random index value for the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="crs"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static int NextRandomIndex(this CryptoRandomStream crs, string value)
        {
            return (int)NextRandom(crs, 0, value.Length - 1);
        }
    }
}