﻿namespace Taskify.DataAccess.Entities
{
    public class RolePermissionEntity
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }
    }
}
