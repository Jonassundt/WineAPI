namespace WineReviewsApplication.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vineyard> Vineyards { get; set; }
    }
}
