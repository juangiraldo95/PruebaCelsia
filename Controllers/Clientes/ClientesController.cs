using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaCelsia.Data;

namespace PruebaCelsia.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BaseContext _context;

        public ClientesController(BaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View (await _context.Clientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id){
            return View(await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id));
        }

        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nombre, NumeroIdentificacion, Direccion, Telefono, CorreoElectronico ")]Cliente usuario){
            if (ModelState.IsValid){
                //GUarda usuario nuevo en la BSD
                _context.Clientes.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        public async Task<IActionResult> Delete (int? id){
            //Buscamos la base de datos
            var user = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(user);
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

            var user = await _context.Clientes.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre, NumeroIdentificacion, Direccion, Telefono, CorreoElectronico ")] Cliente usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }
}
