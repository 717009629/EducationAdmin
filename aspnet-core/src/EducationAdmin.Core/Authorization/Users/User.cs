using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Newtonsoft.Json;

namespace EducationAdmin.Authorization.Users
{
    public class User : AbpUser<User>
    {
        

        [NotMapped]
        public override string Surname { get; set; }
        public DateTime? Birthday { get; set; }

        public bool? Sex { get; set; }

        public UserType? Type { get; set; }

        public string Phone { get; set; }

        public const string DefaultPassword = "123qwe";

        public string IdentityCard { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string PoliticsStatus { get; set; }

        public string SchoolTag { get; set; }

        public string EducationBackground { get; set; }

        public string Nation { get; set; }

        public string MaritalStatus { get; set; }

        public int? WorkingYears { get; set; }
        public DateTime? LastLoginTime { get; set; }



        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }


    }

    public enum UserType
    {
        Business = 0,
        Teacher = 1,
        Admin = 2,

    }
}
