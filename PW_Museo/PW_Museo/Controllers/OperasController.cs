using Microsoft.AspNetCore.Mvc;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperasController : ControllerBase
    {
        private readonly OperasRepository _repository;

        public OperasController(OperasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperas()
        {
            var operas = await _repository.GetAllAsync();
            return Ok(operas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOpera(Guid id)
        {
            var opera = await _repository.GetByIdAsync(id);
            if (opera == null)
                return NotFound();
            return Ok(opera);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpera([FromBody] Opera opera)
        {
            opera.Id = Guid.NewGuid();
            await _repository.CreateAsync(opera);
            return CreatedAtAction(nameof(GetOpera), new { id = opera.Id }, opera);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpera(Guid id, [FromBody] Opera opera)
        {
            if (id != opera.Id)
                return BadRequest();

            var existingOpera = await _repository.GetByIdAsync(id);
            if (existingOpera == null)
                return NotFound();

            await _repository.UpdateAsync(opera);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpera(Guid id)
        {
            var existingOpera = await _repository.GetByIdAsync(id);
            if (existingOpera == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
