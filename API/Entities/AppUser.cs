namespace API.Entities;

public class AppUser
{
    public int Id { get; set; } // EF uses the field called "Id" as the primary key of the table
    public string UserName { get; set; }
}
