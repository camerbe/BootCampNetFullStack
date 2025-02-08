using BootCampDAL;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.Mappings;
using BootCampNetFullStack.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    //.WriteTo.Console()
    .WriteTo.File("Logs/BootCamp_log.log", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateBootstrapLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;

});
builder.Services.AddDbContext<BootCampDalContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Adding Identity services
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<Guid>>()
    //.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Dal")
    .AddEntityFrameworkStores<BootCampDalContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(q =>
{
    q.User.RequireUniqueEmail = false;
    q.Password.RequireNonAlphanumeric = false;
    q.Password.RequireLowercase = false;
    q.Password.RequireUppercase = false;
    q.Password.RequiredLength = 8;
    q.SignIn.RequireConfirmedEmail = true;


});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddAutoMapper<AutoMapperProfile>();

//Add Authentication
builder.Services.AddAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Seed the database Roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var context = services.GetRequiredService<BootCampDalContext>();
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRoleAsync(roleManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
