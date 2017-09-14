using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Upnoid.Core.Abstracts;
using Upnoid.Core.Data;
using Upnoid.Core.Repositories;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Configurations
{
    public static class ServiceCollectionExtentions
    {

        public static IServiceCollection AddEntityFramework(this IServiceCollection services)
        {
            services.AddDbContext<UpnoidContext>(options=>
            options.UseSqlServer(DatabaseSettings.LocalConnection));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UpnoidContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITrailerRepository, TrailerRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            return services;
        }
    }
}
