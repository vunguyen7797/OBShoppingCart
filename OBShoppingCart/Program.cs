using Microsoft.EntityFrameworkCore;
using NLog;
using OBShoppingCart.Data;
using OBShoppingCart.DataAccess.Repositories;
using OBShoppingCart.DataAccess.Repositories.IRepositories;
using OBShoppingCart.Middlewares;
using OBShoppingCart.Services;
using OBShoppingCart.Services.IServices;
using OBShoppingCart.Utility.Extensions;
using OBShoppingCart.Utility.Helpers;
using OBShoppingCart.Utility.LoggerService;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddIdentityServices(builder.Configuration);

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

// Custom middle for global error handling
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
