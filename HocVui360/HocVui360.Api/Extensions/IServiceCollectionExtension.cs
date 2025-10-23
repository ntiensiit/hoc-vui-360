using HocVui360.Entity.Identity;
using HocVui360.Lib.Data;
using HocVui360.Lib.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HocVui360.Api.Extensions;

internal static class IServiceCollectionExtension
{
    public static IServiceCollection AddApplicationIdentity(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionStrings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        ArgumentException.ThrowIfNullOrEmpty(connectionStrings?.DefaultConnection);

        services.AddDbContext<IdentityAppDbContext>(options => options.UseSqlServer(connectionStrings.DefaultConnection));

        services.AddDataProtection();

        services
            .AddIdentityCore<IdentityUserApp>(options =>
            {
                var identitySettings = configuration.GetSection(IdentitySettings.SectionName).Get<IdentitySettings>();

                ArgumentNullException.ThrowIfNull(identitySettings);

                options.Password.RequireDigit = identitySettings.RequireDigit;
                options.Password.RequiredLength = identitySettings.RequiredLength;
                options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = identitySettings.RequireUppercase;
                options.Password.RequireLowercase = identitySettings.RequireLowercase;
                options.Password.RequiredUniqueChars = identitySettings.RequiredUniqueChars;
                options.User.RequireUniqueEmail = identitySettings.RequireUniqueEmail;
            })
            .AddRoles<IdentityRoleApp>()
            .AddRoleManager<RoleManager<IdentityRoleApp>>()
            .AddUserManager<UserManager<IdentityUserApp>>()
            .AddSignInManager<SignInManager<IdentityUserApp>>()
            .AddRoleValidator<RoleValidator<IdentityRoleApp>>()
            .AddEntityFrameworkStores<IdentityAppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<IdentityUserApp, IdentityRoleApp>>()
            .AddDefaultTokenProviders();

        return services;
    }
}
