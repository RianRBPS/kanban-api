using Microsoft.AspNetCore.Mvc;
using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            return await _context.Boards.Include(b => b.Cards).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Board>> CreateBoard(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBoards), new { id = board.Id }, board);
        }
    }
}
