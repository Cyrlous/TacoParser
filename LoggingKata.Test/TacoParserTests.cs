using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("34.071477, -84.296345, Taco Bell Alpharett...", -84.296345)]
        [InlineData("34.324462,-86.503055,Taco Bell Ara...", -86.503055)]
        [InlineData("33.27757,-84.291274,Taco Bell Griffin...", -84.291274)]
        [InlineData("30.459515,-84.35516,Taco Bell Tallahassee...", -84.35516)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();
            
            //Act
            var actual = tacoParser.Parse(line).Location.Longitude;

            //Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("34.071477, -84.296345, Taco Bell Alpharett...", 34.071477)]
        [InlineData("34.324462,-86.503055,Taco Bell Ara...", 34.324462)]
        [InlineData("33.27757,-84.291274,Taco Bell Griffin...", 33.27757)]
        [InlineData("30.459515,-84.35516,Taco Bell Tallahassee...", 30.459515)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();
            
            //Act
            var actual = tacoParser.Parse(line).Location.Latitude;
            
            //Assert
            Assert.Equal(expected, actual);
        }

    }
}
