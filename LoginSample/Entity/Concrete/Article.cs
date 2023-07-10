using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Article : IEntity
    {
        public Article() => CreatedAt = DateTime.Now;

        public bool IsDeleted { get; set; }
        public int Id { get; private set; }
        public string Title { get; set; } = "Başlık";
        public string Content { get; set; } = "İçerik";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey(nameof(User))] public int CreatorId { get; set; }
        [ForeignKey(nameof(User))] public int? DeletedBy { get; set; }

        public User? Creator { get; set; }
        List<Category> Categories { get; set; }
    }
}
