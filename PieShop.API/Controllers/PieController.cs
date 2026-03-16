using Microsoft.AspNetCore.Mvc;
using PieShop.API.Models;

namespace PieShop.API.Controllers
{
    [Route("api/pies")]
    [ApiController]
    public class PieController : ControllerBase
    {
        PieDataStore pieDataStore;
        ILogger<PieController> logger;

        public PieController(PieDataStore pieDataStore, ILogger<PieController> logger)
        {
            this.pieDataStore = pieDataStore ??
                throw new ArgumentNullException(nameof(pieDataStore));
            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PieDto>> GetPies()
        {
            return Ok(pieDataStore.PieData);
        }

        [HttpGet("{pieId}")]
        public ActionResult<PieDto> GetPie(int pieId)
        {
            try
            {
                var pie = pieDataStore.PieData.First(pie => pie.Id == pieId);

                if(pie == null)
                {
                    return NotFound();
                }

                return Ok(pie);
            }
            catch (Exception ex) 
            {
                logger.LogCritical($"Exception while getting a pie with the ID {pieId}", ex);

                return StatusCode(500, "A problem happened while handling your request");
            }
        }

    }
}
