using Entity.Concrete;

namespace Entity.DTOs;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatorName { get; set; }
    public List<string> Categories { get; set; }
}