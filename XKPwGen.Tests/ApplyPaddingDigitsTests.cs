using FluentAssertions;
using XkPwGen;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class ApplyPaddingDigitsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ApplyShouldGenerateCorrectNumberOfBeforeDigits(int digitsBefore)
        {
            //arrange
            var options = new PaddingDigitOptions()
            {
                DigitsBefore = digitsBefore,
            };

            const string pw = "asdf";

            //act
            var result = ApplyPaddingDigits.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            result.Should().MatchRegex(string.Format("^\\d{{{0}}}{1}", digitsBefore, pw));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ApplyShouldGenerateCorrectNumberOfAfterDigits(int digitsAfter)
        {
            //arrange
            var options = new PaddingDigitOptions()
            {
                DigitsAfter = digitsAfter,
            };

            const string pw = "asdf";

            //act
            var result = ApplyPaddingDigits.Apply(pw, options, TestCryptoRandomStream.Instance);

            //assert
            result.Should().MatchRegex(string.Format("{0}\\d{{{1}}}$", pw, digitsAfter));
        }
    }
}