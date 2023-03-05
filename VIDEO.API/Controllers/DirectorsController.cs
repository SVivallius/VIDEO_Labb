using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VIDEO.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDbService _db;
        public DirectorsController(IDbService service)
        {
            _db = service;
        }
        // GET: api/<DirectorsController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAllAsync<Director, DirectorDTO>());

        // GET api/<DirectorsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            _db.Include<Film>();
            var entity = await _db.GetByIdAsync<Director, DirectorDTO>(e => e.Id == id);
            if (entity == null) return Results.NotFound();
            return Results.Ok(entity);
        }

        // POST api/<DirectorsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] DirectorDTO dto)
        {
            try
            {
                var entity = await _db.CreateAsync<Director, DirectorDTO>(dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Director).Name.ToLower();
                    return Results.Created($"/{node}/{entity.Id}", entity);
                }
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
            return Results.BadRequest();
        }

        // PUT api/<DirectorsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
        {
            try
            {
                if (!await _db.AnyAsync<Director>(e => e.Id == id)) return Results.NotFound();

                _db.Update<Director, DirectorDTO>(id, dto);
                if (await _db.SaveChangesAsync()) return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
            return Results.BadRequest();
        }

        // DELETE api/<DirectorsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Director, DirectorDTO>(id)) return Results.NotFound();
                if (await _db.SaveChangesAsync()) return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
            return Results.BadRequest();
        }
    }
}
