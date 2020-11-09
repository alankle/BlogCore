using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
           
             return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias()
            };
            return View(artivm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artivm)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (artivm.Articulo.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extention = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extention), FileMode.Create)) 
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    artivm.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extention;
                    artivm.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(artivm.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            artivm.ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias();
            return View(artivm);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias()
            };


            if (id != null)
            {
                artivm.Articulo = _contenedorTrabajo.Articulo.get(id.GetValueOrDefault());
            }

            return View(artivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artivm)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articulodesdeDb = _contenedorTrabajo.Articulo.get(artivm.Articulo.Id);


                if (archivos.Count()>0 )
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extention = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtention = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal,articulodesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtention), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artivm.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtention;
                    artivm.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(artivm.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    artivm.Articulo.UrlImagen = articulodesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Articulo.Update(artivm.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));

            }
            return View();
             
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));
            
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando articulo" });
            }
            _contenedorTrabajo.Articulo.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Borrado con exito" });

        }

        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        
        #endregion
    }
}
