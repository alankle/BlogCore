using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesosDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _Db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }

        public void Update(Slider slider)
        {
            var objDesdeDb = _Db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
            

            _Db.SaveChanges();
        }
    }
}
