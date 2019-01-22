using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using CorrelationId.Contract;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.StatelessService2
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<StatelessServiceModuleBase>();
                var container = builder.Build();

                ServiceRuntime.RegisterServiceAsync("CorrelationId.StatelessService2Type",
                    context =>
                    {
                        FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(context);
                        return new StatelessService2(context);
                    }).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(StatelessService2).Name);
                GC.KeepAlive(container);
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
