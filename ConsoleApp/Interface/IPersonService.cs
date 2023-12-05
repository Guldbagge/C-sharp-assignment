namespace ConsoleApp.Interface
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        Person GetPerson(string email);
        List<Person> GetPersons();
        bool RemovePerson(string email);
        void UpdatePerson(Person person);
    }
}
