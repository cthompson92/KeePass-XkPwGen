using System;
using FluentAssertions;
using FluentAssertions.Execution;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class WordSequenceCombinerTests
    {
        [Theory]
        [InlineData(new string[] { "word", "the", "there" }, "wordthethere")]
        [InlineData(new string[] { "battery", "horse", "staple" }, "batteryhorsestaple")]
        public void CombineShouldReturnConcatenatedStringWhenNoSeparators(string[] words, string expected)
        {
            //arrange

            //act
            var result = WordSequenceCombiner.Combine(words, null, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(new string[] { "word", "the", "there" }, ' ', " word the there ")]
        [InlineData(new string[] { "battery", "horse", "staple" }, '-', "-battery-horse-staple-")]
        [InlineData(new string[] { "ladder", "pineapple", "hairy" }, '_', "_ladder_pineapple_hairy_")]
        public void CombineShouldReturnConcatenatedStringUsingCorrectSeparator(string[] words, char separator, string expected)
        {
            //arrange

            //act
            var result = WordSequenceCombiner.Combine(words, separator, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(expected);
        }
    }

    [Trait("Category", "Unit")]
    public class GetSeparatorCharacterTests
    {
        [Fact]
        public void GetNextShouldThrowWhenOptionsIsNull()
        {
            //arrange

            //act
            Action action = () => GetSeparatorCharacter.GetNext(null, TestCryptoRandomStream.Instance);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNextShouldThrowWhenSeparatorTypeIsNotValid()
        {
            //arrange
            var options = new SeparatorOptions()
            {
                SeparatorType = (SeparatorType)int.MinValue,
            };

            //act
            Action action = () => GetSeparatorCharacter.GetNext(options, TestCryptoRandomStream.Instance);

            //assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GetNextShouldReturnNullWhenSeparatorTypeIsNone()
        {
            //arrange
            var options = new SeparatorOptions()
            {
                SeparatorType = SeparatorType.None,
            };

            //act
            var result = GetSeparatorCharacter.GetNext(options, TestCryptoRandomStream.Instance);

            //assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineData('_')]
        [InlineData(' ')]
        [InlineData('-')]
        [InlineData('.')]
        public void GetNextShouldReturnSpecifiedSeparatorWhenSeparatorTypeIsSpecifiedCharacter(char separator)
        {
            //arrange
            var options = new SeparatorOptions()
            {
                SeparatorType = SeparatorType.SpecifiedCharacter,
                SpecificSeparatorCharacter = separator,
            };

            //act
            var result = GetSeparatorCharacter.GetNext(options, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Should().Be(separator);
            }
        }

        [Theory]
        [InlineData("_")]
        [InlineData(" ")]
        [InlineData("-")]
        [InlineData(".")]
        [InlineData("!@#$%^&*()_+-=[]{}|\\/.,")]
        public void GetNextShouldReturnValidCharacterInSetWhenSeparatorTypeIsRandom(string alphabet)
        {
            //arrange
            var options = new SeparatorOptions
            {
                SeparatorType = SeparatorType.RandomCharacter,
                SeparatorAlphabet = alphabet,
            };

            //act
            var result = GetSeparatorCharacter.GetNext(options, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                alphabet.ToCharArray().Should().Contain(result.Value);
            }
        }
    }
}