using FluentAssertions;
using FluentAssertions.Execution;
using XkPwGen;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class ApplyPaddingSymbolsTests
    {
        [Fact]
        public void ApplyShouldNotChangePasswordWhenTypeIsNone()
        {
            //arrange
            var options = new PaddingSymbolOptions()
            {
                PaddingType = PaddingType.None,
            };

            const string pw = "qwerty";

            //act
            var result = ApplyPaddingSymbols.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            result.Should().Be(pw);
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
                PaddingCharacterAlphabet = "@!#%=~|",
                TargetLength = targetLength,
            };

            var pw = new string('A', initialLength);

            //act
            var result = ApplyPaddingSymbols.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(targetLength);
                result.Should().MatchRegex(string.Format("{0}[{1}]{{{2}}}", pw, options.PaddingCharacterAlphabet, targetLength - initialLength));
            }
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
                PaddingCharacterAlphabet = "@!#%=~|",
                SymbolsStart = count,
                SymbolsEnd = 0,
            };

            const string pw = "asdf";

            //act
            var result = ApplyPaddingSymbols.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(pw.Length + count);
                result.Should().MatchRegex(string.Format("[{1}]{{{2}}}{0}", pw, options.PaddingCharacterAlphabet, count));
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
                PaddingCharacterAlphabet = "@!#%=~|",
                SymbolsStart = 0,
                SymbolsEnd = count,
            };

            const string pw = "asdf";

            //act
            var result = ApplyPaddingSymbols.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            using (new AssertionScope())
            {
                result.Should().HaveLength(pw.Length + count);
                result.Should().MatchRegex(string.Format("{0}[{1}]{{{2}}}", pw, options.PaddingCharacterAlphabet, count));
            }
        }
    }
}