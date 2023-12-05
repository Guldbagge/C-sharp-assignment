

using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp.Services
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
                using (var sw = new StreamWriter(_filePath))
                {
                    sw.WriteLine(content);
                }
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
                if (File.Exists(_filePath))
                {
                    using (var sr = new StreamReader(_filePath))
                    {
                        return sr.ReadToEnd();
                    }
                }
                else
                {
                    return string.Empty; // Om filen inte finns returneras en tom sträng
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
