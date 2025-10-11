using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //IActionResult for all types of views
        {
            return View();
        }

        public ViewResult About() //ViewResult for only views
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public FileResult GetImage() //FileResult for files
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "site.css");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "site/css");
        }

        public JsonResult GetJson() //JsonResult for JSON data
        {
            var data = new { Name = "Gym Management System", Version = "1.0.0" };
            return Json(data);
        }

        public RedirectResult GoToGoogle() //RedirectResult for redirection
        {
            return Redirect("https://www.google.com");
        }
        public ContentResult GetContent() //ContentResult for plain text
        {
            return Content("<h1>Hello, this is a plain text response.</h1>","HTML");
        }





    }
}
