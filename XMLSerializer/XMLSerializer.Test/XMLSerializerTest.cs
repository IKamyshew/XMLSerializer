using System.Xml;
using FluentAssertions;
using XMLSerializer.Responses;
using Xunit;

namespace XMLSerializer.Test
{
    public class XMLSerializerTest
    {
        #region init
        private readonly XmlDocument BadFormedNumberNodesXML;
        private readonly XmlDocument BadFormedDoubleNodesXML;
        private readonly XmlDocument BadFormedDecimalNodesXML;
        private readonly XmlDocument BadFormedBooleanNodesXML;
        private readonly XmlDocument BadFormedBooleanAttributesXML;
        private readonly XmlDocument BadFormedArrayNodesXML;

        public XMLSerializerTest()
        {
            BadFormedNumberNodesXML = new XmlDocument();
            BadFormedNumberNodesXML.Load("BadFormedNumberNodes.xml");

            BadFormedDoubleNodesXML = new XmlDocument();
            BadFormedDoubleNodesXML.Load("BadFormedDoubleNodes.xml");

            BadFormedDecimalNodesXML = new XmlDocument();
            BadFormedDecimalNodesXML.Load("BadFormedDecimalNodes.xml");

            BadFormedBooleanNodesXML = new XmlDocument();
            BadFormedBooleanNodesXML.Load("BadFormedBooleanNodes.xml");

            BadFormedBooleanAttributesXML = new XmlDocument();
            BadFormedBooleanAttributesXML.Load("BadFormedBooleanAttributes.xml");

            BadFormedArrayNodesXML = new XmlDocument();
            BadFormedArrayNodesXML.Load("BadFormedArrayNodes.xml");
        }
        #endregion

        #region tests
        [Fact]
        public void NumberNodes_Deserialized()
        {
            // arrange
            var expectedNumbers = new TestNumberParsing
            {
                Valid = 123456
            };

            // act
            var result = BadFormedNumberNodesXML.Deserialize<BadFormedNumberNodes>();

            // assert
            result.Should().NotBeNull();
            var numbers = result.TestNumberParsing;
            numbers.Valid.Should().Be(expectedNumbers.Valid);
            numbers.Empty.HasValue.Should().BeFalse();
            numbers.EmptyClosed.HasValue.Should().BeFalse();
        }

        [Fact]
        public void DoubleNodes_Deserialized()
        {
            // arrange
            var expectedNumbers = new TestDoubleParsing
            {
                Decimal = "1,123.01",
                RealDecimal = 24.95,
                Empty = (double?)null
            };

            // act
            var result = BadFormedDoubleNodesXML.Deserialize<BadFormedDoubleNodes>();

            // assert
            result.Should().NotBeNull();
            var numbers = result.TestDoubleParsing;
            numbers.Decimal.Should().Be(expectedNumbers.Decimal);
            numbers.RealDecimal.Should().Be(expectedNumbers.RealDecimal);
            numbers.Empty.HasValue.Should().BeFalse();
        }

        [Fact]
        public void DecimalNodes_Deserialized()
        {
            // arrange
            var expectedNumbers = new TestDecimalParsing
            {
                Decimal = "0.9999999999999999999999999999",
                DecimalThousand = "1,123.01",
                RealDecimal = (decimal?)null,
                Empty = (decimal?)null
            };

            // act
            var result = BadFormedDecimalNodesXML.Deserialize<BadFormedDecimalNodes>();

            // assert
            result.Should().NotBeNull();
            var numbers = result.TestDecimalParsing;
            numbers.Decimal.Should().Be(expectedNumbers.Decimal);
            numbers.DecimalThousand.Should().Be(expectedNumbers.DecimalThousand);
            numbers.RealDecimal.Should().Be(expectedNumbers.RealDecimal);
            numbers.Empty.HasValue.Should().BeFalse();
        }

        [Fact]
        public void BooleanNodes_Deserialized()
        {
            // arrange

            // act
            var result = BadFormedBooleanNodesXML.Deserialize<BadFormedBooleanNodes>();

            // assert
            result.Should().NotBeNull();
            this.IsBooleanValid(result.TestBooleanParsing);
        }

        [Fact]
        public void ArrayNodes_Deserialized()
        {
            // arrange

            // act
            var result = BadFormedArrayNodesXML.Deserialize<BadFormedArrayNodes>();

            // assert
            result.Should().NotBeNull();
            result.Errors.Should().NotBeNull();
            result.Errors.Error.Should().NotBeNull();
            result.Errors.Error.Count.Should().Be(10);
        }

        [Fact]
        public void BooleanAttributes_Deserialized()
        {
            // arrange

            // act
            var result = BadFormedBooleanAttributesXML.Deserialize<BadFormedBooleanAttributes>();

            // assert
            result.Should().NotBeNull();
            this.IsBooleanValid(result.TestBooleanParsing);
        }

        [Fact]
        public void BooleanAttributesIgnoreProperties_Deserialized()
        {
            // arrange

            // act
            var result = BadFormedBooleanAttributesXML.Deserialize<BadFormedBooleanAttributesIgnoreProperties>();

            // assert
            result.Should().NotBeNull();
            this.IsBooleanValid(result.TestBooleanParsing);
        }
        #endregion

        #region private
        private void IsBooleanValid(TestBooleanParsing result)
        {
            result.Should().NotBeNull();

            ((bool)result.Yes).Should().BeTrue();
            ((bool)result.No).Should().BeFalse();
            ((bool)result.On).Should().BeTrue();
            ((bool)result.Off).Should().BeFalse();
            ((bool)result.Enabled).Should().BeTrue();
            ((bool)result.Disabled).Should().BeFalse();
            ((bool)result.One).Should().BeTrue();
            ((bool)result.Zero).Should().BeFalse();
            ((bool)result.True).Should().BeTrue();
            ((bool)result.False).Should().BeFalse();
            ((bool?)result.Empty).Should().BeNull();
            ((bool?)result.EmptyClosed).Should().BeNull();
            ((bool?)result.NullableTrue).Should().BeTrue();
        }

        private void IsBooleanValid(TestBooleanAttributeParsing result)
        {
            result.Should().NotBeNull();

            ((bool)result.Yes).Should().BeTrue();
            ((bool)result.No).Should().BeFalse();
            ((bool)result.One).Should().BeTrue();
            ((bool)result.Zero).Should().BeFalse();
            ((bool)result.True).Should().BeTrue();
            ((bool)result.False).Should().BeFalse();
            ((bool?)result.Empty).Should().BeNull();
            ((bool?)result.NullableTrue).Should().BeTrue();
        }

        private void IsBooleanValid(TestBooleanAttributeParsingIgnoreProperties result)
        {
            result.Should().NotBeNull();

            result.Yes.Should().BeTrue();
            result.No.Should().BeFalse();
            result.One.Should().BeTrue();
            result.Zero.Should().BeFalse();
            result.True.Should().BeTrue();
            result.False.Should().BeFalse();
            result.Empty.Should().BeNull();
            result.NullableTrue.Should().BeTrue();
        }
        #endregion
    }
}
