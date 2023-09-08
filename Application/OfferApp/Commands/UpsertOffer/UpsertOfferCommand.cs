using Application.Common.Mediatr;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.UpsertOffer
{
    public class UpsertOfferCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        //owner data
        public string OwnerName { get; set; }
        
        public string Phone1 { get; set; }
        
        public string? Phone2 { get; set; }


        // lookups
        public int DepartmentId { get; set; }
        
        public int PurposeId { get; set; }
        
        public int TypeId { get; set; }
        
        public int AreaId { get; set; }
        
        public int LocationId { get; set; }
        
        public int SectionId { get; set; }
        
        public int DistributionId { get; set; }


        // public details
        public int Piece { get; set; }

        public int Kasema { get; set; }

        public int Street { get; set; }

        public int House { get; set; }

        public int Price { get; set; }

        public string Details { get; set; } = String.Empty;

        public IFormFileCollection Photos { get; set; } = new FormFileCollection();

        public string Comment { get; set; }

    }
}
