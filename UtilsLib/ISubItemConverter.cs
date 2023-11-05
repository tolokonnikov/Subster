using Subtitles;

namespace UtilsLib
{
    public interface ISubItemConverter
    {
        Subs ConvertLinesToSub(string? name, string[] strings);
        List<string> ConvertSubsToSentences(Subs subs);
    }
}