using WineReviewsApplication.Models;

namespace WineReviewsApplication.Interface
{
    public interface IWineTypeRepository
    {
        ICollection<WineType> GetWineTypes();
        WineType GetWineType(int id);
        ICollection<Wine> GetWineByWineType(int wineTypeId);
        bool WineTypeExists(int id);
        //signatures for the app
        bool CreateWineType(WineType wineType);
        bool UpdateWineType(WineType wineType);
        bool DeleteWineType(WineType wineType);
        bool Save();


    }
}
