using System;

namespace ConsoleApp.Service
{
    public class MenuService
    {
        private readonly PersonService _personService = new PersonService();

        private void DisplayMainMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Add Person");
            Console.WriteLine("2. Remove Person");
            Console.WriteLine("3. Update Person");
            Console.WriteLine("4. Get All Persons");
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
        {
            Console.WriteLine("Enter person email:");
            var emailInput = Console.ReadLine();

            var existingPerson = _personService.GetPerson(emailInput!);
            if (existingPerson != null)
            {
                Console.WriteLine("A person with the same email already exists.");
                return;
            }

            var person = GetPersonDetailsFromUser(emailInput!);
            _personService.AddPerson(person);
        }

        private Person GetPersonDetailsFromUser(string email)
        {
            var person = new Person { Email = email };

            Console.WriteLine("Enter person first name:");
            person.FirstName = Console.ReadLine()!;

            Console.WriteLine("Enter person last name:");
            person.LastName = Console.ReadLine()!;

            Console.WriteLine("Enter person street name:");
            person.StreetName = Console.ReadLine()!;

            Console.WriteLine("Enter person street number:");

            person.StreetNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter person zip code:");
            person.ZipCode = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter person city:");
            person.City = Console.ReadLine()!;

            return person;
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

        private void UpdatePerson()
        {
            Console.WriteLine("Enter person email to update:");
            string personEmailInput = Console.ReadLine()!;

            var person = _personService.GetPerson(personEmailInput);

            if (person == null)
            {
                Console.WriteLine("Person not found. Cannot update.");
                return;
            }

            var updatedPerson = GetPersonDetailsFromUser();
            updatedPerson.Email = personEmailInput;

            _personService.UpdatePerson(updatedPerson);
            Console.WriteLine("Person updated successfully.");
        }


        private void GetAllPersons()
        {
            var persons = _personService.GetPersons();
            foreach (var person in persons)
            {
                DisplayPersonInformation(person);
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

        private Person GetPersonDetailsFromUser()
        {
            var person = new Person();
            Console.WriteLine("Enter person email:");
            person.Email = Console.ReadLine()!;

            Console.WriteLine("Enter person first name:");
            person.FirstName = Console.ReadLine()!;

            Console.WriteLine("Enter person last name:");
            person.LastName = Console.ReadLine()!;

            Console.WriteLine("Enter person street name:");
            person.StreetName = Console.ReadLine()!;

            Console.WriteLine("Enter person street number:");
            if (int.TryParse(Console.ReadLine(), out int streetNumber))
            {
                person.StreetNumber = streetNumber;
            }
            else
            {
                Console.WriteLine("Invalid street number. Please enter a valid number.");
            }

            Console.WriteLine("Enter person zip code:");
            if (int.TryParse(Console.ReadLine(), out int zipCode))
            {
                person.ZipCode = zipCode;
            }
            else
            {
                Console.WriteLine("Invalid zip code. Please enter a valid number.");
            }

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



    }
}
