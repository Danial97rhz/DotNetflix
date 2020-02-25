using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
             CreateMap<Entities.Movies, Models.Movie>();
        }
    }
}
