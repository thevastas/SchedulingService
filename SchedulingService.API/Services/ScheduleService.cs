using SchedulingService.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;




namespace SchedulingService.API.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedulesCollection;
        private readonly IMongoCollection<Company> _companiesCollection;

        public ScheduleService(IOptions<ScheduleDatabaseSettings> scheduleDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                scheduleDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                scheduleDatabaseSettings.Value.DatabaseName);

            _schedulesCollection = mongoDatabase.GetCollection<Schedule>(
                scheduleDatabaseSettings.Value.SchedulesCollectionName);
            _companiesCollection = mongoDatabase.GetCollection<Company>(
                scheduleDatabaseSettings.Value.CompaniesCollectionName);

        }

        public async Task<List<Schedule>> GetAsync()
        {
            return await _schedulesCollection.Find(_ => true).ToListAsync();
        }
    
        public async Task<Schedule> GetAsync(string companyid)
        {
            return await _schedulesCollection.Find(x => x.CompanyId.Equals(companyid)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Company newCompany)
        {

            var schedule = new Schedule();
            DateTime today = DateTime.Today.AddHours(16);
            // Why is this needed? (-3 hours is retrieved from what it should be)

            var notifications = new List<DateTime>();

            bool supported = true;
            Guid id = Guid.NewGuid();
            schedule.CompanyId = id.ToString();
            newCompany.Id = id.ToString();
            switch (newCompany.Market)
            {
                case "DK":
                    notifications.Add(today.AddDays(1));
                    notifications.Add(today.AddDays(5));
                    notifications.Add(today.AddDays(10));
                    notifications.Add(today.AddDays(15));
                    notifications.Add(today.AddDays(20));
                    break;
                case "NO":
                    notifications.Add(today.AddDays(1));
                    notifications.Add(today.AddDays(5));
                    notifications.Add(today.AddDays(10));
                    notifications.Add(today.AddDays(20));
                    break;
                case "SE":
                    if (newCompany.Type != "large")
                    {
                        notifications.Add(today.AddDays(1));
                        notifications.Add(today.AddDays(7));
                        notifications.Add(today.AddDays(14));
                        notifications.Add(today.AddDays(28));
                    }
                    else supported = false;
                    break;
                case "FI":
                    if (newCompany.Type == "large")
                    {
                        notifications.Add(today.AddDays(1));
                        notifications.Add(today.AddDays(5));
                        notifications.Add(today.AddDays(10));
                        notifications.Add(today.AddDays(15));
                        notifications.Add(today.AddDays(20));
                    }
                    else supported = false;
                    break;
                default:
                    supported = false;
                    break;
            }



            schedule.Notifications = notifications;
            await _companiesCollection.InsertOneAsync(newCompany);

            if (supported)  await _schedulesCollection.InsertOneAsync(schedule);
        }
    


    }


}
