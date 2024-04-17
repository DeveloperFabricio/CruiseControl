using CruiseControl.Application.GoogleCalendar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruiseControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleCalendarController : ControllerBase
    {
        private readonly GoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet("events")]
        [Authorize]
        public async Task<IActionResult> GetEventsOnDateAsync(DateTime date)
        {
            try
            {
                var events = await _googleCalendarService.GetEventsOnDateAsync(date, HttpContext.RequestAborted);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
