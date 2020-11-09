using BlogCore.AccesoDatos.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesosDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadorDB(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                //throw;
            }
            if (_db.Roles.Any(ro => ro.Name == Cnt.Admin)) return;
            _roleManager.CreateAsync(new IdentityRole(Cnt.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Cnt.Usuario)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName ="alankle@gmail.com",
                Email = "alankle@gmail.com",
                EmailConfirmed = true,
                Nombre = "alankle"

            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser usuario = _db.ApplicationUser
                .Where(us => us.Email == "alankle@gmail.com")
                .FirstOrDefault();

            _userManager.AddToRoleAsync(usuario,Cnt.Admin).GetAwaiter().GetResult();
        }
    }
}
