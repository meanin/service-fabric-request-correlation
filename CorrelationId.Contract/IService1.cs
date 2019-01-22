using System.Threading.Tasks;

namespace CorrelationId.Contract
{
    public interface IService1 : IServiceBase
    {
        Task<string> Ok();
        Task<string> Nok();
        Task<string> Service2Ok();
        Task<string> Service2Nok();
    }
}
