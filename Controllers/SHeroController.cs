using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//controller

namespace SuperHero.Controllers
{
    //router da page
    [Route("[controller]")]
    [ApiController]
    public class SHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SHeroController(DataContext context)
        {
            this.context = context;
        }

        //get method
        [HttpGet]
        public async Task<ActionResult<List<SHero>>> Get()
        {
            //response http 200
            return Ok(await this.context.SHeroes.ToListAsync());
        }

        //get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<SHero>> Get(int id)
        {
            // salvar na variavel o resultado da busca por id
            var hero = await this.context.SHeroes.FindAsync(id);
            //caso id seja null, retorna um badrequest
            if( hero == null)
                return BadRequest("Hero not Found!");

            //response http 200
            //retorna o dado salvo da variavel
            return Ok(hero);
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<List<SHero>>> Post(SHero hero)
        {
            //add new element to the database
            this.context.SHeroes.Add(hero);
            await this.context.SaveChangesAsync();
            //response http 200
            return Ok(await this.context.SHeroes.ToListAsync());
        }

        //put
        [HttpPut]
        public async Task<ActionResult<List<SHero>>> Put(SHero heroPut)
        {
            // salvar na variavel o resultado da busca por id
            var dbHero = await this.context.SHeroes.FindAsync(heroPut);
            //caso id seja null, retorna um badrequest
            if( dbHero == null)
                return BadRequest("Hero not Found!");

            //salvar a mudanca de dados
            dbHero.Name = heroPut.Name;
            dbHero.FirstName = heroPut.FirstName;
            dbHero.LastName = heroPut.LastName;
            dbHero.Place = heroPut.Place;

            await this.context.SaveChangesAsync();

            //response http 200
            return Ok(await this.context.SHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SHero>>> Delete(int id)
        {
            // salvar na variavel o resultado da busca por id
            var dbHero = await this.context.SHeroes.FindAsync(id);
            //caso id seja null, retorna um badrequest
            if( dbHero == null)
                return BadRequest("Hero not Found!");

            //response http 200
            //retorna o dado salvo da variavel
            this.context.SHeroes.Remove(dbHero);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.SHeroes.ToListAsync());
        }

    }
}