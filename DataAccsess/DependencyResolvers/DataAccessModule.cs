using Core.Utilities.IoC;
using DataAccsess.Concrete.SQL_EntityFrameWork;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers
{
    public class DataAccessModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //services.AddScoped<IGuidanceTestDal, GuidanceTestDal>();
            services.AddScoped<IBookDal, BookDal>();
            services.AddScoped<IUserDal, UserDal>();
        }
    }
}
