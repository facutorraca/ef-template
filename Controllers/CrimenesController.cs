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
    public class CrimenesController : Controller
    {
        private readonly TemplateContext _context;

        public CrimenesController(TemplateContext context)
        {
            _context = context;
        }

        // GET: Crimenes
        public async Task<IActionResult> Index()
        {
            var templateContext = _context.Crimenes.Include(c => c.Villano);
            return View(await templateContext.ToListAsync());
        }

        // GET: Crimenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimen = await _context.Crimenes
                .Include(c => c.Villano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crimen == null)
            {
                return NotFound();
            }

            return View(crimen);
        }

        // GET: Crimenes/Create
        public IActionResult Create()
        {
            ViewData["VillanoCodigo"] = new SelectList(_context.Set<Villano>(), "Codigo", "Nombre");
            return View();
        }

        // POST: Crimenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VillanoCodigo")] Crimen crimen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crimen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillanoCodigo"] = new SelectList(_context.Set<Villano>(), "Codigo", "Nombre", crimen.VillanoCodigo);
            return View(crimen);
        }

        // GET: Crimenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimen = await _context.Crimenes.FindAsync(id);
            if (crimen == null)
            {
                return NotFound();
            }
            ViewData["VillanoCodigo"] = new SelectList(_context.Set<Villano>(), "Codigo", "Nombre", crimen.VillanoCodigo);
            return View(crimen);
        }

        // POST: Crimenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VillanoCodigo")] Crimen crimen)
        {
            if (id != crimen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crimen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrimenExists(crimen.Id))
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
            ViewData["VillanoCodigo"] = new SelectList(_context.Set<Villano>(), "Codigo", "Nombre", crimen.VillanoCodigo);
            return View(crimen);
        }

        // GET: Crimenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimen = await _context.Crimenes
                .Include(c => c.Villano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crimen == null)
            {
                return NotFound();
            }

            return View(crimen);
        }

        // POST: Crimenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crimen = await _context.Crimenes.FindAsync(id);
            _context.Crimenes.Remove(crimen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrimenExists(int id)
        {
            return _context.Crimenes.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<JsonResult> Popular([FromServices] TemplateContext context)
        {
            try
            {
                if (context.Crimenes.Any())
                    return Json(new { Ok = false, Mensaje = "Los datos ya fueron cargados previamente" });

                var random = new Random();

                var superheroes = context.Superheroes;
                var villanos = context.Villanos.ToList();

                var crimenes = new Crimen[]
                {
                    new()
                    {
                        Direccion = new Direccion
                        {
                            Ciudad = "New York",
                            CalleOLugar = "Central Park"
                        },
                        Villano = villanos[random.Next(0, villanos.Count)],
                        Peleas = new List<PeleaContraCrimen>
                        {
                            new ()
                            {
                                SuperheroeCodigo = random.Next(1, superheroes.Count() + 1),
                                Resultado = ResultadoPelea.Victoria
                            },
                            new ()
                            {
                                SuperheroeCodigo = random.Next(1, superheroes.Count() + 1),
                                Resultado = ResultadoPelea.Victoria
                            }
                        }
                    },

                    new()
                    {
                        Direccion = new Direccion
                        {
                            Ciudad = "Buenos Aires",
                            CalleOLugar = "Obelisco"
                        },
                        Villano = villanos[random.Next(0, villanos.Count)],
                        Peleas = new List<PeleaContraCrimen>
                        {
                            new()
                            {
                                SuperheroeCodigo = random.Next(1, superheroes.Count() + 1),
                                Resultado = ResultadoPelea.Derrota
                            },
                            new()
                            {
                                SuperheroeCodigo = random.Next(1, superheroes.Count() + 1),
                                Resultado = ResultadoPelea.Empate
                            }
                        }
                    }
                };

                foreach (var crimen in crimenes)
                    context.Add(crimen);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { Ok = false, Mensaje = $"Ocurrió el siguiente error \n: {ex.Message}" });
            }

            return Json(new { Ok = true, Mensaje = "Datos cargados con exito!" });
        }
    }
}
