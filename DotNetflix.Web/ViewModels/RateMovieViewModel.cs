using DotNetflix.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class RateMovieViewModel
    {
        public int RatingId { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserRating { get; set; }
        public string ReviewText { get; set; }
    }
}
