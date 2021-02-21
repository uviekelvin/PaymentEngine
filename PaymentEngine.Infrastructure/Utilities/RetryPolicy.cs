using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentEngine.Infrastructure.Utilities
{
    public static class RetryPolicy
    {
        public static AsyncPolicy GetPolicyAsync(int retryCount, int incrementalCount)
        {
            return Policy
                     .Handle<Exception>()
                     .WaitAndRetryAsync(retryCount, retryAttempt =>
                     {
                         var timeToWait = TimeSpan.FromSeconds(retryAttempt * incrementalCount);
                         Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds..");
                         return timeToWait;
                     },
                       onRetry: (exception, pollyRetryCount, context) =>
                       {
                           Console.WriteLine($"An exception occured at {exception.Source} with message {exception.Message}");
                       })
                     ;
        }
    }
}
