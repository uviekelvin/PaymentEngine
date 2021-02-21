using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Domain.Exceptions
{
    public class PaymentEngineException : Exception
    {
        public PaymentEngineException(string message)
      : base(message)
        { }

        public PaymentEngineException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
