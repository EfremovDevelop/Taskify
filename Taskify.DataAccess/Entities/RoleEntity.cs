using System.Security;

namespace Taskify.DataAccess.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<PermissionEntity> Permissions { get; set; } = [];

        public ICollection<ProjectUserEntity> ProjectUsers { get; set; } = [];
    }
}
