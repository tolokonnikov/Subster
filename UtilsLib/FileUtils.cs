namespace UtilsLib
{
    public class FileUtils : IFileReader
    {
        public string[] ReadFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            return lines;
        }
    }
}
