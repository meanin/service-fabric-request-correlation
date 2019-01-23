using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using CorrelationId.Contract;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace CorrelationId.StatelessService1
{
    internal sealed class StatelessService1 : AiTracedStatelessService, IService1
    {
        private readonly IService2 _service2;

        public StatelessService1(
            StatelessServiceContext context, 
            string instrumentationKey, 
            IService2 service2)
            : base(context, instrumentationKey)
        {
            _service2 = service2;
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        public Task<string> Ok()
        {
            return Task.FromResult("Hi from stateless service 1");
        }

        public Task<string> Nok()
        {
            throw new Exception("Not ok from service 1");
        }

        public async Task<string> Service2Ok()
        {
            return $"Service 1 got from s2: {await _service2.Ok()}";
        }

        public async Task<string> Service2Nok()
        {
            return $"Service 1 got from s2: {await _service2.Nok()}";
        }
    }
}
