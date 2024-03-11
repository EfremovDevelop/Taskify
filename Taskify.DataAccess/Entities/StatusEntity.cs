using System.ComponentModel;

namespace Taskify.DataAccess.Entities
{
    public enum StatusEntity
    {
        [Description("New")]
        New,
        [Description("Assigned")]
        Assigned,
        [Description("Review")]
        Review,
        [Description("Reopened")]
        Reopened,
        [Description("Closed")]
        Closed
    }
}
