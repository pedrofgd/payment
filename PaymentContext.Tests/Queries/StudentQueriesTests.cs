using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handler;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
  [TestClass]
  public class StudentQueriesTests
  {
    private IList<Student> _students;

    /* Criação estática de alunos */
    public StudentQueriesTests()
    {
      _students = new List<Student>();
      for (var i = 0; i < 10; i++)
      {
        _students.Add(new Student(
          new Name("Pedro", "Figueiredo" + i.ToString()),
          new Document("1234567890" + i.ToString(), EDocumentType.CPF),
          new Email(i.ToString() + "pedro@pedro.com")
        ));
      }
    }

    [TestMethod]
    public void ShouldReturnErrorWhenDocumentNotExists()
    {
      var expression = StudentQueries.GetStudent("12345678923");
      var student = _students.AsQueryable().Where(expression).FirstOrDefault();

      Assert.AreEqual(null, student);
    }
  }
}