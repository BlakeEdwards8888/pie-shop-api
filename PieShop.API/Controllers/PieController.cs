using AutoMapper;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using PieShop.API.Entities;
using PieShop.API.Models;
using PieShop.API.Services;
using System.Text.Json;

namespace PieShop.API.Controllers
{
    [Route("api/pies")]
    [ApiController]
    public class PieController : ControllerBase
    {
        const int MAX_PAGE_SIZE = 20;

        private readonly IPieShopRepository pieShopRepository;
        private readonly IMapper mapper;

        public PieController(IPieShopRepository pieShopRepository, IMapper mapper)
        {
            this.pieShopRepository = pieShopRepository ?? throw new ArgumentNullException(nameof(pieShopRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PieDto>>> GetPies(string? category,
            string? searchQuery, int pageNumber = 1, int pageSize = 3)
        {
            pageSize = Math.Min(pageSize, MAX_PAGE_SIZE);

            var (pieEntities, paginationMetadata) = await pieShopRepository.GetPiesAsync(category, searchQuery,
                pageNumber, pageSize);

            Response.Headers.Append("X_Pagination", JsonSerializer.Serialize(paginationMetadata));

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
        public async Task<ActionResult<PieDto>> CreatePie(PieCreationDto pieToCreate)
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

        [HttpDelete("{pieId}")]
        public async Task<ActionResult> DeletePie(int pieId)
        {
            var pieToDelete = await pieShopRepository.GetPieAsync(pieId);

            if(pieToDelete == null) return NotFound();

            pieShopRepository.DeletePie(pieToDelete);
            await pieShopRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{pieId}")]
        public async Task<ActionResult> UpdatePie(int pieId, PieUpdateDto updatedPie)
        {
            var pieEntity = await pieShopRepository.GetPieAsync(pieId);

            if(pieEntity == null) return NotFound();

            mapper.Map(updatedPie, pieEntity);

            await pieShopRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pieId}")]
        public async Task<ActionResult> PatchPie(int pieId, [FromBody] JsonPatchDocument<PieUpdateDto> patchDocument)
        {
            var pieEntity = await pieShopRepository.GetPieAsync(pieId);

            if(pieEntity == null) return NotFound();

            var pieToPatch = mapper.Map<PieUpdateDto>(pieEntity);

            patchDocument.ApplyTo(pieToPatch, jsonPatchError =>
            {
                var key = jsonPatchError.AffectedObject.GetType().Name;
                ModelState.AddModelError(key, jsonPatchError.ErrorMessage);
            });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!TryValidateModel(pieToPatch)) return BadRequest(ModelState);

            mapper.Map(pieToPatch, pieEntity);

            await pieShopRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
