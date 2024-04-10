namespace NukeTest.Models;

public class User
{
    private string Name { get; set; }
    private char Gender { get; set; }
    private string Address { get; set; }
    private string Surname { get; set; }

    public User(string name, string surname, string address)
    {
        Name = name;
        Address = address;
        Surname = surname;
        Gender = 'M';
    }
}