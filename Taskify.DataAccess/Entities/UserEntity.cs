namespace Taskify.DataAccess.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Projects = new HashSet<ProjectUserEntity>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<ProjectUserEntity> Projects { get; set; }
    }
}
