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
        public ActionResult quizz()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(FormCollection Fc)

        {
            int genderID;
            if (int.TryParse(Fc["Gender"], out genderID))
            {
                genderID = int.Parse(Fc["Gender"]);
             
            }
            else
            {
                return RedirectToAction("Analysis", "Search");
            }

            int ageID;
            if (int.TryParse(Fc["age"], out ageID))
            {
                ageID = int.Parse(Fc["age"]);
              
            }
            else
            {
                return RedirectToAction("Analysis", "Search");
            }

            int educationID;
            int highest_education;
            if (int.TryParse(Fc["education"], out educationID))
            {
                educationID = int.Parse(Fc["education"]);
                foreach (var x in ds.Highest_education)
                {
                    if (x.id == educationID)
                    {
                        highest_education = x.value;
                    }
                }
            }
            else
            {
              return RedirectToAction("Analysis", "Search");

            }

            int englishID;
            if (int.TryParse(Fc["English_Profeciency"], out ageID))
            {
                englishID = int.Parse(Fc["English_Profeciency"]);
              
            }
            else
            {
                return RedirectToAction("Analysis", "Search");
            }

            foreach (var y in ds.Prediction)
            {
                if (y.gender == genderID && y.age_group_10y==ageID && y.english_proficiency==englishID && y.highest_education==educationID )
                {
                    ViewBag.pred1 = y.pred_1;
                    ViewBag.pred2 = y.pred_2;
                    ViewBag.pred3 = y.pred_3;
                    ViewBag.pred4 = y.pred_4;
                    ViewBag.pred5 = y.pred_5;
                }
            }

            return View();
        }

    }
}