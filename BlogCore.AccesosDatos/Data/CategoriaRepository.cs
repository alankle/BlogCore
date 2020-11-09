using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesosDatos.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {

        private readonly ApplicationDbContext _Db;

        public CategoriaRepository(ApplicationDbContext db):base(db)
        {
            _Db = db; 
        }
        
        public IEnumerable<SelectListItem> GetListCategorias()
        {
            return _Db.Categoria.Select(i => new SelectListItem()
            {
                    Text = i.Nombre, 
                    Value = i.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _Db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            _Db.SaveChanges();



        }


    }
}