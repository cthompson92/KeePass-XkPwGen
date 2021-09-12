using System;
using System.Diagnostics;
using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;
using XKPwGen.Options;
using XKPwGen.SharedKernel;

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

        private PasswordGeneratorOptions _options = null;

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

            var options = OptionsManager.LoadCurrentOptions();

            try
            {
                if (options == null)
                {
#if DEBUG 
                    options = new PasswordGeneratorOptions()
                    {
                        WordOptions = new WordOptions()
                        {
                            NumberOfWords = 3,
                            MaxLength = 8,
                            MinLength = 3
                        },
                        Transformations = new TransformationOptions()
                        {
                            CaseTransformation = CaseTransformationType.AlternatingWordCase,
                        },
                        Separator = new SeparatorOptions()
                        {
                            SpecificSeparatorCharacter = ' ',
                            SeparatorType = SeparatorType.SpecifiedCharacter,
                        },
                        PaddingDigits = new PaddingDigitOptions()
                        {
                            DigitsAfter = 2,
                            DigitsBefore = 2,
                        },
                        PaddingSymbols = new PaddingSymbolOptions()
                        {
                            SymbolsStart = 2,
                            SymbolsEnd = 2,
                            PaddingType = PaddingType.Fixed,
                            PaddingCharacterAlphabet = PwCharSet.Special,
                        }
                    };
#else
                    options = new PasswordGeneratorOptions();
#endif
                }

                var pw = Algorithm.GeneratePassword(crsRandomSource, options);

                return new ProtectedString(false, pw);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }            
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
            var optionsUi = new OptionsUiForm();
            optionsUi.ShowDialog();

            return optionsUi.ProfileName;
        }
    }
}
