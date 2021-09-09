using System;
using KeePassLib.Cryptography;

namespace XKPwGen.Tests
{
    internal static class TestCryptoRandomStream
    {
        private static CryptoRandomStream _instance;

        internal static CryptoRandomStream Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Generate(CrsAlgorithm.ChaCha20);
                }

                return _instance;
            }
            set
            {
                throw new InvalidOperationException("Singleton.");
            }
        }

        private static CryptoRandomStream Generate(CrsAlgorithm a)
        {
            return new CryptoRandomStream(a, new byte[]
            {
                0x23, 0x9B, 0x8C, 0x92,
                0x33, 0x9A, 0xFF, 0x4B,
                0xA6, 0x4C, 0xC2, 0xAA,
                0x2B, 0x08, 0x40, 0x54,
            });
        }
    }
}