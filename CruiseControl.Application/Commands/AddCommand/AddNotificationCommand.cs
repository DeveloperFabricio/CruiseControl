using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddNotificationCommand : IRequest<Unit>
    {
        public NotificationDTO notificationDTO { get; set; }
    
    }
}
