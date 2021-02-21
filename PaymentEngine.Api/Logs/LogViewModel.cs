using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Logs
{
    public class LogViewModel
    {

        public string HostIp { get; set; }
        public string TraceId { get; set; }

        public string ThreadId { get; set; }

        public virtual string Method { get; set; }

        public virtual DateTime RequestTime { get; set; }

        public virtual string RequestMessage { get; set; }

        public virtual DateTime ResponseTime { get; set; }

        public virtual string ResponseCode { get; set; }

        public virtual string ResponseMessage { get; set; }

        public virtual int TimeTaken
        {
            get
            {
                return (ResponseTime - RequestTime).Milliseconds;
            }
        }

        public virtual string UserAgent { get; set; }
        public string ClientIp { get; set; }
        public string RequestHeaders { get; set; }
        public string ResponseHeaders { get; set; }
        public string Direction { get; set; }
        public string User { get; set; }
        public string Uri { get; set; }
        public LogTypes LogType { get; set; }

    }
    public enum LogTypes
    {
        Information = 1,
        Error = 2
    }
}
