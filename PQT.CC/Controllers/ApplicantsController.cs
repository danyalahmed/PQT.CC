using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PQT.CC.Data;
using PQT.CC.Models;
using PQT.CC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQT.CC.Controllers
{
    public class ApplicantsController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public ApplicantsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // POST: Applicants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyForCreditCard(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                //check if user has applied already
                var temp = await RegisterUserIfRequired(applicant);

                //checking the age restrictions
                if ((DateTime.Now.Subtract(temp.DOB).TotalDays / 365) < 18)
                {
                    var res2 = new Results
                    {
                        Applicant = temp,
                        Card = null,
                        Date = DateTime.Now,
                        IsFailled = true
                    };

                    dbContext.Results.Add(res2);
                    await dbContext.SaveChangesAsync();


                    return RedirectToAction(nameof(Failled));
                }

                //preparing result Model ...
                var result = new ResultViewModel
                {
                    Applicant = temp,
                };

                //checking salary restrictions
                if (temp.AnnualIncome > 30000)
                {
                    var Barclaycard = await dbContext.Cards
                        .Where(c => c.Name.Equals("Barclaycard"))
                        .Include(c => c.Promotion)
                        .FirstOrDefaultAsync();
                    if (Barclaycard == null)
                    {
                        //we could not find the card, someting went wrong!
                        return RedirectToLocal("/Home/Error/400");
                    }

                    result.CardsShown = Barclaycard;
                }
                else
                {
                    var Vanquis = await dbContext.Cards
                        .Where(c => c.Name.Equals("Vanquis"))
                        .Include(c => c.Promotion)
                        .FirstOrDefaultAsync();
                    if (Vanquis == null)
                    {
                        //we could not find the card, someting went wrong!
                        return RedirectToLocal("/Home/Error/400");
                    }

                    result.CardsShown = Vanquis;
                }

                var res = new Results
                {
                    Applicant = result.Applicant,
                    Card = result.CardsShown,
                    Date = DateTime.Now,
                    IsFailled = false
                };

                dbContext.Results.Add(res);
                await dbContext.SaveChangesAsync();

                return View("~/Views/Results/ShowResult.cshtml", result);

            }

            //if we reach here something went wrong, send user back to application form view!
            return View("~/Views/Applicants/Create.cshtml", applicant);
        }

        private async Task<Applicant> RegisterUserIfRequired(Applicant applicant)
        {

            var temp = await dbContext.Applicants
                    .FirstOrDefaultAsync(a => a.FirstName.Equals(applicant.FirstName)
                    && a.LastName.Equals(applicant.LastName)
                    && a.DOB == applicant.DOB
                    && a.AnnualIncome == applicant.AnnualIncome);
            return temp ?? applicant;
        }

        public ActionResult Failled() => View();


        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}