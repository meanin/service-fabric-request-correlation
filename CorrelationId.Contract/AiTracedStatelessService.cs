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
        private readonly DependencyTrackingTelemetryModule _dependencyTrackingTelemetryModule;
        private readonly ServiceRemotingRequestTrackingTelemetryModule _serviceRemotingRequestTrackingTelemetryModule;
        private readonly ServiceRemotingDependencyTrackingTelemetryModule _serviceRemotingDependencyTrackingTelemetryModule;

        protected AiTracedStatelessService(StatelessServiceContext serviceContext) 
            : base(serviceContext)
        {
            var instrumentationKey = "58cc30ca-ead8-4b8d-9416-079cd9975b61";
            TelemetryConfiguration.Active.TelemetryInitializers.Add(
                FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(Context)
            );
            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

            _dependencyTrackingTelemetryModule = new DependencyTrackingTelemetryModule();
            _dependencyTrackingTelemetryModule.Initialize(TelemetryConfiguration.Active);
            _serviceRemotingRequestTrackingTelemetryModule = new ServiceRemotingRequestTrackingTelemetryModule();
            _serviceRemotingRequestTrackingTelemetryModule.Initialize(TelemetryConfiguration.Active);
            _serviceRemotingDependencyTrackingTelemetryModule = new ServiceRemotingDependencyTrackingTelemetryModule();
            _serviceRemotingDependencyTrackingTelemetryModule.Initialize(TelemetryConfiguration.Active);
        }
    }
}
