using WineReviewsApplication.Data;
using WineReviewsApplication.Interface;
using WineReviewsApplication.Models;

namespace WineReviewsApplication.Repository
{
    public class VineyardRepository : IVineyardRepository
    {
        private readonly DataContext _context;

        public VineyardRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateVineyard(Vineyard vineyard)
        {
            _context.Add(vineyard);
            return Save();
        }

        public bool DeleteVineyard(Vineyard vineyard)
        {
            _context.Remove(vineyard);
            return Save();
        }

        public Vineyard GetVineyard(int vineyardId)
        {
            return _context.Vineyards.Where(o => o.Id == vineyardId).FirstOrDefault();
        }

        public ICollection<Vineyard> GetVineyardOfAWine(int wineId)
        {
            return _context.WineVineyards.Where(p => p.Wine.ID == wineId).Select(o => o.Vineyard).ToList();
        }

        public ICollection<Vineyard> GetVineyards()
        {
            return _context.Vineyards.ToList();
        }

        public ICollection<Wine> GetWineByVineyard(int vineyardId)
        {
            return _context.WineVineyards.Where(p => p.Vineyard.Id == vineyardId).Select(p => p.Wine).ToList();
        }

        public bool VineyardExists(int vineyardId)
        {
            return _context.Vineyards.Any(o => o.Id == vineyardId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateVineyard(Vineyard vineyard)
        {
            _context.Update(vineyard);
            return Save();
        }
    }
}
