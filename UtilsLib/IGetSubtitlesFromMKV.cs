namespace Subtitles
{
    public interface IGetSubtitlesFromMKV
    {
        public List<string> GetSubtitlesListFromMKV(string mkvFileName, string mkvDir);
    }
}