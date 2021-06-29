namespace TimeSheetApproval.WebApi.Extensions.Cors
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfigs = configuration.GetSection("corsPolicies").Get<IEnumerable<CorsPolicyConfig>>();
            if (corsConfigs != null)
            {
                services.AddCors(corsOptions =>
                {
                    foreach (var corsConfig in corsConfigs)
                    {
                        corsOptions.AddPolicy(corsConfig.Name, corsConfig);
                    }
                });
            }

            return services;
        }
    }
}
