using System.Threading.Tasks;

namespace CorrelationId.Contract
{
    public interface IService2 : IServiceBase
    {
        Task<string> Ok();
        Task<string> Nok();
    }
}