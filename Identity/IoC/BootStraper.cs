using Data.Unit;
using Identity.Application;
using Identity.Application.Interfaces;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Repository;
using Identity.Domain.Interfaces.UnitOfWork;
using Identity.Domain.Services;
using Identity.Infra.Data.Context;
using Identity.Infra.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infra.IoC;

public static class BootStraper
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterDbContext(services, configuration);
        RegisterUnitOfWork(services);
        RegisterApplication(services);
        RegisterRepositories(services);
        RegisterServices(services);
        RegisterIdentity(services);
        
        return services;
    }

    public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options => 
            options.UseMySql(configuration.GetConnectionString("Way2CommerceConnection"), 
                new MySqlServerVersion(new Version(8, 0, 21))));
    }

    private static void RegisterApplication(IServiceCollection services)
    {
       services.AddScoped<IIdentityApplication, IdentityApplication>();
    }

    public static void RegisterRepositories(IServiceCollection services)
    {
       services.AddScoped<IRoleClaimRepositoryAsync, RoleClaimRepositoryAsync>();
       services.AddScoped<IRoleRepositoryAsync, RoleRepositoryAsync>();
       services.AddScoped<IUserClaimRepositoryAsync, UserClaimRepositoryAsync>();
       services.AddScoped<IUserLoginRepositoryAsync, UserLoginRepositoryAsync>();
       services.AddScoped<IUserRepositoryAsync, UserRepositoryAsync>();
       services.AddScoped<IUserRoleRepositoryAsync, UserRoleRepositoryAsync>();
       services.AddScoped<IUserTokenRepositoryAsync, UserTokenRepositoryAsync>();
    }
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();            
    }
    
    public static void RegisterUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();            
    }
    
    public static void RegisterIdentity(IServiceCollection services)
    {
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();       
    }
    
}