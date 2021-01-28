using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
  public class Name : ValueObject
  {
    public Name(string firstname, string lastname)
    {
      FirstName = firstname;
      LastName = lastname;
      
      AddNotifications(new Contract()
        .Requires()
        .HasMinLen(FirstName, 2, "Name.FirstName", "First name should have at least 3 chars")
        .HasMaxLen(FirstName, 40, "Name.FirstName", "First name should have no more than 3 chars")
        .HasMinLen(LastName, 2, "Name.LastName", "Last name should have at least 3 chars")
        .HasMaxLen(LastName, 40, "Name.LastName", "Last name should have no more than 3 chars")
      );
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }


    public override string ToString()
    {
      return $"{FirstName} {LastName}";
    }
  }
}