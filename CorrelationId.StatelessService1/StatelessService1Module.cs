using System;
using Autofac;
using CorrelationId.Contract;

namespace CorrelationId.StatelessService1
{
    public class StatelessService1Module : StatelessServiceModuleBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new RemotingProxy().Create<IService2>(
                new Uri("fabric:/CorrelationId/CorrelationId.StatelessService2"))).As<IService2>();
            base.Load(builder);
        }
    }
}
