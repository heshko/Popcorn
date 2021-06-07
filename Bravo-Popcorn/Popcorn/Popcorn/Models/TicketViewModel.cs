using System.Collections.Generic;

namespace Popcorn.Models
{
    public class TicketViewModel
    {
        public IList<TicketModel> Ticket { get; set; }
        public ShowModel Show { get; set; }
    }
}
