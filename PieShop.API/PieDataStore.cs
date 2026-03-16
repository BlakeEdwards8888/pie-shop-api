using PieShop.API.Models;

namespace PieShop.API
{
    public class PieDataStore
    {
        public List<PieDto> PieData;

        public PieDataStore()
        {
            PieData = new List<PieDto>()
            {
                new PieDto() {
                    Id = 1,
                    Name = "Apple Pie",
                    Description = "Our famous apple pie",
                    Price = 12.95
                },
                new PieDto() {
                    Id = 2,
                    Name = "Blueberry Pie",
                    Description = "A delicious pie filled with succulent blueberries",
                    Price = 12.95
                },
                new PieDto() {
                    Id = 3,
                    Name = "Cheesecake",
                    Description = "A decadent, creamy cheesecake",
                    Price = 14.95
                }
            };
        }
    }
}
