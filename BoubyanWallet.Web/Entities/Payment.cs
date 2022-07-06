namespace BoubyanWallet.Web.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public Customer Payer { get; set; }
    public IdentifierType IdentifierType { get; set; }
    public string ReceiverIdentifier { get; set; }
    public double Amount { get; set; }
    public string Comment { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Error,
}