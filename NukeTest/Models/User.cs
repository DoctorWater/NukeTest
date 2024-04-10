namespace NukeTest.Models;

public class User
{
    private string Name { get; set; }
    private char Gender { get; set; }
    private string Address { get; set; }

    public User(string name, string address)
    {
        Name = name;
        Address = address;
        Gender = 'M';
    }
}