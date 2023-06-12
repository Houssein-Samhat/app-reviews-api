using Apps_Review_Api.Models;

namespace Apps_Review_Api.Interface
{
    public interface IGenre
    {
        Task<List<Genre>> GetAllGenresAsync(GenreReqBinding detail);
    }
}
