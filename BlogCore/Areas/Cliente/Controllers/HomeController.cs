using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogCore.Models;
using BlogCore.AccesosDatos.Data.Repository;
using BlogCore.Models.ViewModels;

namespace BlogCore.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _ContenedorTrabajo;

        public HomeController(IContenedorTrabajo ContenedorTrabajo)
        {
            _ContenedorTrabajo = ContenedorTrabajo;
        }

        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                Slider =  _ContenedorTrabajo.Slider.GetAll(),
                ListaArticulos = _ContenedorTrabajo.Articulo.GetAll()
            };


            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            var articuloDesdeDb = _ContenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == id);
            return View(articuloDesdeDb);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
