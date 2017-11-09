using System;

namespace GSC.Models
{
    public class Client
    {
       public int Id { get; set; }
       public string Username { get; set; }
       public string Password { get; set; }
       public string FirstName { get; set;}
       public string LastName { get; set; }
       public string MiddleName { get; set; }
    }
}