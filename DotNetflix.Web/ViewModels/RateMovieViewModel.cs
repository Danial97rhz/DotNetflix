﻿using DotNetflix.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class RateMovieViewModel
    {
        public IEnumerable<RatedMovie> RatedMovies { get; set; }
    }
}
