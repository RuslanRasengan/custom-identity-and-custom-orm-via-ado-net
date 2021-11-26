using CustomORM.Attributes;
using Entities.Attributes;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Auth.Models
{
    [TableName("UserRole")]
    public class AppRole : IdentityRole<int>
    {
        [PrimaryKey]
        [Column("Id", DbType.Int32)]
        public int Id { get; set; }
        [Column("Name", DbType.String)]
        public string RoleName { get; set; }

        [NormalizedName]
        [Column("NormalizedRoleName", DbType.String)]
        public string NormalizedRoleName { get; set; }

        public AppRole(int id, RoleType roleType, string normalizedRoleName)
        {
            Id = id;
            RoleName = roleType.ToString();
            NormalizedRoleName = normalizedRoleName;
        }

        public AppRole() { }
    }
}
