using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.ServiceInstallers
{
    public interface IDataInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
