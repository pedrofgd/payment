using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
  [TestClass]
  public class CreateBoletoSubscriptionTests
  {
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
      var command = new CreateBoletoSubscriptionCommand();
      command.FirstName = "";
      
      command.Validate();
      Assert.AreEqual(false, command.Valid);
    }
  }
}