using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Iteration_2.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

using SelectPdf;
//using Syncfusion.Pdf;
//using Syncfusion.Pdf.Graphics;
//using System.Drawing;


namespace Iteration_2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        webModel ds = new webModel();
      
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
        public ActionResult Email()
        {

            return View();
        }
        public ActionResult Result()
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
            if (int.TryParse(Fc["education"], out highest_education))
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
            if (int.TryParse(Fc["English_Profeciency"], out englishID))
            {
                englishID = int.Parse(Fc["English_Profeciency"]);
              
            }
            else
            {
                return RedirectToAction("Analysis", "Search");
            }

            foreach (var y in ds.prediction)
            {
                if (y.gender == genderID && y.age_group_10y == ageID && y.english_proficiency == englishID && y.highest_education == highest_education)
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
        //public ActionResult CreateDocument()
        //{
        //Create an instance of PdfDocument.



        //    using (Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument())
        //    {
        //        //Add a page to the document
        //        Syncfusion.Pdf.PdfPage page = document.Pages.Add();

        //        //Create PDF graphics for the page
        //        PdfGraphics graphics = page.Graphics;

        //        //Set the standard font
        //        Syncfusion.Pdf.Graphics.PdfFont font = new Syncfusion.Pdf.Graphics.PdfStandardFont(PdfFontFamily.Helvetica, 20);

        //        //Draw the text
        //        graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

        //        // Open the document in browser after saving it
        //        document.Save("Output.pdf", HttpContext.ApplicationInstance.Response, HttpReadType.Save);
        //    }
        //    return View();
        //}





            
        public void CallEmail(string prediction1, string prediction2, string prediction3, string prediction4, string prediction5,
            string email)
        {
            Execute(prediction1, prediction2, prediction3, prediction4, prediction5, email).Wait();

        }


        static async Task Execute(string prediction1, string prediction2, string prediction3, string prediction4, string prediction5
            , string email)
        {
            //The below code is using the sendgrid API to send an e-mail. Code is taken from the sendgrid website.
            String UNIQUE_KEY = "";
            var client = new SendGridClient(UNIQUE_KEY);
            var from = new EmailAddress("hopmekiwiprod@gmail.com", "Your Predictions!");
            var to = new EmailAddress(email, "");
            var plainTextContent = "Your Predictions!"; //use your prediction strings here to form the template
            var htmlContent =
            "<p>" + "hopMe-kiwiprod <br><br> Hello! Your result is ready based on our Employability Solution. <br> <br> Areas you will most likely be employed in:<br>"
                + "Prediction 1- " + prediction1 + "<br>" + "Prediction 2- " + prediction2 + "<br>" + "Prediction 3- " + prediction3 +
                "<br>" + "Prediction 4- " + prediction4 + "<br>" + "Prediction 5- " + prediction5 + "<br> <br>" +
                "Read related articles:https://iteration2.hopme-kiwiprod.ml/Search/Articles" + "<br><br>" + "Thank you for trying out our AI model. <br> <br> hopMe truly wishes you" +
                "all the best with your career journey!<br><br>" + "Best Regards,<br>Kiwiprod<br>" +
                "</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, "Your Predictions!", plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        }


    }
}