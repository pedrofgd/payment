using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
  public abstract class Payment : Entity /* abstract -> não posso instanciar essa classe direto */
  {
    public Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Address address, Email email)
    {
      PaymentNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
      PaidDate = paidDate;
      ExpireDate = expireDate;
      Total = total;
      TotalPaid = totalPaid;
      Payer = payer;
      Document = document;
      Address = address;
      Email = email;

      AddNotifications(new Contract()
        .Requires()
        .IsLowerOrEqualsThan(0, Total, "Payment.Total", "The total cannot be zero")
        .IsLowerOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "The total paid cannot be less than the amount payment")
      );
    }

    public string PaymentNumber { get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public decimal Total { get; private set; }
    public decimal TotalPaid { get; private set; }
    public string Payer { get; private set; }
    public Document Document { get; private set; }
    public Address Address { get; private set; }
    public Email Email { get; private set; }
  }
}