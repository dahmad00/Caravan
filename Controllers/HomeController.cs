using Microsoft.AspNetCore.Mvc;
using RailwaySystem.Models;
using RailwaySystem.Models.Admin;
using RailwaySystem.Models.User;
using System.Diagnostics;
using Microsoft.Extensions.Primitives;


namespace RailwaySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            LayoutData = new Models.User.LayoutData();
            ViewBag.LayoutData = LayoutData;


            _logger = logger;
        }

        public Models.User.LayoutData LayoutData { get; set; }
        public IActionResult Index()
        {

            ViewBag.LayoutData = LayoutData;

            Models.User.IndexData indexData = new Models.User.IndexData();

            return View(indexData);
        }

        public IActionResult SearchTickets()
        {
            string sourceLocation = Request.Form["from"];
            string destLocation = Request.Form["to"];

            Models.User.SearchTicketsData searchTicketsData;

            string on;
            on = Request.Form["on"];


            searchTicketsData = new Models.User.SearchTicketsData(sourceLocation, destLocation, on);
            return View("/Views/Home/SearchTicket.cshtml", searchTicketsData);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BookTicket()
        {
            string ticketClass = Request.Form["class"];
            string ticketRoute = Request.Form["route"];

            if (HttpContext.Session.GetString("UserName") == null)
            {
                HttpContext.Session.SetString("class", ticketClass);
                HttpContext.Session.SetString("route", ticketRoute);

                Models.User.LoginData loginData = new Models.User.LoginData();
                loginData.Error = "Please login to book ticket";
                return View("/Views/Home/Login.cshtml", loginData);
            }

            string email = HttpContext.Session.GetString("Email");
            string ticketCLass = HttpContext.Session.GetString("class");
            RailwaySystem.Models.User.BookTicketData bookTicketData = new Models.User.BookTicketData(Int32.Parse(ticketRoute), email, ticketCLass);
            return View("/Views/Home/BookTicket.cshtml", bookTicketData);
        }

        public IActionResult Register()
        {
            RailwaySystem.Models.User.RegisterData registerData = new RailwaySystem.Models.User.RegisterData();
            return View("/Views/Home/Register.cshtml", registerData);
        }

        public IActionResult CompleteRegistration()
        {
            string? error = null;
            
            RailwaySystem.Models.User.RegisterData registerData = new RailwaySystem.Models.User.RegisterData();

            if (Request.Form["name"] == StringValues.Empty || Request.Form["email"] == StringValues.Empty || Request.Form["password"] == StringValues.Empty || Request.Form["repeat-password"] == StringValues.Empty)
            {
                registerData.Error = "Please enter all the values";
                return View("/Views/Home/Register.cshtml", registerData);
            }
            
            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string password = Request.Form["password"];
            string repeatedPassword = Request.Form["repeat-password"];

            if (password != repeatedPassword)
            {
                error = "Passwords do not match";
            }
            else if (!IsValidEmail(email))
            {
                error = "Invalid email address";
            }    
            else if (Request.Form["terms"] == StringValues.Empty)
            {
                error = "You must agree to the terms to create account.";
            }
            else if (password.Length < 8)
            {
                error = "Password must have at least 8 characters";
            }
            else if (name.Length == 0)
            {
                error = "Name can not be empty";
            }
            if (error != null)
            {
                registerData.Error = error;
                return View("/Views/Home/Register.cshtml", registerData);
            }

            RailwaySystem.Models.User.User user = new RailwaySystem.Models.User.User(email, password, name);

            if (!user.uniqueEmail())
            {
                registerData.Error = "user with this email already exists";
                return View("/Views/Home/Register.cshtml", registerData);
            }
            
            user.updateDatabase();

            return View();
        }

        public IActionResult Login()
        {
            Models.User.LoginData loginData = new Models.User.LoginData();
            return View("/Views/Home/Login.cshtml", loginData);
        }

        public IActionResult CompleteLogin()
        {

            string email = Request.Form["email"];
            string password = Request.Form["password"];

            Models.Utility.Login login = new Models.Utility.Login(email, password);

            if (login.tryLogin())
            {
                Models.User.IndexData indexData = new Models.User.IndexData();
                string username = login.getName(email);
                HttpContext.Session.SetString("UserName",username.Substring(0, username.IndexOf(" ")));
                HttpContext.Session.SetString("Email", email);

                if (HttpContext.Session.GetString("route") != null)
                {
                    email = HttpContext.Session.GetString("Email");
                    string route = HttpContext.Session.GetString("route");
                    string ticketClass = HttpContext.Session.GetString("class");

                    RailwaySystem.Models.User.BookTicketData bookTicketData = new Models.User.BookTicketData(Int32.Parse(route), email, ticketClass);

                    return View("/Views/Home/BookTicket.cshtml", bookTicketData);
                }

                return View("Views/Home/Index.cshtml", indexData);
            }

            Models.User.LoginData loginData = new Models.User.LoginData();
            loginData.Error = "Incorrect login details";
            return View("/Views/Home/Login.cshtml", loginData);
        }

        public IActionResult Test()
        {
            return View("Test");
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();

            Models.User.IndexData indexData = new Models.User.IndexData();
            return View("/Views/Home/Index.cshtml", indexData);
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public IActionResult CompleteBooking()
        {
            int routeID = Int32.Parse(Request.Form["route"]);
            string email = (string)Request.Form["email"];
            string type = (string)Request.Form["type"];

            Models.User.Booking booking = new Models.User.Booking();
            booking.Type =  type;
            booking.UserEmail = email;  
            booking.RouteId = routeID;

            booking.makeBooking();

            return View("/Views/Home/BookingSuccess.cshtml");
        }

        public IActionResult MyTickets()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                Models.User.LoginData loginData = new Models.User.LoginData();
                return View("/Views/Home/Login.cshtml", loginData);
            }

            RailwaySystem.Models.User.MyTicketsData myTicketsData = new Models.User.MyTicketsData(HttpContext.Session.GetString("Email"));
            return View("/Views/Home/MyTickets.cshtml", myTicketsData);
        }

        public IActionResult DeleteBooking()
        {
            int id = Int32.Parse(Request.Form["ID"]);

            Booking booking = new Booking();
            booking.Id = id;

            booking.deleteBooking();

            return View("/Views/Home/BookingDeletionSuccess.cshtml");
        }
    }
}