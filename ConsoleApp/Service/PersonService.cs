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
        private readonly List<Person> _persons = new List<Person>();

        public PersonService()
        {
            LoadPersonsFromJsonFile();
        }

        private void LoadPersonsFromJsonFile()
        {
            try
            {
                var content = _fileService.GetContentFromFile();
                if (!string.IsNullOrEmpty(content))
                {
                    _persons.Clear();
                    _persons.AddRange(JsonConvert.DeserializeObject<List<Person>>(content));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void AddPerson(Person person)
        {
            try
            {
                LoadPersonsFromJsonFile(); // Ladda anställda från filen för att säkerställa aktuell data

                Debug.WriteLine($"Trying to add person with ID: {person.Id}");

                if (!_persons.Any(e => e.Id == person.Id))
                {
                    _persons.Add(person);
                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_persons));
                    Debug.WriteLine("Person added successfully!");
                }
                else
                {
                    throw new InvalidOperationException("An person with the same ID already exists.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public bool RemovePerson(int id)
        {
            var person = _persons.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                _persons.Remove(person);
                return true;
            }

            Console.WriteLine("Error, try again");
            return false;
        }

        public void UpdatePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Id == person.Id);
            existingPerson?.UpdateDetails(person);
        }

        public List<Person> GetPersons() => _persons;

        public Person GetPerson(int id)
        {
            try
            {
                var person = _persons.FirstOrDefault(e => e.Id == id);
                return person;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        Person IPersonService.GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetPerson()
        {
            throw new NotImplementedException();
        }
    }

    public static class PersonExtensions
    {
        public static void UpdateDetails(this Person existingPerson, Person newPerson)
        {
            existingPerson.Name = newPerson.Name;
            existingPerson.Position = newPerson.Position;
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using ConsoleApp.Interface;
//using ConsoleApp.Services;
//using Newtonsoft.Json;

//namespace ConsoleApp.Service
//{
//    // En serviceklass för att hantera anställdas operationer
//    public class EmployeeService : IEmployeeService
//    {
//        // En privat lista som håller anställda
//        private readonly FileService _fileService = new FileService(@"C:\Education\EmployeeCleanDependencyInjection\content.json");
//        private readonly List<Employee> _employees = new List<Employee>();

//        // Lägger till en ny anställd i listan
//        //public void AddEmployee(Employee employee) => _employees.Add(employee);

//        public void AddEmployee(Employee employee)
//        {
//            try
//            {
//                if (!_employees.Any(e => e.Id == employee.Id))
//                {
//                    _employees.Add(employee);
//                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_employees));
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.Message);
//            }
//        }

//        //public IEnumerable<Employee> GetEmployeeFromList()
//        //{
//        //    try
//        //    {
//        //        var content = _fileService.GetContentFromFile();
//        //        if (!string.IsNullOrEmpty(content))
//        //        {
//        //            _employees = JsonConvert.DeserializeObject<List<Employee>>(content);
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Debug.WriteLine(ex.Message);
//        //    }
//        //    return _employees;
//        //}


//        // Tar bort en anställd från listan baserat på ID
//        public bool RemoveEmployee(int id)
//        {
//            var employee = _employees.FirstOrDefault(x => x.Id == id);
//            if (employee != null)
//            {
//                _employees.Remove(employee);
//                return true;
//            }

//            Console.WriteLine("Error, try again"); // Meddelande om anställd inte hittades
//            return false;
//        }

//        // Uppdaterar informationen för en befintlig anställd
//        public void UpdateEmployee(Employee employee)
//        {
//            var existingEmployee = _employees.FirstOrDefault(x => x.Id == employee.Id);
//            existingEmployee?.UpdateDetails(employee); // Anropar metoden för att uppdatera detaljer
//        }

//        // Hämtar alla anställda
//        public List<Employee> GetEmployees() => _employees;

//        public Employee GetEmployee(int id)
//        {
//            try
//            {
//                var content = _fileService.GetContentFromFile();
//                if (!string.IsNullOrEmpty(content))
//                {
//                    var deserializedEmployees = JsonConvert.DeserializeObject<List<Employee>>(content);
//                    var employee = deserializedEmployees.FirstOrDefault(e => e.Id == id);
//                    return employee;
//                }
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.Message);
//            }
//            return null; // Returnera null om det inte går att hitta anställd med det angivna ID:et
//        }




//        // Hämtar en anställd baserat på ID
//        // public Employee GetEmployee(int id) => _employees.FirstOrDefault(employee => employee.Id == id);
//    }

//    // En statisk klass som innehåller en metod för att uppdatera anställdas detaljer
//    public static class EmployeeExtensions
//    {
//        // En extensionsmetod som uppdaterar detaljer för en befintlig anställd
//        public static void UpdateDetails(this Employee existingEmployee, Employee newEmployee)
//        {
//            existingEmployee.Name = newEmployee.Name; // Uppdaterar namnet
//            existingEmployee.Position = newEmployee.Position; // Uppdaterar positionen
//        }
//    }
//}
