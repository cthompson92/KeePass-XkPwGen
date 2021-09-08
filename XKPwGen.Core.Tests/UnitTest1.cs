using System;
using System.Linq;
using FluentAssertions;
using KeePassLib;
using Xunit;

namespace XKPwGen.Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var uuid = new PwUuid(true);

            var code = string.Join(", ", BitConverter.ToString(uuid.UuidBytes).Split('-').Select(x => "0x" + x));

            code.Should().StartWith("0x");
        }
    }
}
