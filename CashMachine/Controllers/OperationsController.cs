using CashMachine.BusinessLayer.Services.Abstraction;
using CashMachine.Model.DAL.Repositories.Abstraction;
using CashMachine.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CashMachine.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly IOperationsRepository _operationRepository;
        private readonly ICreditCardRepository _creditCardRepository;

        public OperationsController(ICreditCardService creditCardService,
            IOperationsRepository operationRepository, ICreditCardRepository creditCardRepository)
        {
            _creditCardService = creditCardService;
            _operationRepository = operationRepository;
            _creditCardRepository = creditCardRepository;
        }
        public ActionResult Index()
        {
            var creditCardId = Convert.ToInt32(TempData["creditCardId"]);
            TempData.Keep("creditCardId");
            if (creditCardId != 0)
            {
                return View(new BalanceModel { CreditCardId = creditCardId });
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Balance()
        {
            var creditCardId = Convert.ToInt32(TempData["creditCardId"]);
            TempData.Keep("creditCardId");
            if (creditCardId != 0)
            {
                var creditCardDTO = await _creditCardService.GetCreditCard(creditCardId);
                var model = new BalanceModel
                {
                    CreditCardId = creditCardId,
                    Balance = creditCardDTO.Balance,
                    CreditCardNumber = creditCardDTO.CreditCardNumber,
                    Date = DateTime.Now,
                };
                await _operationRepository.AddOperation(creditCardId, 1, "Balance was requested");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Withdraw()
        {
            var creditCardId = Convert.ToInt32(TempData["creditCardId"]);
            TempData.Keep("creditCardId");
            if (creditCardId != 0)
            {
                ViewBag.TableId = "t-withdraw";
                var model = new WithdrawModel { CreditCardId = creditCardId };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Withdraw(WithdrawModel model)
        {
            var creditCardDTO = await _creditCardService.GetCreditCard(model.CreditCardId);
            if (creditCardDTO.Balance < model.WithdrawalAmount)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = "Not enough money on your account "});
            }
            await _operationRepository.AddOperation(model.CreditCardId, 2, $"{model.WithdrawalAmount}$ was withdrawn");
            var balance = creditCardDTO.Balance - model.WithdrawalAmount;
            await _creditCardRepository.SetBalance(model.CreditCardId, balance);

            var reportModel = new ReportModel
            {
                CreditCardId = model.CreditCardId,
                WithdrawalAmount = model.WithdrawalAmount,
                Balance = balance,
                CreditCardNumber = creditCardDTO.CreditCardNumber,
                DateOfWithdrawal = DateTime.Now
            };
            TempData["ReportModel"] = reportModel;

            return RedirectToAction("Report");
        }

        public ActionResult Report()
        {
            var model = TempData["ReportModel"] as ReportModel;
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Back()
        {
            var creditCardId = Convert.ToInt32(TempData["creditCardId"]);
            TempData.Keep("creditCardId");
            return RedirectToAction("Index");
        }
    }
}