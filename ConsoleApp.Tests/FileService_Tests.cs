using Shared.Service;
using System;
using Xunit;

namespace ConsoleApp.Tests
{
    public class FileService_Tests
    {
        [Fact]
        public void SaveContentToFile_ShouldReturnTrue_WhenContentIsSaved()
        {
            // Arrange
            string filePath = @"C:\Education\C-sharp-assignment\test.txt";
            string content = "test content";

            IFileService fileService = new FileService(filePath);

            // Act
            bool result = fileService.SaveContentToFile(content);

            // Assert
            Assert.True(result);
        }
    }
}
