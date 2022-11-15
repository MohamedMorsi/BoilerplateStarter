using api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//Configure Nlog and loggerService.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.ConfigureLoggerService();

//ApplicationDbContext
builder.Services.ConfigureSqlServerContext(builder.Configuration);

//Repositories
builder.Services.ConfigureRepositoryWrapper();

// Add services to the container.
builder.Services.ConfigureCors(); // there is another config blow

//ConfigureIIS
builder.Services.ConfigureIISIntegration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.  //Configure Development env.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();  //Configure StaticFiles   enables using static files for the request. If we don’t set a path to the static files, it will use a wwwroot folder in our solution explorer by default.

app.UseForwardedHeaders(new ForwardedHeadersOptions   //will forward proxy headers to the current request. This will help us during the Linux deployment.
{
    ForwardedHeaders = ForwardedHeaders.All
});


app.UseCors("CorsPolicy");    //configure CORS above the UseAuthorization method

app.UseAuthorization();

app.MapControllers();

app.Run();
