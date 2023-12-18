/// <summary>
/// Interface for a person.
/// </summary>
public interface IPerson
{
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// Gets or sets the street name.
    /// </summary>
    string StreetName { get; set; }

    /// <summary>
    /// Gets or sets the street number.
    /// </summary>
    int StreetNumber { get; set; }

    /// <summary>
    /// Gets or sets the zip code.
    /// </summary>
    int ZipCode { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    string City { get; set; }

    /// <summary>
    /// Gets or sets the phonenumber.
    /// </summary> 

    int PhoneNumber { get; set; }
}
