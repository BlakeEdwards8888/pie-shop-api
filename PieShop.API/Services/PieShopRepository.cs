using Microsoft.EntityFrameworkCore;
using PieShop.API.DbContexts;
using PieShop.API.Entities;

namespace PieShop.API.Services
{
    public class PieShopRepository : IPieShopRepository
    {
        private readonly PieShopContext context;

        public PieShopRepository(PieShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<Pie>, PaginationMetadata)> GetPiesAsync(string? category, string? searchQuery,
            int pageNumber, int pageSize)
        {
            var collection = context.Pies as IQueryable<Pie>;

            if (!string.IsNullOrEmpty(category))
            {
                category = category.Trim();
                collection = collection.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                collection = collection.Where(p => p.Name.ToLower().Contains(searchQuery)
                || (p.Description != null && p.Description.ToLower().Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(p => p.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Pie?> GetPieAsync(int pieId)
        {
            return await context.Pies.Where(p => p.Id == pieId).FirstOrDefaultAsync();
        }

        public async Task AddPieAsync(Pie pie)
        {
            context.Pies.Add(pie);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }

        public void DeletePie(Pie pie)
        {
            context.Pies.Remove(pie);
        }
    }
}
