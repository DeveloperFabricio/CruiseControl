using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
