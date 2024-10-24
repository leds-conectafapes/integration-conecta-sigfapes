using Hangfire;
using Hangfire.MemoryStorage;
using Integrador.Dashboard;
using Integrador.User.Process;
using Integrador.User.Sink;
using Integrador.User.Source;
using Integrador.User.Transform;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddControllers();

// Registrar HttpClient como um serviço
builder.Services.AddHttpClient<UserSource>();

// Registra as classes Transform e Sink como serviços
builder.Services.AddSingleton<UserTransformer>();
builder.Services.AddSingleton<ConsoleSink>();

var app = builder.Build();


app.UseHangfireDashboard("", new DashboardOptions
{
    Authorization = new[] { new DashboardNoAuthorizationFilter() }
});


app.UseHangfireServer();

var serviceProvider = app.Services;
RecurringJob.AddOrUpdate<ProcessFromAPIToConsole>(service => service.Execute(), Cron.Minutely);

app.Run();