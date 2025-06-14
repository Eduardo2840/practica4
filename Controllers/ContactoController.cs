using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using practica4.Data;
using practica4.Models;
using Microsoft.ML;

using zapat.ML;




namespace practica4.Controllers
{
    
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactoController(ILogger<ContactoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Registrar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Load sample data
                    var sampleData = new ML_ContacZapatillas.ModelInput()
                    {
                        Col0 = contacto.Mensaje,
                         // No es necesario, pero lo dejamos como placeholder
                    };

                    //Load model and predict output
                    var result = ML_ContacZapatillas.Predict(sampleData);
                    var predictedLabel = result.PredictedLabel;
                    var scorePositive = result.Score[1];
                    var scoreNegative = result.Score[0];

                    //Check if the result is positive or negative
                    if (predictedLabel == 1)
                    {
                        contacto.Etiqueta = "Positivo";
                        contacto.Puntuacion = scorePositive;
                    }
                    else
                    {
                        contacto.Etiqueta = "Negativo";
                        contacto.Puntuacion = scoreNegative;
                    }

                    _context.DbSetContactos.Add(contacto);
                    _context.SaveChanges();
                    _logger.LogInformation("Se registró en el formulario");
                    ViewData["Message"] = "Se registró el formulario";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al registrar el formulario");
                    ViewData["Message"] = "Error al registrar el formulario: " + ex.Message;
                }
            }
            else
            {
                ViewData["Message"] = "Datos de entrada no válidos";
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}