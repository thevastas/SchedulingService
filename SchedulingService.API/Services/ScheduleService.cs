using SchedulingService.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;




namespace SchedulingService.API.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedulesCollection;


        public ScheduleService(IOptions<ScheduleDatabaseSettings> scheduleDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                scheduleDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                scheduleDatabaseSettings.Value.DatabaseName);

            _schedulesCollection = mongoDatabase.GetCollection<Schedule>(
                scheduleDatabaseSettings.Value.SchedulesCollectionName);
        }

        public async Task<List<Schedule>> GetAsync()
        {
            return await _schedulesCollection.Find(_ => true).ToListAsync();
        }
    
        public async Task<Schedule> GetAsync(Guid companyid)
        {
            // Y U NO WORK DICKHEAD
            return await _schedulesCollection.Find(x => x.CompanyId.Equals(companyid)).FirstOrDefaultAsync();
            // 265d5fb2-c70b-4eab-b513-1c2e158aaac3

        }

        public async Task CreateAsync(Company newCompany)
        {

            var schedule = new Schedule();
            var notifications = new List<DateTime>()
            {
                new DateTime(2015, 12, 25),
                new DateTime(2016, 12, 25),
                new DateTime(2017, 12, 25)
            };
            schedule.CompanyId = newCompany.Id;
            schedule.Notifications = notifications;

            await _schedulesCollection.InsertOneAsync(schedule);
        }
    


    }


}
