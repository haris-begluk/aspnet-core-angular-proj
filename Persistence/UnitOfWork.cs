using System.Threading.Tasks;
using AspNetCoreAngularApp.Core;
namespace AspNetCoreAngularApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext context){
        this.context = context;

        }
        public async Task CompleteAsync()
        {
           await context.SaveChangesAsync();
        }
    }
}