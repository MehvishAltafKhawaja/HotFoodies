using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.Models;

public class CreateProd
{
    [DisplayName("Product Id. ")]
    [Required(ErrorMessage ="Please Enter ProdId")]
    public int? ProdId {get; set;}

    [DisplayName("Product Name ")]
    [Required(ErrorMessage ="Please Enter Your Name")]
    public string? prodname {get; set;}



    [DisplayName("Date of Expire ")]
    [Required(ErrorMessage ="Please Enter Your Dob")]
    public DateOnly? Expire {get; set;}


     [DisplayName("Company Email ")]
     [Required(ErrorMessage ="Please Enter Your Enter")]
     [EmailAddress(ErrorMessage ="Please Enter A valid Email!")]
    public string? email {get; set;}



     [DisplayName("Product Batch ")]
     [Required(ErrorMessage ="Please Enter Your Number")]
     [Length(10,10)]
    public string? Batch {get; set;}
}