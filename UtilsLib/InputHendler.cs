using System.Text.RegularExpressions;

namespace Subtitles
{
    public class InputHendler
    {
        private static readonly string _inputPath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\subs01.srt";
        private static readonly string _outputPath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\subs01_Google.srt";
        private static readonly string _timeLinePattern = @"(\d{2}:\d{2}:\d{2},\d{3})\s*-->\s*(\d{2}:\d{2}:\d{2},\d{3})";
        private static readonly string _numberPattern = @"\b([1-9]|[1-9]\d{1,3}|9999)\b";
        private Subs _inputSubs = new Subs();

        public InputHendler()
        {
            _inputSubs = GetInputSubs();
        }

        private Subs GetInputSubs()
        {
            var lines = File.ReadAllLines(_inputPath);
            
            PrintLines(lines);

            return new Subs();
        }

        private static void PrintLines(string[] lines)
        {
            List<string> res = new List<string>();
            List<string> preRes = new List<string>();

            var pointer = 0;

            try
            {
                Regex timeLineRegex = new Regex(_timeLinePattern);
                Regex numberRegex = new Regex(_numberPattern);

                for (var i = 0; i < lines.Length; i++)
                    lines[i] = lines[i].Trim();

                for (var i = 0; i < lines.Length; i++)
                    if (!timeLineRegex.Match(lines[i]).Success)
                        if (!numberRegex.Match(lines[i]).Success)
                            // if (!string.IsNullOrEmpty(lines[i]))
                            preRes.Add(lines[i]);



                while (pointer < preRes.Count)
                {
                    for (var i = 0; i < preRes.Count; i++)
                    {
                        pointer++;
                        if (preRes[i].Length > 0 &&
                            preRes[i][preRes[i].Length - 1] != '.'
                            && preRes[i][preRes[i].Length - 1] != '!'
                            && preRes[i][preRes[i].Length - 1] != '?'
                            && i < (preRes.Count - 1))
                        {
                            var ttt = preRes[i][preRes[i].Length - 1];
                            preRes[i] = preRes[i] + ' ' + preRes[i + 1];
                            pointer = 0;
                            preRes.RemoveAt(i + 1);
                            i = 0;
                        }
                    }
                }

                //Console.WriteLine(lines[i]);
                //sw.WriteLine(lines[i]);

                //Pass the filepath and filename to the StreamWriter Constructor
                using StreamWriter sw = new StreamWriter(_outputPath);

                foreach (var ln in preRes)
                    sw.WriteLine(ln);


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }
    }
}