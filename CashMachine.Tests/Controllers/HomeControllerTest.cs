using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashMachine.Controllers;
using CashMachine.BusinessLayer.Services.Abstraction;
using Moq;
using CashMachine.Models;
using CashMachine.Model.DTOs;
using System.Threading.Tasks;

namespace CashMachine.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var mockRepo = new Mock<ICreditCardService>();
            HomeController controller = new HomeController(mockRepo.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostIndex()
        {
            // Arrange
            var model = new CreditCardModel { CreditCardNumber = "1111222233334444" };
            var creditCardValidityDto = new CreditCardValidityDTO { CreditCardId = 1, isValid = true };
            var mockRepo = new Mock<ICreditCardService>();
            mockRepo.Setup(x => x.IsCreditCardValid(model.CreditCardNumber))
                .Returns(Task.FromResult(creditCardValidityDto));
            HomeController controller = new HomeController(mockRepo.Object);

            // Act
            var result = controller.Index(model).Result as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Pin", result.RouteValues["Action"]);
        }

    }
}
