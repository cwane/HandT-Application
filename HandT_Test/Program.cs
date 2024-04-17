using HandT_Api_Layer.DomainInterface;
using HandT_Api_Layer.DomainRepository;
using HandT_Test.Authentication;
using HandT_Test.DbContext;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_Mysql.DomainRepository;
using HandT_Test_PG.Authentication;
using HandT_Test_PG.DbContext;
using HandT_Test_PG.DomainInterface;
using HandT_Test_PG.DomainRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
//for fetchng appsettings.json jwit configs
var jwt_settings = builder.Configuration.GetSection("JWT").Get<ConfigJwt>();

// Add sbearer jwt token service
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(
//        options =>
//        {
//            options.SaveToken = true;
//            options.RequireHttpsMetadata = false;
//            options.TokenValidationParameters = new TokenValidationParameters()
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidAudience = jwt_settings.ValidAudience,
//                ValidIssuer = jwt_settings.ValidIssuer,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_settings.Secret))
//            };
//        }
//    );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:JWTToken").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


//cors
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

//db context for aspnetcore identity using entityframework
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authorization with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{{new OpenApiSecurityScheme{Reference = new OpenApiReference
    {Id = "Bearer",Type = ReferenceType.SecurityScheme,}},new List<string>()}});
});

//add identity params
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

//dapper mysql connection
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IAuthenticationRepo, AuthenticationRepo>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICommonRepo, CommonRepo>();
builder.Services.AddScoped<IProfileRepo, ProfileRepo>();
builder.Services.AddScoped<IFrontRepo,FrontRepo>();
builder.Services.AddScoped<IGoogleMapRepo, GoogleMapRepo>();


//serilog config
//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.FromLogContext()
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

//var logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .WriteTo.File("../logs/log.txt")
//    .WriteTo.Console()
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

//serilog

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
