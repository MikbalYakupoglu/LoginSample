using Core.Entity;

namespace Entity.Abstract
{
    public interface IUser : IEntity
    {
        public string Email { get; set; }
    }
}
