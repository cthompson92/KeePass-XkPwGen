using System.Collections.Generic;

namespace XKPwGen.Core
{
    public class PasswordGeneratorOptions
    {
        public CaseTransformationType CaseTransformation { get; set; }

        public SeparatorType SeparatorType { get; set; }

        public IEnumerable<char> SeparatorAlphabet { get; set; }

        public PaddingType PaddingType { get; set; }

        public int PaddingDigitsBefore { get; set; }

        public int PaddingDigitsAfter { get; set; }

        public PaddingSymbolCharacterType PaddingSymbolType { get; set; }

        public IEnumerable<char> PaddingCharacterAlphabet { get; set; }
    }
}