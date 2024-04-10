namespace WineReviewsApplication.Models
{
    public class WineCategory
    {
        public int WineId { get; set; }
        public int CategoryId { get; set; }
        public Wine Wine { get; set; }
        public WineType WineType { get; set; }
    }
}
