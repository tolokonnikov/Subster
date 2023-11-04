using Subtitles;
using System.Text.RegularExpressions;

namespace UtilsLib
{
    public class SubItemConverter : ISubItemConverter
    {
        string _timeStampPattern = @"^\d{2}:\d{2}:\d{2},\d{3}\s-->\s\d{2}:\d{2}:\d{2},\d{3}$";

        public Subs ConvertLinesToSub(string? name, string[] lines)
        {
            if (lines == null)
                throw new ArgumentNullException("Argument \"strings\" is null!");

            if (lines.Length == 0)
                throw new ArgumentException("Argument \"strings\" length is 0!");

            if (!int.TryParse(lines[0], out var index))
                throw new FormatException("Argument \"First ID\" is incorrect format");

            var res = new Subs() { Name = name };

            string delimeter = string.Empty;

            List<List<string>> subArrays = new List<List<string>>();
            List<string> currentSubArray = new List<string>();

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] == delimeter)
                {
                    if (currentSubArray.Count > 0)
                    {
                        subArrays.Add(currentSubArray);
                        currentSubArray = new List<string>();
                    }
                }
                else
                {
                    currentSubArray.Add(lines[i]);
                }
            }

            if (currentSubArray.Count > 0)
            {
                subArrays.Add(currentSubArray);
            }

            int id;

            foreach (var subArray in subArrays)
            {
                var subItem = new SubItem();

                if (!int.TryParse(subArray[0], out id))
                {
                    throw new FormatException($"Id {subArray[0]} format is corrupt!");
                }
                else
                {
                    subItem.Id = id;
                }

                if (!Regex.IsMatch(subArray[1], _timeStampPattern))
                {
                    throw new FormatException($"TimeStamp {subArray[1]} format is corrupt!");
                }
                else
                {
                    subItem.TimeStamp = subArray[1];
                }

                subItem.Lines = subArray.GetRange(2, subArray.Count - 2);

                res.Items.Add(subItem);
            }

            return res;
        }
    }
}
