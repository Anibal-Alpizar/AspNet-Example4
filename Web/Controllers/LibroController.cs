using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        public ActionResult Index()
        {
            IEnumerable<Libro> lista = null;
            try
            {
                IServiceLibro _ServiceLibro = new ServiceLibro();
                lista = _ServiceLibro.GetLibro();
                ViewBag.title = "Lista Libros";
                //Lista autores
                IServiceAutor _ServiceAutor = new ServiceAutor();
                ViewBag.listaAutores = _ServiceAutor.GetAutors();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<Libro> lista = null;
            try
            {
                IServiceLibro _ServiceLibro = new ServiceLibro();
                lista = _ServiceLibro.GetLibro();
                

                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Libro/Details/5
        public ActionResult Details(int? id)
        {
            ServiceLibro _ServiceLibro = new ServiceLibro();
            Libro libro = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                libro = _ServiceLibro.GetLibroByID(Convert.ToInt32(id));
                if (libro == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(libro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Libro/Create
        [HttpPost]
        public ActionResult Create()
        {
            // Que recursos necesito para crear un libro
            // lista de autores
            ViewBag.idAutor = listaAutores();
            // lista de categorias
            ViewBag.idCategoria = listaCategorias();

            return View();
        }

        private SelectList listaAutores(int idAutor = 0)
        {
            IServiceAutor _serviceAutor = new ServiceAutor();
            IEnumerable<Autor> lista = _serviceAutor.GetAutors();
            return new SelectList(lista, "IdAutor", "Nombre", idAutor);
        }

        private MultiSelectList listaCategorias(ICollection<Categoria> categorias = null)
        {
            IServiceCategoria _serviceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> lista = _serviceCategoria.GetCategoria();
            // seleccionar categorias
            int[] listaCategoriasSelect = null;
            if (categorias != null)
            {
                listaCategoriasSelect = categorias.Select(c => c.IdCategoria).ToArray();
            }
            return new SelectList(lista, "IdCategoria", "Nombre", listaCategoriasSelect);
        }


        // POST: Libro/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Libro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Libro/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Libro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Libro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
