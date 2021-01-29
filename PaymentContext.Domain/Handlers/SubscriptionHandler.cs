using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handler
{
  public class SubscriptionHandler : 
    Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>    
  {
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
      _repository = repository;
      _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
      // Fail Fast Validation
      command.Validate();
      if (command.Invalid)
      {
        AddNotifications(command);
        return new CommandResult(false, "It was not possible finish your subscription");
      }

      // Document exists
      if (_repository.DocumentExists(command.Document))
        AddNotification("Document", "This document already have a subscription");

      // Email exists
      if (_repository.EmailExists(command.Email))
        AddNotification("Email", "This email already have a subscription");

      // Generate Value Objects
      var name = new Name(command.FirstName, command.LastName);
      var document = new Document(command.Document, EDocumentType.CPF);
      var email = new Email(command.Email);
      var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

      // Generate Entities
      var student = new Student(name, document, email);
      var subscription = new Subscription(DateTime.Now.AddMonths(1));
      var payment = new BoletoPayment(
      command.BarCode,
      command.BoletoNumber,
      command.PaidDate,
      command.ExpireDate,
      command.Total,
      command.TotalPaid,
      command.Payer,
      new Document(command.PayerDocument, command.PayerDocumentType),
      address,
      email
      );

      subscription.AddPayment(payment);
      student.AddSubscription(subscription);

      // Validate
      AddNotifications(name, document, email, address, student, subscription, payment);
      if (Invalid)
        return new CommandResult(false, "It was not possible finish your subscription");
      
      // Save informations
      _repository.CreateSubscription(student);

      // Send welcome email
      _emailService.Send(student.Name.ToString(), student.Email.Adress, "Welcome", "Your subscription was accepted");

      // Retornar informações
      return new CommandResult(true, "Successfull subscription");
    }
  }
}