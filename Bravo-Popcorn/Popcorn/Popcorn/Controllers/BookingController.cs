using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Popcorn.Areas.Repository;
using Popcorn.Data;
using Popcorn.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Popcorn.Controllers
{


    public class BookingController : Controller  
    {
        private readonly ApplicationDbContext context;

        public UserManager<IdentityUser> UserManager { get; }

        public BookingController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            UserManager = userManager;
        }

        public async Task<IActionResult> Index(int id, string dateOfShow)
        {
            FilterModel filters = new FilterModel
            {
                DateOfShow = dateOfShow

            };

            ViewBag.FilterSearch = filters;

            DateTime dt = DateTime.Now;
            dt = dt.AddSeconds(-dt.Second);

            context.Tickets.RemoveRange(context.Tickets.Where(x => x.TicketsStatus == "Selected" && x.TicketHoldExpires < dt));

            context.SaveChanges();

            TicketViewModel mymodel = new TicketViewModel();
            mymodel.Show = await context.Shows
                                .Include(x => x.movie)
                                .ThenInclude(x => x.GenresInMovies)
                                .ThenInclude(x => x.GenreModel)
                                .Include(x => x.saloon)
                                .Include(x => x.cinema)
                                .ThenInclude(x => x.city)
                                .FirstOrDefaultAsync(x => x.id == id);

            DateTime controllDate = DateTime.Parse(dateOfShow);
            string checkString = controllDate.ToString("yyyy-MM-dd");

            mymodel.Ticket = await context.Tickets.Where(x => x.Shows.id == id &&
                                                         x.ShowDate.Contains(checkString))
                                                        .ToListAsync();

            return View(mymodel);
        }

        public async Task<IActionResult> Confirm(int id, string[] Ticket, string dateOfShow)
        {
            List<TicketModel> ticketDetails = new List<TicketModel>();
            
            var user = UserManager.GetUserAsync(User).Result;

            for (int i = 0; i < Ticket.Length; i++)
            {
                string[] addTicket = Ticket[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                
                Guid ticketNumber = Guid.NewGuid(); // Create Unique TicketNumber with Guid

                DateTime expireTicket = DateTime.Now.AddMinutes(20).AddSeconds(-DateTime.Now.Second); // Create 20min tickethold for a ticket to expire

                var NewTicket = new TicketModel {
                    TicketNumber = ticketNumber,
                    SelectedRow = int.Parse(addTicket[0]),
                    SelectedCol = int.Parse(addTicket[1]),
                    ShowModelId = id
                };

                ticketDetails.Add(NewTicket); // Add values to list for viewbag          
                
                var newTicket = new TicketModel(ticketNumber, int.Parse(addTicket[0]), int.Parse(addTicket[1]), id, user.Id, "Selected", expireTicket, dateOfShow);
                context.Tickets.Add(newTicket);
            }

            ViewBag.ActiveFilters = ticketDetails; // Define viewbag items
     
            context.SaveChanges();

            TicketViewModel mymodel = new TicketViewModel();

            mymodel.Show = await context.Shows
                                .Include(x => x.movie)
                                .ThenInclude(x => x.GenresInMovies)
                                .ThenInclude(x => x.GenreModel)
                                .Include(x => x.saloon)
                                .Include(x => x.cinema)
                                .ThenInclude(x => x.city)
                                .FirstOrDefaultAsync(x => x.id == id);

            DateTime controllDate = DateTime.Parse(dateOfShow);
            string checkString = controllDate.ToString("yyyy-MM-dd");

            mymodel.Ticket = await context.Tickets.Where(x => x.Shows.id == id &&
                                                         x.ShowDate.Contains(checkString))
                                                        .ToListAsync();

            return View(mymodel);
        }


        public IActionResult Submit(string ticketNumbers, bool action, int showId, string stripeToken)
        {
            string[] stringTicketNumberList = ticketNumbers.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<TicketModel> tickets = new List<TicketModel>();

            foreach (var ticketNumber in stringTicketNumberList) // konvertera string till Guid och plocka ut rätt biljetter
            {
                Guid guidTicketNumber = Guid.Parse(ticketNumber);

                tickets.Add(context.Tickets
                                .Include(x => x.Customer)
                                .Include(x => x.Shows)
                                    .ThenInclude(x => x.movie)
                                .Include(x => x.Shows)
                                    .ThenInclude(x => x.saloon)
                                .Include(x => x.Shows)
                                    .ThenInclude(x => x.cinema)
                                        .ThenInclude(x => x.city)
                                .FirstOrDefault(x => x.TicketNumber == guidTicketNumber));
            }




            // action = true ska vara betala, false reservera.

            foreach (var ticket in tickets)
            {
                if (!action)
                {
                    ticket.TicketsStatus = "Reserved";
                }
                else
                {
                    ticket.TicketsStatus = "Paid";
                }

                context.SaveChanges();
            }


            var user = UserManager.GetUserAsync(User).Result;

            var theShow = context.Shows
                                .Include(x => x.movie)
                                .ThenInclude(x => x.GenresInMovies)
                                .ThenInclude(x => x.GenreModel)
                                .Include(x => x.saloon)
                                .Include(x => x.cinema)
                                .ThenInclude(x => x.city)
                                .FirstOrDefault(x => x.id == showId);

            // SendMail.SendEmail(user.Email, "Thank you for booking" + tickets.movie.Title.Take(1), "Message", showDetails, ticketNumbers);
            SendMail.SendEmail(user.Email, "Thank you for booking the movie " + theShow.movie.Title, "Message", theShow, tickets);
            // Skickar mail till oss själva just nu, behöver kundifo samt biljettinfo


            // **** PAYMENT ****

            if (!string.IsNullOrEmpty(stripeToken)) // kör om string INTE är tom eller null
            {
                StripeConfiguration.ApiKey = "sk_test_vj7oMGcsnXJxyKecNGK6yTAh0011otS4gr";

                var options = new ChargeCreateOptions
                {
                    Amount = (theShow.Price * tickets.Count) * 100, // (pris * antal biljetter) * 100 => stripe drar i öre, inte kronor så 200 == 2,00 kr
                    Currency = "sek",
                    Source = stripeToken,
                    Description = $"Popcorn - {theShow.movie.Title} - {user.UserName}",
                };

                var service = new ChargeService();
                service.Create(options);
            }


            // **********************

            return View(tickets);
        }
    }
}