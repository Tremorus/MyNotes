using System.ComponentModel.DataAnnotations;

namespace MyNotes.ViewModels
{
    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
