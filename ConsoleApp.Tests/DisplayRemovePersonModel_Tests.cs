using System;
using ConsoleApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shared.Interface;
using Shared.Service;
using Xunit;

public class DisplayRemovePersonModelTests
{
    [Fact]
    public void RemovePerson_WithValidEmail_RemovesSuccessfully()
    {
        // Arrange
        var email = "test@example.com";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(service => service.SaveContentToFile(It.IsAny<string>())).Returns(true);

        var personService = new PersonService();

        personService.AddPerson(new Person { Email = email });

        // Act
        var result = personService.RemovePerson(email);

        // Assert
        Assert.True(result, "Expected the person to be removed successfully");
    }


    [Fact]
    public void RemovePerson_WithInvalidEmail_FailsToRemove()
    {
        // Arrange
        var email = "test@example.com";
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(service => service.GetContentFromFile()).Returns("[]");
        fileServiceMock.Setup(service => service.SaveContentToFile(It.IsAny<string>())).Returns(true);

        var personService = new PersonService();

        // Act
        var result = personService.RemovePerson(email);

        // Assert
        Assert.False(result);
    }
}
