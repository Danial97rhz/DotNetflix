using DotNetflix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Data
{
    public interface IMovieData
    {
        IEnumerable<Movie> GetMoviesByTitle(string title);

    }
}
