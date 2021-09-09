using FluentAssertions;
using XkPwGen;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class WordSequenceCombinerTests
    {
        [Theory]
        [InlineData(new string[]{ "word", "the", "there"}, "wordthethere")]
        [InlineData(new string[]{ "battery", "horse", "staple"}, "batteryhorsestaple")]
        public void CombineShouldReturnConcatenatedStringWhenNoSeparators(string[] words, string expected)
        {
            //arrange
            var separatorOptions = new SeparatorOptions()
            {
                SeparatorType = SeparatorType.None,
            };

            //act
            var result = WordSequenceCombiner.Combine(words, separatorOptions, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(new string[]{ "word", "the", "there"}, ' ', " word the there ")]
        [InlineData(new string[]{ "battery", "horse", "staple"}, '-', "-battery-horse-staple-")]
        [InlineData(new string[]{ "ladder", "pineapple", "hairy"}, '_', "_ladder_pineapple_hairy_")]
        public void CombineShouldReturnConcatenatedStringWhenSpecificSeparator(string[] words, char separator, string expected)
        {
            //arrange
            var separatorOptions = new SeparatorOptions()
            {
                SeparatorType = SeparatorType.SpecifiedCharacter,
                SpecificSeparatorCharacter = separator,
            };

            //act

            var result = WordSequenceCombiner.Combine(words, separatorOptions, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(new string[]{ "word", "the", "there"}, " @_-")]
        [InlineData(new string[]{ "battery", "horse", "staple"}, " @_-")]
        [InlineData(new string[]{ "ladder", "pineapple", "hairy"}, " @_-")]
        public void CombineShouldReturnConcatenatedStringWhenRandomSeparator(string[] words, string alphabet)
        {
            //arrange
            var separatorOptions = new SeparatorOptions()
            {
                SeparatorType = SeparatorType.RandomCharacter,
                SeparatorAlphabet = alphabet,
            };

            //act
            var result = WordSequenceCombiner.Combine(words, separatorOptions, TestCryptoRandomStream.Instance);

            //assert
            var separatorRegex = string.Format("[{0}]", alphabet);
            result.Should().MatchRegex(separatorRegex + string.Join(separatorRegex, words) + separatorRegex);
        }
    }
}