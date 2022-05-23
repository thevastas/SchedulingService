using SchedulingService.API.Models;

namespace SchedulingService.API.Services
{
    public class ScheduleService : IScheduleService
    {
        public async Task<Schedule> GetAsync(Guid companyid)
        {
            var schedule = new Schedule();
            var notifications = new List<DateTime>()
            {
                new DateTime(2015, 12, 25),
                new DateTime(2016, 12, 25),
                new DateTime(2017, 12, 25)
            };

            schedule.CompanyId = companyid;
            schedule.Notifications = notifications;

            return schedule;
        }



    }


}
