using System;

namespace ReadingLog.App
{
    public class GoogleBooksResult
    {
        public int TotalItems { get; set; }
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        public VolumeInfo VolumeInfo { get; set; }
    }

    public partial class VolumeInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Authors { get; set; }
        public ImageLinks ImageLinks { get; set; }
    }

    public partial class ImageLinks
    {
        public Uri SmallThumbnail { get; set; }
        public Uri Thumbnail { get; set; }
    }
}