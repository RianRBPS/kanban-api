using Microsoft.AspNetCore.Mvc;
using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("board/{boardId}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsByBoard(int boardId)
        {
            return await _context.Cards.Where(c => c.BoardId == boardId).ToListAsync();
        }

        [HttpPost("board/{boardId}")]
        public async Task<ActionResult<Card>> CreateCard(int boardId, Card card)
        {
            var board = await _context.Boards.FindAsync(boardId);
            if (board == null)
                return NotFound("Board not found");

            card.BoardId = boardId;
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCardsByBoard), new { boardId = boardId }, card);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCardStatus(int id, [FromBody] string newStatus)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
                return NotFound();

            card.Status = newStatus;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
                return NotFound();

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
