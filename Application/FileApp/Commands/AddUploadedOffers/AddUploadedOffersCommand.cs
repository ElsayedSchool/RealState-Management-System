using Application.Common.Mediatr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileApp.Commands.AddUploadedOffers
{
    public class AddUploadedOffersCommand : IRequestWrapper<string>
    {
        public IFormFile NewOffersFile { get; set; }
    }
}
