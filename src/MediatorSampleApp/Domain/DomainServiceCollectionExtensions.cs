using System.Linq;
using Domain.Behaviors;
using FluentValidation;
using MediatR;
using MediatR.Registration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            var assembly = typeof(DomainServiceCollectionExtensions).Assembly;
            ServiceRegistrar.AddMediatRClasses(services, new[] { assembly });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationPreProcessor<,>));
            foreach (var validatorImplType in assembly.DefinedTypes.Where(t => !t.IsAbstract && !t.IsInterface))
            {
                foreach (var validatorServiceType in validatorImplType.FindInterfacesThatClose(typeof(IValidator<>)))
                {
                    services.AddTransient(validatorServiceType, validatorImplType);
                }
            }

            return services;
        }
    }
}
