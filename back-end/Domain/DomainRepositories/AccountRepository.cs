using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Models.DTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talos.API.User;
using System;
using System.Linq;
using Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace Domain.DomainRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public async Task<bool> Create(ApplicationUser usuario, string password)
        {
            usuario.UserName = usuario.Email;
            usuario.Id = Guid.NewGuid().ToString();

            var respuesta = await userManager.CreateAsync(usuario, password);

            if (respuesta.Succeeded)
            {
                return respuesta.Succeeded;
            }
            else
                return false;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<ApplicationUser> Update(UsuarioDTO usuario)
        {
            var user = await userManager.FindByIdAsync(usuario.Id);

            if (user != null)
            {
                user.PhoneNumber = usuario.PhoneNumber;
                user.UserName = usuario.Email;
                user.FirstName = usuario.FirstName;
                user.MiddleName = usuario.MiddleName;
                user.FirstLastName = usuario.FirstLastName;
                user.SecondLastName = usuario.SecondLastName;
                user.Gender = usuario.Gender;
                user.Country = usuario.Country;
                user.Address = usuario.Address;

                if(usuario.Password != "abc")
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user,usuario.Password);

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return user;
            }

            return new ApplicationUser();
        }

        public async Task<IList<Claim>> GetClaims(ApplicationUser usuario)
        {
            return await userManager.GetClaimsAsync(usuario);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> SignIn(Credenciales credenciales)
        {
            var respuesta = await signInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password, isPersistent: false, lockoutOnFailure: false);

            if (respuesta.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(credenciales.Email);
                if (user != null)
                    return user;
            }
            return new ApplicationUser { };
        }

        public IQueryable<UsuarioDTO> getUsers()
        {
            return userManager.Users.Select(x=> new UsuarioDTO() { 
                Id = x.Id,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                FirstLastName = x.FirstLastName,
                SecondLastName = x.SecondLastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Country = x.Country,
                Gender = x.Gender
            }).AsQueryable();
        }

        public async Task<List<UsuarioDTO>> getRoles(List<UsuarioDTO> usuarios)
        {
            foreach (var item in usuarios)
            {
                string roles = null;
                var usuario = await userManager.FindByIdAsync(item.Id);

                var claims = await userManager.GetClaimsAsync(usuario);

                foreach (var claim in claims)
                {
                    if (roles == null)
                        roles = roles + claim.Value;
                    else
                        roles = roles + ", " + claim.Value;
                }

                item.Roles = roles;
            }

            return usuarios;
        }

        public async Task<ApplicationUser> getUser(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<string> getUserID(string email)
        {
            var result = await userManager.FindByEmailAsync(email);

            if (result != null)
                return result.Id;
            else
                return string.Empty;
        }

        public async Task<bool> MakeAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var claimsDB = await GetClaims(user);

                if (claimsDB.FirstOrDefault(x => x.Value.Equals("admin")) == null)
                {
                    var result = await userManager.AddClaimAsync(user, new Claim("role", "admin"));

                    if (result.Succeeded)
                        return true;
                }
                else return true;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await userManager.RemoveClaimAsync(user, new Claim("role", "admin"));

                if (result.Succeeded)
                    return true;
            }
            return false;
        }

        public List<Genero> getGenders()
        {
            var genders = context.Genero.Where(x => x.Descripcion != null).ToList();

            return genders;
        }

        public List<Pais> getCountries()
        {
            return context.Pais.Where(x => x.Nombre != null).ToList();
        }
    }
}
