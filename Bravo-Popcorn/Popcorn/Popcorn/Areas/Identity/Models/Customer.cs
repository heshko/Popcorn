using Microsoft.AspNetCore.Identity;
using Popcorn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Popcorn.Areas.Identity.Models
{
    public class Customer :IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]

        public string lastName { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int age { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string street { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public string zipCode { get; set; }

        public List<TicketModel> Tickets { get; set; }
    }
}
