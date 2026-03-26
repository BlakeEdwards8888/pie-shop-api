using PieShop.API.Entities;

namespace PieShop.API.Services
{
    public interface IPieShopRepository
    {
        Task<IEnumerable<Pie>> GetPiesAsync();
        Task<Pie?> GetPieAsync(int pieId);
        Task AddPieAsync(Pie pie);
        Task<bool> SaveChangesAsync();
        void DeletePie(Pie pie);
    }
}
