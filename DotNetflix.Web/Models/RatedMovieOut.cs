﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class RatedMovieOut
    {
        public int RatingId { get; set; }
        public string MovieId { get; set; }
        public int UserId { get; set; }
        //User not yet implemented.
        //public User User { get; set; }
        [Range(1, 10)]
        public int UserRating { get; set; }
        public string ReviewText { get; set; }
    }
}