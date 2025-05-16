using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GuineaPigController : ControllerBase
    {
        private readonly IGuineaPigService _guineaPigService;

        public GuineaPigController(IGuineaPigService guineaPigService)
        {
            _guineaPigService = guineaPigService;
        }
        [HttpGet]
        public ActionResult<List<GuineaPigDto>> GetGuineaPigs([FromQuery] string email)
        {
            List<GuineaPigDto> guineaPigs = _guineaPigService.GetGuineaPigs(email);
            return Ok(guineaPigs);
        }
        [HttpGet("{name}")]
        public ActionResult<GuineaPigDto> GetGuineaPig([FromQuery] string email, [FromRoute] string name)
        {
            GuineaPigDto guineaPig = _guineaPigService.GetGuineaPig(email, name);
            return Ok(guineaPig);
        }
        [HttpGet("weights/{name}")]
        public ActionResult<List<GuineaPigWeightsDto>> GetWeights([FromQuery] string email, [FromRoute] string name)
        {
            List<GuineaPigWeightsDto> weights = _guineaPigService.GetWeights(email, name);
            return Ok(weights);
        }

        [HttpPost]
        public ActionResult AddGuineaPig([FromQuery] string email, [FromBody] GuineaPigDto dto)
        {
            _guineaPigService.Add(email, dto);
            return Ok(new { message = "Profil świnki morskiej został dodany!" });
        }
        [HttpPatch("update-weight/{name}")]
        public ActionResult UpdateWeightGuineaPig([FromQuery] string email, [FromRoute] string name, [FromBody] GuineaPigWeightDto dto)
        {
            _guineaPigService.UpdateWeight(email, name, dto);
            return Ok(new { message = "Waga świnki została zaaktualizowana!" });
        }

        [HttpDelete("{name}")]
        public ActionResult RemoveGuineaPig([FromQuery] string email, [FromRoute] string name)
        {
            _guineaPigService.RemoveGuineaPig(email, name);
            return NoContent();
        }
    }
}
