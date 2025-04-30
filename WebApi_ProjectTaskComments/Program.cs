using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi_ProjectTaskComments.Extensions;
using WebApi_ProjectTaskComments.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

// „тобы указывать enum  в форме строки
// Ћибо над свойством ставим аттрибут[JsonConverter(typeof(JsonStringEnumConverter))]
//≈сли все же определ€ем глобально, что все enum- строки, то потом,
//чтобы один какой-нибудь enum сделать числами, потребуетс€ это 
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

//    options.JsonSerializerOptions.DefaultIgnoreCondition =
//        JsonIgnoreCondition.WhenWritingNull;
//});
//Because EF Core automatically does fix-up of navigation properties,
//you can end up with cycles in your object graph. For example,
//loading a blog and its related posts will result in a blog object that references
//a collection of posts. Each of those posts will have a reference back to the blog.
//https://stackoverflow.com/questions/78099397/how-to-solve-json-exception-a-possible-object-cycle-was-detected-in-net-core
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddApplicationServices();


var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());//.WithHeaders("authorization", "accept", "content-type", "origin"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
