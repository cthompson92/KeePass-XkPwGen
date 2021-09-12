using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using XkPwGen;
using XKPwGen.SharedKernel;
using Xunit;

namespace XKPwGen.Tests
{
    [Trait("Category", "Unit")]
    public class ApplyWordTransformationTests
    {
        [Fact]
        public void TransformShouldNotChangeWordsWhenTypeIsNone()
        {
            //arrange
            var words = new[] { "battery", "horse", "staple" };

            var options = new TransformationOptions()
            {
                CaseTransformation = CaseTransformationType.None,
            };

            //act
            var result = ApplyWordTransformation.Transform(words, options);

            //assert
            using (new AssertionScope())
            {
                result.Should().BeSameAs(words);
                result.Should().SatisfyRespectively(
                    first => first.Should().Be("battery"),
                    second => second.Should().Be("horse"),
                    third => third.Should().Be("staple"));
            }
        }

        [Fact]
        public void TransformShouldReturnAllLowercaseWhenTypeIsLowerCase()
        {
            //arrange
            var words = new[] { "BAttERY", "HOrSE", "STapLE" };

            var options = new TransformationOptions()
            {
                CaseTransformation = CaseTransformationType.LowerCase,
            };

            //act
            var result = ApplyWordTransformation.Transform(words, options);

            //assert
            result.Should().SatisfyRespectively(
                first => first.Should().Be("battery"),
                second => second.Should().Be("horse"),
                third => third.Should().Be("staple"));
        }

        [Fact]
        public void TransformShouldReturnAllUppercaseWhenTypeIsUpperCase()
        {
            //arrange
            var words = new[] { "battery", "horse", "staple" };

            var options = new TransformationOptions()
            {
                CaseTransformation = CaseTransformationType.UpperCase,
            };

            //act
            var result = ApplyWordTransformation.Transform(words, options);

            //assert
            result.Should().SatisfyRespectively(
                first => first.Should().Be("BATTERY"),
                second => second.Should().Be("HORSE"),
                third => third.Should().Be("STAPLE"));
        }

        [Fact]
        public void TransformShouldReturnOddIndicesLowercaseWhenTypeIsAlternatingWordCase()
        {
            //arrange
            var words = new[] { "battery", "horse", "staple" };

            var options = new TransformationOptions()
            {
                CaseTransformation = CaseTransformationType.AlternatingWordCase,
            };

            //act
            var result = ApplyWordTransformation.Transform(words, options);

            //assert
            result.Should().SatisfyRespectively(
                first => first.Should().Be("battery"),
                second => { },
                third => third.Should().Be("staple"));
        }

        [Fact]
        public void TransformShouldReturnEvenIndicesUppercaseWhenTypeIsAlternatingWordCase()
        {
            //arrange
            var words = new[] { "battery", "horse", "staple", "pineapple" };

            var options = new TransformationOptions()
            {
                CaseTransformation = CaseTransformationType.AlternatingWordCase,
            };

            //act
            var result = ApplyWordTransformation.Transform(words, options);

            //assert
            result.Should().SatisfyRespectively(
                first => { },
                second => { second.Should().Be("HORSE"); },
                third => { },
                fourth => { fourth.Should().Be("PINEAPPLE"); });
        }
    }
}