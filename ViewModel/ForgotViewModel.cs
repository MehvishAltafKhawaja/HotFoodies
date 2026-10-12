using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.ViewModel
{
   public class ForgotViewModel
    {

        [EmailAddress(ErrorMessage ="Please Enter a Valid Email")]
         public string Email {get; set;}
    }
    
}