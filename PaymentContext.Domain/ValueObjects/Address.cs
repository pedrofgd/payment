using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
  public class Address : ValueObject
  {
    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipcode)
    {
      AddNotifications(new Contract()
        .Requires()
        .HasMinLen(Street, 3, "Address.Street", "Street should have at least 3 chars")
      );

      Street = street;
      Number = number;
      City = city;
      Neighborhood = neighborhood;
      State = state;
      Country = country;
      ZipCode = zipcode;
    }
    
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
  }
}