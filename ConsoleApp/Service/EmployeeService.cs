﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConsoleApp.Interface;
using ConsoleApp.Services;
using Newtonsoft.Json;

namespace ConsoleApp.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FileService _fileService = new FileService(@"C:\Education\EmployeeCleanDependencyInjection\content.json");
        private readonly List<Employee> _employees = new List<Employee>();

        public EmployeeService()
        {
            LoadEmployeesFromJsonFile();
        }

        private void LoadEmployeesFromJsonFile()
        {
            try
            {
                var content = _fileService.GetContentFromFile();
                if (!string.IsNullOrEmpty(content))
                {
                    _employees.Clear();
                    _employees.AddRange(JsonConvert.DeserializeObject<List<Employee>>(content));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                LoadEmployeesFromJsonFile(); // Ladda anställda från filen för att säkerställa aktuell data

                Debug.WriteLine($"Trying to add employee with ID: {employee.Id}");

                if (!_employees.Any(e => e.Id == employee.Id))
                {
                    _employees.Add(employee);
                    _fileService.SaveContentToFile(JsonConvert.SerializeObject(_employees));
                    Debug.WriteLine("Employee added successfully!");
                }
                else
                {
                    throw new InvalidOperationException("An employee with the same ID already exists.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public bool RemoveEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                return true;
            }

            Console.WriteLine("Error, try again");
            return false;
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(x => x.Id == employee.Id);
            existingEmployee?.UpdateDetails(employee);
        }

        public List<Employee> GetEmployees() => _employees;

        public Employee GetEmployee(int id)
        {
            try
            {
                var employee = _employees.FirstOrDefault(e => e.Id == id);
                return employee;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }

    public static class EmployeeExtensions
    {
        public static void UpdateDetails(this Employee existingEmployee, Employee newEmployee)
        {
            existingEmployee.Name = newEmployee.Name;
            existingEmployee.Position = newEmployee.Position;
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
