using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Popcorn.Data;

namespace Popcorn.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Admin/TicketModels
        public async Task<IActionResult> Index(string id, string CustomerId)
        {
            if (id == null)
            {
                id = CustomerId;
            }
            var applicationDbContext = _context.Tickets.Include(t => t.Customer).Where(x => x.CustomerId == id)
                                                       .Include(t => t.Shows)
                                                       .ThenInclude(x => x.saloon).ThenInclude(x => x.Cinema).ThenInclude(x => x.city)
                                                       .Include(x => x.Shows).ThenInclude(x => x.movie);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Admin/TicketModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticketModel = await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Shows).ThenInclude(x => x.movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketModel == null)
            {
                return NotFound();
            }
            return View(ticketModel);
        }
        // POST: Admin/TicketModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string CustomerId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == CustomerId);
            var ticketModel = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticketModel);
            await _context.SaveChangesAsync();
            //return RedirectToAction("Index","Tickets", customer);
            var url = "/Admin/Ticket/Index/" + CustomerId;
            return Redirect(url);
        }
        private bool TicketModelExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}