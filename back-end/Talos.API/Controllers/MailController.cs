using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost("SendMail")]
        public IActionResult SendMail()
        {
            // SMTP server settings
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("youremail@gmail.com", "yourpassword"),
                EnableSsl = true,
            };

            // Email message settings
            var message = new MailMessage
            {
                From = new MailAddress("youremail@gmail.com"),
                Subject = "Test Email",
                Body = "This is a test email sent from a .NET Core application.",
                IsBodyHtml = false,
            };
            message.To.Add("recipient@example.com");

            // Send the email
            try
            {
                smtpClient.Send(message);
                return Ok("¡Correo enviado exitosamente!");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
