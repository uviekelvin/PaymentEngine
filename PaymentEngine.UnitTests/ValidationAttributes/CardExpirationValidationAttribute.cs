using NUnit.Framework;
using PaymentEngine.Api.ValidationAttributes;
using System;

namespace PaymentEngine.UnitTests.ValidationAttributes
{
    [TestFixture]
    public class CardExpirationValidationAttribute
    {

        [Test]
        public void ValidateExpiryDate_IfDateinthepast_ShoudldBeInvalid()
        {
            var validationAttribute = new CardExpirationAttribute();

            var result = validationAttribute.IsValid(DateTime.Now.AddDays(-1));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ValidateExpiryDate_IfDateinthePresent_ShoudldBeValid()
        {
            var validationAttribute = new CardExpirationAttribute();

            var result = validationAttribute.IsValid(DateTime.Now);
            Assert.That(result, Is.True);
        }


        [Test]
        public void ValidateExpiryDate_IfDateintheFuture_ShoudldBeValid()
        {
            var validationAttribute = new CardExpirationAttribute();

            var result = validationAttribute.IsValid(DateTime.Now.AddDays(1));
            Assert.That(result, Is.True);
        }
    }
}
