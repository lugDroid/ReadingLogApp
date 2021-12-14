using System;

namespace ReadingLog.App
{
    public class ApiResult
    {
        public int TotalItems { get; set; }
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        public VolumeInfo VolumeInfo { get; set; }
        public SaleInfo SaleInfo { get; set; }
    }

    public partial class VolumeInfo : IEquatable<VolumeInfo>
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string[] Authors { get; set; }
        public ImageLinks ImageLinks { get; set; }
        public string InfoLink { get; set; }
        public string Language { get; set; }

        public bool Equals(VolumeInfo other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return Title.Equals(other.Title);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = Title == null ? 0 : Title.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName;
        }
    }

    public partial class ImageLinks
    {
        public Uri SmallThumbnail { get; set; }
        public Uri Thumbnail { get; set; }
    }

    public partial class SaleInfo{
        public string Country { get; set; }
        public bool isEbook { get; set; }
    }
}