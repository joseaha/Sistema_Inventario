using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Inventario.Datos;
using Sistema_Inventario.Models;
using System.Diagnostics;
using static Sistema_Inventario.Datos.ApplicationDbContext;

namespace Sistema_Inventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContexto _context;

        public HomeController(ApplicationDbContexto contexto)
        {
            _context = contexto;
        }

        public IActionResult Index()
        {
            int totalCategorias = _context.categorias.Count();
            int totalProductos = _context.productos.Count();
            ViewData["TotalCategorias"] = totalCategorias;
            ViewData["TotalProductos"] = totalProductos;
            var categoriaConMasProductos =_context.categorias
            .Include(c => c.Productos)
            .OrderByDescending(c => c.Productos.Count).FirstOrDefault();
            ViewData["CantidadProductosCategoriaConMasProductos"] = categoriaConMasProductos != null ? categoriaConMasProductos.Nombre : "N/A";
            ViewData["cantidad"] = categoriaConMasProductos != null ? categoriaConMasProductos.Productos.Count() : "0";
            var ultimosTresProductos = _context.productos.OrderByDescending(p => p.Id).Where(p => p.Estado == true).Take(3).ToList();
            ViewData["UltimosTresProductos"] = ultimosTresProductos;
            var ultimasTresCategorias = _context.categorias.OrderByDescending(p => p.Id).Where(p => p.Estado == true).Take(3).ToList();
            ViewData["ultimasTresCategorias"] = ultimasTresCategorias;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}