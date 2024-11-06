using Business.DependencyResolvers;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using DataAccess.Contexts;
using DataAccess.DependencyResolvers;
using Microsoft.EntityFrameworkCore;
using Core.Extensions;
using AutoMapper;
using Business.AutoMapperProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Api;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Entities.Login;
using Business.Services;
using DataAccsess.Concrete.SQL_EntityFrameWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new BusinessModule(),
    new DataAccessModule(),
    new CoreModule()
});

#region AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BusinessProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion 

builder.Services.AddDbContext<EfContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

////////////////////////////////////////////////////////////////////////// Login

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,// De�i�tirildi Token Ya�am S�resini do�ruluyor e�er false b�rak�l�rsa token s�resiz hale gelmi� bir duruma giriyor
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

//////////////////////////////////////////////////////////////////////////

var app = builder.Build();



/////////////////////////////////////////////////////////////////// Create Token
// User Giri� parametreleri entities katman�na ta��nacak ve if i�ine busines'tan giri� servisi verilecek 
app.MapGet("/security/getMessage",
() => "Hello World!").RequireAuthorization();
app.MapPost("api/security/createToken",
[AllowAnonymous] async (UserLoginDto user,IUserService userService) =>
{
    var res = await userService.UserVerify(user);
    if (res !=null)
    {
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
        (builder.Configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, res.UserName),
                new Claim(JwtRegisteredClaimNames.Email, res.Mail),
                new Claim(ClaimTypes.Role, res.Role),
                new Claim(ClaimTypes.Name, res.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(2),//Token ExpiresDate verilen s�rre +5 dk olcak �ekilde hesaplan�yor e�er DateTime.UtcNow.AddMinutes(1)
            Issuer = issuer,                        //yaz�l�rsa 6 dk'l�k bir Expires date'e sahip oluyor +5 dk'n�n sebebi olu�abilecek zaman farklar�n�n �n�ne ge�mek 
            Audience = audience,
            //\\Claims = ,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return Results.Ok(stringToken);
    }
    return Results.Unauthorized();
});
/////////////////////////////////////////////////////////////////// TEST

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/////////////////   TEST
app.UseAuthentication();   /////  BUNU SONRADAN EKLED�M
/////////////////

app.UseAuthorization();

app.MapControllers();

using (var context = new EfContext())
{
    context.Database.Migrate();
}

app.Run();
