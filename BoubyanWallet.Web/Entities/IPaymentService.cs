namespace BoubyanWallet.Web.Entities;

public interface IPaymentService
{
    Task<bool> SubmitPaymentRequest(Payment payment);
    Task<bool> SubmitPaymentTransfer(Payment payment);
}