using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Produces("application/json")]
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

        /// <summary>
        /// Gets all of the pies stored in the Pie Shop database
        /// </summary>
        /// <param name="category">Filter by category ex: 'fruit pie', 'cheesecake'</param>
        /// <param name="searchQuery">Searches for specified query in the name or description of each pie</param>
        /// <returns>A list of pies with the specified category filter and search query. Supports pagination</returns>
        /// <response code ="200">Returns the list of pies</response>
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

        /// <summary>
        /// Returns a single pie from the database
        /// </summary>
        /// <param name="pieId">The ID number associated with the pie</param>
        /// <returns>A single pie with the specified ID number</returns>
        /// /// <response code ="200">Returns the specified pie</response>
        [HttpGet("{pieId}", Name = "GetPie")]
        public async Task<ActionResult<PieDto>> GetPie(int pieId)
        {
            var pieEntity = await pieShopRepository.GetPieAsync(pieId);

            if (pieEntity == null) return NotFound();

            return Ok(mapper.Map<PieDto>(pieEntity));
        }

        /// <summary>
        /// POST method for adding new pies to the database
        /// </summary>
        /// <param name="pieToCreate">Json format pie data to add to the database</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
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

        /// <summary>
        /// Removes a pie from the database
        /// </summary>
        /// <param name="pieId">The ID number associated with the pie to remove</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpDelete("{pieId}")]
        public async Task<ActionResult> DeletePie(int pieId)
        {
            var pieToDelete = await pieShopRepository.GetPieAsync(pieId);

            if(pieToDelete == null) return NotFound();

            pieShopRepository.DeletePie(pieToDelete);
            await pieShopRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates a pie in the database
        /// </summary>
        /// <param name="pieId">The ID number associated with the existing pie</param>
        /// <param name="updatedPie">Json with the updated pie data</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPut("{pieId}")]
        public async Task<ActionResult> UpdatePie(int pieId, PieUpdateDto updatedPie)
        {
            var pieEntity = await pieShopRepository.GetPieAsync(pieId);

            if(pieEntity == null) return NotFound();

            mapper.Map(updatedPie, pieEntity);

            await pieShopRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// For partially updating a pie in the database
        /// </summary>
        /// <param name="pieId">The ID number associated with the pie to patch</param>
        /// <param name="patchDocument">Json patch document containing data for updating the pie</param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpPatch("{pieId}")]
        [Consumes("application/json-patch+json")]
        public async Task<ActionResult> PatchPie(int pieId, JsonPatchDocument<PieUpdateDto> patchDocument)
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
