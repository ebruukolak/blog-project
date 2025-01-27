using Blogs.Application.Database;
using Blogs.Application.Repositories;
using Blogs.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IPostService, PostService>();
            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(_ => new SqlDbConnectionFactory(connectionString));
            services.AddSingleton<DbInitializer>();
            return services;
        }
    }
}
