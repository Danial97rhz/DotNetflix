using System.ComponentModel.DataAnnotations;

namespace DotNetflix.Web.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }
}
