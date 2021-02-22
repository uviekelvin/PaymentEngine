using NUnit.Framework;
using PaymentEngine.Api.ValidationAttributes;
namespace PaymentEngine.UnitTests.ValidationAttributes
{
    [TestFixture]
    public class CreditCardValidationAttributeTest
    {


        [Test]
        public void CreditCardValidation_IfInvalidCreditCardNumber_ShouldbeInvalid()
        {
            var creditCardValidation = new CreditCardValidationAttribute();
            var result = creditCardValidation.IsValid("123456789000000000000");
            Assert.That(result, Is.False);
        }
        [Test]
        [TestCase("4192270016128109")]
        [TestCase("5399412001717771")]
        public void CreditCardValidation_IfValidCreditCardNumber_ShouldbeValid(string cardNumber)
        {
            var creditCardValidation = new CreditCardValidationAttribute();
            var result = creditCardValidation.IsValid(cardNumber);
            Assert.That(result, Is.True);
        }
    }
}
