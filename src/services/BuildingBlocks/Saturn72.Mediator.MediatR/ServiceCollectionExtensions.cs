using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Saturn72.CommandMediator.MediatR
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatR(this IServiceCollection serviceCollection, Assembly assembly)
        {
            serviceCollection.AddMediatR(assembly);
        }
    }
}
