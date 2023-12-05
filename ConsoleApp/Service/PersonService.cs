using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConsoleApp.Interface;
using ConsoleApp.Services;
using Newtonsoft.Json;

namespace ConsoleApp.Service
{
    public class PersonService : IPersonService
    {
        private readonly FileService _fileService = new FileService(@"C:\Education\C-sharp-assignment\content.json");
        private List<Person> _persons;

        public PersonService()
        {
            LoadPersonsFromJsonFile();
        }

        private void LoadPersonsFromJsonFile()
        {
            try
            {
                var content = _fileService.GetContentFromFile();
                _persons = string.IsNullOrEmpty(content) ? new List<Person>() : JsonConvert.DeserializeObject<List<Person>>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _persons = new List<Person>();
            }
        }

        public void AddPerson(Person person)
        {
            try
            {
                if (_persons.Any(e => e.Email == person.Email))
                    throw new InvalidOperationException("A person with the same email already exists.");

                _persons.Add(person);
                SavePersonsToJsonFile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool RemovePerson(string email)
        {
            var person = _persons.FirstOrDefault(x => x.Email == email);
            if (person != null)
            {
                _persons.Remove(person);
                SavePersonsToJsonFile();
                return true;
            }

            Console.WriteLine("Error, try again");
            return false;
        }

        public void UpdatePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Email == person.Email);
            existingPerson?.UpdateDetails(person);
            SavePersonsToJsonFile();
        }

        private void SavePersonsToJsonFile()
        {
            try
            {
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_persons));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public List<Person> GetPersons() => _persons;

        public Person GetPerson(string email)
        {
            try
            {
                return _persons.FirstOrDefault(e => e.Email == email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Person> GetPerson() => _persons;

        Person IPersonService.GetPerson(string email)
        {
            throw new NotImplementedException();
        }
    }

    public static class PersonExtensions
    {
        public static void UpdateDetails(this Person existingPerson, Person newPerson)
        {
            existingPerson.FirstName = newPerson.FirstName;
            existingPerson.LastName = newPerson.LastName;
            existingPerson.StreetName = newPerson.StreetName;
            existingPerson.StreetNumber = newPerson.StreetNumber;
            existingPerson.ZipCode = newPerson.ZipCode;
            existingPerson.City = newPerson.City;
        }
    }
}
