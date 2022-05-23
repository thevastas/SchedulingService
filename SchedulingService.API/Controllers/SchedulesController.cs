using Microsoft.AspNetCore.Mvc;
using SchedulingService.API.Models;
using SchedulingService.API.Services;


namespace SchedulingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController: ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService) =>
            _scheduleService = scheduleService;

        [HttpGet]
        public async Task<ActionResult<Schedule>> Get()
        {
            var schedule = await _scheduleService.GetAsync(Guid.NewGuid());

            if (schedule is null)
            {
                return NotFound();
            }

            return schedule;
        }


    }
}
