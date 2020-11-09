using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesosDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {

        private readonly ApplicationDbContext _Db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _Db = db;
            Categoria = new CategoriaRepository(_Db);
            Articulo = new ArticuloRepository(_Db);
            Slider = new SliderRepository(_Db);
            Usuario = new UsuarioRepository(_Db);

        }
        
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo{ get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public void Dispose()
        {
            _Db.Dispose();
        }

        public void Save()
        {
            _Db.SaveChanges();
        }
    }
}
