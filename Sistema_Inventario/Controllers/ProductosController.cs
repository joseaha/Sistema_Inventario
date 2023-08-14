using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_Inventario.Models;
using System.Diagnostics.Contracts;
using System.Drawing.Printing;
using static Sistema_Inventario.Datos.ApplicationDbContext;

namespace Sistema_Inventario.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContexto _context;

        public ProductosController(ApplicationDbContexto context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.productoModels.Include(p => p.Categoria)
                .Where(p => p.Estado==true)
                .ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> ProductosEliminados()
        {
            return View(await _context.productoModels.Where(c => c.Estado == false).ToListAsync());
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

                var producto = await _context.productoModels.FindAsync(id);

                if (producto == null)
                {
                    return NotFound();
                }

                producto.Estado = !producto.Estado; // Cambia el estado actual

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Index), new { saveChangesError = true });
            }
        }

        [HttpGet]
        public IActionResult Create() {

            ViewData["CategoriaId"] = new SelectList(_context.categoriaModels.Where(n => n.Estado == true), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoModel Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Model.Estado = true;
                    _context.productoModels.Add(Model);
                    _context.SaveChanges();
                }
                //Regresa a la vista Index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }
        [HttpGet]
        public IActionResult detalle(int id)
        {

            var producto = _context.productoModels.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);

            return PartialView("detalle", producto);

        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var productoModel = await _context.productoModels.FindAsync(id);
            if (productoModel == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                productoModel.Estado = false;
                _context.productoModels.Update(productoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var producto = _context.productoModels.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
            return PartialView("delete",producto);
        }
        [HttpGet]
        public IActionResult _editarProducto(int id)
        {
            var producto = _context.productoModels.FirstOrDefault(c => c.Id == id);
            ViewData["CategoriaId"] = new SelectList(_context.categoriaModels.Where(n => n.Estado == true), "Id", "Nombre");
            return View("_Editar", producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id,ProductoModel modelo)
        {
            if (id != modelo.Id)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    modelo.Estado = true;
                    _context.Update(modelo);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception EX )
                {
                    Console.WriteLine(EX.Message);
                }
            }

            return NotFound();

        }
    }
}
