using GymMangmentBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            this._analyticsService = analyticsService;
        }
        #region Action Return Types
        public ActionResult Index() //IActionResult for all types of views
        {
            var Data = _analyticsService.GetAnalyticsData();
            return View(Data);
        }
       

        //public ViewResult About() //ViewResult for only views
        //{
        //    ViewData["Message"] = "Your application description page.";
        //    return View();
        //}

        //public FileResult GetImage() //FileResult for files
        //{
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "site.css");
        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "site/css");
        //}

        //public JsonResult GetJson() //JsonResult for JSON data
        //{
        //    var data = new { Name = "Gym Management System", Version = "1.0.0" };
        //    return Json(data);
        //}

        //public RedirectResult GoToGoogle() //RedirectResult for redirection
        //{
        //    return Redirect("https://www.google.com");
        //}
        //public ContentResult GetContent() //ContentResult for plain text
        //{
        //    return Content("<h1>Hello, this is a plain text response.</h1>","HTML");
        //} 

        #endregion



    }
}
