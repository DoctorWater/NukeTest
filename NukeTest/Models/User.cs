namespace NukeTest.Models;

public class User
{
    private string Name { get; set; }
    private char Gender { get; set; }

    public User(string name)
    {
        Name = name;
        Gender = 'M';
    }
}