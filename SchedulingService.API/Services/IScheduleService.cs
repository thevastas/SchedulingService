using SchedulingService.API.Models;
namespace SchedulingService.API.Services

{
    public interface IScheduleService
    {
        public Task<Schedule> GetAsync(string companyid);
        public Task CreateAsync(Company newCompany);
        public Task<List<Schedule>> GetAsync();
    }
}
