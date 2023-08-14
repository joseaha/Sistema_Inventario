using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistema_Inventario.Datos;
using Sistema_Inventario.Models;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using static Sistema_Inventario.Datos.ApplicationDbContext;

namespace Sistema_Inventario.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContexto _context;

        public CategoriasController(ApplicationDbContexto context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.categoriaModels.Where(c => c.Estado == true).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> CategoriasEliminadas()
        {
            return View(await _context.categoriaModels.Where(c => c.Estado == false).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Restablecer(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var categoria = await _context.categoriaModels.FindAsync(id);

                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.Estado = !categoria.Estado; // Cambia el estado actual

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Index), new { saveChangesError = true });
            }
        }
        /*************
         Create
      *************/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaModel categoriaModel)
        {
            try
            {

                    categoriaModel.Estado = true;
                if (!ModelState.IsValid)
                {
                    _context.categoriaModels.Add(categoriaModel);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return NotFound();

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        /*************
          Details
       *************/
        [HttpGet]
        public IActionResult DetalleCategoria(int id)
        {
            var categoria = _context.categoriaModels.Find(id);

            return PartialView("_Detalles", categoria);

        }
        [HttpGet]
        [ActionName("Detalle")]
        public async Task<IActionResult> Detalle(int? id)
        {
            //Validamos que se ingreso un Id correcto
            if (id == null)
            {
                return NotFound();
            }
            //Si el Id Ingresado es correcto obtenemos el conctacto que se desea obtner los detalles
            var categoria = await _context.categoriaModels.FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        } /*************
          Delete
       *************/
        [HttpGet]
        public IActionResult DeleteCategoria(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = _context.categoriaModels.Find(id);
            return PartialView("_Delete", categoria);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var categoria = await _context.categoriaModels.FindAsync(id);

                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.Estado = false;


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Nombre,Descripcion")] CategoriaModel modelo)
        {
            if (id != modelo.Id)
            {
                return NotFound();
            }
            modelo.Estado = true;
            if (!ModelState.IsValid)
            {
                try
                {

                    var categoriaExistente = await _context.categoriaModels.FindAsync(id);
                    if (categoriaExistente == null)
                    {
                        return NotFound();
                    }

                    // Actualizar propiedades de la categoría existente
                    categoriaExistente.Nombre = modelo.Nombre;
                    categoriaExistente.Descripcion = modelo.Descripcion;
                    categoriaExistente.Estado = modelo.Estado; // Asegúrate de actualizar el estado

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "No se pueden guardar los cambios. " +
                                "Inténtalo de nuevo y, si el problema persiste, " +
                                "consulta con el administrador del sistema.");

                }
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult EditarCategoria(int id)
        {
            var categoriaModel = _context.categoriaModels.FirstOrDefault(c => c.Id == id);
            return PartialView("Editar", categoriaModel);
        }
        public IActionResult ObtenerProductosPorCategoria(int id)
        {
            return PartialView(_context.productoModels.Where(p => p.CategoriaId == id).ToList());

        }

    }
}

