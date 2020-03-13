using DotNetflix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class CarouselViewModel
    {
        public IEnumerable<Carousel> Carouseldata { get; set; }
    }
}
