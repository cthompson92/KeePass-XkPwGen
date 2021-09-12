using System;
using FluentAssertions;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class OptionsManagerTests
    {
        [Theory]
        [InlineData("profile1")]
        [InlineData("profile2")]
        [InlineData("something-custom")]
        [InlineData("wasd.json")]
        [InlineData("This Is My Super Awesome Profile Name")]
        public void GetOptionsFileNameTest1(string profileName)
        {
            var result = OptionsManager.GetOptionsFileName(profileName);

            result.Should().EndWith(string.Format(@"\AppData\Roaming\XkPwGen\{0}.json", profileName));
        }
    }
}
