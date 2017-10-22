using CashMachine.BusinessLayer.Services.Abstraction;
using CashMachine.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CashMachine.Controllers
{
    public class HomeController : Controller
    {
        private ICreditCardService _creditCardService;
        public HomeController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        public ActionResult Index()
        {
            ViewBag.TableId = "credit-card";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreditCardModel model)
        {
            ViewBag.TableId = "credit-card";
            if (ModelState.IsValid)
            {
                try
                {
                    var creditCardDTO = await _creditCardService.IsCreditCardValid(model.CreditCardNumber);

                    if (creditCardDTO.isValid)
                    {
                        TempData["creditCardId"] = creditCardDTO.CreditCardId;
                        return RedirectToAction("Pin");
                    }

                    return RedirectToAction("Error", new { errorMessage = "Credit Card is blocked!"});

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CreditCardNotFound", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Pin()
        {
            ViewBag.TableId = "pin";
            var creditCardId = Convert.ToInt32(TempData["creditCardId"]);
            if (creditCardId != 0)
            {
                return View(new PinModel { CreditCardId = creditCardId });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Pin(PinModel model)
        {
            ViewBag.TableId = "pin";
            if (ModelState.IsValid)
            {
                var isPinValid =  _creditCardService.IsPinValid(model.Pin, model.CreditCardId);
                if (isPinValid)
                {
                    TempData["creditCardId"] = model.CreditCardId;
                    return RedirectToAction("Index", "Operations");
                }
                try
                {
                    await _creditCardService.PinFailedAttempt(model.CreditCardId);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { errorMessage = ex.Message });
                }
                ModelState.AddModelError("IncorrectPin", "Pin code is incorrect");
            }
            return View(model);
        }

        public ActionResult Error(string errorMessage)
        {
            var model = new ErrorModel
            {
                ErrorMessage = errorMessage,
                PreviousUrl = Request.UrlReferrer.ToString()
            };
            return View(model);
        }
    }
}