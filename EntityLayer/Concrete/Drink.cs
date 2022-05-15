﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }

        public double Price { get; set; }

        [NotMapped]
        public IFormFile MainPhotoFile { get; set; }

        public bool Active { get; set; }
    }
}
