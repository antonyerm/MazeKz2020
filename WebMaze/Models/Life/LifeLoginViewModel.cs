using System.ComponentModel.DataAnnotations;

namespace WebMaze.Models.Life
{
    public class LifeLoginViewModel
    {
        [Required(ErrorMessage = "Требуется ввести логин")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Требуется ввести пароль")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
