using WineReviewsApplication.Data;
using WineReviewsApplication.Interface;
using WineReviewsApplication.Models;

//Repository - where you can put your database calls
namespace WineReviewsApplication.Repository
{
    public class WineTypeRepository : IWineTypeRepository
    {
        private DataContext _context;
        public WineTypeRepository(DataContext context)
        {
            _context = context;
        }
        public bool WineTypeExists(int id)
        {
            return _context.WineTypes.Any(c => c.Id == id);
        }

        public bool CreateWineType(WineType wineType)
        {
            //Change tracker
            //connected vs disconnected state
            _context.Add(wineType); //entity framework puts it in the database
            return Save();
        }

        public bool DeleteWineType(WineType wineType)
        {
            _context.Remove(wineType);
            return Save();
        }

        public ICollection<WineType> GetWineTypes()
        {
            return _context.WineTypes.ToList();
        }

        public WineType GetWineType(int id)
        {
            return _context.WineTypes.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Wine> GetWineByWineType(int wineTypeId)
        {
            return _context.WineCategories.Where(e => e.CategoryId == wineTypeId).Select(c => c.Wine).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            //savechanges -> sql will be generated and sent to database
            //turning it into sql.
            return saved > 0;
        }

        public bool UpdateWineType(WineType wineType)
        {
            _context.Update(wineType);
            return Save();
        }
    }
}
