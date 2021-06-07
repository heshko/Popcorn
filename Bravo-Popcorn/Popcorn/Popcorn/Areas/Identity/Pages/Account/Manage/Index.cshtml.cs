using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Popcorn.Areas.Identity.Models;
using Popcorn.Data;

namespace Popcorn.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<Customer> userManager,
            SignInManager<IdentityUser> signInManager,
             ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "First Name")]
            public string firstName { get; set; }

            [Display(Name = "Last Name")]
            public string lastName { get; set; }

            [Display(Name = "Age")]
            public int age { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "Street")]
            public string street { get; set; }
            [Display(Name = "Zib Code")]
            public string zipCode { get; set; }


        }

        private async Task LoadAsync(Customer user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.firstName;
            var LastName = user.lastName;
            var city = user.city;
            var street = user.street;
            var zipCode = user.zipCode;
            var age = user.age;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                firstName = firstName,
                lastName = LastName,
                age = age,
                City = city,
                street = street,
                zipCode = zipCode
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.firstName;
            var lastName = user.lastName;
            var city = user.city;
            var street = user.street;
            var zipCode = user.zipCode;
            var age = user.age;

            if (Input.PhoneNumber != phoneNumber || Input.firstName != firstName ||
                Input.lastName != lastName || Input.age != age || Input.City != city ||
                Input.street != street || Input.zipCode != zipCode)
            {

                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                user.firstName = Input.firstName;
                user.lastName = Input.lastName;
                user.age = Input.age;
                user.city = Input.City;
                user.street = Input.street;
                user.zipCode = Input.zipCode;

                _context.Customers.Update(user);
                _context.SaveChanges();
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
