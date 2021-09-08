using FluentAssertions;
using Xunit;

namespace XKPwGen.Core.Tests
{
    [Trait("Category", "Unit")]
    public class OptionsSaverTests
    {
        [Fact]
        public void GetOptionsFileNameTest1()
        {
            var result = OptionsManager.GetOptionsFileName("profile1");

            result.Should().EndWith(@"\AppData\Roaming\XkPwGen\profile1.json");
        }
    }
}
