using api.Configurations;
using api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

#region nfigure IOption Pattern

//Configurtion IOption Pattern
builder.Services.Configure<TitleConfiguration>(builder.Configuration.GetSection("Pages:HomePage"));


// the same but with validation 
builder.Services.AddOptions<TitleConfiguration>()
    .Bind(builder.Configuration.GetSection("Pages:HomePage"))
    .ValidateDataAnnotations()  //Options Validation using DataAnnotations
    .Validate(config =>   //Options Validation using Delegates
    {
         if (string.IsNullOrEmpty(config.WelcomeMessage) || config.WelcomeMessage.Length > 60)
             return false;
         return true;
     }, "Welcome message must be defined and it must be less than 60 characters long.");


// the same but Complex Validation Scenarios with IValidateOptions
builder.Services.AddSingleton<IValidateOptions<TitleConfiguration>, TitleConfigurationValidation>();

#endregion


//Configure Nlog and loggerService.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.ConfigureLoggerService();

//ApplicationDbContext
builder.Services.ConfigureSqlServerContext(builder.Configuration);

//Repositories
builder.Services.ConfigureRepositoryWrapper();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

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
