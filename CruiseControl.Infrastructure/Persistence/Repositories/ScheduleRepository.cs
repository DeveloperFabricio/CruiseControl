using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _appDbContext;

        public ScheduleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return await _appDbContext.Schedules.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            return await _appDbContext.Schedules.ToListAsync();
        }

        public async Task AddSchedule(Schedule schedule)
        {
            _appDbContext.Schedules.Add(schedule);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateSchedule(int id, Schedule updatedSchedule)
        {
            var existingSchedule = await _appDbContext.Schedules.FindAsync(id);
            if (existingSchedule == null)
            {
                throw new NotFoundException($"Schedule with id {id} not found");
            }

            existingSchedule.Date = updatedSchedule.Date;
            existingSchedule.RentalId = updatedSchedule.RentalId;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    throw new NotFoundException($"Schedule with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteSchedule(int id)
        {
            var schedule = await _appDbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                throw new NotFoundException($"Schedule with id {id} not found");
            }

            _appDbContext.Schedules.Remove(schedule);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool ScheduleExists(int id)
        {
            return _appDbContext.Schedules.Any(e => e.Id == id);
        }
    }
}
