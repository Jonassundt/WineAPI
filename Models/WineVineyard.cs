namespace WineReviewsApplication.Models
{
    public class WineVineyard
    {
        public int WineId { get; set; }
        public int VineyardId { get; set; }
        public Wine Wine { get; set; }
        public Vineyard Vineyard { get; set; }
    }
}
