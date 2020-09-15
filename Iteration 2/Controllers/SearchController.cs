using System.Web.Mvc;

using Iteration_2.Models;
namespace Iteration_2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        AnalysisDBEntities1 ds = new AnalysisDBEntities1();
      
        public ActionResult Analysis()
        {
            ViewBag.age = new SelectList(ds.AgeGroup, "Id", "ageGroup1");
            ViewBag.gender = new SelectList(ds.Gender, "Id", "Gender1");
            ViewBag.English_Profeciency = new SelectList(ds.English_Profeciency, "Id", "degree", "value");
            ViewBag.education = new SelectList(ds.Highest_education, "Id", "degree", "value");
            return View();
        }



        public ActionResult Articles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(FormCollection Fc)

        {
           int test = 122333;
            ViewBag.result = test;

            return View();
        }

    }
}