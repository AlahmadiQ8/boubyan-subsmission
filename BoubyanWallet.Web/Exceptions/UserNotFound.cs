namespace BoubyanWallet.Web.Exceptions;

public class UserNotFound : Exception
{
    public UserNotFound(string message) : base(message)
    {
    }
}