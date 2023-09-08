using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
        public Category? Department { get; set; }


        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public ICollection<Share> SharedToList { get; set; } = new  List<Share>();
        
        public ICollection<Share> SharedFromList { get; set; } = new List<Share>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        public ICollection<Offer> CreatedOffers { get; set; } = new List<Offer>();

        public ICollection<Offer> ModifiedOffers { get; set; } = new List<Offer>();
    }
}
