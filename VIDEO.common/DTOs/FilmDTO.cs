namespace VIDEO.common.DTOs;
public class FilmDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Url { get; set; }
    public int DirectorId { get; set; }
    public List<int> GenreIds { get; set; }
    public bool free { get; set; }
}
