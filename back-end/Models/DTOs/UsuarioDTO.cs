using System.ComponentModel.DataAnnotations;
namespace Models.DTOs
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public string UserIP { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
