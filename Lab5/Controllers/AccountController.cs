using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        // Inject the Auth0 Management API client
        private readonly IManagementApiClient _managementApiClient;

        public AccountController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        // Login method for Auth0 authentication
        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl) // Define where to redirect after successful login
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        // Logout method to sign the user out
        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home")) // Where to redirect after logout
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Clear the local cookie
        }

        // Show the registration form
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle the registration logic here
                try
                {
                    var userCreateRequest = new UserCreateRequest
                    {
                        Email = model.Email,
                        Password = model.Password,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Connection = "Username-Password-Authentication",
                        EmailVerified = true, // Set email as verified
                        PhoneVerified = true  // Set phone number as verified
                    };

                    // Assuming you have access to an Auth0 Management API client
                    var user = await _managementApiClient.Users.CreateAsync(userCreateRequest);

                    // Redirect to the login page or show a success message
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    // Handle exception and show an error message to the user
                    ViewBag.ErrorMessage = "An error occurred during registration: " + ex.Message;
                    return View(model);
                }
            }

            // If validation fails, redisplay the form with validation messages
            return View(model);
        }
        [Authorize]
        public IActionResult Profile()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            return View(new
            {
                Name = User.Identity.Name,
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "phone_number")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
    }
}
