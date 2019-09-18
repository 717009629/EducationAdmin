using System.ComponentModel.DataAnnotations;

namespace EducationAdmin.Users.Dto
{
    public class ChangePasswordByAdminDto
    {
        public long Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
