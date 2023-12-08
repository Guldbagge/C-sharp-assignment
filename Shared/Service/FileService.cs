using System;
using System.Diagnostics;
namespace Shared.Service
{
    public interface IFileService
    {
        bool SaveContentToFile(string content);
        string GetContentFromFile();
    }

    public class FileService : IFileService
    {
        private readonly string _filePath;

        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        public bool SaveContentToFile(string content)
        {
            try
            {
                File.WriteAllText(_filePath, content);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public string GetContentFromFile()
        {
            try
            {
                return File.Exists(_filePath) ? File.ReadAllText(_filePath) : string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
