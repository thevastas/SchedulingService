using SchedulingService.API.Models;
namespace SchedulingService.API
{
    public class ScheduleConstructor
        {
        static public readonly int[] dkdays = { 1, 5, 10, 15, 20 };
        static public readonly int[] nodays = { 1, 5, 10, 20 };
        static public readonly int[] sedays = { 1, 7, 14, 28 };
        static public readonly int[] fidays = { 1, 5, 10, 15, 20 };
        public Schedule? CalculateSchedule(Company newCompany)
        {
            var schedule = new Schedule();
            

            DateTime today = DateTime.UtcNow.Date;

            int[] days = Array.Empty<int>();

            var notifications = new List<DateTime>();
            bool supported = true;

            Guid id = Guid.NewGuid();
            schedule.CompanyId = id.ToString();

            newCompany.Id = schedule.CompanyId;

            switch (newCompany.Market)
            {
                case "DK":
                    days = dkdays;
                    break;
                case "NO":
                    days = nodays;
                    break;
                case "SE":
                    if (newCompany.Type != "large") days = sedays;
                    else return null;
                    break;
                case "FI":
                    if (newCompany.Type == "large") days = fidays;
                    else return null;
                    break;
                default:
                    return null;
            }

            for (int i = 0; i < days.Length; i++) notifications.Add(today.AddDays(days[i]));
            schedule.Notifications = notifications;

            return schedule;
        }
    }


public static class CountryConstants
    {
        public const string denmark = "DK";
        public const string norway = "NO";
        public const string sweden = "SE";
        public const string finland = "FI";
    }
}
