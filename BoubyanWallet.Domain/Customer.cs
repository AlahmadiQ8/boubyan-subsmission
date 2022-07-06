using Microsoft.AspNetCore.Identity;

namespace BoubyanWallet.Domain;

public class Customer : IdentityUser
{
    [PersonalData]
    public string Cif { get; set; }
    
    [PersonalData]
    public string NameOnWallet { get; set; }
    
    [PersonalData]
    public double Ammount { get; set; } = 100.0;

}