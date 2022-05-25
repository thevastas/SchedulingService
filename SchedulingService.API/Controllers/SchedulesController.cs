using Microsoft.AspNetCore.Mvc;
using SchedulingService.API.Models;
using SchedulingService.API.Services;


namespace SchedulingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController: ControllerBase
    {
        private readonly IScheduleService _schedulesService;

        public SchedulesController(IScheduleService schedulesService) =>
            _schedulesService = schedulesService;

        [HttpGet]
        public async Task<List<Schedule>> Get()
        {
            return await _schedulesService.GetAsync();
        }

        [HttpGet("{CompanyId}")]
        public async Task<ActionResult<Schedule>> Get(Guid CompanyId)
        {
            var schedule = await _schedulesService.GetAsync(CompanyId);

            if (schedule is null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Company newCompany)
        {
            await _schedulesService.CreateAsync(newCompany);

            return CreatedAtAction(nameof(Get), new { id = newCompany.Id }, newCompany);
        }

    }
}
