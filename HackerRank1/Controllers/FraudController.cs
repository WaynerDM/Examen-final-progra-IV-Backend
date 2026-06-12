using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FraudController : ControllerBase
    {
        private readonly IFraudService _fraudService;

        public FraudController(IFraudService fraudService)
        {
            _fraudService = fraudService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var frauds = await _fraudService.GetAll();
            return Ok(frauds);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Fraud fraud)
        {
            if (fraud == null ||
                string.IsNullOrEmpty(fraud.ImpostorDetails) ||
                string.IsNullOrEmpty(fraud.ContactInfo))
                return BadRequest("ImpostorDetails y ContactInfo son obligatorios.");

            var result = await _fraudService.Add(fraud);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
    }
}