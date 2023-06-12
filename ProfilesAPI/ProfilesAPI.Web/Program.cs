using FluentValidation;
using ProfilesAPI.Application.Mappings;
using ProfilesAPI.Application.Validators;
using ProfilesAPI.Presentation.Controllers;
using ProfilesAPI.Web.Extensions;
using ProfilesAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");
builder.Services.ConfigureMassTransit(builder.Configuration, "MassTransitSettings");

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureServices();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
    .AddApplicationPart(typeof(DoctorController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(ServiceMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientValidator>();

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
