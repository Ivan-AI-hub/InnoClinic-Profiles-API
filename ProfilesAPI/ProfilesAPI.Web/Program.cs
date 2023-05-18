using FluentValidation;
using ProfilesAPI.Presentation.Controllers;
using ProfilesAPI.Services.Mappings;
using ProfilesAPI.Services.Settings;
using ProfilesAPI.Services.Validators;
using ProfilesAPI.Web.Extensions;
using ProfilesAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureServices();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(DoctorController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(ServiceMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();


builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("BlobStorageConfig"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();