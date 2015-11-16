using System;
using Fall2015.Controllers;
using Fall2015.Models;
using Fall2015.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fall2015.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTests
    {
        [TestMethod]
        public void TestMethodEmail()
        {
            Mock<IStudentsRepository> mockRepo =
                new Mock<IStudentsRepository>();
            Mock<IEmailer> fakeEmailer = new Mock<IEmailer>();

            StudentsController controller = 
                new StudentsController(mockRepo.Object, fakeEmailer.Object);

            Student s = new Student
            {
                Firstname = "Daniel",
                Lastname = "Something"
            };

            controller.Create(s, null);

            fakeEmailer.Verify(a=>a.Send("Welcome to our website..."));
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            Mock<IStudentsRepository> mockRepo = 
                new Mock<IStudentsRepository>();

            StudentsController controller = 
                new StudentsController(mockRepo.Object, null);

            Student s = new Student
            {
                Firstname = "Daniel",
                Lastname = "Something"
            };

            //Act - Where you call the method to test
            controller.Create(s, null);

            //Assert - verify that insertorupdate was called
            mockRepo.Verify(a=>a.InsertOrUpdate(s));
            mockRepo.Verify(a=>a.Save());
        }
    }
}







