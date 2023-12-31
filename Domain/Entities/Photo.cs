﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
