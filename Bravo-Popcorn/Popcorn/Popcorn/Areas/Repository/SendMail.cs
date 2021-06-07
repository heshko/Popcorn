using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Popcorn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Popcorn.Areas.Repository
{
    public class SendMail
    {


        public static void SendEmail(string receiver, string subject, string message, ShowModel showmodel, List<TicketModel> tickets)
        {
            //var user = UserManager.GetUserAsync(User).Result;

            var movieDate = showmodel.date.ToShortDateString();
            StringBuilder movieInfo = new StringBuilder();
            
            movieInfo.AppendLine(showmodel.movie.Title);
            movieInfo.AppendLine("About the movie: " + showmodel.movie.Description);

            movieInfo.AppendLine("\nStarts " + movieDate + " at " + showmodel.cinema.name + " in " + showmodel.saloon.Name);

            int totalSum = 0;
            foreach(var item in tickets)
            {
                movieInfo.AppendLine("Ticket " + " at seat " + item.SelectedCol + " at row " + item.SelectedRow + " price " + showmodel.Price.ToString());
                totalSum += showmodel.Price;
            }

            movieInfo.AppendLine("Total Price: " + totalSum);

            





            try
            {
              
                    var senderEmail = new MailAddress("BravoPopcornBravo@gmail.com", "Popcorn Cinema");
                    var receiverEmail = new MailAddress(receiver, "Customer of Popcorn Cinema"); // Hämta namn ifrån Identityuser
                    var password = "JonasMinBaby";
                    var sub = subject;
                    var body = movieInfo.ToString(); 
               
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);

                }
            }
            catch (Exception e)
            {
               
            }
            
        }
    }
}
