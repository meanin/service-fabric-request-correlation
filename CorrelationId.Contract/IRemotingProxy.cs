using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting;

namespace CorrelationId.Contract
{
    public interface IRemotingProxy
    {
        TServiceInterface Create<TServiceInterface>(Uri serviceUri, ServicePartitionKey partitionKey = null,
            TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default, string listenerName = null)
            where TServiceInterface : IService;
    }
}
