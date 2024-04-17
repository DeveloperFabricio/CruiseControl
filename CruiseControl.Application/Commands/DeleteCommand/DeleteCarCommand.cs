using MediatR;

namespace CruiseControl.Application.Commands.DeleteCommand
{
    public class DeleteCarCommand : IRequest<Unit>
    {
        public int CarId { get; set; }
    }
}
