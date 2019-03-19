using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
