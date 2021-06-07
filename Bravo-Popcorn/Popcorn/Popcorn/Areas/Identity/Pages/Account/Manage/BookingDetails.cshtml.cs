using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Popcorn.Areas.Identity.Models;
using Popcorn.Data;
using Popcorn.Models;

namespace Popcorn.Areas.Identity.Pages.Account.Manage
{
    public class BookingDetails : PageModel
    {
        private readonly UserManager<Customer> _userManager;

        private readonly ApplicationDbContext _context;
        public List<TicketModel> Tickets { get; set; }

        public BookingDetails(UserManager<Customer> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
           await GetAllTicktes();

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if(ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            await GetAllTicktes();

            return Page();
        }

        private async Task GetAllTicktes()
        {
            var user = await _userManager.GetUserAsync(User);
            var tickets = await _context.Tickets.Where(x => x.CustomerId == user.Id)
                                                .Include(x => x.Shows)
                                                .ThenInclude(x => x.saloon)
                                                .ThenInclude(x => x.Cinema)
                                                .ThenInclude(x => x.city)
                                                .Include(x => x.Shows)
                                                .ThenInclude(x => x.movie)
                                                .ToListAsync();
            Tickets = tickets;
        }
    }
}
