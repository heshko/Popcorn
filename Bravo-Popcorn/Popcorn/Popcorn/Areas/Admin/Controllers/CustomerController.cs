using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Popcorn.Areas.Identity.Models;
using Popcorn.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popcorn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly UserManager<IdentityUser> __userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;
        public CustomerController(ApplicationDbContext context, UserManager<Customer> userManager,
            SignInManager<IdentityUser> signInManager, IEmailSender emailSender,
            ILogger<RegisterModel> logger, UserManager<IdentityUser> __userManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
           this.__userManager = __userManager;
        }

        public Input input { get; set; }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Customers.ToListAsync();

            return View(users);
        }          

      
        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var userName = await _userManager.FindByIdAsync(Id);

            return View(userName);
        }    

        [HttpPost]
        public async Task<IActionResult> Edit(string Id, [Bind("Id,firstName,lastName,Email,EmailConfirmed,PhoneNumber,age,city,street,zipCode")] Customer customer)
        {
            var userName = await _userManager.FindByIdAsync(Id);

            if (Id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userName.firstName = customer.firstName;
                    userName.lastName = customer.lastName;
                    userName.Email = customer.Email;
                    userName.EmailConfirmed = customer.EmailConfirmed;
                    userName.PhoneNumber = customer.PhoneNumber;
                    userName.age = customer.age;
                    userName.city = customer.city;
                    userName.street = customer.street;
                    userName.zipCode = customer.zipCode;

                    _context.Customers.Update(userName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerModelExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        private bool CustomerModelExists(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(string id)
        {
            // Båda fungerar
            //var user = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id); 
            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Delete([Bind("Id")] Customer customer)
        {
            var user = await _userManager.FindByIdAsync(customer.Id);


            // Fungerar på samma sätt

            //try
            //{
            //    _context.Customers.Remove(user);
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index"); 
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("firstName,lastName,email,phoneNumber,age,city,street,zipcode,password,confirmPassword")] Input customer)
        {
            
            if (ModelState.IsValid)
            {
                var userName = new Customer
                {
                    UserName = customer.email,
                    firstName = customer.firstName,
                    lastName = customer.lastName,
                    Email = customer.email,
                    PhoneNumber = customer.phoneNumber,
                    age = customer.age,
                    city = customer.city,
                    street = customer.street,
                    zipCode = customer.zipcode,
                    EmailConfirmed = true                
                };
                
                var result = await __userManager.CreateAsync(userName, customer.password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await __userManager.GenerateEmailConfirmationTokenAsync(userName);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }               
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
    public class Input 
    {
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int age { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string street { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public string zipcode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
    }    
}
