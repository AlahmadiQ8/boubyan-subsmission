using BoubyanWallet.Web.Data;
using BoubyanWallet.Web.Entities;
using BoubyanWallet.Web.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoubyanWallet.Web.Services;

public class EfPaymentService : IPaymentService
{
    private ApplicationDbContext _context;
    private readonly UserManager<Customer> _userManager;

    public EfPaymentService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> SubmitPaymentRequest(Payment payment)
    {
        var payer = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == payment.Payer.Id);
        if (payer == null)
        {
            throw new UserNotFound($"user {payment.Payer.UserName} not found");
        }
        
        payer.Payments.Add(payment);
        
        // get receiver
        Customer? receiver = null; 
        switch (payment.IdentifierType)
        {
            case IdentifierType.Cif:
                receiver = await _userManager.Users.FirstOrDefaultAsync(c => c.Cif == payment.ReceiverIdentifier);
                break;
            case IdentifierType.Username:
                receiver = await _userManager.Users.FirstOrDefaultAsync(c => c.UserName == payment.ReceiverIdentifier);
                break;
            case IdentifierType.PhoneNumber:
                // TODO: Adrress Phone numebr comparison
                receiver = await _userManager.Users.FirstOrDefaultAsync(
                    c => c.PhoneNumber == payment.ReceiverIdentifier);
                break;
        }
        if (receiver == null)
        {
            throw new UserNotFound($"receiver with identifier type {payment.IdentifierType} not found");
        }
        

        // validate payer has enough funds
        if (payer.Ammount < payment.Amount)
        {
            throw new NotEnoughFunds();
        }

        // transfer money
        using var transaction = await _context.Database.BeginTransactionAsync();

        // substract amount
        payer.Ammount -= payment.Amount;
        await _userManager.UpdateAsync(payer);

        // add amount
        receiver.Ammount += payment.Amount;
        await _userManager.UpdateAsync(payer);

        // update payment status
        payment.PaymentStatus = PaymentStatus.Completed;
            
        // commit
        transaction.Commit();

        return true;
    }

    public Task<bool> SubmitPaymentTransfer(Payment payment)
    {
        throw new NotImplementedException();
    }
}