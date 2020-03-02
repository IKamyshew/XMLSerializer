using FluentAssertions;
using XMLSerializer.CustomTypes;
using Xunit;

namespace XMLSerializer.Test
{
    public class CustomTypesTest
    {
        #region tests
        [Fact]
        public void XBooleanUsable()
        {
            // arrange
            XBoolean custTrueBool = new XBoolean(true);
            XBoolean custFalseBool = new XBoolean(false);

            // act
            bool trueResult = custTrueBool;
            bool falseResult = custFalseBool;

            // assert
            trueResult.Should().BeTrue();
            falseResult.Should().BeFalse();

            custTrueBool.Equals(true).Should().BeTrue();
            custTrueBool.Equals(false).Should().BeFalse();
            custTrueBool.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void XNullableBooleanUsable()
        {
            // arrange
            XNullableBoolean custTrueBool = new XNullableBoolean(true);
            XNullableBoolean custFalseBool = new XNullableBoolean(false);
            XNullableBoolean custNullBool = new XNullableBoolean(null);

            // act
            bool? trueResult = custTrueBool;
            bool? falseResult = custFalseBool;
            bool? nullResult = custNullBool;

            // assert
            trueResult.Should().BeTrue();
            falseResult.Should().BeFalse();
            nullResult.Should().BeNull();

            trueResult.HasValue.Should().BeTrue();
            falseResult.HasValue.Should().BeTrue();
            nullResult.HasValue.Should().BeFalse();

            custTrueBool.Equals(true).Should().BeTrue();
            custFalseBool.Equals(true).Should().BeFalse();
            custFalseBool.Equals(null).Should().BeFalse();
            custNullBool.Equals(true).Should().BeFalse();
            custNullBool.Equals(null).Should().BeTrue();
        }
        #endregion
    }
}
