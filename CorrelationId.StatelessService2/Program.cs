using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.StatelessService2
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var instrumentationKey = "58cc30ca-ead8-4b8d-9416-079cd9975b61";
                ServiceRuntime.RegisterServiceAsync("CorrelationId.StatelessService2Type",
                    context => new StatelessService2(context, instrumentationKey)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(StatelessService2).Name);
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
