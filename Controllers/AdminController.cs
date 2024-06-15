using Microsoft.AspNetCore.Mvc;
using RailwaySystem.Models.Admin;

namespace RailwaySystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRoute()
        {
            AddRouteData addRouteData = new AddRouteData();
            return View("/Views/Admin/Route/Add.cshtml", addRouteData);
        }

        public IActionResult ViewCurrentRoutes()
        {
            ShowCurrentRoutesData showCurrentRoutesData = new ShowCurrentRoutesData();
            return View("/Views/Admin/Route/ShowCurrentRoutes.cshtml", showCurrentRoutesData);
        }
        
        public IActionResult ViewOldRoutes()
        {
            ShowOldRoutesData showOldRoutesData = new ShowOldRoutesData();
            return View("/Views/Admin/Route/ShowOldRoutes.cshtml", showOldRoutesData);
        }

        public IActionResult DeleteRoute()
        {
            int id = Int32.Parse(Request.Form["id"]);
            Models.Admin.Route route = new Models.Admin.Route(id);
            route.delete();
            return View("/Views/Admin/Route/DeleteRouteSuccess.cshtml");
        }

        public IActionResult RouteDetails()
        {
            int id = Int32.Parse(Request.Form["ID"]); 
            Models.Admin.Route route = new Models.Admin.Route(id);
            return View("/Views/Admin/Route/RouteDetails.cshtml", route);
        }
        public string Text()
        {
            return "Controller fired";
        }

        public ActionResult submitRoute()
        {
            SaveRouteData save = new SaveRouteData();
            save.TrainId = Int32.Parse(Request.Form["train"]);

            LinkedList<string> errors = new LinkedList<string>();



            string dep = Request.Form["dep_date"] + " " + Request.Form["dep_time"];
            string arr = Request.Form["arr_date"] + " " + Request.Form["arr_time"];

            if (dep == " " || arr == " ")
            {
                string error = "Please select a date/time";
                errors.AddLast(error);
            }
            else if (dep == arr)
            {
                string error = "Departure and Arrival time can not be same";
                errors.AddLast(error);
            } 
            else
            {
                DateTime departureDate = Convert.ToDateTime(dep);
                DateTime arrivalDate = Convert.ToDateTime(arr);

                if (DateTime.Compare(departureDate, arrivalDate) > 0)
                {
                    string error = "Departure Time must be earlier than arrival Time";
                    errors.AddLast(error);
                }
            }

            save.Dep = dep;
            save.Arr = arr;
            save.SourceStationId = Int32.Parse(Request.Form["st-source"]);
            save.DestStationId = Int32.Parse(Request.Form["st-dest"]);

            if (save.SourceStationId == save.DestStationId)
            {
                string error = "Departure Station must be different from arrival Station";
                errors.AddLast(error);
            }

            try
            {
                save.CountA = Int32.Parse(Request.Form["count-class-a"]);
                save.CountB = Int32.Parse(Request.Form["count-class-b"]);
                save.CountC = Int32.Parse(Request.Form["count-class-c"]);
                save.CountC_Seats = Int32.Parse(Request.Form["count-class-c-no-berth"]);
            }
            catch (FormatException Ex)
            {
                string error = "Please enter all the seat counts correctly";
                errors.AddLast(error);
            }


            try
            {
                save.FareA = Int32.Parse(Request.Form["fare-class-a"]);
                save.FareB = Int32.Parse(Request.Form["fare-class-b"]);
                save.FareC = Int32.Parse(Request.Form["fare-class-c"]);
                save.FareC_Seats = Int32.Parse(Request.Form["fair-class-c-no-berth"]);
            }
            catch (FormatException ex)
            {
                string error = "Please enter all the fares correctly";
                errors.AddLast(error);
            }

            if (errors.Count > 0)
            {
                AddRouteData addRouteData = new AddRouteData(); 
                addRouteData.addErrors(errors);
                return View("/Views/Admin/Route/Add.cshtml", addRouteData);
            }

            save.saveData();
            return View("/Views/Admin/Route/AddRouteSuccess.cshtml");
        }


        
    }
}
