using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _db;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext db)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.get(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(categoria);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {


            var objFromDb = _contenedorTrabajo.Categoria.get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando categoria" });
            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "categoria borrada correctamente" });

        }

        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            var categorias = _db.Categoria.FromSqlRaw<Categoria>("spGetCategorias").ToList();
            var oldCategorias = _contenedorTrabajo.Categoria.GetAll();
            return Json(new { data = categorias });
        }
        
        #endregion





    }
}
