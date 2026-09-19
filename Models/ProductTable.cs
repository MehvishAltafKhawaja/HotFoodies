using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVC.Models;

public partial class ProductTable
{   
    [DisplayName("Product Id")]
    [Required(ErrorMessage ="Please Entry Id")]
    public int ProdId { get; set; }
    [DisplayName("Product Name")]
    [Required(ErrorMessage ="Please Entry Name")]
    public string? ProdName { get; set; }
    [DisplayName("Product Expiry")]
    [Required(ErrorMessage ="Please Entry Expire Date")]
    public DateOnly? ProdExpire { get; set; }
    [DisplayName("Product Price")]
    [Required(ErrorMessage ="Please Entry Price")]
    public decimal? ProdPrice { get; set; }
    [DisplayName("Product BatchId")]
    [Required(ErrorMessage ="Please Entry Batch Id")]
    public string? ProdBatchId { get; set; }
}
