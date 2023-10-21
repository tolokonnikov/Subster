using System.Diagnostics;

namespace Subtitles
{

    public class MPlayer
    {
        private readonly string _mkvFilePath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\Bosch (2014) - S03E03 - God Sees (1080p AMZN WEB-DL x265 Ghost).mkv";
        private readonly string _mkvOutSubPath = @"D:\movies\series\Bosch (2014) Season 1-7 S01-S07 (1080p AMZN WEB-DL x265 HEVC 10bit EAC3 5.1 Ghost)\Season 3\subs.srt";
        // private readonly LibVLC _libVLC = new LibVLC();
        // private static MediaPlayer _mediaPlayer = new MediaPlayer(_libVLC);

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

        // private static void PlayMkvFile(string filePath)
        // {
        //     // Перевірка, чи файл існує
        //     if (!System.IO.File.Exists(filePath))
        //     {
        //         Console.WriteLine("Файл не існує.");
        //         return;
        //     }

        //     // Ініціалізація LibVLC
        //     LibVLCSharp.Shared.Core.Initialize();

        //     // Створення медіа
        //     var media = new Media(_libVLC, filePath, FromType.FromPath);
        //     // media.AddOption(string.Format(@":sub-track-id={0}", int.MaxValue));
        //     _mediaPlayer.SetSpu(0);


        //     // Встановлення медіа для відтворення
        //     _mediaPlayer.Play(media);
        //     _mediaPlayer.SetSpu(0);
        // }
    }
}