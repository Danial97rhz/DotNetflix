using DotNetflix.Web.Models;
using System.Collections.Generic;

namespace DotNetflix.Web.ViewModels
{
    public class MovieListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}
