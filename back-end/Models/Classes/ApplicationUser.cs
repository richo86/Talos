using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Talos.API.User
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string FirstLastName { get; set; }
        [StringLength(50)]
        public string SecondLastName { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string UserIP { get; set; }
    }
}
