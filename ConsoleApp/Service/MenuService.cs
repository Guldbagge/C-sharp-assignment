using System;

namespace ConsoleApp.Service
{
    public class MenuService
    {
        // En instans av EmployeeService används för att hantera anställda
        private readonly EmployeeService _employeeService = new EmployeeService();

        public MenuService(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Visar huvudmenyvalen
        private void DisplayMainMenuOptions()
        {
            Console.WriteLine("1. Add employee");
            Console.WriteLine("2. Remove employee");
            Console.WriteLine("3. Update employee");
            Console.WriteLine("4. Get employees");
            Console.WriteLine("5. Exit");
        }

        // Metod för att visa huvudmenyn och hantera användarinput
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear(); // Rensar konsolfönstret
                DisplayMainMenuOptions(); // Visar huvudmenyvalen
                var option = Console.ReadLine(); // Läser användarens val

                switch (option)
                {
                    case "1":
                        AddEmployee(); // Lägger till en ny anställd
                        break;
                    case "2":
                        RemoveEmployee(); // Tar bort en anställd
                        break;
                    case "3":
                        UpdateEmployee(); // Uppdaterar anställdsinformation
                        break;
                    case "4":
                        GetEmployees(); // Hämtar information om anställda
                        break;
                    case "5":
                        return; // Avslutar programmet
                    default:
                        Console.WriteLine("Invalid option"); // Felmeddelande vid ogiltigt val
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }


        // Lägger till en ny anställd
        //private void AddEmployee()
        //{
        //    var employee = GetEmployeeDetailsFromUser(); // Hämtar detaljer om en ny anställd från användaren
        //    _employeeService.AddEmployee(employee); // Lägger till den nya anställde i systemet
        //}

        private void AddEmployee()
        {
            var employees = _employeeService.GetEmployees(); // Hämtar befintliga anställda
            var employee = GetEmployeeDetailsFromUser(); // Hämtar detaljer om en ny anställd från användaren

            var existingEmployee = employees.FirstOrDefault(e => e.Id == employee.Id); // Kontrollera om det angivna ID:t redan finns

            if (existingEmployee != null)
            {
                Console.WriteLine("An employee with the same ID already exists. Please enter a different ID.");
            }
            else
            {
                _employeeService.AddEmployee(employee); // Lägger till den nya anställde i systemet
            }
        }


        // Tar bort en anställd
        private void RemoveEmployee()
        {
            Console.WriteLine("Enter employee ID to remove:");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                var success = _employeeService.RemoveEmployee(employeeId); // Försöker ta bort den anställde baserat på ID
                Console.WriteLine(success ? "Employee removed successfully." : "Employee could not be removed.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid employee ID.");
            }
        }

        // Uppdaterar information om en anställd
        private void UpdateEmployee()
        {
            Console.WriteLine("Enter employee ID to update:");
            if (int.TryParse(Console.ReadLine(), out int employeeId))
            {
                var employee = GetEmployeeDetailsFromUser(); // Hämtar nya detaljer för att uppdatera den anställde
                employee.Id = employeeId; // Sätter ID för den anställde som ska uppdateras
                _employeeService.UpdateEmployee(employee); // Uppdaterar den anställde
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid employee ID.");
            }
        }

        // Hämtar information om anställda
        private void GetEmployees()
        {
            Console.WriteLine("Enter employee ID:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var result = _employeeService.GetEmployee(id); // Hämtar den anställde baserat på ID
                if (result != null)
                {
                    DisplayEmployeeInformation(result); // Visar information om den anställde
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid employee ID.");
            }
        }

        // Hämtar information från användaren för att skapa en ny anställd
        private Employee GetEmployeeDetailsFromUser()
        {
            var employee = new Employee();
            Console.WriteLine("Enter employee ID:");
            employee.Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter employee Name:");
            employee.Name = Console.ReadLine();

            Console.WriteLine("Enter employee position:");
            employee.Position = Console.ReadLine();

            return employee;
        }

        // Visar information om en specifik anställd
        private void DisplayEmployeeInformation(Employee employee)
        {
            Console.WriteLine($"Employee Information:\nID: {employee.Id}\nName: {employee.Name}\nPosition: {employee.Position}\n");
        }
    }
}
