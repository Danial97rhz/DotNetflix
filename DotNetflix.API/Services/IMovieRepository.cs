using DotNetflix.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Services
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies(string title);
    }
}
