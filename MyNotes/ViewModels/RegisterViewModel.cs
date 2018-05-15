using System.ComponentModel.DataAnnotations;

namespace MyNotes.ViewModels
{
    //определим новый класс RegisterViewModel, который будет представлять регистрирующегося пользователя:

    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        //[Required]
        //[Display(Name = "Year of birth")]
        //public int
        // nafig ne nado at this time ☺

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }

    }
}
