using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using CorrelationId.Contract;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace CorrelationId.StatelessService2
{
    internal sealed class StatelessService2 : StatelessService, IService2
    {
        public StatelessService2(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            FabricTelemetryInitializerExtension.SetServiceCallContext(Context);
            FabricTelemetryInitializerExtension.CreateFabricTelemetryInitializer(Context);
            return this.CreateServiceRemotingInstanceListeners();
        }

        public Task<string> Ok()
        {
            return Task.FromResult("Hi from stateless service 2");
        }

        public Task<string> Nok()
        {
            throw new Exception("Not ok from service 2");
        }
    }
}
