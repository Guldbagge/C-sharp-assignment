

namespace Shared.Interface
{
    /// <summary>
    /// Interface for person service.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Adds a person.
        /// </summary>
        /// <param name="person">The person to add.</param>
        void AddPerson(Person person);

        /// <summary>
        /// Gets a person by email.
        /// </summary>
        /// <param name="email">The email of the person.</param>
        /// <returns>The person.</returns>
        Person GetPerson(string email);

        /// <summary>
        /// Gets all persons.
        /// </summary>
        /// <returns>A list of persons.</returns>
        List<Person> GetPersons();

        /// <summary>
        /// Removes a person by email.
        /// </summary>
        /// <param name="email">The email of the person.</param>
        /// <returns>True if the person was removed, false otherwise.</returns>
        bool RemovePerson(string email);

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="person">The person to update.</param>
        void UpdatePerson(Person person);
    }
}
