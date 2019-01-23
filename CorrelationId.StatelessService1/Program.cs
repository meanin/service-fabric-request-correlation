using System;
using System.Diagnostics;
using System.Threading;
using CorrelationId.Contract;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.StatelessService1
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var instrumentationKey = "58cc30ca-ead8-4b8d-9416-079cd9975b61";
                ServiceRuntime.RegisterServiceAsync("CorrelationId.StatelessService1Type",
                    context => new StatelessService1(context, instrumentationKey, new RemotingProxy().Create<IService2>(
                        new Uri("fabric:/CorrelationId/CorrelationId.StatelessService2")))).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(StatelessService1).Name);
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
