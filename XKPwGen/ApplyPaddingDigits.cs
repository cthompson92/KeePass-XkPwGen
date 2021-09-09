using System.Text;
using KeePassLib.Cryptography;
using XKPwGen;

namespace XkPwGen
{
    public static class ApplyPaddingDigits
    {
        public static string Before(PaddingDigitOptions options, CryptoRandomStream crsRandomSource)
        {
            return GetDigitString(options.DigitsBefore, crsRandomSource);
        }

        public static string After(PaddingDigitOptions options, CryptoRandomStream crsRandomSource)
        {
            return GetDigitString(options.DigitsAfter, crsRandomSource);
        }

        public static string Apply(string pw, PaddingDigitOptions options, CryptoRandomStream crsRandomSource)
        {
            return Before(options, crsRandomSource)
                 + pw
                 + After(options, crsRandomSource);
        }

        private static string GetDigitString(int length, CryptoRandomStream crsRandomSource)
        {
            if (length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                var digit = crsRandomSource.NextRandom((int)'0', (int)'9');
                sb.Append((char)digit);
            }

            return sb.ToString();
        }
    }
}