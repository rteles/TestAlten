using Alten.Booking.API.Filters;
using Alten.Booking.API.Middlewares;
using Alten.Booking.Infra.IoC.Extensions;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

if (!string.IsNullOrEmpty(environment))
    builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Filters.Add(typeof(NotificationFilter)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterOptions(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

builder.Host.ConfigureLogging(_ => _.AddSerilog());
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    var appName =  typeof(Program).Assembly.GetName().FullName;
    
    loggerConfiguration.Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
        .Enrich.FromGlobalLogContext()
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .Enrich.WithEnvironmentUserName()
        .Enrich.WithProperty("ApplicationName", appName)
        .Enrich.WithProperty("service", appName)
        .ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.Services.GetRequiredService<HotelBookingContext>().Database.EnsureCreated();
app.UseErrorHandlerMiddleware();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
