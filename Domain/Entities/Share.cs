using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Share
    {
        public int Id { get; set; }

        public string SharedFromId { get; set; }
        public User? TransferFromUser { get; set; }

        public string SharedToId { get; set; }
        public User? TransferToUser { get; set; }

        public int OfferId { get; set; }
        public Offer? Offer { get; set; }

        public bool IsWatched { get; set; }

        public DateTime Date { get; set; }
    }
}
