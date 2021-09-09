using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class PasswordGeneratorTests
    {
        [Fact]
        public void Test1()
        {
            var inst = TestCryptoRandomStream.Instance;
        }

        [Fact]
        public void GeneratePasswordShouldNotThrow()
        {
            //arrange
            var options = new PasswordGeneratorOptions();
            options.PaddingSymbols.PaddingCharacterAlphabet = "!@#$%^&*";
            options.PaddingSymbols.PaddingType = PaddingType.Fixed;

            //act
            var result = PasswordGenerator.GeneratePassword(TestCryptoRandomStream.Instance, options);

            //assert

        }
    }
}