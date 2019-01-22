using Autofac;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.ServiceFabric.Module;

namespace CorrelationId.Contract
{
    public class StatelessServiceModuleBase : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceRemotingDependencyTrackingTelemetryModule>().As<ITelemetryModule>().SingleInstance();
            builder.RegisterType<ServiceRemotingRequestTrackingTelemetryModule>().As<ITelemetryModule>().SingleInstance();
            builder.Register(c => new TelemetryClient { InstrumentationKey = "58cc30ca-ead8-4b8d-9416-079cd9975b61" }).SingleInstance();
            base.Load(builder);
        }
    }
}
