using BootCampDAL;
using BootCampDAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BootCampDalContext>(options=>options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//Add Authentication
builder.Services.AddAuthentication();
builder.Services.AddIdentityCore<User>(q =>
    {
        q.User.RequireUniqueEmail = false;
        q.Password.RequiredLength = 8;
        q.Password.RequireNonAlphanumeric = false;
        q.Password.RequireLowercase = false;
        q.Password.RequireUppercase = false;
        q.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<BootCampDalContext>()
        .AddSignInManager<SignInManager<User>>()
        .AddUserManager<UserManager<User>>()
        .AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
