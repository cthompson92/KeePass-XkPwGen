using XKPwGen.SharedKernel;
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

            //act
            var result = Algorithm.GeneratePassword(TestCryptoRandomStream.Instance, options);

            //assert
        }
    }
}