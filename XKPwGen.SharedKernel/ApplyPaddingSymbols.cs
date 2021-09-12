using System;
using KeePassLib.Cryptography;
using XKPwGen;
using XKPwGen.SharedKernel;

namespace XkPwGen
{
    public static class ApplyPaddingSymbols
    {
        public static string Apply(string pw, PaddingSymbolOptions options, CryptoRandomStream crsRandomSource)
        {
            switch (options.PaddingType)
            {
                case PaddingType.None:
                    return pw;

                case PaddingType.Fixed:
                {
                    var symbol = GetRandomSymbol(options.PaddingCharacterAlphabet, crsRandomSource);

                    return GetSymbolString(options.SymbolsStart, symbol, crsRandomSource)
                         + pw
                         + GetSymbolString(options.SymbolsEnd, symbol, crsRandomSource);
                }

                case PaddingType.Adaptive:
                {
                    if (pw.Length >= options.TargetLength)
                    {
                        return pw;
                    }

                    var symbol = GetRandomSymbol(options.PaddingCharacterAlphabet, crsRandomSource);

                    return pw
                         + GetSymbolString(options.TargetLength - pw.Length, symbol, crsRandomSource);
                }

                default:
                    throw new ArgumentException("Unknown padding type specified.");
            }
        }

        private static char GetRandomSymbol(string alphabet, CryptoRandomStream crsRandomSource)
        {
            if (string.IsNullOrWhiteSpace(alphabet))
            {
                throw new ArgumentNullException("alphabet");
            }

            var symbolIndex = crsRandomSource.NextRandomIndex(alphabet);
            return alphabet[symbolIndex];
        }

        private static string GetSymbolString(int length, char symbol, CryptoRandomStream crsRandomSource)
        {
            if (length == 0)
            {
                return string.Empty;
            }

            return new string(symbol, length);
        }
    }
}