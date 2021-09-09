using FluentAssertions;
using XkPwGen;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class GetRandomWordsTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void GetDataFileNameShouldReturnCorrectEndPath(int length)
        {
            var result = GetRandomWords.GetDataFileName(WordDictionary.English, length);

            result.Should().EndWith(string.Format(@"\AppData\Roaming\XkPwGen\English\words_alpha_{0}.txt", length));
        }
    }
}