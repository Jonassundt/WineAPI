using WineReviewsApplication.Data;
using WineReviewsApplication.Interface;
using WineReviewsApplication.Models;

namespace WineReviewsApplication.Repository
{
    public class WineRepository : IWineRepository
    {
        private readonly DataContext _context;
        public WineRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateWine(int vineyardId, int wineTypeId, Wine wine)
        {
            var wineVineyardEntity = _context.Vineyards.Where(a => a.Id == vineyardId).FirstOrDefault();
            //fetch many-to-many relationships
            var wineType = _context.WineTypes.Where(a => a.Id == wineTypeId).FirstOrDefault();

            var wineVineyard = new WineVineyard()
            {
                Vineyard = wineVineyardEntity,
                Wine = wine,
            };

            _context.Add(wineVineyard);

            var wineCategory = new WineCategory()
            {
                WineType = wineType,
                Wine = wine,
            };

            _context.Add(wineCategory);

            _context.Add(wine);

            return Save();

        }

        public bool DeleteWine(Wine wine)
        {
            _context.Remove(wine);
            return Save();
        }

        public Wine GetWine(int id)
        {
            return _context.Wines.Where(p => p.ID == id).FirstOrDefault();
        }

        public Wine GetWine(string name)
        {
            return _context.Wines.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetWineRating(int wineId)
        {
            var review = _context.Reviews.Where(p => p.Wine.ID == wineId);

            if (review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Wine> GetWines()
        {
            return _context.Wines.OrderBy(p => p.ID).ToList();
        }

        public bool WineExists(int pokeId)
        {
            return _context.Wines.Any(p => p.ID == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateWine(int vineyardId, int wineTypeId, Wine wine)
        {
            _context.Update(wine);
            return Save();
        }
    }
}
