using Hangfire;
using Hangfire.MemoryStorage;
using Integrador.Authentication.Process;
using Integrador.Authentication.Sink;
using Integrador.Dashboard;
using Integrador.User.Process;
using Integrador.User.Sink;
using Integrador.User.Source;
using Integrador.User.Transform;
using Integrador.Authentication.Source;
using Integrador.Authentication.Transform;
using Integrador.SigFapes.Edital.Process;
using Integrador.SigFapes.Edital.Sink;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config => config.UseMemoryStorage());

builder.Services.AddHangfireServer();
builder.Services.AddControllers();

// Registrar HttpClient como um serviço
builder.Services.AddHttpClient<UserSource>();

// Registra as classes Transform e Sink como serviços
builder.Services.AddSingleton<UserTransformer>();
builder.Services.AddSingleton<ConsoleSink>();

//Autenticacao
builder.Services.AddScoped<IAuthenticationSource, SigFapesSource>();
builder.Services.AddScoped<IAuthenticationTransform, HttpAuthenticationTransform>();
builder.Services.AddScoped<IAuthenticationSink, MemoryAuthenticationSink>();


//Editais
builder.Services.AddScoped<EditalDetailProcess>();
builder.Services.AddScoped<IEditalSource, AuthenticationEditalSource>();
builder.Services.AddScoped<IEditalTransform, HttpEditalTransform>();
builder.Services.AddScoped<IEditalSink, EditalSink>();
builder.Services.AddScoped<EditalProcess>();

//Edital Detail
builder.Services.AddScoped<EditalDetailProcessCreator>();
builder.Services.AddScoped<IEditalDetailSink, EditalDetailSink>();
builder.Services.AddScoped<IEditalDetailTransform, HttpEditalDetailTransform>();



var app = builder.Build();


app.UseHangfireDashboard("", new DashboardOptions
{
    Authorization = new[] { new DashboardNoAuthorizationFilter() }
});


app.UseHangfireServer();

var serviceProvider = app.Services;
//RecurringJob.AddOrUpdate<ProcessFromAPIToConsole>(service => service.Execute(), Cron.Minutely);
RecurringJob.AddOrUpdate<AuthenticationProcess>(service => service.Execute(), Cron.Minutely);

app.Run();