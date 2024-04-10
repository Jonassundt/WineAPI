namespace WineReviewsApplication.Models
{
    public class WineType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<WineCategory> WineCategories { get; set; }
    }
}
