using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public AdsCategories category { get; set; }

        public ICollection<Offer> Departments { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Purposes { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Types { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Areas { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Locations { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Sections { get; set; } = new List<Offer>();
        
        public ICollection<Offer> Distributions { get; set; } = new List<Offer>();
        
        public ICollection<User> Employees { get; set; } = new List<User>();

    }
}
