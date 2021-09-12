using System.Text;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class WordSequenceCombiner
    {
        public static string Combine(string[] words, char? separator, CryptoRandomStream crsRandomSource)
        {
            var sb = new StringBuilder();
            
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

        
    }
}