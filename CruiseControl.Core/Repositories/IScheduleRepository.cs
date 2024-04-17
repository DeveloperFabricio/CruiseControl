using CruiseControl.Core.Entities;

namespace CruiseControl.Core.Repositories
{
    public interface IScheduleRepository
    {
        Task<Schedule> GetScheduleById(int id);
        Task<IEnumerable<Schedule>> GetAllSchedules();
        Task AddSchedule(Schedule schedule);
        Task UpdateSchedule(int id, Schedule schedule);
        Task DeleteSchedule(int id);
        Task SaveChangesAsync();
    }
}
