using Newtonsoft.Json;
using System.Diagnostics;
using Shared.Interface;
using Shared.Service;

namespace ConsoleApp.Service
{
    public class PersonService : IPersonService
    {
        private readonly FileService _fileService = new FileService(@"C:\Education\C-sharp-assignment\content.json");
        private List<Person> _persons;

        public List<Person> GetPersons() => _persons;

        public Person GetPerson(string email) => _persons.FirstOrDefault(e => e.Email == email)!;

        public PersonService()
        {
            _persons = InitializePersons();
        }

        private List<Person> InitializePersons()
        {
            try
            {
                var content = _fileService.GetContentFromFile();
                return JsonConvert.DeserializeObject<List<Person>>(content) ?? new List<Person>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Person>();
            }
        }

        public void AddPerson(Person person)
        {
            try
            {
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
            try
            {
                var person = _persons.FirstOrDefault(x => x.Email == email);
                if (person != null)
                {
                    _persons.Remove(person);
                    SavePersonsToJsonFile();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.GetType().Name} - Message: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }

            return false;
        }


        public void UpdatePerson(Person updatedPerson)
        {
            try
            {
                var existingPerson = _persons.FirstOrDefault(x => x.Email == updatedPerson.Email);
                if (existingPerson != null)
                {
                    var isEmailTaken = _persons.Any(p => p.Email == updatedPerson.Email && p != existingPerson);

                    if (!isEmailTaken || updatedPerson.Email == existingPerson.Email)
                    {
                        existingPerson.Email = updatedPerson.Email;
                        existingPerson.FirstName = updatedPerson.FirstName;
                        existingPerson.LastName = updatedPerson.LastName;
                        existingPerson.StreetName = updatedPerson.StreetName;
                        existingPerson.StreetNumber = updatedPerson.StreetNumber;
                        existingPerson.ZipCode = updatedPerson.ZipCode;
                        existingPerson.City = updatedPerson.City;
                        existingPerson.PhoneNumber = updatedPerson.PhoneNumber;

                        SavePersonsToJsonFile();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
    }
}
