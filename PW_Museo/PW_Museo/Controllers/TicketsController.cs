using Microsoft.AspNetCore.Mvc;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsRepository _repository;

        public TicketsController(TicketsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _repository.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var ticket = await _repository.GetByIdAsync(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            await _repository.CreateAsync(ticket);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            var existingTicket = await _repository.GetByIdAsync(id);
            if (existingTicket == null)
                return NotFound();

            await _repository.UpdateAsync(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var existingTicket = await _repository.GetByIdAsync(id);
            if (existingTicket == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
