using Omdb.API.Models;
using System.Threading.Tasks;

namespace Omdb.API
{
    public interface IOmdbRepository
    {
        Task<MovieResponse> Search(string title);

    }
}