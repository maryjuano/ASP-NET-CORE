using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Stark.DAL.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [DataMember(Name = "FirstName")]
        public string IdentityId { get; set; }
        public string UserName { get; set; }
        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "LastName")]
        public string LastName { get; set; }
        [DataMember(Name = "MiddleName")]
        public string MiddleName { get; set; }
        [DataMember(Name = "FullName")]
        public string FullName { get; set; }
    }
}
