namespace Taskify.DataAccess.Entities
{
    public class ProjectUserEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public Guid ProjectId { get; set; }
        public virtual ProjectEntity Project { get; set;}
    }
}
