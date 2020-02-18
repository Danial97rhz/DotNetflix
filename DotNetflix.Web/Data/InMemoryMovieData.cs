using DotNetflix.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Data
{
    public class InMemoryMovieData : IMovieData
    {
        List<Movie> movies;
        public InMemoryMovieData()
        {
            movies = new List<Movie>
            {
                new Movie{Id="1", Title="Shawshank Redemption"},
                new Movie{Id="2", Title="Portrait of a Lady on Fire"}
            };
        }
        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = title.ToLower();
            }
            return movies
                .Where(m => string.IsNullOrEmpty(title) || m.Title.ToLower().Contains(title))
                .OrderBy(m => m.Title)
                .Select(m => m);
        }
    }
}
