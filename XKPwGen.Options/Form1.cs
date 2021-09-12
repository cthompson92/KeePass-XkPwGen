﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XKPwGen.SharedKernel;

namespace XKPwGen.Options
{
    public partial class Form1 : Form
    {
        private const string DefaultCharacterAlphabet = "!@$%^&*-_+=:|~?/.;()[]{}";

        public Form1()
        {
            InitializeComponent();

            ProfileNameCombobox.DataSource = OptionsManager.GetProfileFiles()
                                                           .Select(x => Path.GetFileNameWithoutExtension(x.Name))
                                                           .ToList();

            DictionaryDropdown.DataSource = Enum.GetValues(typeof(WordDictionary));
            NumberOfWordsDropdown.DataSource = Enumerable.Range(2, 9).ToList();
            MinimumWordLengthDropdown.DataSource = Enumerable.Range(4, 9).ToList();
            MaximumWordLengthDropdown.DataSource = Enumerable.Range(4, 9).ToList();

            CaseTransformationDropdown.DataSource = Enum.GetValues(typeof(CaseTransformationType));

            SeparatorTypeDropdown.DataSource = Enum.GetValues(typeof(SeparatorType));
            SeparatorAlphabetTextbox.Text = DefaultCharacterAlphabet;

            PaddingDigitsBeforeDropdown.DataSource = Enumerable.Range(0, 6).ToList();
            PaddingDigitsAfterDropdown.DataSource = Enumerable.Range(0, 6).ToList();

            PaddingSymbolsBeforeDropdown.DataSource = Enumerable.Range(0, 6).ToList();
            PaddingSymbolsAfterDropdown.DataSource = Enumerable.Range(0, 6).ToList();
            PaddingTypeDropdown.DataSource = Enum.GetValues(typeof(PaddingType));
            PaddingCharacterDropdown.DataSource = Enum.GetValues(typeof(PaddingSymbolCharacterType));
            PaddingCharacterAlphabetTextbox.Text = DefaultCharacterAlphabet;

            NumberOfExamplePasswordsDropdown.DataSource = Enumerable.Range(1, 20).ToList();
        }

        internal static Form1 Default()
        {
            return From(new PasswordGeneratorOptions(), "");
        }

        public static Form1 From(PasswordGeneratorOptions options, string profileName)
        {
            var form = new Form1();
            form.ApplyValues(options, profileName);
            return form;
        }

        private void ApplyValues(PasswordGeneratorOptions options, string profileName)
        {
            ProfileName = profileName;
            ApplyProfile(options);
        }

        private void ApplyProfile(PasswordGeneratorOptions options)
        {
            DictionaryDropdown.SelectedItem = options.WordOptions.Dictionary;
            CaseTransformationDropdown.SelectedItem = options.Transformations.CaseTransformation;
            SeparatorTypeDropdown.SelectedItem = options.Separator.SeparatorType;
            PaddingTypeDropdown.SelectedItem = options.PaddingSymbols.PaddingType;
            PaddingCharacterDropdown.SelectedItem = options.PaddingSymbols.PaddingSymbolType;
            PaddingDigitsBeforeDropdown.SelectedItem = options.PaddingDigits.DigitsBefore;
            PaddingDigitsAfterDropdown.SelectedItem = options.PaddingDigits.DigitsAfter;
            PaddingSymbolsBeforeDropdown.SelectedItem = options.PaddingSymbols.SymbolsStart;
            PaddingSymbolsAfterDropdown.SelectedItem = options.PaddingSymbols.SymbolsEnd;
            SeparatorAlphabetTextbox.Text = options.Separator.SeparatorAlphabet;
            PaddingCharacterAlphabetTextbox.Text = options.PaddingSymbols.PaddingCharacterAlphabet;
        }

        private PasswordGeneratorOptions BuildOptions()
        {
            var options = new PasswordGeneratorOptions();
            options.WordOptions.Dictionary = (WordDictionary)(DictionaryDropdown.SelectedItem ?? WordDictionary.English);
            options.WordOptions.NumberOfWords = (int)NumberOfWordsDropdown.SelectedItem;
            options.WordOptions.MinLength = (int)MinimumWordLengthDropdown.SelectedItem;
            options.WordOptions.MaxLength = (int)MaximumWordLengthDropdown.SelectedItem;

            options.Transformations.CaseTransformation = (CaseTransformationType)(CaseTransformationDropdown.SelectedItem ?? CaseTransformationType.None);
            
            options.PaddingDigits.DigitsAfter = (int)PaddingDigitsAfterDropdown.SelectedItem;
            options.PaddingDigits.DigitsBefore = (int)PaddingDigitsBeforeDropdown.SelectedItem;

            options.Separator.SeparatorAlphabet = SeparatorAlphabetTextbox.Text;
            options.Separator.SeparatorType = (SeparatorType)(SeparatorTypeDropdown.SelectedItem ?? SeparatorType.None);
            if (options.Separator.SeparatorType == SeparatorType.SpecifiedCharacter)
            {
                options.Separator.SpecificSeparatorCharacter = SeparatorAlphabetTextbox.Text[0];
            }

            options.PaddingSymbols.PaddingCharacterAlphabet = PaddingCharacterAlphabetTextbox.Text;
            options.PaddingSymbols.PaddingSymbolType = (PaddingSymbolCharacterType)(PaddingCharacterDropdown.SelectedItem ?? PaddingSymbolCharacterType.UseSeparatorCharacter);
            options.PaddingSymbols.PaddingType = (PaddingType)(PaddingTypeDropdown.SelectedItem ?? PaddingType.None);
            options.PaddingSymbols.SymbolsStart = (int)PaddingSymbolsBeforeDropdown.SelectedItem;
            options.PaddingSymbols.SymbolsEnd = (int)PaddingSymbolsAfterDropdown.SelectedItem;

            return options;
        }

        public delegate void SaveButtonClicked(PasswordGeneratorOptions options, string profileName);

        public event SaveButtonClicked OnSaveButtonClicked;

        public string ProfileName
        {
            get
            {
                return ProfileNameCombobox.Text;
            }
            private set
            {
                foreach (var item in ProfileNameCombobox.Items)
                {
                    if ((string)item == value)
                    {
                        ProfileNameCombobox.SelectedItem = item;
                        return;
                    }
                }

                ProfileNameCombobox.Text = value;
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            var options = BuildOptions();

            if (OnSaveButtonClicked != null)
            {
                OnSaveButtonClicked.Invoke(options, ProfileName);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            XkpasswordSiteLink.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://xkpasswd.net/s/");
        }

        private void ProfileNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var options = OptionsManager.LoadOptions(ProfileName);
            ApplyProfile(options);
        }

        private void GenerateExamplesButton_Click(object sender, EventArgs e)
        {
            var count = (int)NumberOfExamplePasswordsDropdown.SelectedItem;

            var sb = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                sb.AppendLine(Algorithm.GeneratePassword(SimpleCryptoRandomStream.Instance, BuildOptions()));
            }

            ExamplePasswordsTextbox.Text = sb.ToString();
        }
    }
}
