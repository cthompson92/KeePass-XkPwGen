using System;
using System.Text;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class WordSequenceCombiner
    {
        public static string Combine(string[] words, SeparatorOptions options, CryptoRandomStream crsRandomSource)
        {
            var sb = new StringBuilder();

            var separator = GetNext(options, crsRandomSource);
            if (separator.HasValue)
            {
                sb.Append(separator.Value);
            }
            
            foreach (var t in words)
            {
                sb.Append(t);
                if (separator.HasValue)
                {
                    sb.Append(separator.Value);
                }
            }

            return sb.ToString();
        }

        public static char? GetNext(SeparatorOptions options, CryptoRandomStream crsRandomSource)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            switch (options.SeparatorType)
            {
                case SeparatorType.None:
                    return null;

                case SeparatorType.SpecifiedCharacter:
                    {
                        if (!options.SpecificSeparatorCharacter.HasValue)
                        {
                            throw new ArgumentException("Specified separator was not provided.");
                        }

                        return options.SpecificSeparatorCharacter.Value;
                    }

                case SeparatorType.RandomCharacter:
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

                default:
                    throw new ArgumentOutOfRangeException("options", options.SeparatorType, "Unknown separator type specified.");
            }
        }
    }
}