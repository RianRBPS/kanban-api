using System;
using System.Collections.Generic;

namespace KanbanAPI.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Card> Cards { get; set; }
    }
}
