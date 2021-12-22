using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _dataContext;
        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
          
            return Ok( await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero){
             await _dataContext.SuperHeroes.AddAsync(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSpacificHero(int id)
        {

           var chosenHero =   await _dataContext.SuperHeroes.FindAsync(id);
            if (chosenHero is null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                return Ok(chosenHero);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(request.Id);
            if (hero is null)
            {
                return BadRequest("Hero Not Found");
            }
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
          
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync( id);
            if(hero is null)
            {
                return BadRequest("Hero Not Found");
            }
             _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
            
    }
}
