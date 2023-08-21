using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Sistema_Inventario.Models;
using System;
using System.Data;
using static Sistema_Inventario.Datos.ApplicationDbContext;
using static Sistema_Inventario.Models.Persona;

namespace Sistema_Inventario.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContexto _context;

        public ClienteController(ApplicationDbContexto context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.Where(c => c.estado == true).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TiposCedula = Enum.GetValues(typeof(TipoCedula)).Cast<TipoCedula>();
            ViewBag.TipoSexo = Enum.GetValues(typeof(SexoEnum)).Cast<SexoEnum>();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            try
            {
                var existe = await _context.Clientes.FindAsync(cliente.Id);
                if (existe != null)
                {
                    return NotFound();
                }
                cliente.estado = true;
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pueden guardar los cambios.Vuelva a intentarlo y, si el problema persiste, consulte al administrador del sistema.");
            }

            return View(cliente);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {


            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }
            cliente.estado = true;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "No se pueden guardar los cambios.Vuelva a intentarlo y, si el problema persiste, consulte al administrador del sistema.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }



            return PartialView(cliente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(string id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                cliente.estado = !cliente.estado;
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            var provincia = _context.tbProvincias.FirstOrDefault(p => p.Cod == cliente.Provincia.ToString());
            var canton = _context.tbCantones.FirstOrDefault(c => c.ProvinciaID == cliente.Provincia.ToString() && c.Canton == cliente.Canton.ToString());
            var distrito = _context.tbDistritos.FirstOrDefault(d => d.ProvinciaID == cliente.Provincia.ToString() && d.CantonID == cliente.Canton.ToString() && d.Distrito == cliente.Distrito.ToString());
            var barrio = _context.tbBarrios.FirstOrDefault(b => b.ProvinciaID == cliente.Provincia.ToString() && b.CantonID == cliente.Canton.ToString() && b.DistritoID == cliente.Distrito.ToString() && b.Barrio == cliente.Barrio.ToString());

            ViewData["Provincia"] = provincia.Nombre;
            ViewData["Canton"] = canton.Nombre;
            ViewData["Distrito"] = distrito.Nombre;
            ViewData["Barrio"] = barrio.Nombre;
            if (cliente == null)
            {
                return NotFound();
            }

            return PartialView("Details", cliente);
        }

        [HttpGet]
        public async Task<IActionResult> ClienteEliminados()
        {
            return View(await _context.Clientes.Where(c => c.estado == false).ToListAsync());
        }
        public IActionResult GetProvincias()
        {
            var provincias = _context.tbProvincias.Select(p => new { value = p.Cod, text = p.Nombre }).ToList();
            return Json(provincias);
        }

        public IActionResult GetCantones(string provinciaId)
        {
            var cantones = _context.tbCantones
                .Where(c => c.ProvinciaID == provinciaId)
                .Select(c => new { value = c.Canton, text = c.Nombre })
                .ToList();
            return Json(cantones);
        }

        public IActionResult GetDistritos(string provinciaId, string cantonId)
        {
            var distritos = _context.tbDistritos
                .Where(d => d.ProvinciaID == provinciaId && d.CantonID == cantonId)
                .Select(d => new { value = d.Distrito, text = d.Nombre })
                .ToList();
            return Json(distritos);
        }

        public IActionResult GetBarrios(string provinciaId, string cantonId, string distritoId)
        {
            var barrios = _context.tbBarrios
                .Where(b => b.ProvinciaID == provinciaId && b.CantonID == cantonId && b.DistritoID == distritoId )
                .Select(b => new { value = b.Barrio, text = b.Nombre })
                .ToList();
            return Json(barrios);
        }
        public IActionResult GetLocationNames(string provinciaId, string cantonId, string distritoId, string barrioId)
        {
            var provincia = _context.tbProvincias.FirstOrDefault(p => p.Cod == provinciaId);
            var canton = _context.tbCantones.FirstOrDefault(c => c.ProvinciaID == provinciaId && c.Canton == cantonId);
            var distrito = _context.tbDistritos.FirstOrDefault(d => d.ProvinciaID == provinciaId && d.CantonID == cantonId && d.Distrito == distritoId);
            var barrio = _context.tbBarrios.FirstOrDefault(b => b.ProvinciaID == provinciaId && b.CantonID == cantonId && b.DistritoID == distritoId && b.Barrio == barrioId);

            var result = new
            {
                Provincia = provincia.Nombre,
                Canton = canton.Nombre,
                Distrito = distrito.Nombre,
                Barrio = barrio.Nombre
            };

            return Json(result);
        }


    }
}
