using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BoubyanWallet.Web.Entities;

public class Customer : IdentityUser
{
    [PersonalData]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Cif { get; set; }
    
    [PersonalData]
    public string NameOnWallet { get; set; }
    
    [PersonalData]
    public double Ammount { get; set; } = 100.0;

    public IList<Payment> Payments { get; set; }
    
    [ProtectedPersonalData]
    [Phone]
    [RegularExpression("^\\+965")]
    public string PhoneNumber { get; set; }

}