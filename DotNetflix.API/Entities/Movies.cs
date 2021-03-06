﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class Movies
    {
        public string MovieId { get; set; }
        public int? MoviesDetailsId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int? Year { get; set; }
        public int? RunTimeMinutes { get; set; }
        public bool IsAdult { get; set; }
        public int? NumberOfVotes { get; set; }
        public string AvgRating { get; set; }
        public MoviesDetails Details { get; set; }
        public ICollection<MovieGenres> MovieGenres { get; set; }
    }
}