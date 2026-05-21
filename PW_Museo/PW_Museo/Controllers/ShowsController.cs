using Microsoft.AspNetCore.Mvc;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly ShowsRepository _repository;

        public ShowsController(ShowsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShows()
        {
            var shows = await _repository.GetAllAsync();
            return Ok(shows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShow(Guid id)
        {
            var show = await _repository.GetByIdAsync(id);
            if (show == null)
                return NotFound();
            return Ok(show);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShow([FromBody] Show show)
        {
            show.Id = Guid.NewGuid();
            await _repository.CreateAsync(show);
            return CreatedAtAction(nameof(GetShow), new { id = show.Id }, show);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShow(Guid id, [FromBody] Show show)
        {
            if (id != show.Id)
                return BadRequest();

            var existingShow = await _repository.GetByIdAsync(id);
            if (existingShow == null)
                return NotFound();

            await _repository.UpdateAsync(show);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(Guid id)
        {
            var existingShow = await _repository.GetByIdAsync(id);
            if (existingShow == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
