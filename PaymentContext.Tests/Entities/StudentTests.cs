using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
  [TestClass]
  public class StudentTests
  {
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;

    public StudentTests()
    {
      _name = new Name("Pedro", "Figueiredo");
      _document = new Document("34225545806", EDocumentType.CPF);
      _address = new Address("Rua xyz", "1234", "Bairro", "São Paulo", "São Paulo", "Brasil", "12345678");
      _email = new Email("pedro@pedro.com");
      _student = new Student(_name, _document, _email);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    { 
      var subscription = new Subscription(null);
      var payment = new PayPalPayment("1234567", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Figs Inc", _document, _address, _email);
      subscription.AddPayment(payment);
      _student.AddSubscription(subscription);
      _student.AddSubscription(subscription);

      Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
      var subscription = new Subscription(null);
      _student.AddSubscription(subscription);

     Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
      var subscription = new Subscription(null);
      var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Figs Inc", _document, _address, _email);
      subscription.AddPayment(payment);
      _student.AddSubscription(subscription);
      Assert.IsTrue(_student.Valid);
    }
  }
}