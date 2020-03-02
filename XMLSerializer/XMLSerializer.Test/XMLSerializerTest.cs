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
        private readonly XmlDocument BadFormedBooleanNodesXML;
        private readonly XmlDocument BadFormedBooleanAttributesXML;
        private readonly XmlDocument BadFormedArrayNodesXML;

        public XMLSerializerTest()
        {
            BadFormedNumberNodesXML = new XmlDocument();
            BadFormedNumberNodesXML.Load("BadFormedNumberNodes.xml");

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
                Decimal = 1123.23M,
                Int = 1123
            };

            // act
            var result = BadFormedNumberNodesXML.Deserialize<BadFormedNumberNodes>();

            // assert
            result.Should().NotBeNull();
            var numbers = result.TestNumberParsing;
            numbers.Decimal.Should().Be(expectedNumbers.Decimal);
            numbers.Int.Should().Be(expectedNumbers.Int);
            numbers.Empty.Should().BeNull();
            numbers.EmptyClosed.Should().BeNull();
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
            ((bool)result.One).Should().BeTrue();
            ((bool)result.Zero).Should().BeFalse();
            ((bool)result.True).Should().BeTrue();
            ((bool)result.False).Should().BeFalse();
            ((bool?)result.Empty).Should().BeNull();
            ((bool?)result.EmptyClosed).Should().BeNull();
            ((bool?)result.Null).Should().BeNull();
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
            ((bool?)result.Null).Should().BeNull();
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
            result.Null.Should().BeNull();
            result.NullableTrue.Should().BeTrue();
        }
        #endregion
    }
}