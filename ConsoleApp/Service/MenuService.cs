using System;

namespace ConsoleApp.Service
{
    public class MenuService
    {
        private readonly PersonService _personService = new PersonService();

        public MenuService(PersonService personService)
        {
            _personService = personService;
        }

        private void DisplayMainMenuOptions()
        {
            Console.WriteLine("1. Add person");
            Console.WriteLine("2. Remove person");
            Console.WriteLine("3. Update person");
            Console.WriteLine("4. Get all persons in the list");
            Console.WriteLine("5. Get one persons detail information");
            Console.WriteLine("6. Exit");
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
                        GetPersons();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void AddPerson()
        {
            var persons = _personService.GetPersons();
            var person = GetPersonDetailsFromUser();

            var existingPerson = persons.FirstOrDefault(e => e.Email == person.Email);

            if (existingPerson != null)
            {
                Console.WriteLine("An person with the same Email already exists. Please enter a different ID.");
            }
            else
            {
                _personService.AddPerson(person);
            }
        }

        private void RemovePerson()
        {
            Console.WriteLine("Enter person email to remove:");
            string personEmailInput = Console.ReadLine();

            var existingPerson = _personService.GetPerson(personEmailInput);

            if (existingPerson == null)
            {
                Console.WriteLine("Person with the provided email does not exist. Cannot remove.");
                return;
            }

            _personService.RemovePerson(personEmailInput);

            Console.WriteLine("Person removed successfully.");
        }

        private void UpdatePerson()
        {
            Console.WriteLine("Enter person email to update:");
            string personEmailInput = Console.ReadLine();

            var person = GetPersonDetailsFromUser();
            person.Email = personEmailInput;
            _personService.UpdatePerson(person);
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

        private void GetPersons()
        {
            Console.WriteLine("Enter person email:");
            string emailInput = Console.ReadLine();

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
            person.Email = Console.ReadLine();

            Console.WriteLine("Enter person first name:");
            person.FirstName = Console.ReadLine();

            Console.WriteLine("Enter person last name:");
            person.LastName = Console.ReadLine();

            Console.WriteLine("Enter person street name:");
            person.StreetName = Console.ReadLine();

            Console.WriteLine("Enter person street number:");
            
            person.StreetNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter person zip code:");
            person.ZipCode = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter person city:");
            person.City = Console.ReadLine();

            return person;
        }

        private void DisplayPersonInformation(Person person)
        {
            Console.WriteLine($"Person Information:\nEmail: {person.Email}\nFirst name: {person.FirstName}\nLast name: {person.LastName}\nStreeat name: {person.StreetName}\nStreeat number: {person.StreetNumber}\nZip code: {person.ZipCode}\nCity: {person.City}\n");
        }
    }
}
