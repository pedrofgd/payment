using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
  public class Email : ValueObject
  {
    public Email(string adress)
    {
      AddNotifications(new Contract()
        .Requires()
        .IsEmail(adress, "Email.Adress", "Invalid email")
      );

      if (Valid)
        Adress = adress;
    }
    
    public string Adress { get; private set; }
  }
}