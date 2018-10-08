using System.Threading.Tasks;

namespace AspNetCoreAngularApp.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}