using System;
using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using EducationAdmin.Authorization.Users;

namespace EducationAdmin.Users.Dto
{
    [AutoMapTo(typeof(User))]
    public class CreateUserDto : IShouldNormalize
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        //[Required]
        //[StringLength(AbpUserBase.MaxSurnameLength)]
        //public string Surname { get; set; }

        //[Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }


        public DateTime? Birthday { get; set; }

        public bool? Sex { get; set; }

        public UserType? Type { get; set; }

        public string Phone { get; set; }


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


        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public void Normalize()
        {
            if (RoleNames == null)
            {
                RoleNames = new string[0];
            }
        }
    }
}
