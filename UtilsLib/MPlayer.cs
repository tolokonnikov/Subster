using System.Diagnostics;

namespace Subtitles
{

    public class MPlayer
    {
        private readonly string _mkvFilePath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\Bosch (2014) - S03E03 - God Sees (1080p AMZN WEB-DL x265 Ghost).mkv";
        private readonly string _mkvOutSubPath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\subs.srt";

        public List<string> GetSubtitlesListFromMKV()
        {
            var res = new List<string>();

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                // p.StartInfo.CreateNoWindow = true;
                // p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/ffmpeg/bin/ffmpeg.exe");
                p.StartInfo.Arguments = $"-i \"{_mkvFilePath}\" -map 0:s:0 \"{_mkvOutSubPath}\"";
                //p.StartInfo.Arguments = "-loop 1 -i D:\\Temp\\temp.png -c:v libx264 -t 15 -pix_fmt yuv420p -vf scale=720:480 D:\\Temp\\1.avi";
                p.Start();
                p.WaitForExit();
            }

            return res;
        }

    }
}