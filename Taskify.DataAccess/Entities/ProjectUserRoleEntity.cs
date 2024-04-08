namespace Taskify.DataAccess.Entities
{
    public class ProjectUserRoleEntity
    {
        public int Id { get; set; }

        public Guid ProjectUserId { get; set; }

        public int RoleId { get; set; }
    }
}
