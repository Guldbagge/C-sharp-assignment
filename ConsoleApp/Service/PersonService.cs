using ConsoleApp.Interface;
using ConsoleApp.Services;
using Newtonsoft.Json;

namespace ConsoleApp.Service
{
    public class PersonService : IPersonService
    {
        private readonly FileService _fileService = new FileService(@"C:\Education\C-sharp-assignment\content.json");

        // Collection of Person objects
        private List<Person> _persons;
        
        // Retrieves all persons
        public List<Person> GetPersons() => _persons;
        
        // By email
        public Person GetPerson(string email) => _persons.FirstOrDefault(e => e.Email == email)!;

        public PersonService()
        {
            _persons = InitializePersons();
        }

        // Loads persons from file
        private List<Person> InitializePersons()
        {
            try
            {
                // Deserializes file content into a list of Persons or initializes a new list if empty
                var content = _fileService.GetContentFromFile();
                return JsonConvert.DeserializeObject<List<Person>>(content) ?? new List<Person>();
            }
            catch (Exception)
            {
                // Return an empty list if an exception occurs
                return new List<Person>();
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SavePersonsToJsonFile();
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

            return false;
        }

        public void UpdatePerson(Person updatedPerson)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Email == updatedPerson.Email);
            if (existingPerson != null)
            {
                var isEmailTaken = _persons.Any(p => p.Email == updatedPerson.Email && p != existingPerson);

                if (!isEmailTaken || updatedPerson.Email == existingPerson.Email)
                {
                    // Update details
                    existingPerson.Email = updatedPerson.Email;
                    existingPerson.FirstName = updatedPerson.FirstName;
                    existingPerson.LastName = updatedPerson.LastName;
                    existingPerson.StreetName = updatedPerson.StreetName;
                    existingPerson.StreetNumber = updatedPerson.StreetNumber;
                    existingPerson.ZipCode = updatedPerson.ZipCode;
                    existingPerson.City = updatedPerson.City;

                    SavePersonsToJsonFile();
                }
            }
        }

        private void SavePersonsToJsonFile()
        {
            try
            {
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_persons));
            }
            catch (Exception)
            {
                Console.WriteLine("Could not save persons to file.");
            }
        }
    }
}
