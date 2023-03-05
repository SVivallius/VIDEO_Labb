namespace VIDEO.common.DTOs;
public class FilmDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Url { get; set; }
    public string EmbedString { get; set; }
    public int DirectorId { get; set; }
    public DirectorDTO Director { get; set; }
    public List<GenreDTO> Genres { get; set; }
    public bool free { get; set; }
}
