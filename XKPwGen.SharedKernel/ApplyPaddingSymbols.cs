using System;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class ApplyPaddingSymbols
    {
        public static string Apply(string pw, PaddingSymbolOptions options, char? separatorCharacter, CryptoRandomStream crsRandomSource)
        {
            switch (options.PaddingType)
            {
                case PaddingType.None:
                    return ApplyNoSymbolPadding(pw);

                case PaddingType.Fixed:
                    return ApplyFixedSymbolPadding(pw, options, separatorCharacter, crsRandomSource);

                case PaddingType.Adaptive:
                    return ApplyAdaptiveSymbolPadding(pw, options, separatorCharacter, crsRandomSource);

                default:
                    throw new ArgumentException("Unknown padding type specified.");
            }
        }

        private static string ApplyNoSymbolPadding(string pw)
        {
            return pw;
        }

        private static string ApplyAdaptiveSymbolPadding(string pw, PaddingSymbolOptions options, char? separatorCharacter, CryptoRandomStream crsRandomSource)
        {
            if (pw.Length >= options.TargetLength)
                return pw;

            var symbol = GetRandomSymbol(options, separatorCharacter, crsRandomSource);

            return pw
                 + GetSymbolString(options.TargetLength - pw.Length, symbol, crsRandomSource);
        }

        private static string ApplyFixedSymbolPadding(string pw, PaddingSymbolOptions options, char? separatorCharacter, CryptoRandomStream crsRandomSource)
        {
            var symbol = GetRandomSymbol(options, separatorCharacter, crsRandomSource);

            return GetSymbolString(options.SymbolsStart, symbol, crsRandomSource)
                 + pw
                 + GetSymbolString(options.SymbolsEnd, symbol, crsRandomSource);
        }

        private static char GetRandomSymbol(PaddingSymbolOptions options, char? separatorCharacter, CryptoRandomStream crsRandomSource)
        {
            if (options.PaddingSymbolType == PaddingSymbolCharacterType.UseSeparatorCharacter)
            {
                return GetSeparatorCharacterAsSymbol(separatorCharacter);
            }

            return GenerateRandomSymbol(options.PaddingCharacterAlphabet, crsRandomSource);
        }

        private static char GetSeparatorCharacterAsSymbol(char? separatorCharacter)
        {
            if (separatorCharacter == null)
            {
                throw new ArgumentNullException("separatorCharacter");
            }

            return separatorCharacter.Value;
        }

        private static char GenerateRandomSymbol(string alphabet, CryptoRandomStream crsRandomSource)
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