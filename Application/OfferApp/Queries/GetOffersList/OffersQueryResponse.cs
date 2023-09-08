using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Queries.GetOffersList
{
    public class OffersQueryResponse
    {
        public List<OffersListVm> Offers { get; set; } = new List<OffersListVm>();
        public int TotalCount { get; set; }
    }
}
