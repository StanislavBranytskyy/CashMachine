using CashMachine.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace CashMachine.Model.DAL
{
    public class CashMachineInitializer : DropCreateDatabaseAlways<CashMachineContext>
    {
        protected override void Seed(CashMachineContext context)
        {
            var creditCardList = new List<CreditCard>
            {
                new CreditCard {CreditCardId = 1, CreditCardNumber = "pcJf1K1THiGX2YI/51A1/Yb5o9mCfmyYKX/t9BGFUVQ=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
                new CreditCard {CreditCardId = 2, CreditCardNumber = "FwErnocXDlcnjC53M05Ep9U0tKXkmfDNfUwNzxUT4dE=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 0 },
                new CreditCard {CreditCardId = 3, CreditCardNumber = "NJsaUIdCOpB9HqYcbj0sgkHAQybyOBSkgumponiHnOI=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = false, Balance = 1000 },
                new CreditCard {CreditCardId = 4, CreditCardNumber = "oDfRcxRtYlBX86bSaJRTeepFw9BsYx2uJyJ2ZBAuuJw=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
                new CreditCard {CreditCardId = 5, CreditCardNumber = "gGH/QZ3J8V5dGa3AanmArMORe+1yrakxjNNJ/LrgTOw=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
                new CreditCard {CreditCardId = 6, CreditCardNumber = "pXzrjAP8btBaw9MhUTVCVi1W/XNT4poNJiRnsymzLyo=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = false, Balance = 0 },
                new CreditCard {CreditCardId = 7, CreditCardNumber = "okj/zc6oK1BeXbm5XiM9Tx9fDkLzO5+bXUzYbpjd3zI=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
                new CreditCard {CreditCardId = 8, CreditCardNumber = "2KCcJdm0a0VgJMHg4Nah/XIFHuZLSip/ar8Uaft1hL4=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
                new CreditCard {CreditCardId = 9, CreditCardNumber = "yGEErLit295bjrhwgPEEL2bzOZ84qq9Ln7Sm3+l2dnA=", CreditCardPin = "e3spk+2fx3Xneek5YOT0vg==", isValid = true, Balance = 1000 },
            };
            context.CreditCards.AddRange(creditCardList);
            base.Seed(context);
        }
    }
}
