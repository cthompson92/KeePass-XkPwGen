using System;
using System.Linq;
using System.Windows.Forms;
using XKPwGen.Core;

namespace XKPwGen.Options
{
    public partial class Form1 : Form
    {
        private const string DefaultCharacterAlphabet = "!@$%^&*-_+=:|~?/.;()[]{}";

        public Form1()
        {
            InitializeComponent();

            CaseTransformationDropdown.DataSource = Enum.GetValues<CaseTransformationType>();

            SeparatorTypeDropdown.DataSource = Enum.GetValues<SeparatorType>();

            PaddingTypeDropdown.DataSource = Enum.GetValues<PaddingType>();
            PaddingCharacterDropdown.DataSource = Enum.GetValues<PaddingSymbolCharacterType>();

            PaddingDigitsBeforeDropdown.DataSource = Enumerable.Range(0, 6).ToList();
            PaddingDigitsAfterDropdown.DataSource = Enumerable.Range(0, 6).ToList();

            PaddingSymbolsBeforeDropdown.DataSource = Enumerable.Range(0, 6).ToList();
            PaddingSymbolsAfterDropdown.DataSource = Enumerable.Range(0, 6).ToList();

            SeparatorAlphabetTextbox.Text = DefaultCharacterAlphabet;
            PaddingCharacterAlphabetTextbox.Text = DefaultCharacterAlphabet;
        }

        private PasswordGeneratorOptions BuildOptions()
        {
            return new PasswordGeneratorOptions()
            {
                CaseTransformation = (CaseTransformationType)(CaseTransformationDropdown.SelectedItem ?? CaseTransformationType.None),
                PaddingCharacterAlphabet = PaddingCharacterAlphabetTextbox.Text,
                PaddingDigitsAfter = (int)PaddingDigitsAfterDropdown.SelectedItem,
                PaddingDigitsBefore = (int)PaddingDigitsBeforeDropdown.SelectedItem,
                PaddingSymbolType = (PaddingSymbolCharacterType)(PaddingCharacterDropdown.SelectedItem ?? PaddingSymbolCharacterType.UseSeparatorCharacter),
                PaddingType = (PaddingType)(PaddingTypeDropdown.SelectedItem ?? PaddingType.None),
                SeparatorAlphabet = SeparatorAlphabetTextbox.Text,
                SeparatorType = (SeparatorType)(SeparatorTypeDropdown.SelectedItem ?? SeparatorType.None),
            };
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            var options = BuildOptions();

            OptionsManager.SaveOptions(options, "default");
        }
    }
}
