using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SendMail.EfCore;

public static class Extensions
{
    public static IServiceCollection AddEfCore(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> action)
    {
        services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<SendMailContext>>().CreateDbContext());

        services.AddDbContextFactory<SendMailContext>((sp, options) =>
        {
            action?.Invoke(sp, options);
        });

        return services;
    }
}
