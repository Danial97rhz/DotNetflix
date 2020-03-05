﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class UserMovieRating
    {
        public int UserId { get; set; }
        public string MovieId { get; set; }
        [Range(1,10)]
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}