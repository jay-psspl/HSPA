using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Repository;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using WebAPI.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = key
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(
        options => {
            options.Run(
                async context => 
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var ex = context.Features.Get<IExceptionHandlerFeature>();

                    if(ex != null)
                    {
                        await context.Response.WriteAsync(ex.Error.Message); 
                    }
                }
            );
        }
    );
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseHsts();

app.UseHttpsRedirection();

app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
