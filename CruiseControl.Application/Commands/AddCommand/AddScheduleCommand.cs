using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddScheduleCommand : IRequest<Unit>
    {
        public ScheduleDTO scheduleDTO { get; set; }
    
    }
}
