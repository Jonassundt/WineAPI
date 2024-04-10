using WineReviewsApplication.Models;

namespace WineReviewsApplication.Interface
{
    public interface IWineRepository
    {
        ICollection<Wine> GetWines();
        Wine GetWine(int id);
        Wine GetWine(string name);
        decimal GetWineRating(int wineId);
        bool WineExists(int wineId);

        //the more relationships, obviously more advanced creation
        bool CreateWine(int vineyardId, int wineTypeId, Wine wine);
        bool UpdateWine(int vineyardId, int wineTypeId, Wine wine);
        bool DeleteWine(Wine wine); //...
        bool Save();

    }
}
