﻿using System.Collections.Generic;
using System.Linq;
using KeePassLib.Cryptography;

namespace XKPwGen.SharedKernel
{
    public static class Algorithm
    {
        public static string GeneratePassword(CryptoRandomStream crsRandomSource, PasswordGeneratorOptions options)
        {
            // Get the set of words that will be used in the password
            var words = GetWordSequence(options.WordOptions, crsRandomSource)
                // apply word transformations
               .Transform(options.Transformations);

            // combine the words into a string with separators (if/as needed)
            var pw = WordSequenceCombiner.Combine(words.ToArray(), options.Separator, crsRandomSource);

            // pad with digits before/after
            pw = ApplyPaddingDigits.Apply(pw, options.PaddingDigits, crsRandomSource);

            // pad with symbols at the start/end
            pw = ApplyPaddingSymbols.Apply(pw, options.PaddingSymbols, crsRandomSource);

            return pw;
        }

        private static IEnumerable<string> GetWordSequence(WordOptions options, CryptoRandomStream crsRandomSource)
        {
            return GetRandomWords
               .GetWords(options.NumberOfWords, options.MinLength, options.MaxLength, crsRandomSource);
        }
    }
}