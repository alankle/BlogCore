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
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {

        private readonly ApplicationDbContext _Db;

        public ArticuloRepository(ApplicationDbContext db):base(db)
        {
            _Db = db; 
        }
        
        

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _Db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;

            _Db.SaveChanges();



        }


    }
}