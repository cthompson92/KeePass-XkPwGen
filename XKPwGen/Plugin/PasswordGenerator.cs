using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;
using XkPwGen;

namespace XKPwGen
{
    public class PasswordGenerator : CustomPwGenerator
    {
        private static readonly PwUuid _guid = new PwUuid(new byte[]
        {
            0x23, 0x9B, 0x8C, 0x92,
            0x33, 0x9A, 0xFF, 0x4B,
            0xA6, 0x4C, 0xC2, 0xAA,
            0x2B, 0x08, 0x40, 0x54,
        });

        /// <summary>Password generation function.</summary>
        /// <param name="prf">Password generation options chosen
        /// by the user. This may be <c>null</c>, if the default
        /// options should be used.</param>
        /// <param name="crsRandomSource">Source that the algorithm
        /// can use to generate random numbers.</param>
        /// <returns>Generated password or <c>null</c> in case
        /// of failure. If returning <c>null</c>, the caller assumes
        /// that an error message has already been shown to the user.</returns>
        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            if (prf == null) { Debug.Assert(false); }
            else
            {
                Debug.Assert(prf.CustomAlgorithmUuid == Convert.ToBase64String(_guid.UuidBytes, Base64FormattingOptions.None));
            }

            var options = OptionsManager.LoadOptions(prf.Name);

            var pw = GeneratePassword(crsRandomSource, options);

            return new ProtectedString(false, pw);
        }

        internal static string GeneratePassword(CryptoRandomStream crsRandomSource, PasswordGeneratorOptions options)
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

        /// <summary>
        /// Each custom password generation algorithm must have
        /// its own unique UUID.
        /// </summary>
        public override PwUuid Uuid
        {
            get { return _guid; }
        }

        /// <summary>
        /// Displayable name of the password generation algorithm.
        /// </summary>
        public override string Name
        {
            get { return "XKPwGen"; }
        }

        public override bool SupportsOptions
        {
            get { return true; }
        }

        public override string GetOptions(string strCurrentOptions)
        {
            return base.GetOptions(strCurrentOptions);
        }
    }
}
