using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PieShop.API.Entities;
using PieShop.API.Models;
using PieShop.API.Services;
using System.Threading.Tasks;

namespace PieShop.API.Controllers
{
    [Route("api/pies")]
    [ApiController]
    public class PieController : ControllerBase
    {
        private readonly IPieShopRepository pieShopRepository;
        private readonly IMapper mapper;

        public PieController(IPieShopRepository pieShopRepository, IMapper mapper)
        {
            this.pieShopRepository = pieShopRepository ?? throw new ArgumentNullException(nameof(pieShopRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PieDto>>> GetPies()
        {
            var pieEntities = await pieShopRepository.GetPiesAsync();

            return Ok(mapper.Map<IEnumerable<PieDto>>(pieEntities));
        }

        [HttpGet("{pieId}", Name = "GetPie")]
        public async Task<ActionResult<PieDto>> GetPie(int pieId)
        {
            var pieEntity = await pieShopRepository.GetPieAsync(pieId);

            if (pieEntity == null) return NotFound();

            return Ok(mapper.Map<PieDto>(pieEntity));
        }

        [HttpPost]
        public async Task<ActionResult<PieDto>> CreatePie([FromBody] PieCreationDto pieToCreate)
        {
            var pieEntity = mapper.Map<Pie>(pieToCreate);

            await pieShopRepository.AddPieAsync(pieEntity);
            await pieShopRepository.SaveChangesAsync();

            var pieDto = mapper.Map<PieDto>(pieEntity);

            return CreatedAtRoute("GetPie",
                new
                {
                    pieId = pieDto.Id,
                }, pieDto);
        }

    }
}
