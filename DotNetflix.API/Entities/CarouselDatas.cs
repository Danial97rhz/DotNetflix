﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class CarouselDatas
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgPath { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
