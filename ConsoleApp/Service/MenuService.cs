using System;

namespace ConsoleApp.Service
{
    public class MenuService
    {
        // En instans av personService används för att hantera anställda
        private readonly PersonService _personService = new PersonService();

        public MenuService(PersonService personService)
        {
            _personService = personService;
        }

        // Visar huvudmenyvalen
        private void DisplayMainMenuOptions()
        {
            Console.WriteLine("1. Add person");
            Console.WriteLine("2. Remove person");
            Console.WriteLine("3. Update person");
            Console.WriteLine("4. Get persons");
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
                        AddPerson(); // Lägger till en ny anställd
                        break;
                    case "2":
                        RemovePerson(); // Tar bort en anställd
                        break;
                    case "3":
                        UpdatePerson(); // Uppdaterar anställdsinformation
                        break;
                    case "4":
                        GetPersons(); // Hämtar information om anställda
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
        //private void Addperson()
        //{
        //    var person = GetpersonDetailsFromUser(); // Hämtar detaljer om en ny anställd från användaren
        //    _personService.Addperson(person); // Lägger till den nya anställde i systemet
        //}

        private void AddPerson()
        {
            var persons = _personService.GetPersons(); // Hämtar befintliga anställda
            var person = GetPersonDetailsFromUser(); // Hämtar detaljer om en ny anställd från användaren

            var existingPerson = persons.FirstOrDefault(e => e.Id == person.Id); // Kontrollera om det angivna ID:t redan finns

            if (existingPerson != null)
            {
                Console.WriteLine("An person with the same ID already exists. Please enter a different ID.");
            }
            else
            {
                _personService.AddPerson(person); // Lägger till den nya anställde i systemet
            }
        }


        // Tar bort en anställd
        private void RemovePerson()
        {
            Console.WriteLine("Enter person ID to remove:");
            if (int.TryParse(Console.ReadLine(), out int personId))
            {
                var success = _personService.RemovePerson(personId); // Försöker ta bort den anställde baserat på ID
                Console.WriteLine(success ? "Person removed successfully." : "Person could not be removed.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid person ID.");
            }
        }

        // Uppdaterar information om en anställd
        private void UpdatePerson()
        {
            Console.WriteLine("Enter person ID to update:");
            if (int.TryParse(Console.ReadLine(), out int personId))
            {
                var person = GetPersonDetailsFromUser(); // Hämtar nya detaljer för att uppdatera den anställde
                person.Id = personId; // Sätter ID för den anställde som ska uppdateras
                _personService.UpdatePerson(person); // Uppdaterar den anställde
                Console.WriteLine("Person updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid person ID.");
            }
        }

        // Hämtar information om anställda
        private void GetPersons()
        {
            Console.WriteLine("Enter person ID:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var result = _personService.GetPerson(id); // Hämtar den anställde baserat på ID
                if (result != null)
                {
                    DisplayPersonInformation(result); // Visar information om den anställde
                }
                else
                {
                    Console.WriteLine("Person not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid person ID.");
            }
        }

        // Hämtar information från användaren för att skapa en ny anställd
        private Person GetPersonDetailsFromUser()
        {
            var person = new Person();
            Console.WriteLine("Enter person ID:");
            person.Id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter person Name:");
            person.Name = Console.ReadLine();

            Console.WriteLine("Enter person position:");
            person.Position = Console.ReadLine();

            return person;
        }

        // Visar information om en specifik anställd
        private void DisplayPersonInformation(Person person)
        {
            Console.WriteLine($"Person Information:\nID: {person.Id}\nName: {person.Name}\nPosition: {person.Position}\n");
        }
    }
}
