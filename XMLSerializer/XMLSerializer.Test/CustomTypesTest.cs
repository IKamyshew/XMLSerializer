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
            XBool custTrueBool = new XBool(true);
            XBool custFalseBool = new XBool(false);

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
            XNullableBool custTrueBool = new XNullableBool(true);
            XNullableBool custFalseBool = new XNullableBool(false);
            XNullableBool custNullBool = new XNullableBool(null);

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

        [Fact]
        public void XDoubleUsable()
        {
            // arrange
            double expectedDThousand = 1000.01;
            double expectedD2Dec = .05;

            // act
            XDouble dThousand = new XDouble("1,000.01");
            XDouble d2Dec = new XDouble(".05");

            // assert
            dThousand.Should().Be(expectedDThousand);
            d2Dec.Should().Be(expectedD2Dec);
        }

        [Fact]
        public void XNullableDoubleUsable()
        {
            // arrange
            double expectedDThousand = 1000.01;
            double expectedD2Dec = .05;

            // act
            XNullableDouble dThousand = new XNullableDouble("1,000.01");
            XNullableDouble d2Dec = new XNullableDouble(".05");
            XNullableDouble dNullable = new XNullableDouble(null);
            XNullableDouble dEmpty = new XNullableDouble("");

            // assert
            dThousand.Should().Be(expectedDThousand);
            d2Dec.Should().Be(expectedD2Dec);
            dNullable.HasValue.Should().BeFalse();
            dEmpty.HasValue.Should().BeFalse();
        }

        [Fact]
        public void XNullableIntUsable()
        {
            // arrange
            int validExpected = 123456;

            // act
            var emptyInt = new XNullableInt("");
            var nullInt = new XNullableInt((int?)null);
            var nullStringInt = new XNullableInt((string)null);
            var validInt = new XNullableInt("123456");

            // assert
            emptyInt.HasValue.Should().BeFalse();
            nullInt.HasValue.Should().BeFalse();
            nullStringInt.HasValue.Should().BeFalse();
            validInt.Should().Be(validExpected);
        }
        #endregion
    }
}
