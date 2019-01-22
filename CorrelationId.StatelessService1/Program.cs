using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using CorrelationId.Contract;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.StatelessService1
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<StatelessService1Module>();
                var container = builder.Build();

                ServiceRuntime.RegisterServiceAsync("CorrelationId.StatelessService1Type",
                    context =>
                    {
                        FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(context);
                        return new StatelessService1(context, container.Resolve<IService2>());
                    }).GetAwaiter().GetResult();

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
