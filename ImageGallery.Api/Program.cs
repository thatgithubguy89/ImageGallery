using ImageGallery.Api.Data;
using ImageGallery.Api.Extensions;
using ImageGallery.Api.Models.Identity;
using ImageGallery.Api.Profiles;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ImageGalleryDbContext>(options => options.UseSqlServer(builder.Configuration["ImageGalleryConnectionString"]));
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<ImageGalleryDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRepositories();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<AppUser>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
