using System.Text.Json.Serialization;
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
        .ForMember(
            dest => dest.GenreIds,
            opt => opt.MapFrom(
                src => src.Genres.Select(
                    g => g.Id)))
        .ForMember(
            dest => dest.DirectorId,
            opt => opt.MapFrom(
                src => src.DirectorId))
        .ReverseMap();
    cfg.CreateMap<Genre, GenreDTO>()
        .ForMember(
            dest => dest.FilmIds,
            opt => opt.MapFrom(
                src => src.Films.Select(
                    g => g.Id)))
        .ReverseMap();

    cfg.CreateMap<Director, DirectorDTO>()
        .ForMember(
            dest => dest.FilmIds,
            opt => opt.MapFrom(
                src => src.Films.Select(
                    f => f.Id)))
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

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

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
