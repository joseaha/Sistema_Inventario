using Microsoft.AspNetCore.Mvc;
using Sistema_Inventario.Datos;
using static Sistema_Inventario.Datos.ApplicationDbContext;

namespace Sistema_Inventario.Controllers
{
    public class PersonaController : Controller
    {
        private readonly ApplicationDbContexto _context;

        public PersonaController(ApplicationDbContexto context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
