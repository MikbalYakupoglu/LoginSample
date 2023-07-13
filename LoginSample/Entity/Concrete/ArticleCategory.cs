using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity;

namespace Entity.Concrete;

public class ArticleCategory : IEntity
{
    public int Id { get; private set; }
    [ForeignKey(nameof(Article))] public int ArticleId { get; set; }
    [ForeignKey(nameof(Category))] public int CategoryId { get; set; }

    public Article Article { get; set; }
    public Category Category { get; set; }
}