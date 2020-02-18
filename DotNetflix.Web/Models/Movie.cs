using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public int StoryLineId { get; set; }
        public StoryLine StoryLine { get; set; }
        public int PosterId { get; set; }
        public Poster Poster { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public int RunTime { get; set; }
        public bool IsAdult { get; set; }
        public List<UserMovies> Movies { get; set; }
        public List<MovieGenres> Genres { get; set; }
    }
}
