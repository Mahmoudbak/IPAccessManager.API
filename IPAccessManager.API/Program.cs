using IPAccessManager.API.Extensions;
using IPAccessManager.API.middlewares;
using IPAccessManager.Services.TemporalBlockCleanupService;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddApplicationServices();
builder.Services.AddHostedService<TemporalBlockCleanupService>();


builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();


app.UseStatusCodePagesWithReExecute("/Errors/{0}");
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint(url: "/openapi/v1.json", name: "Country"));
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
