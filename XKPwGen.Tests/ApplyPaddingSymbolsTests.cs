using System;
using FluentAssertions;
using FluentAssertions.Execution;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class ApplyPaddingSymbolsTests
    {
        private const string Password = "asdf";

        [Fact]
        public void ApplyShouldNotChangePasswordWhenTypeIsNone()
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.None,
                PaddingSymbolType = PaddingSymbolCharacterType.RandomCharacter,
            };

            //act
            var result = ApplyPaddingSymbols.Apply(Password, options, null, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(Password);
        }

        [Fact]
        public void ApplyShouldNotThrowWhenWhenTypeIsNoneAndCharacterIsSeparatorCharacterAndSeparatorCharacterIsNull()
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.None,
                PaddingSymbolType = PaddingSymbolCharacterType.UseSeparatorCharacter,
            };

            //act
            Action action = () => ApplyPaddingSymbols.Apply(Password, options, null, TestCryptoRandomStream.Instance);

            //assert
            action.Should().NotThrow();
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(5, 5)]
        [InlineData(5, 10)]
        [InlineData(28, 32)]
        [InlineData(32, 64)]
        public void ApplyShouldAddCorrectNumberOfPaddingSymbolsAtEndWhenTypeIsAdaptive(int initialLength, int targetLength)
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.Adaptive,
                PaddingSymbolType = PaddingSymbolCharacterType.RandomCharacter,
                PaddingCharacterAlphabet = "@!#%=~|",
                TargetLength = targetLength,
            };

            var pw = new string('A', initialLength);

            //act
            var result = ApplyPaddingSymbols.Apply(pw, options, null, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(targetLength);
                result.Should().MatchRegex(string.Format("{0}[{1}]{{{2}}}", pw, options.PaddingCharacterAlphabet, targetLength - initialLength));
            }
        }

        [Fact]
        public void ApplyShouldThrowWhenTypeIsAdaptiveAndCharacterIsSeparatorCharacterAndSeparatorCharacterIsNull()
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.Adaptive,
                PaddingSymbolType = PaddingSymbolCharacterType.UseSeparatorCharacter,
                PaddingCharacterAlphabet = "@!#%=~|",
                TargetLength = 7,
            };

            var pw = new string('A', 4);

            //act
            Action action = () => ApplyPaddingSymbols.Apply(pw, options, null, TestCryptoRandomStream.Instance);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ApplyShouldThrowWhenTypeIsFixedAndCharacterIsSeparatorCharacterAndSeparatorCharacterIsNull(int count)
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.Fixed,
                PaddingSymbolType = PaddingSymbolCharacterType.UseSeparatorCharacter,
                PaddingCharacterAlphabet = "@!#%=~|",
                SymbolsStart = count,
                SymbolsEnd = 0,
            };

            //act
            Action action = () => ApplyPaddingSymbols.Apply(Password, options, null, TestCryptoRandomStream.Instance);

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ApplyShouldAddCorrectNumberOfPaddingSymbolsAtStartWhenTypeIsFixed(int count)
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.Fixed,
                PaddingSymbolType = PaddingSymbolCharacterType.RandomCharacter,
                PaddingCharacterAlphabet = "@!#%=~|",
                SymbolsStart = count,
                SymbolsEnd = 0,
            };

            //act
            var result = ApplyPaddingSymbols.Apply(Password, options, null, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(Password.Length + count);
                result.Should().MatchRegex(string.Format("[{1}]{{{2}}}{0}", Password, options.PaddingCharacterAlphabet, count));
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ApplyShouldAddCorrectNumberOfPaddingSymbolsAtEndWhenTypeIsFixed(int count)
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.Fixed,
                PaddingSymbolType = PaddingSymbolCharacterType.RandomCharacter,
                PaddingCharacterAlphabet = "@!#%=~|",
                SymbolsStart = 0,
                SymbolsEnd = count,
            };

            //act
            var result = ApplyPaddingSymbols.Apply(Password, options, null, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(Password.Length + count);
                result.Should().MatchRegex(string.Format("{0}[{1}]{{{2}}}", Password, options.PaddingCharacterAlphabet, count));
            }
        }
    }
}