using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string? Phone2 { get; set; }


        public int Piece { get; set; }
        
        public int Kasema { get; set; }

        public int Street { get; set; }
        
        public int House { get; set; }
        
        public int Price { get; set; }
        
        public string Details { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Category? Department { get; set; }

        public int PurposeId { get; set; }
        public Category? Purpose { get; set; }

        public int TypeId { get; set; }
        public Category? Type { get; set; }

        public int? AreaId { get; set; }
        public Category? Area { get; set; }

        public int? LocationId { get; set; }
        public Category? Location { get; set; }

        public int? SectionId { get; set; }
        public Category? Section { get; set; }

        public int? DistributionId { get; set; }
        public Category? Distribution { get; set; }

        public string CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? ModifiedById { get; set; }
        public User? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        public ICollection<Share> SharedList { get; set; } = new List<Share>();
        
        public ICollection<Favorite> FavoritesList { get; set; } = new List<Favorite>();
    }
}
