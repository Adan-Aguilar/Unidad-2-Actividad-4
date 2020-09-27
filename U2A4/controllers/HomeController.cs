using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2A4.Models;

namespace U2A4.controllers
{

   
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Peliculas")]
        public IActionResult Peliculas()
        {
            pixarContext context = new pixarContext();
            var peliculas = context.Pelicula.OrderBy(x => x.Nombre);
            return View(peliculas);
        }


        [Route("Peliculas/{id}")]
        public IActionResult InfoPelicula(string id)
        {
            pixarContext context = new pixarContext();
            var nombre = id.Replace("-", " ").ToUpper();
            var pelicula = context.Pelicula.Include(x => x.Apariciones).FirstOrDefault(x => x.Nombre.ToLower() == nombre);

            if (pelicula==null)
            {
                return RedirectToAction("peliculas");
            }

            else
            {
                InformacionPeliculasViewModel vm = new InformacionPeliculasViewModel();
                vm.Id = pelicula.Id;
                vm.Nombre = pelicula.Nombre;
                vm.FechaEstreno = pelicula.FechaEstreno;
                vm.NombreOriginal = pelicula.NombreOriginal;
                vm.Descripcion = pelicula.Descripción;

                var aparicion = context.Apariciones.Include(x => x.IdPersonajeNavigation).Include(x => x.IdPeliculaNavigation).Where(x => (x.IdPelicula == pelicula.Id)).Select(x => x);
                vm.Apariciones = aparicion;

                return View(vm);
            }
            
        }

        [Route("cortos")]
        public IActionResult Cortos()
        {
            pixarContext context = new pixarContext();
            CortometrajeViewModel vm = new CortometrajeViewModel();
            var categoria = context.Categoria.Include(x => x.Cortometraje).OrderBy(x => x.Nombre);
            vm.Categoria = categoria;
            return View(vm);
        }

        [Route("cortos/{id}")]
        
        public IActionResult InfoCorto(string id)
        {
            pixarContext context = new pixarContext();
            var nombre = id.Replace("-", " ").ToUpper();
            var corto= context.Cortometraje.FirstOrDefault(x => x.Nombre.ToLower() == nombre);

            if (corto==null)
            {
                return RedirectToAction("Cortometrajes");
            }

            else { return View(corto); }
        }



    }
}
