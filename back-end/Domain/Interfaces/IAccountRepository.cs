using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talos.API.User;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> Create(ApplicationUser usuario, string password);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<IList<Claim>> GetClaims(ApplicationUser usuario);
        Task<ApplicationUser> SignIn(Credenciales credenciales);
        Task<ApplicationUser> Update(UsuarioDTO usuario);
        Task<bool> Delete(string id);
        IQueryable<UsuarioDTO> getUsers();
        Task<bool> MakeAdmin(string id);
        Task<bool> DeleteAdmin(string id);
        List<Genero> getGenders();
        List<Pais> getCountries();
        Task<List<UsuarioDTO>> getRoles(List<UsuarioDTO> usuarios);
        Task<ApplicationUser> getUser(string id);
        Task<string> getUserID(string email);
    }
}
