using System.ComponentModel;

namespace ASPNETMVC.Models;

public class log
{   
    [DisplayName("USERNAME")]
    public string username {get; set;}

    [DisplayName("PASSWORD")]
    public string password {get; set;}
}