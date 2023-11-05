using Moq;
using NUnit.Framework;
using Subtitles;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using UtilsLib;

namespace TestProject
{
    public class Tests
    {
        ISubItemConverter _converter;
        string _subsName = "Subs Name";
        string[] _inputStrings = Array.Empty<string>();
        Subs Subs { get; set; }

        [SetUp]
        public void Setup()
        {
            //Arrange
            _converter = new SubItemConverter();
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

            Subs = new Subs()
            {
                Name = "",
                Items = new List<SubItem>
                {
                    new SubItem{Id = 291, TimeStamp = "00:15:08,033 --> 00:15:10,201", Lines = new List<string>{ "THACKER:", "Jack Killoran's" } },
                    new SubItem{Id = 292, TimeStamp = "00:15:10,243 --> 00:15:12,454", Lines = new List<string>{ "-about to jump into the race.", "-Killoran?" } },
                    new SubItem{Id = 293, TimeStamp = "00:15:12,495 --> 00:15:14,289", Lines = new List<string>{ "The car dealer?" } },
                    new SubItem{Id = 294, TimeStamp = "00:15:14,331 --> 00:15:16,374", Lines = new List<string>{ "He's white and self-funded," } },
                    new SubItem{Id = 295, TimeStamp = "00:15:16,416 --> 00:15:19,294", Lines = new List<string>{ "and he is gonna hurt you", "in the Valley." } },
                    new SubItem{Id = 296, TimeStamp = "00:15:19,336 --> 00:15:21,296", Lines = new List<string>{ "Susanna Lopez", "has East L.A. sewn up," } },
                    new SubItem{Id = 297, TimeStamp = "00:15:21,338 --> 00:15:23,381", Lines = new List<string>{ "and the two of you", "split the south side." } },
                    new SubItem{Id = 298, TimeStamp = "00:15:23,423 --> 00:15:27,844", Lines = new List<string>{ "Whoever takes the liberal", "Westside takes it all." } },
                    new SubItem{Id = 299, TimeStamp = "00:15:27,886 --> 00:15:30,263", Lines = new List<string>{ "And both have deeper pockets." } },
                    new SubItem{Id = 300, TimeStamp = "00:15:32,474 --> 00:15:34,059", Lines = new List<string>{ "And lighter complexions." } },
                    new SubItem{Id = 301, TimeStamp = "00:15:35,894 --> 00:15:37,479", Lines = new List<string>{ "That's the reality." } },
                    new SubItem{Id = 302, TimeStamp = "00:15:43,943 --> 00:15:44,944", Lines = new List<string>{ "(tires screech)" } },
                    new SubItem{Id = 303, TimeStamp = "00:16:09,302 --> 00:16:11,304", Lines = new List<string>{ "LAPD. Anyone home?" } }
                }
            };
        }

        [Test]
        public void Test_ConvertSubsToSentences()
        {
            //Act
            List<string> convertedSentences = _converter.ConvertSubsToSentences(Subs);

            //Arrange
            var goodResult = new List<string>
            {
                "THACKER: Jack Killoran's -about to jump into the race.",
                "-Killoran?",
                "The car dealer?",
                "He's white and self-funded, and he is gonna hurt you in the Valley.",
                "Susanna Lopez has East L.A. sewn up, and the two of you split the south side.",
                "Whoever takes the liberal Westside takes it all.",
                "And both have deeper pockets.",
                "And lighter complexions.",
                "That's the reality.",
                "(tires screech)",
                "LAPD. Anyone home?"
            };

            //Assert
            CollectionAssert.AreEqual(convertedSentences, goodResult);
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