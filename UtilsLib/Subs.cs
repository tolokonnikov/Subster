namespace Subtitles
{
    public class Subs
    {
        public  string? Name { get; set; } = string.Empty;
        public List<SubItem> Items { get; set; } = new List<SubItem>();
    }
}