using CruiseControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationById(int id);
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task AddNotification(Notification notification);
        Task UpdateNotification(int id, Notification notification);
        Task DeleteNotification(int id);
        Task SaveChangesAsync();
    }
}
