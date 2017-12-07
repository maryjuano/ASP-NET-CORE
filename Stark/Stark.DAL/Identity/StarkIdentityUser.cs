using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Stark.DAL
{
    [DataContract]
    public class StarkIdentityUser : IdentityUser
    {
        [DataMember(Name = "Id")]
        public override string Id { get => base.Id; set => base.Id = value; }      
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
