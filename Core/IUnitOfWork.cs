using System.Threading.Tasks;

namespace AspNetCoreAngularApp.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}