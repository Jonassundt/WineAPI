namespace WineReviewsApplication.Models
{
    public class Vineyard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Estate { get; set; }
        public Country Country { get; set; }
        public ICollection<WineVineyard> WineVineyards { get; set; }
    }
}
