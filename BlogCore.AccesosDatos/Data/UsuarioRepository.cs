using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesosDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDB.LockoutEnd = DateTime.Now.AddYears(100);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDB.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}