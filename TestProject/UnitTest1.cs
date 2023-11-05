using Subtitles;
using UtilsLib;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace TestProject
{
    public class Tests
    {
        ISubItemConverter _converter;
        string _subsName = "Subs Name";
        string[] _inputStrings = Array.Empty<string>();

        public Tests(ISubItemConverter converter)
        {
            _converter = converter;
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            //_converter = new SubItemConverter();
            _inputStrings = new string[]
            {
                  "889",
                  "00:45:54,752 --> 00:45:56,337",
                  "Bosch.",
                  "Hi.",
                  "",
                  "890",
                  "00:45:56,379 --> 00:45:59,340",
                  "Let me hear from you.",
                  "",
                  "891",
                  "00:45:59,382 --> 00:46:00,800",
                  "Will do.",
                  "",
                  "892",
                  "00:46:04,888 --> 00:46:06,931",
                  "(car door opens, closes)",
                  "",
                  "893",
                  "00:46:06,973 --> 00:46:08,725",
                  "(engine starts)",
                  "",
                  "894",
                  "00:46:09,767 --> 00:46:11,769",
                  "♪ ♪",
                  "",
                  "",
            };
        }

        [Test]
        public void Test_ConvertStringsToSubs_ValidInput()
        {
            //Act
            Subs subs = _converter.ConvertLinesToSub(_subsName, _inputStrings);

            //Assert
            Assert.IsNotNull(subs);
            Assert.That(subs.Name, Is.EqualTo(_subsName));
            Assert.That(subs.Items.Count, Is.EqualTo(6));
            Assert.That(subs.Items[4].Id, Is.EqualTo(893));
            Assert.That(subs.Items[2].TimeStamp, Is.EqualTo("00:45:59,382 --> 00:46:00,800"));
            CollectionAssert.AreEqual(subs.Items[0].Lines, new[] { "Bosch.", "Hi." });

        }

        [Test]
        public void Test_ConvertStringsToSubs_NullInput()
        {
            string[] inputStrings = null;
            var subsName = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _converter.ConvertLinesToSub(subsName, inputStrings));
        }

        [Test]
        public void Test_ConvertStringsToSubs_EmptyInput()
        {
            string[] inputStrings = new string[0];

            // Assert
             Assert.Throws<ArgumentException>(() => _converter.ConvertLinesToSub(string.Empty, inputStrings));
        }

        [Test]
        public void Test_ConvertStringsToSubs_Timestamp_InvalidInput()
        {
            string[] inputStrings = new string[] { "1", "Invalid Timestamp", "Line 1", "Line 2" };

            // Act & Assert
            Assert.Throws<FormatException>(() => _converter.ConvertLinesToSub(string.Empty, inputStrings));
        }

        [Test]
        public void Test_ConvertStringsToSubs_FirstId_InvalidInput()
        {
            string[] inputStrings = new string[] { "0A", "00:45:59,382 --> 00:46:00,800", "Line 1", "Line 2" };

            // Act & Assert
            Assert.Throws<FormatException>(() => _converter.ConvertLinesToSub(string.Empty, inputStrings));
        }

        //var mockFileReader = new Mock<IFileReader>();
        //mockFileReader.Setup(fr => fr.ReadFile("test.txt")).Returns("Mock file content");

        ////var myClass = new MyClass(mockFileReader.Object);

        ////var result = myClass.ReadFromFile("test.txt");

        //Assert.AreEqual("Mock file content", result);
    }
}