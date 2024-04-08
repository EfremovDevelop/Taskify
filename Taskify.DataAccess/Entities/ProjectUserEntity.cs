namespace Taskify.DataAccess.Entities
{
    public class ProjectUserEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProjectId { get; set; }

        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
