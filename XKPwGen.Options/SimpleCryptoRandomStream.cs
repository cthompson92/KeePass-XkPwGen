using System;
using KeePassLib.Cryptography;

namespace XKPwGen.Options
{
    /// <summary>
    /// A simple CRS implementation to be used for generating example passwords on the UI.
    ///
    /// This is not intended to be used to generate real, strong passwords.
    /// </summary>
    internal static class SimpleCryptoRandomStream
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

        private static readonly Random Randomizer = new Random();

        private static CryptoRandomStream Generate(CrsAlgorithm a)
        {
            var buffer = new byte[16];
            Randomizer.NextBytes(buffer);

            return new CryptoRandomStream(a, buffer);
        }
    }
}