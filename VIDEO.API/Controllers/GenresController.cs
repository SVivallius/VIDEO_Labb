using Microsoft.AspNetCore.Mvc;
using VIDEO.common.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VIDEO.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IDbService _db;
    public GenresController(IDbService db)
    {
        _db = db;
    }

    // GET: api/<GenresController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        _db.Include<Film>();

        var entities = await _db.GetAllAsync<Genre, GenreDTO>();
        if (entities == null) return Results.BadRequest();
        return Results.Ok(entities);
    }

    // GET api/<GenresController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var entity = await _db.GetByIdAsync<Genre, GenreDTO>(e => e.Id == id);
        if (entity == null) return Results.NotFound();
        return Results.Ok(entity);
    }

    // POST api/<GenresController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] GenreDTO Dto)
    {
        try
        {
            var entity = await _db.CreateAsync<Genre, GenreDTO>(Dto);
            if (await _db.SaveChangesAsync())
            {
                var node = typeof(Film).Name.ToLower();
                return Results.Created($"/{node}/{entity.Id}", entity);
            }
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
        return Results.BadRequest();
    }

    // PUT api/<GenresController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] GenreDTO Dto)
    {
        try
        {
            if (!await _db.AnyAsync<Genre>(e => e.Id == id)) return Results.NotFound();

            _db.Update<Genre, GenreDTO>(id, Dto);
            if (await _db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
        return Results.BadRequest();
    }

    // DELETE api/<GenresController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if (!await _db.DeleteAsync<Genre, GenreDTO>(id)) return Results.NotFound();
            if (await _db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
        return Results.BadRequest();
    }
}
