using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Dtos
{
    public class AppSettingsDtos
    {
        public string LogUrl { get; set; }
        public string SensitiveDataKeys { get; set; }
        public string SensitiveDataDefaultValues { get; set; }
    }
}
