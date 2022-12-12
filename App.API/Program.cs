using App.API.Filters;
using App.API.Middleware;
using App.Core.Configuraiton;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Repository;
using App.Repository.Repositories;
using App.Repository.UnitOfWorks;
using App.Service.Mapping;
using App.Service.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Kendi oluþturduðumuz filtrenin fluentvalidations filtresini ezmesi için kullanýlýr
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

#region DI

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImp<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericServiceImp<>));

builder.Services.AddScoped<IAuthenticationService, AuthenticationServiceImp>();
builder.Services.AddScoped<ITokenService, TokenServiceImp>();

builder.Services.AddScoped<IUserService, UserServiceImp>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImp>();

builder.Services.AddScoped<IUserRegisterHistoryService, UserRegisterHistoryServiceImp>();
builder.Services.AddScoped<IUserRegisterHistoryRepository, UserRegisterHistoryRepositoryImp>();

builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
builder.Services.AddScoped<IUserRefreshTokenService, UserRefreshTokenServiceImp>();

builder.Services.AddScoped<IEmailSettingService, EmailSettingServiceImp>();
builder.Services.AddScoped<IEmailSettingRepository, EmailSettingRepositoryImp>();

builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepositoryImp>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateServiceImp>();
#endregion

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCustomException();
app.UseAuthorization();

app.MapControllers();

app.Run();
