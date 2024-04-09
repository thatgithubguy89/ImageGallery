using ImageGallery.Api.Data;
using ImageGallery.Api.Models.Identity;
using ImageGallery.Api.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ImageGalleryDbContext>(options => options.UseSqlServer(builder.Configuration["ImageGalleryConnectionString"]));
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<ImageGalleryDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
