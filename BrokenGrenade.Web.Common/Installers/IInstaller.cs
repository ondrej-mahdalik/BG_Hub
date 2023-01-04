
using Microsoft.Extensions.DependencyInjection;
namespace BrokenGrenade.Web.Common.Installers;

public interface IInstaller
{
    void Install(IServiceCollection serviceCollection);
}
