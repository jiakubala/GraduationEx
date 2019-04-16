using Graduation.Controllers;
using Graduation.Managers;
using Graduation.Models;
using Graduation.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static UserDefinedBuilder AddUserDefined(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ILoginStore, LoginStore<UnifiedDbContext>>();
            services.AddScoped<IIndexStore, IndexStore<UnifiedDbContext>>();
            services.AddScoped<IOrderStore, OrderStore<UnifiedDbContext>>();
            services.AddScoped<IUserStore, UserStore<UnifiedDbContext>>();
            services.AddScoped<LoginManager>();
            services.AddScoped<IndexManager>();
            services.AddScoped<OrderManager>();
            services.AddScoped<UserManager>();

            return new UserDefinedBuilder(services);
        }
    }
}

