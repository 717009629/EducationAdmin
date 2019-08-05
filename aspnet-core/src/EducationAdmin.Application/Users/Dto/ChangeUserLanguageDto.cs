using System.ComponentModel.DataAnnotations;

namespace EducationAdmin.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}