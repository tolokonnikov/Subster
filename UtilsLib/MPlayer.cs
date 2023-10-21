using System.Diagnostics;

namespace Subtitles
{
    public class MPlayer
    {
        private readonly string _srtExtension = "srt";

        public List<string> GetSubtitlesListFromMKV(string mkvFileName, string mkvDir)
        {
            var res = new List<string>();

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                // p.StartInfo.CreateNoWindow = true;
                // p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/ffmpeg/bin/ffmpeg.exe");
                p.StartInfo.Arguments = $"-i \"{Path.Combine(mkvDir, mkvFileName)}\" -map 0:s:0 \"{Path.ChangeExtension(Path.Combine(mkvDir, mkvFileName), _srtExtension)}\"";
                //p.StartInfo.Arguments = "-loop 1 -i D:\\Temp\\temp.png -c:v libx264 -t 15 -pix_fmt yuv420p -vf scale=720:480 D:\\Temp\\1.avi";
                p.Start();
                p.WaitForExit();
            }

            return res;
        }

    }
}