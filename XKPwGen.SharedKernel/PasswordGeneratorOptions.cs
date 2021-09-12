namespace XKPwGen.SharedKernel
{
    public class PasswordGeneratorOptions
    {
        public WordOptions WordOptions { get; set; }

        public TransformationOptions Transformations { get; set; }

        public SeparatorOptions Separator { get; set; }

        public PaddingDigitOptions PaddingDigits { get; set; }

        public PaddingSymbolOptions PaddingSymbols { get; set; }

        public PasswordGeneratorOptions()
        {
            WordOptions = new WordOptions();
            Transformations = new TransformationOptions();
            Separator = new SeparatorOptions();
            PaddingDigits = new PaddingDigitOptions();
            PaddingSymbols = new PaddingSymbolOptions();
        }
    }
}