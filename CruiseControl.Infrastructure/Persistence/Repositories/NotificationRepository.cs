using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            return await _context.Notifications.SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotification(int id, Notification updatedNotification)
        {
            var existingNotification = await _context.Notifications.FindAsync(id);
            if (existingNotification == null)
            {
                throw new NotFoundException($"Notification with id {id} not found");
            }

            existingNotification.Message = updatedNotification.Message;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
                {
                    throw new NotFoundException($"Notification with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                throw new NotFoundException($"Notification with id {id} not found");
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
