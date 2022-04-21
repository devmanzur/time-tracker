using TimeTracker.Core.Auth.Utils;
using TimeTracker.Core.Shared.Utils;
using TimeTracker.Web.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCrossCuttingModule(builder.Configuration);
builder.Services.AddAuthenticationModule(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSpaStaticFiles(options => options.RootPath = "ClientApplication/dist");
builder.Host.AddSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionFormatter();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSpa(spa =>
{
    spa.Options.SourcePath = NuxtIntegration.SpaSource;
    if (builder.Environment.IsDevelopment())
    {
        // Launch development server for Nuxt
        spa.UseNuxtDevelopmentServer();
    }
});

app.Run();