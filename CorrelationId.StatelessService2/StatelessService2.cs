using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using CorrelationId.Contract;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace CorrelationId.StatelessService2
{
    internal sealed class StatelessService2 : AiTracedStatelessService, IService2
    {
        public StatelessService2(StatelessServiceContext context, string instrumentationKey)
            : base(context, instrumentationKey)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
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
