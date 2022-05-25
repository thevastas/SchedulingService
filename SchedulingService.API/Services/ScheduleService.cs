﻿using SchedulingService.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
//using MongoDB.Bson;




namespace SchedulingService.API.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedulesCollection;
        private readonly IMongoCollection<Company> _companiesCollection;
        //private readonly contryschedule = new CountrySchedule();
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
            ScheduleConstructor _scheduleConstructor = new ScheduleConstructor();
            _scheduleConstructor.CalculateSchedule(newCompany);

            await _schedulesCollection.InsertOneAsync(_scheduleConstructor.CalculateSchedule(newCompany));

            await _companiesCollection.InsertOneAsync(newCompany);
        }
    


    }


}
