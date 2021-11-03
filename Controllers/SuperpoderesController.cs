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
    public class SuperpoderesController : Controller
    {
        private readonly TemplateContext _context;

        public SuperpoderesController(TemplateContext context)
        {
            _context = context;
        }

        // GET: Superpoderes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Superpoderes.ToListAsync());
        }

        // GET: Superpoderes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoder = await _context.Superpoderes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superpoder == null)
            {
                return NotFound();
            }

            return View(superpoder);
        }

        // GET: Superpoderes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Superpoderes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsosPorDia,Id,Nombre,Danio")] Superpoder superpoder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superpoder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(superpoder);
        }

        // GET: Superpoderes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoder = await _context.Superpoderes.FindAsync(id);
            if (superpoder == null)
            {
                return NotFound();
            }
            return View(superpoder);
        }

        // POST: Superpoderes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsosPorDia,Id,Nombre,Danio")] Superpoder superpoder)
        {
            if (id != superpoder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superpoder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperpoderExists(superpoder.Id))
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
            return View(superpoder);
        }

        // GET: Superpoderes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoder = await _context.Superpoderes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (superpoder == null)
            {
                return NotFound();
            }

            return View(superpoder);
        }

        // POST: Superpoderes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superpoder = await _context.Superpoderes.FindAsync(id);
            _context.Superpoderes.Remove(superpoder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperpoderExists(int id)
        {
            return _context.Superpoderes.Any(e => e.Id == id);
        }
    }
}
