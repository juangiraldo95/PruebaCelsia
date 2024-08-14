using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaCelsia.Data;

namespace PruebaCelsia.Controllers
{
   public class FacturasController : Controller
   {
        private readonly BaseContext _context;

        public FacturasController(BaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Facturas.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id){
            return View(await _context.Facturas.FirstOrDefaultAsync(m => m.Id == id));
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("NumeroFactura, PeriodoFacturacion, MontoFacturado, MontoPagado, TransaccionId")] Factura fact)
        {
            if (ModelState.IsValid)
            {
                // Verifica si el TransaccionId existe en la base de datos
                var transaccion = await _context.Transacciones.FindAsync(fact.TransaccionId);
                
                if (transaccion == null)
                {
                    ModelState.AddModelError("TransaccionId", "La transacción seleccionada no existe.");
                    return View(fact);
                }

                try
                {
                    // Guarda la nueva factura en la base de datos
                    _context.Facturas.Add(fact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar la factura: " + ex.Message);
                }
            }

            return View(fact);
        }
        public async Task<IActionResult> Delete (int? id){
            //Buscamos la base de datos
            var user = await _context.Facturas.FindAsync(id);
            _context.Facturas.Remove(user);
            await _context.SaveChangesAsync();
            //Redirecciona a la vista
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Facturas.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroFactura, PeriodoFacturacion, MontoFacturado, MontoPagado, TransaccionId ")] Factura fact)
        {
            if (id != fact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(fact);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(fact);
        }
   }
}
