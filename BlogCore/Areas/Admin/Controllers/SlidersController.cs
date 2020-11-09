using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extention = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extention), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extention;
                    

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(slider);
        }



       public IActionResult Edit(int id)
        {
            Slider slider = new Slider();
            slider = _contenedorTrabajo.Slider.get(id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderdesdeDb = _contenedorTrabajo.Slider.get(slider.Id);


                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extention = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtention = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderdesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtention), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtention;
                    

                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderdesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));

            }
            return View();

        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {


            var objFromDb = _contenedorTrabajo.Slider.get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando Slider" });
            }
            _contenedorTrabajo.Slider.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrada correctamente" });

        }

        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }

        #endregion
    }
}
