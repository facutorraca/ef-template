using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ef_template.Data;
using ef_template.Models;

namespace ef_template.Controllers
{
    public class SuperheroesController : Controller
    {
        private readonly TemplateContext _context;

        public SuperheroesController(TemplateContext context)
        {
            _context = context;
        }

        // GET: Superheroes
        public async Task<IActionResult> Index()
        {
            var templateContext = _context.Superheroes;
            return View(await templateContext.ToListAsync());
        }

        // GET: Superheroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superheroe = await _context.Superheroes
                .FirstOrDefaultAsync(m => m.Codigo == id);

            if (superheroe == null)
            {
                return NotFound();
            }

            return View(superheroe);
        }

        // GET: Superheroes/Create
        public IActionResult Create()
        {
            ViewData["SuperpoderId"] = new SelectList(_context.Superpoderes, "Id", "Nombre");
            return View();
        }

        // POST: Superheroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,NombreReal,SuperpoderId")] Superheroe superheroe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superheroe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SuperpoderId"] = new SelectList(_context.Superpoderes, "Id", "Nombre", superheroe.SuperpoderId);
            return View(superheroe);
        }

        // GET: Superheroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superheroe = await _context.Superheroes.FindAsync(id);
            if (superheroe == null)
            {
                return NotFound();
            }
            ViewData["SuperpoderId"] = new SelectList(_context.Superpoderes, "Id", "Nombre", superheroe.SuperpoderId);
            return View(superheroe);
        }

        // POST: Superheroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nombre,NombreReal,SuperpoderId")] Superheroe superheroe)
        {
            if (id != superheroe.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superheroe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperheroeExists(superheroe.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SuperpoderId"] = new SelectList(_context.Superpoderes, "Id", "Nombre", superheroe.SuperpoderId);
            return View(superheroe);
        }

        // GET: Superheroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superheroe = await _context.Superheroes
                .Include(s => s.Superpoder)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (superheroe == null)
            {
                return NotFound();
            }

            return View(superheroe);
        }

        // POST: Superheroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superheroe = await _context.Superheroes.FindAsync(id);
            _context.Superheroes.Remove(superheroe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperheroeExists(int id)
        {
            return _context.Superheroes.Any(e => e.Codigo == id);
        }
    }
}
