using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ef_template.Data;
using ef_template.Models;

namespace ef_template.Controllers
{
    public class PoderesBasicosController : Controller
    {
        private readonly TemplateContext _context;

        public PoderesBasicosController(TemplateContext context)
        {
            _context = context;
        }

        // GET: PoderesBasicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PoderesBasicos.ToListAsync());
        }

        // GET: PoderesBasicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poderBasico = await _context.PoderesBasicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poderBasico == null)
            {
                return NotFound();
            }

            return View(poderBasico);
        }

        // GET: PoderesBasicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoderesBasicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TiempoDeRegeneracion,Id,Nombre,Danio")] PoderBasico poderBasico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poderBasico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poderBasico);
        }

        // GET: PoderesBasicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poderBasico = await _context.PoderesBasicos.FindAsync(id);
            if (poderBasico == null)
            {
                return NotFound();
            }
            return View(poderBasico);
        }

        // POST: PoderesBasicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TiempoDeRegeneracion,Id,Nombre,Danio")] PoderBasico poderBasico)
        {
            if (id != poderBasico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poderBasico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoderBasicoExists(poderBasico.Id))
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
            return View(poderBasico);
        }

        // GET: PoderesBasicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poderBasico = await _context.PoderesBasicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poderBasico == null)
            {
                return NotFound();
            }

            return View(poderBasico);
        }

        // POST: PoderesBasicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poderBasico = await _context.PoderesBasicos.FindAsync(id);
            _context.PoderesBasicos.Remove(poderBasico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoderBasicoExists(int id)
        {
            return _context.PoderesBasicos.Any(e => e.Id == id);
        }
    }
}
