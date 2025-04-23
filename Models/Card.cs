using System;

namespace KanbanAPI.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }  // "To Do", "Doing", "Done"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
