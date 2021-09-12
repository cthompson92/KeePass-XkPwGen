using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class GetRandomWordsTests
    {
        [Fact]
        public void GetDataFileNameShouldReturnCorrectBasePath()
        {
            //arrange

            //act
            var result = GetRandomWords.GetDataFileName(WordDictionary.English, 1);

            //assert
            result.Should().Contain(@"\AppData\Roaming\XkPwGen\English\");
        }

        private static IEnumerable<string> GetDictionaryFileBaseNames()
        {
            yield return "words_alpha";
            yield return "google-10000-english-usa-no-swears";
        }

        private static IEnumerable<int> GetLengths()
        {
            return Enumerable.Range(3, 8);
        }

        public static IEnumerable<object[]> GetDataFileNameTestCases()
        {
            return GetDictionaryFileBaseNames()
               .SelectMany(baseName => GetLengths(),
                    (baseName, length) => new object[] { baseName, length });
        }

        [Theory]
        [MemberData("GetDataFileNameTestCases")]
        public void GetDataFileNameShouldReturnCorrectEndPath(string baseFileName, int length)
        {
            //arrange
            GetRandomWords.UseDictionaryDataFile(baseFileName);

            //act
            var result = GetRandomWords.GetDataFileName(WordDictionary.English, length);

            //assert
            result.Should().EndWith(string.Format(@"\{0}_{1}.dictdata", baseFileName, length));
        }
    }
}