using HocVui360.Lib.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace HocVui360.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected class BaseControllerParameter(IServiceProvider serviceProvider)
    {
        private readonly ConcurrentDictionary<Type, object> _serviceCache = new();
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private TService Service<TService>() where TService : notnull
        {
            return (TService)_serviceCache.GetOrAdd(typeof(TService), type => _serviceProvider.GetRequiredService<TService>());
        }

        public IdentityAppDbContext IdentityAppDbContext => Service<IdentityAppDbContext>();
        public required ILogger Logger { get; init; }
    }

    protected BaseControllerParameter _parameter => new(HttpContext.RequestServices)
    {
        Logger = HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(GetType())
    };
}
