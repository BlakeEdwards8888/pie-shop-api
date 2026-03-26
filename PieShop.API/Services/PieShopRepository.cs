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

        public async Task<IEnumerable<Pie>> GetPiesAsync()
        {
            return await context.Pies.OrderBy(p => p.Id).ToListAsync();
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
