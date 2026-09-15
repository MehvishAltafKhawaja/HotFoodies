using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.Models;

public class valid
{  
    [DisplayName("Roll No. ")]
    [Required]
    public int Rollno {get; set;}

    [DisplayName("Name ")]
    [Required(ErrorMessage ="Please Enter Your Name")]
    public string name {get; set;}



    [DisplayName("Date of Birth ")]
    [Required(ErrorMessage ="Please Enter Your Dob")]
    public DateOnly DOB {get; set;}


     [DisplayName("Email ")]
     [Required(ErrorMessage ="Please Enter Your Enter")]
     [EmailAddress(ErrorMessage ="Please Enter A valid Email!")]
    public string email {get; set;}



     [DisplayName("Contact ")]
     [Required(ErrorMessage ="Please Enter Your Number")]
     [Length(10,10)]
    public string phone {get; set;}

}
