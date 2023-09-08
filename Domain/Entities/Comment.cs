using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Detail { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        
        public User User { get; set; }
        
        public int OfferId { get; set; }
        
        public Offer Offer { get; set; }
    }
}
