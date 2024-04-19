using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;


        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving to the database
                user.Password = user.Password; // No hashing needed

                // Add the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                // Debug: Log success message with user details
                Console.WriteLine("User created successfully:");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"PasswordHash: {user.Password}");
                Console.WriteLine($"FullName: {user.FullName}");
                Console.WriteLine($"PhoneNumber: {user.PhoneNumber}");
                Console.WriteLine($"Role: {user.Role}");

                // Redirect to login page after successful signup
                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            Console.WriteLine($"Received email: {email}, password: {password}");

            if (user != null)
            {
                // Debug: Log successful login attempt
                Console.WriteLine("Successful login attempt");

                // Create claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                    // You can add more claims as needed
                };

                // Create identity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Create principal
                var principal = new ClaimsPrincipal(identity);

                // Sign in user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect based on user role
                if (user.Role == "user")
                {
                    // Redirect to home page for users
                    return RedirectToAction("Index", "Home");
                }
                else if (user.Role == "admin")
                {
                    // Redirect to admin page
                    return Redirect("/Admin");
                }
            }

            // Debug: Log failed login attempt
            Console.WriteLine("Failed login attempt");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ForgotPassword()
        {
            // Sign out user
            
            return View("forget");
        }
        public async Task<IActionResult> ChangePassword(string email, string password, string confirmPassword)
        {
            // Validate the email, password, and confirmPassword fields
            if (ModelState.IsValid)
            {
                // Check if the password and confirmPassword match
                if (password != confirmPassword)
                {
                    // If passwords do not match, return a JSON error response
                    return BadRequest("Passwords do not match.");
                }

                // Find the user by email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    // If user not found, return a JSON error response
                    return NotFound("User not found.");
                }

                // Update the user's password
                user.Password = password;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                // Log success message with user details
                Console.WriteLine("Password changed successfully:");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"New Password: {user.Password}");

                // Return a JSON success response
                return Ok("Password changed successfully.");
            }

            // If ModelState is invalid, return a JSON error response
            return BadRequest("Invalid data.");
        }


    }
}