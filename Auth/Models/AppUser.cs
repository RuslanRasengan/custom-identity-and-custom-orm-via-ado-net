using CustomORM.Attributes;
using Entities.Attributes;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Auth.Models
{
    [TableName("AppUser")]
    public class AppUser : IdentityUser<int>
    {
        [PrimaryKey]
        [Column("Id", DbType.Int32)]
        public int Id { get; set; }

        [Column("Email", DbType.String)]
        public string Email { get; set; }
        public string UserPassword { get; set; }

        [Column("PasswordHash", DbType.String)]
        public string PasswordHash { get; set; }

        [Column("RoleId", DbType.Int32)]
        public int RoleId { get; set; }

        [Column("Login", DbType.String)]
        public string UserLogin { get; set; }

        [Column("UserName", DbType.String)]
        public virtual string UserName { get; set; }

        [NormalizedName]
        [Column("NormalizedUserName", DbType.String)]
        public string NormalizedUserName { get; set; }



        public AppUser(string email, string password, RoleType roleType, string login, string userName, string normalizedName)
        {
            Email = email;
            UserPassword = password;
            RoleId = (int)roleType;
            UserLogin = login;
            UserName = userName;
            NormalizedUserName = normalizedName;
        }

        public AppUser(string email, string password)
        {
            Email = email;
            UserPassword = password;
        }

        public AppUser() { }
    }
}
