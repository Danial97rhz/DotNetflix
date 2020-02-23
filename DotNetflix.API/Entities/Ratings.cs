using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class Ratings
    {
        public int Id { get; set; }
        public int AvgRating { get; set; }
        public int NumberOfVotes { get; set; }
    }
}
