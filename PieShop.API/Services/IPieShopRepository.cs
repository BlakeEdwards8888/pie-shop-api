using PieShop.API.Entities;

namespace PieShop.API.Services
{
    public interface IPieShopRepository
    {
        Task<(IEnumerable<Pie>, PaginationMetadata)> GetPiesAsync(string? category, string? searchQuery, int pageNumber, int pageSize);
        Task<Pie?> GetPieAsync(int pieId);
        Task AddPieAsync(Pie pie);
        Task<bool> SaveChangesAsync();
        void DeletePie(Pie pie);
    }
}
