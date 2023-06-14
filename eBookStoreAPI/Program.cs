using BusinessObject.Models;
using DataAccess.Intentions;
using DataAccess.Repository;
using eBookStoreAPI.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(options => options
    .EnableQueryFeatures()
    .Select().Filter().Count().Expand().SetMaxTop(100).OrderBy()
    .AddRouteComponents("odata", GetEdmModel()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<EnableQueryFilter>();
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