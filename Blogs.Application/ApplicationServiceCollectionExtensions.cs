using Blogs.Application.Database;
using Blogs.Application.Repositories;
using Blogs.Application.Services.Auth;
using Blogs.Application.Services.Categories;
using Blogs.Application.Services.Email;
using Blogs.Application.Services.Posts;
using Blogs.Application.Services.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<JwtTokenService>();
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
