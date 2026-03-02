using API_Assignment.Data;
using API_Assignment.Services;
using API_Assignment.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Assignment",
        Version = "v1",
        Description = "API for managing exceptions, attendance, and loans"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token.\r\n\r\n" +
                      "Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<UOW>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IExceptionService, ExceptionService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = "name")
                .AddJwtBearer("name",
                option =>
                {
                    string secretKey = "welcome to my world where you can convert your dreams into reality";
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
