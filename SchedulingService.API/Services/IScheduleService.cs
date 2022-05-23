using SchedulingService.API.Models;

namespace SchedulingService.API.Services

{
    public interface IScheduleService
    {

        public Task<Schedule> GetAsync(Guid companyid);
    }
}
