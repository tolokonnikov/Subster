using Subtitles;

namespace UtilsLib
{
    public interface ISubItemConverter
    {
        Subs GetSubs(string? name, string[] strings);
    }
}