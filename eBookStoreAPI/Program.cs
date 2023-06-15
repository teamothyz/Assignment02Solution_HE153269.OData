using BusinessObject.Models;
using DataAccess.Intentions;
using DataAccess.Repository;
using eBookStoreAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    NullValueHandling = NullValueHandling.Ignore
};
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtKey"))),
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration["JwtIssuer"],
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers().AddOData(options => options
    .EnableQueryFeatures()
    .Select().Filter().Count().Expand().SetMaxTop(100).OrderBy()
    .AddRouteComponents("odata", GetEdmModel()));
builder.Services.AddAutoMapper(Assembly.Load("Mapper"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<EnableQueryFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Assignment02Solution.HE153269.API", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    });
});

builder.Services.AddDbContext<EBookStoreDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EBookStoreDB")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<User>("Users").EntityType.Ignore(e => e.Password);
    builder.EntitySet<Author>("Authors");
    builder.EntitySet<Book>("Books");
    builder.EntitySet<BookAuthor>("BookAuthors");
    builder.EntitySet<Publisher>("Publishers");
    builder.EntitySet<Role>("Roles");
    return builder.GetEdmModel();
}