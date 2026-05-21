using Microsoft.AspNetCore.Mvc;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuidedVisitsController : ControllerBase
    {
        private readonly GuidedVisitsRepository _repository;

        public GuidedVisitsController(GuidedVisitsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuidedVisits()
        {
            var guidedVisits = await _repository.GetAllAsync();
            return Ok(guidedVisits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuidedVisit(Guid id)
        {
            var guidedVisit = await _repository.GetByIdAsync(id);
            if (guidedVisit == null)
                return NotFound();
            return Ok(guidedVisit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuidedVisit([FromBody] Guided_Visit guidedVisit)
        {
            guidedVisit.Id = Guid.NewGuid();
            await _repository.CreateAsync(guidedVisit);
            return CreatedAtAction(nameof(GetGuidedVisit), new { id = guidedVisit.Id }, guidedVisit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuidedVisit(Guid id, [FromBody] Guided_Visit guidedVisit)
        {
            if (id != guidedVisit.Id)
                return BadRequest();

            var existingGuidedVisit = await _repository.GetByIdAsync(id);
            if (existingGuidedVisit == null)
                return NotFound();

            await _repository.UpdateAsync(guidedVisit);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuidedVisit(Guid id)
        {
            var existingGuidedVisit = await _repository.GetByIdAsync(id);
            if (existingGuidedVisit == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
