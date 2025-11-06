using Horus.Domain.SeedWork.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Horus.Infrastructure.Database
{

	public static class DataAccessModule
	{
		public static IServiceCollection AddDataAccessModule(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}

}