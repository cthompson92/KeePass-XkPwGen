using System;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class GetSeparatorCharacter
    {
        public static char? GetNext(SeparatorOptions options, CryptoRandomStream crsRandomSource)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            switch (options.SeparatorType)
            {
                case SeparatorType.None:
                    return GetNoSeparatorCharacter();

                case SeparatorType.SpecifiedCharacter:
                    return GetSpecifiedSeparatorCharacter(options);

                case SeparatorType.RandomCharacter:
                    return GetRandomSeparatorCharacter(options, crsRandomSource);

                default:
                    throw new ArgumentOutOfRangeException("options", options.SeparatorType, "Unknown separator type specified.");
            }
        }

        private static char? GetRandomSeparatorCharacter(SeparatorOptions options, CryptoRandomStream crsRandomSource)
        {
            if (string.IsNullOrEmpty(options.SeparatorAlphabet))
            {
                throw new ArgumentException("Random separator alphabet was not provided.");
            }

            // for perf, don't do any random generation if alphabet is only 1 character
            // this is effectively the same behavior as SpecifiedCharacter
            if (options.SeparatorAlphabet.Length == 1)
            {
                return options.SeparatorAlphabet[0];
            }

            var randomIndex = crsRandomSource.NextRandomIndex(options.SeparatorAlphabet);
            return options.SeparatorAlphabet[randomIndex];
        }

        private static char? GetNoSeparatorCharacter()
        {
            return null;
        }

        private static char? GetSpecifiedSeparatorCharacter(SeparatorOptions options)
        {
            if (!options.SpecificSeparatorCharacter.HasValue)
            {
                throw new ArgumentException("Specified separator was not provided.");
            }

            return options.SpecificSeparatorCharacter.Value;
        }
    }
}