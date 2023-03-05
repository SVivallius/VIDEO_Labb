namespace VIDEO.common.DTOs;

public class GenreDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<FilmDTO> Film { get; set; }
}
