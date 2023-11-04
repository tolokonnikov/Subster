using System.Diagnostics;
using UtilsLib;

namespace Subtitles
{
    public class FFMpegUtils : IGetSubtitlesFromMKV
    {
        private readonly string _srtExtension = "srt";
        private readonly string _ffmpeg = "ffmpeg";

        public List<string> GetSubtitlesListFromMKV(string mkvFileName, string mkvDir)
        {
            var res = new List<string>();

            var ffmpegPath = Utils.FindProgramPath(_ffmpeg);

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                // p.StartInfo.CreateNoWindow = true;
                // p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ffmpegPath);
                p.StartInfo.Arguments = $"-i \"{Path.Combine(mkvDir, mkvFileName)}\" -map 0:s:0 \"{Path.ChangeExtension(Path.Combine(mkvDir, mkvFileName), _srtExtension)}\"";
                //p.StartInfo.Arguments = "-loop 1 -i D:\\Temp\\temp.png -c:v libx264 -t 15 -pix_fmt yuv420p -vf scale=720:480 D:\\Temp\\1.avi";
                p.Start();
                p.WaitForExit();
            }

            return res;
        }

    }
}