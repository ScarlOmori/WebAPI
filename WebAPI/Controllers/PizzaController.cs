using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {

        }

        
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll;
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id) 
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            return pizza;
        }

        [HttpPost]
        public ActionResult Create(Pizza pizza) 
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }

        [HttpPost("{id}")]
        public IActionResult Update(Pizza pizza, int id) 
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }
    }
}
