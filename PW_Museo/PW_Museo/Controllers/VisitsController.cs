using Microsoft.AspNetCore.Mvc;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly VisitsRepository _repository;

        public VisitsController(VisitsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetVisits()
        {
            var visits = await _repository.GetAllAsync();
            return Ok(visits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisit(Guid id)
        {
            var visit = await _repository.GetByIdAsync(id);
            if (visit == null)
                return NotFound();
            return Ok(visit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisit([FromBody] Visit visit)
        {
            visit.Id = Guid.NewGuid();
            await _repository.CreateAsync(visit);
            return CreatedAtAction(nameof(GetVisit), new { id = visit.Id }, visit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisit(Guid id, [FromBody] Visit visit)
        {
            if (id != visit.Id)
                return BadRequest();

            var existingVisit = await _repository.GetByIdAsync(id);
            if (existingVisit == null)
                return NotFound();

            await _repository.UpdateAsync(visit);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(Guid id)
        {
            var existingVisit = await _repository.GetByIdAsync(id);
            if (existingVisit == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
