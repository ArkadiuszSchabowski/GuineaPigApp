using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuineaPigApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuineaPigController : ControllerBase
    {
        private readonly IGuineaPigService _guineaPigService;

        public GuineaPigController(IGuineaPigService guineaPigService)
        {
            _guineaPigService = guineaPigService;
        }
        [HttpGet]
        public ActionResult<List<GuineaPig>> GetGuineaPigs([FromQuery] string email)
        {
            List<GuineaPig> guineaPigs = _guineaPigService.GetGuineaPigs(email);
            return Ok(guineaPigs);
        }
        [HttpGet("{name}")]
        public ActionResult<GuineaPigDto> GetGuineaPig([FromQuery] string email, [FromRoute] string name)
        {
            GuineaPigDto guineaPig = _guineaPigService.GetGuineaPig(email, name);
            return Ok(guineaPig);
        }
        [HttpPost]
        public ActionResult AddGuineaPig([FromQuery] string email, [FromBody] GuineaPigDto dto)
        {
            _guineaPigService.AddGuineaPig(email, dto);
            return Ok(new { message = "Profil świnki morskiej został dodany!" });
        }

        [HttpDelete("{name}")]
        public ActionResult RemoveGuineaPig([FromQuery] string email, [FromRoute] string name)
        {
            _guineaPigService.RemoveGuineaPig(email, name);
            return NoContent();
        }
    }
}
