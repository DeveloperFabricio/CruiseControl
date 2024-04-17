using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddPaymentCommand : IRequest<Unit>
    {
        public PaymentDTO paymentDTO { get; set; }
    }

    
}
