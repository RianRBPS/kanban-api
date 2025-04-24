using Xunit;
using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace KanbanAPI.Tests
{
    public class CardTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CanCreateCard()
        {
            using var context = GetInMemoryDbContext();
            var board = new Board { Name = "Test Board" };
            context.Boards.Add(board);
            await context.SaveChangesAsync();

            var card = new Card
            {
                Title = "New Card",
                Description = "Card description",
                Status = "To Do",
                BoardId = board.Id
            };

            context.Cards.Add(card);
            await context.SaveChangesAsync();

            var savedCard = context.Cards.FirstOrDefault(c => c.Title == "New Card");
            Assert.NotNull(savedCard);
            Assert.Equal("To Do", savedCard.Status);
            Assert.Equal(board.Id, savedCard.BoardId);
        }
    }
}
