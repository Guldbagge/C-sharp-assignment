using System;

namespace ConsoleApp.Service
{
    public class MenuService
    {
        // PersonService instance to handle person-related functionalities
        private readonly PersonService _personService = new PersonService();

        private void DisplayMainMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Add Person");
            Console.WriteLine("2. Remove Person");
            Console.WriteLine("3. Update Person");
            Console.WriteLine("4. Get All Persons, Only Name");
            Console.WriteLine("5. Get One Person's Detailed Information");
            Console.WriteLine("6. Exit");
            Console.WriteLine("-------------------------------");
            Console.Write("Enter your choice: ");
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                DisplayMainMenuOptions();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddPerson();
                        break;
                    case "2":
                        RemovePerson();
                        break;
                    case "3":
                        UpdatePerson();
                        break;
                    case "4":
                        GetAllPersons();
                        break;
                    case "5":
                        GetPersonDetails();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        private void AddPerson()

        {   // Adds a new person after taking user input
            var newPerson = GetPersonDetailsFromUser(null!);

            if (newPerson != null)
            {
                _personService.AddPerson(newPerson);
                Console.WriteLine("Person added successfully.");
            }
        }

        private void UpdatePerson()
        {
            Console.WriteLine("Enter person email to update:");
            string personEmailInput = Console.ReadLine()!;

            var personToUpdate = _personService.GetPerson(personEmailInput);

            if (personToUpdate == null)
            {
                Console.WriteLine("Person not found.");
                return;
            }

            Console.WriteLine("Do you want to update the email? (Y/N)");
            var response = Console.ReadLine()?.ToLower();

            if (response == "y")
            {
                UpdatePersonEmail(personToUpdate);
                Console.WriteLine("Email updated successfully.");
            }

            UpdatePersonDetails(personToUpdate);
            _personService.UpdatePerson(personToUpdate);
            Console.WriteLine("Person details updated successfully.");
        }

        private void UpdatePersonEmail(Person person)
        {
            // Helper method to update a person's email
            Console.WriteLine("Enter new person email:");
            var newEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEmail))
            {
                person.Email = newEmail;
            }
        }

        private void UpdatePersonDetails(Person person)
        {
            // Helper method to update a person's details
            Console.WriteLine("Enter person first name:");
            person.FirstName = Console.ReadLine()!;

            Console.WriteLine("Enter person last name:");
            person.LastName = Console.ReadLine()!;

            Console.WriteLine("Enter person street name:");
            person.StreetName = Console.ReadLine()!;

            Console.WriteLine("Enter person street number:");
            person.StreetNumber = Convert.ToInt32(Console.ReadLine()!);

            Console.WriteLine("Enter person zip code:");
            person.ZipCode = Convert.ToInt32(Console.ReadLine()!);

            Console.WriteLine("Enter person city:");
            person.City = Console.ReadLine()!;
        }

        private void RemovePerson()
        {
            Console.WriteLine("Enter person email to remove:");
            string personEmailInput = Console.ReadLine()!;

            if (!_personService.RemovePerson(personEmailInput))
            {
                Console.WriteLine("Person not found. Cannot remove.");
            }
            else
            {
                Console.WriteLine("Person removed successfully.");
            }
        }

        private void GetAllPersons()
        {
            var persons = _personService.GetPersons();
            Console.Clear();
            foreach (var person in persons)
            {
                DisplayPersonName(person);
            }
        }

        private void GetPersonDetails()
        {
            Console.WriteLine("Enter person email:");
            string emailInput = Console.ReadLine()!;

            var result = _personService.GetPerson(emailInput);

            if (result != null)
            {
                DisplayPersonInformation(result);
            }
            else
            {
                Console.WriteLine("Person not found.");
            }
        }

        private static Person GetPersonDetailsFromUser(string email)
        {   
            // Helper method to get person details from user input
            var person = new Person();

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Enter person email:");
                person.Email = Console.ReadLine()!;
            }
            else
            {
                // If an email was passed, use it directly
                person.Email = email;
            }

            Console.WriteLine("Enter person first name:");
            person.FirstName = Console.ReadLine()!;

            Console.WriteLine("Enter person last name:");
            person.LastName = Console.ReadLine()!;

            Console.WriteLine("Enter person street name:");
            person.StreetName = Console.ReadLine()!;

            Console.WriteLine("Enter person street number:");
            person.StreetNumber = Convert.ToInt32(Console.ReadLine()!);

            Console.WriteLine("Enter person zip code:");
            person.ZipCode = Convert.ToInt32(Console.ReadLine()!);

            Console.WriteLine("Enter person city:");
            person.City = Console.ReadLine()!;

            return person;
        }

        private void DisplayPersonInformation(Person person)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Person Information:\n----------------------------------");
            Console.WriteLine($"Email: {person.Email}");
            Console.WriteLine($"First Name: {person.FirstName}");
            Console.WriteLine($"Last Name: {person.LastName}");
            Console.WriteLine($"Address:\n\tStreet Name: {person.StreetName}");
            Console.WriteLine($"\tStreet Number: {person.StreetNumber}");
            Console.WriteLine($"\tZip Code: {person.ZipCode}");
            Console.WriteLine($"\tCity: {person.City}");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("\n");
        }

        private void DisplayPersonName(Person person)
        {
            Console.WriteLine($"{person.FirstName} {person.LastName}");
            Console.WriteLine("----------------------------------");
        }
    }
}
