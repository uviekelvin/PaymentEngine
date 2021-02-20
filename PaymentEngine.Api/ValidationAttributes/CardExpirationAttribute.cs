using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.ValidationAttributes
{
    public class CardExpirationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.Date >= DateTime.Now.Date;
            }

            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return "Invalid expiration date";
        }
    }
}
