using WineReviewsApplication.Models;

namespace WineReviewsApplication.Interface
{
    public interface IVineyardRepository
    {
        ICollection<Vineyard> GetVineyards();
        Vineyard GetVineyard(int vineyardId);
        ICollection<Vineyard> GetVineyardOfAWine(int wineId);
        ICollection<Wine> GetWineByVineyard(int vineyardId);
        bool VineyardExists(int vineyardId);
        bool CreateVineyard(Vineyard vineyard);
        bool UpdateVineyard(Vineyard vineyard);
        bool DeleteVineyard(Vineyard vineyard);
        bool Save();
    }
}
