using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumToWordsCurrency.Models;
using NumToWordsCurrency.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;

namespace NumToWordsCurrency.Services.Tests
{
    [TestClass()]
    public class NumToWordsConverterTests
    {
        [TestMethod()]
        public void PositiveNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "123.45" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", resultForm.Output);
        }
        [TestMethod()]
        public void PositiveNumZeroCentsTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "123.00" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("ONE HUNDRED AND TWENTY-THREE DOLLARS AND ZERO CENTS", resultForm.Output);
        }
        [TestMethod()]
        public void PositiveNumZeroDollarsTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "0.98" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("ZERO DOLLARS AND NINETY-EIGHT CENTS", resultForm.Output);
        }
        [TestMethod()]
        public void MaxPositiveNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "999999999.99" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS", resultForm.Output);
        }

        [TestMethod()]
        public void MinPositiveNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "0.00" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("ZERO DOLLARS AND ZERO CENTS", resultForm.Output);
        }
        [TestMethod()]
        public void SingleValuePositiveNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "100" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act
            Form resultForm = converter.ConvertNumToWords(form);

            // Assert
            Assert.AreEqual("ONE HUNDRED DOLLARS AND ZERO CENTS", resultForm.Output);
        }

        [TestMethod()]
        public void NegativeNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "-123.45" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act & Assert
            Assert.ThrowsException<System.FormatException>(() =>
            {
                Form resultForm = converter.ConvertNumToWords(form);
            });
        }
        [TestMethod()]
        public void ExceedMaxPositiveNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "1000000000" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act & Assert
            Assert.ThrowsException<System.FormatException>(() =>
            {
                // Call the method that is expected to throw the exception
                Form resultForm = converter.ConvertNumToWords(form);
            });
        }

        [TestMethod()]
        public void ExceedDecimalPlacesNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "99.999999" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act & Assert
            Assert.ThrowsException<System.FormatException>(() =>
            {
                // Call the method that is expected to throw the exception
                Form resultForm = converter.ConvertNumToWords(form);
            });
        }

        [TestMethod()]
        public void ExceedMaxCentsNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "99.100" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);
            // Act & Assert
            Assert.ThrowsException<System.FormatException>(() =>
            {
                // Call the method that is expected to throw the exception
                Form resultForm = converter.ConvertNumToWords(form);
            });
        }

        [TestMethod()]
        public void NonNumericNumTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NumToWordsConverter>>();
            Form form = new Form { Input = "abc" };
            NumToWordsConverter converter = new NumToWordsConverter(loggerMock.Object);

            // Act & Assert
            Assert.ThrowsException<System.FormatException>(() =>
            {
                // Call the method that is expected to throw the exception
                Form resultForm = converter.ConvertNumToWords(form);
            });
        }
    }
}