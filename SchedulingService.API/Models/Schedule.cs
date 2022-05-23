namespace SchedulingService.API.Models
{
    public class Schedule
    {
        public Guid CompanyId { get; set; }
        public List<DateTime> Notifications { get; set; }
    }
}
