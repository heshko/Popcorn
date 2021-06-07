using Popcorn.Areas.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Popcorn.Models
{
    public class TicketModel
    {
        public TicketModel()
        {
            
        }

        public TicketModel(Guid ticketNumber, int selectedRow, int selectedCol, int showModelId, string customerId, string ticketsStatus, DateTime ticketHoldExpires, string showDate)
        {
            TicketNumber = ticketNumber;
            SelectedRow = selectedRow;
            SelectedCol = selectedCol;
            ShowModelId = showModelId;
            CustomerId = customerId;
            TicketsStatus = ticketsStatus;
            TicketHoldExpires = ticketHoldExpires;
            ShowDate = showDate;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid TicketNumber { get; set; }

        [Required]
        public int SelectedRow { get; set; } 

        [Required]
        public int SelectedCol { get; set; } 

        [Required]
        public string TicketsStatus { get; set; } // True = Purchase, False = Reserve 

        public DateTime TicketHoldExpires { get; set; } // Controller to Expire ticket, system autoremoves it after 20min.

        [Required]
        public int ShowModelId { get; set; } 
        public ShowModel Shows { get; set; } // Id from Show

        public string ShowDate { get; set; } // This connect the Ticket of a specifik day with ShowId

        //[Required]
        public string CustomerId { get; set; } 
        public Customer Customer { get; set; }

    }
}
