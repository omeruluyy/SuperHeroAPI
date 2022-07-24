﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero { Id = 1, Name = "Spider-Man",
                FirstName="Peter",LastName="Parker",Place="Kayseri",
                },
                new SuperHero { Id = 2, Name = "Batman",
                FirstName="Bruce",LastName="Wayne",Place="Darkham City",
                },


            };


        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero= await _context.SuperHeroes.FindAsync(request.Id);

            if (request == null)
                return BadRequest("Hero not found");
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;
           dbhero.Name = request.Name;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
            

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbhero=await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return NotFound();
            
            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


       



        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id){

            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero==null)
                return BadRequest("Hero was not found");
           
            return Ok(hero);


        }

        [HttpGet]
        public async Task <ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();  
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
