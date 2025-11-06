using Horus.Application.Configuration;
using Microsoft.AspNetCore.Http.Features;

namespace Horus.Api.Configurations
{
	/// <summary>
	/// Provides extension methods to register configuration-related services in the application's dependency injection container.
	/// </summary>
	public static class ConfigurationsModule
	{
		/// <summary>
		/// Registers application configuration services such as the <see cref="IExecutionContextAccessor"/>.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
		/// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
		/// <remarks>
		/// This method ensures that infrastructure-level dependencies used for accessing execution context 
		/// (e.g., request context) are properly registered.
		/// </remarks>
		public static IServiceCollection AddConfigurations(this IServiceCollection services)
		{
			services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();

			services.Configure<FormOptions>(options =>
			{
				options.MultipartBodyLengthLimit = 500_000_000; // 500 MB
			});

			return services;
		}
	}
}