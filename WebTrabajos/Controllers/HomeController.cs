using Microsoft.AspNetCore.Mvc;
using System;

namespace TuProyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmailService _emailService;

        public HomeController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SolicitarPresupuesto(string nombre, string email, string mensaje)
        {
            // Enviar correo electrónico
            await _emailService.SendEmailAsync(nombre, email, mensaje);

            // Redirigir a una página de confirmación o mostrar un mensaje
            TempData["Mensaje"] = "Gracias por tu solicitud. Nos pondremos en contacto contigo pronto.";
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public async Task<IActionResult> EnviarMensaje(string nombre, string email, string mensaje,string telefono, string imagePath = null)
        //{
        //    // Enviar correo electrónico
        //    await _emailService.SendEmailAsync(nombre, email, mensaje,telefono);

        //    // Redirigir a una página de confirmación o mostrar un mensaje
        //    TempData["Mensaje"] = "Gracias por tu solicitud. Nos pondremos en contacto contigo pronto.";
        //    return RedirectToAction("Index");
        //}



        [HttpPost]
        public async Task<IActionResult> EnviarMensaje(string nombre, string email, string mensaje, string telefono)
        {
            string imagePath = null;

            //if (imagen != null && imagen.Length > 0)
            //{
            //    // Guardar la imagen en el servidor
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imagen.FileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await imagen.CopyToAsync(stream);
            //    }
            //    imagePath = filePath;
            //}

            // Enviar correo electrónico con la imagen adjunta
            //await _emailService.SendEmailAsync(nombre, email, mensaje, telefono, imagePath);
            await _emailService.SendEmailAsync(nombre, email, mensaje, telefono);

            // Redirigir a una página de confirmación o mostrar un mensaje
            TempData["Mensaje"] = "Gracias por tu solicitud. Nos pondremos en contacto contigo pronto.";
            return RedirectToAction("Index");
        }



        //VISTAS SIMPLES
        // Acción para mostrar la página estática
        public IActionResult Index()
        {
            return View();
        }
               
        public IActionResult Servicios()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult Destacar()
        {
            return View();
        }
    }
}