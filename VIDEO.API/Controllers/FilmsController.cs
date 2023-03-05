using Microsoft.AspNetCore.Mvc;
using VIDEO.common.DTOs;
using VIDEO.Membership.data.Entities;
using VIDEO.Membership.data.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VIDEO.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmsController : ControllerBase
{
    private readonly IDbService _db;

    public FilmsController(IDbService service)
    {
        _db = service;
    }

    // GET: api/<FilmsController>
    [HttpGet]
    public async Task<IResult> Get(bool freeOnly)
    {
        try
        {
            _db.Include<Director>();
            _db.Include<Genre>();

            var entities = freeOnly ?
                await _db.GetFilteredAsync<Film, FilmDTO>(e => e.free == freeOnly) :
                await _db.GetAllAsync<Film, FilmDTO>();
            if (entities == null) return Results.BadRequest();
            return Results.Ok(entities);
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
    }

    // GET api/<FilmsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id, bool freeOnly)
    {
        _db.Include<Genre>();
        _db.Include<Director>();
        var entity = await _db.GetByIdAsync<Film, FilmDTO>(e => e.Id == id);

        if (entity == null) return Results.NotFound(id);
        if (entity.free == freeOnly) return Results.Unauthorized();

        return Results.Ok(entity);
    }

    // POST api/<FilmsController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] FilmDTO Dto)
    {
        try
        {
            var entity = await _db.CreateAsync<Film, FilmDTO>(Dto);
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

    // PUT api/<FilmsController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] FilmDTO dto)
    {
        try
        {
            if (!await _db.AnyAsync<Film>(e => e.Id == id)) return Results.NotFound();

            _db.Update<Film, FilmDTO>(id, dto);
            if (await _db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
        return Results.BadRequest();
    }

    // DELETE api/<FilmsController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if (!await _db.DeleteAsync<Film, FilmDTO>(id)) return Results.NotFound();
            if (await _db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
        return Results.BadRequest();
    }
}
