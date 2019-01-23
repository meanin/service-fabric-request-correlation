using System.Fabric;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.ApplicationInsights.ServiceFabric.Module;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.Contract
{
    public abstract class AiTracedStatelessService : StatelessService
    {
        protected AiTracedStatelessService(StatelessServiceContext serviceContext, string instrumentationKey) 
            : base(serviceContext)
        {
            TelemetryConfiguration.Active.TelemetryInitializers.Add(
                FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(Context)
            );
            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

            new DependencyTrackingTelemetryModule().Initialize(TelemetryConfiguration.Active);
            new ServiceRemotingRequestTrackingTelemetryModule().Initialize(TelemetryConfiguration.Active);
            new ServiceRemotingDependencyTrackingTelemetryModule().Initialize(TelemetryConfiguration.Active);
        }
    }
}
