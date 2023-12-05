// Interface defining file service functionalities
using System.Diagnostics;

namespace ConsoleApp.Services
{
    public interface IFileService
    {
        bool SaveContentToFile(string content); // Save content to file
        string GetContentFromFile(); // Retrieve content from file
    }

    // Implementation of file service interface
    public class FileService : IFileService
    {
        private readonly string _filePath; // File path

        public FileService(string filePath) // Constructor setting file path
        {
            _filePath = filePath;
        }

        public bool SaveContentToFile(string content) // Save content to file
        {
            try
            {
                File.WriteAllText(_filePath, content); // Write content to file
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Log any exceptions
                return false;
            }
        }

        public string GetContentFromFile() // Retrieve content from file
        {
            try
            {
                return File.Exists(_filePath) ? File.ReadAllText(_filePath) : string.Empty; // Check if file exists and read content
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message); // Log any exceptions
                return string.Empty;
            }
        }
    }
}
