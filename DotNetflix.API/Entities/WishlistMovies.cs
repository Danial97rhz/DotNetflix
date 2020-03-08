﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class WishlistMovies
    {
        public string MovieId { get; set; }
        public Movies Movie { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public int ListPosition { get; set; }
    }
}
