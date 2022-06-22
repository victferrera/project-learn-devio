using AppMercadoBasico.Data;
using AppMercadoBasico.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    var env = hostContext.HostingEnvironment;

    config.Sources.Clear();

    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json");

    config.AddEnvironmentVariables();
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AppLocalDb")));

builder.Services.AddAppServices();

var app = builder.Build();

app.AddAppUses();

app.MapControllerRoute("Default","{Controller=Home}/{Action=Index}/{id?}");

app.Run();
