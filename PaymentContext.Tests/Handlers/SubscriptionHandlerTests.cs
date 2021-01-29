using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handler;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
  [TestClass]
  public class SubscriptionHandlerTests
  {
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
      var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
      var command = new CreateBoletoSubscriptionCommand();
      command.FirstName = "Pedro";
      command.LastName = "Figueiredo";
      command.Document = "12345678900";
      command.Email = "pedro@pedro.com";

      command.BarCode = "12345678";
      command.BoletoNumber = "12345";
      
      command.PaymentNumber = "12345";
      command.PaidDate = DateTime.Now;
      command.ExpireDate = DateTime.Now.AddMonths(30);
      command.Total = 60;
      command.TotalPaid = 60;
      command.Payer = "Pedro";
      command.PayerDocument = "99999999999";
      command.PayerDocumentType = EDocumentType.CPF;
      command.PayerEmail = "pedro@pedro.com";

      command.Street = "Rua";
      command.Number = "12";
      command.Neighborhood = "Bairro";
      command.City = "São Paulo";
      command.State = "São Paulo";
      command.Country = "Brasil";
      command.ZipCode = "1234567";

      handler.Handle(command);
      Assert.AreEqual(false, handler.Valid);
    }
  }
}