namespace XKPwGen
{
    public class PaddingSymbolOptions
    {
        public PaddingType PaddingType { get; set; }

        public int SymbolsStart { get; set; }

        public int SymbolsEnd { get; set; }

        public PaddingSymbolCharacterType PaddingSymbolType { get; set; }

        public string PaddingCharacterAlphabet { get; set; }

        public int TargetLength { get; set; }
    }
}