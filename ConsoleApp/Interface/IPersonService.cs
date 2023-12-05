namespace ConsoleApp.Interface
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        Person GetPerson(int id);
        List<Person> GetPerson();
        bool RemovePerson(int id);
        void UpdatePerson(Person person);
    }
}