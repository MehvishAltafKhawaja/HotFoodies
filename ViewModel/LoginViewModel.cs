using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.ViewModel
{
    public class LoginViewModel
    {   
        [Required(ErrorMessage ="Please Enter Email or Username")]
        [DisplayName("Email or Username")]
        public string EmailOrUsername {get; set;}


        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)] 
        public string Password {get; set;}


        [DisplayName("Remember Me")]
        public bool RememberMe {get; set;}
    }
}