namespace Subtitles
{
    public class SubItem
    {
        public int Id { get; set; }
        public required string TimePrint { get; set; }
        public required List<string> Lines { get; set; }
    }
}