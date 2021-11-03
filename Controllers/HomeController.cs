using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ef_template.Data;
using ef_template.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ef_template.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ef_template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? 
                            HttpContext.TraceIdentifier
            });
        }


        [HttpPost]
        public async Task<JsonResult> Popular([FromServices] TemplateContext context)
        {
            /*
             * By default, if the database provider supports transactions,
             * all changes in a single call to SaveChanges are applied in a transaction.
             * If any of the changes fail, then the transaction is rolled back and none
             * of the changes are applied to the database. This means that SaveChanges
             * is guaranteed to either completely succeed, or leave the database unmodified
             * if an error occurs.
             * 
             * For most applications, this default behavior is sufficient.
             * You should only manually control transactions if your application
             * requirements deem it necessary.
             */            
            
            try
            {
                if (context.Superheroes.Any())
                    return Json(new {Ok = false, Mensaje = "Los datos ya fueron cargados previamente"});

                var poderesBasicos = new PoderBasico[]
                {
                    new() {Nombre = "Super Puño", Danio = 10, TiempoDeRegeneracion = TimeSpan.FromSeconds(5)},
                    new() {Nombre = "Patada voladora", Danio = 50, TiempoDeRegeneracion = TimeSpan.FromSeconds(50)},
                    new() {Nombre = "Vuelo", Danio = 0, TiempoDeRegeneracion = TimeSpan.FromSeconds(5)},
                    new() {Nombre = "Teletransportación", Danio = 0, TiempoDeRegeneracion = TimeSpan.FromSeconds(120)},
                    new() {Nombre = "Adherencia", Danio = 0, TiempoDeRegeneracion = TimeSpan.FromSeconds(120)},
                    new() {Nombre = "Sexto Sentido", Danio = 0, TiempoDeRegeneracion = TimeSpan.FromSeconds(0)},
                    new() {Nombre = "Golpe con Martillo", Danio = 40, TiempoDeRegeneracion = TimeSpan.FromSeconds(20)},
                };

                var superheroes = new Superheroe[]
                {
                    new()
                    {
                        Nombre = "Spider-Man",
                        NombreReal = "Peter Benjamin Parker",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Telaraña",
                            Danio = 10,
                            UsosPorDia = 1000
                        },
                        PoderesBasicos = new List<PoderBasico>
                        {
                            poderesBasicos[0],
                            poderesBasicos[1],
                            poderesBasicos[4]
                        }
                    },

                    new()
                    {
                        Nombre = "Deadpool",
                        NombreReal = "Wade Winston Wilson",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Regeneración",
                            Danio = 0,
                            UsosPorDia = 9999
                        },
                        PoderesBasicos = new List<PoderBasico>
                        {
                            poderesBasicos[0],
                            poderesBasicos[1],
                            poderesBasicos[5]
                        }
                    },

                    new()
                    {
                        Nombre = "Capitan America",
                        NombreReal = "Steve Rogers",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Lanzamiento de Escudo",
                            Danio = 150,
                            UsosPorDia = 200
                        },
                    },

                    new()
                    {
                        Nombre = "Thor",
                        NombreReal = "Thor Odinson",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Elektrokinesis",
                            Danio = 850,
                            UsosPorDia = 60
                        },
                        PoderesBasicos = new List<PoderBasico>
                        {
                            poderesBasicos[2],
                            poderesBasicos[3],
                            poderesBasicos[5],
                            poderesBasicos[6]
                        }
                    },
                };

                foreach (var superheroe in superheroes)
                    context.Add(superheroe);

                var villanos = new Villano[]
                {
                    new()
                    {
                        Nombre = "Doctor Octopus",
                        NombreReal = "Otto Octavius",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Tentaculos Mecanicos",
                            Danio = 400,
                            UsosPorDia = 1000
                        },
                    },

                    new()
                    {
                        Nombre = "Red Skull",
                        NombreReal = "Johann Shmidt",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Super Forma Humana",
                            Danio = 880,
                            UsosPorDia = 500
                        },
                    },

                    new()
                    {
                        Nombre = "Kingpin",
                        NombreReal = "Wilson Fisk",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Super Fuerza",
                            Danio = 300,
                            UsosPorDia = 150
                        },
                    },

                    new()
                    {
                        Nombre = "Duende Verde",
                        NombreReal = "Norman Osborn",
                        Superpoder = new Superpoder
                        {
                            Nombre = "Goblin Glider",
                            Danio = 100,
                            UsosPorDia = 450
                        },
                    },
                };

                _logger.LogInformation("Carga finalizada");

                foreach (var villano in villanos)
                    context.Add(villano);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new {Ok = false, Mensaje = $"Ocurrió el siguiente error \n: {ex.Message}"});
            }

            return Json(new {Ok = true, Mensaje = "Datos cargados con exito!"});
        }
    }
}
