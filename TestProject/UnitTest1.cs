using Moq;
using UtilsLib;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestFileReader()
        {
            //var mockFileReader = new Mock<IFileReader>();
            //mockFileReader.Setup(fr => fr.ReadFile("test.txt")).Returns("Mock file content");

            ////var myClass = new MyClass(mockFileReader.Object);

            ////var result = myClass.ReadFromFile("test.txt");

            //Assert.AreEqual("Mock file content", result);
        }
    }
}