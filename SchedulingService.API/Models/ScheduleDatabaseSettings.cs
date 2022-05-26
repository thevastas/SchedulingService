namespace SchedulingService.API.Models;

public class ScheduleDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SchedulesCollectionName { get; set; } = null!;
    public string CompaniesCollectionName { get; set; } = null!;
}