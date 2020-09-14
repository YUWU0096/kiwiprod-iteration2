using System.Web.Mvc;

using Iteration_2.Models;
namespace Iteration_2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        AnalysisDBEntities ds = new AnalysisDBEntities();
      
        public ActionResult Analysis()
        {
            ViewBag.age = new SelectList(ds.AgeGroup, "Id", "ageGroup");
            ViewBag.gender = new SelectList(ds.Gender, "Id", "Gender");
            ViewBag.English_Profeciency = new SelectList(ds.English_Profeciency, "Id", "english_Profeciency");
            ViewBag.education = new SelectList(ds.Highest_education, "Id", "degree");
            return View();
        }


        public ActionResult Articles()
        {
            return View();
        }


    }
}