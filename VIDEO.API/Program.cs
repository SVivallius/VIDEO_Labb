

using VIDEO.common.DTOs;
using VIDEO.Membership.data.Entities;
using VIDEO.Membership.data.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VideoDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("VIDEOConnection")));

builder.Services.AddScoped<IDbService, DbService>();

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<Film, FilmDTO>()
        .ReverseMap()
        .ForMember(dest => dest.Director, dest => dest.Ignore());
    cfg.CreateMap<Genre, GenreDTO>()
        .ReverseMap();
    cfg.CreateMap<Director, DirectorDTO>()
        .ReverseMap();
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});

builder.Services.AddControllers();

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

app.UseCors("CorsAllAccessPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
