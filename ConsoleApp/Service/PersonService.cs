using System.Diagnostics;
using ConsoleApp.Interface;
using ConsoleApp.Services;
using Newtonsoft.Json;

namespace ConsoleApp.Service
{
    public class PersonService : IPersonService
    {
        // FileService to handle file operations
        private readonly FileService _fileService = new FileService(@"C:\Education\C-sharp-assignment\content.json");
        // Collection of Person objects
        private List<Person> _persons;

        // Retrieves all persons
        public List<Person> GetPersons() => _persons;

        // Retrieves a person by email
        public Person GetPerson(string email) => _persons.FirstOrDefault(e => e.Email == email)!;

        // Initializes the Person collection from file content
        public PersonService()
        {
            _persons = InitializePersons();
        }

        // Loads persons from file
        private List<Person> InitializePersons()
        {
            try
            {
                var content = _fileService.GetContentFromFile();
                // Deserializes file content into a list of Persons or initializes a new list if empty
                return JsonConvert.DeserializeObject<List<Person>>(content) ?? new List<Person>();
            }
            catch (Exception ex)
            {
                // Log any exceptions during file content retrieval
                Debug.WriteLine(ex.Message);
                // Return an empty list if an exception occurs
                return new List<Person>();
            }
        }

        // Adds a new person to the collection and saves changes to the file
        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SavePersonsToJsonFile();
        }

        // Removes a person by email from the collection and saves changes to the file
        public bool RemovePerson(string email)
        {
            var person = _persons.FirstOrDefault(x => x.Email == email);
            if (person != null)
            {
                _persons.Remove(person);
                SavePersonsToJsonFile();
                return true;
            }

            return false;
        }

        // Updates a person's details in the collection and saves changes to the file
        public void UpdatePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Email == person.Email);
            existingPerson?.UpdateDetails(person);
            SavePersonsToJsonFile();
        }

        // Saves the collection of Persons to a JSON file
        private void SavePersonsToJsonFile()
        {
            try
            {
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_persons));
            }
            catch (Exception ex)
            {
                // Log any exceptions during file save
                Debug.WriteLine(ex.Message);
            }
        }
    }

    // Extension method for updating Person details
    public static class PersonExtensions
    {
        public static void UpdateDetails(this Person existingPerson, Person newPerson)
        {
            // Updates existing Person's details with new information
            existingPerson.Email = newPerson.Email;
            existingPerson.FirstName = newPerson.FirstName;
            existingPerson.LastName = newPerson.LastName;
            existingPerson.StreetName = newPerson.StreetName;
            existingPerson.StreetNumber = newPerson.StreetNumber;
            existingPerson.ZipCode = newPerson.ZipCode;
            existingPerson.City = newPerson.City;
        }
    }
}
