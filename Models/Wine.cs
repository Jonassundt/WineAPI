namespace WineReviewsApplication.Models
{
    public class Wine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Vintage { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WineVineyard> WineVineyards {  get; set; }
        public ICollection<WineCategory> WineCategories { get; set; }
    }
}
