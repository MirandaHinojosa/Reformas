using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string nombre, string email, string mensaje, string telefono = null)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Empresa de Reformas", emailSettings["FromEmail"]));
        emailMessage.To.Add(new MailboxAddress("Destinatario", emailSettings["ToEmail"]));
        emailMessage.Subject = "Nueva solicitud de presupuesto";

        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Nombre: {nombre}\nEmail: {email}\nMensaje: {mensaje}"
        };

        if (!string.IsNullOrEmpty(telefono))
        {
            bodyBuilder.TextBody += $"\nTeléfono: {telefono}";
        }

        //if (!string.IsNullOrEmpty(imagePath))
        //{
        //    bodyBuilder.Attachments.Add(imagePath);
        //}

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }


    /*, IFormFile imagePath =null*/
    //public async Task SendEmailAsync(string nombre, string email, string mensaje, string telefono= null)
    //{
    //    var emailMessage = new MimeMessage();
    //    emailMessage.From.Add(new MailboxAddress("Juan Carlos", "juancmirandahinojosa@gmail.com"));
    //    emailMessage.To.Add(new MailboxAddress(nombre, email));
    //    emailMessage.Subject = "Nuevo mensaje de contacto";

    //    var bodyBuilder = new BodyBuilder
    //    {
    //        HtmlBody = $"<p>Nombre: {nombre}</p><p>Email: {email}</p><p>Mensaje: {mensaje}</p>"
    //    };
    //    if (!string.IsNullOrEmpty(telefono))
    //    {
    //        bodyBuilder.TextBody += $"\nTeléfono: {telefono}";
    //    }

    //    //if (imagePath != null)
    //    //{
    //    //    using (var stream = new MemoryStream())
    //    //    {
    //    //        await imagePath.CopyToAsync(stream);
    //    //        bodyBuilder.Attachments.Add(imagePath.FileName, stream.ToArray());
    //    //    }
    //    //}

    //    emailMessage.Body = bodyBuilder.ToMessageBody();

    //    using (var client = new SmtpClient())
    //    {
    //        await client.ConnectAsync("smtp.dominio.com", 587, false);
    //        await client.AuthenticateAsync("juancmirandahinojosa@gmail.com", "oioo fufx vdat ezyb");
    //        await client.SendAsync(emailMessage);
    //        await client.DisconnectAsync(true);
    //    }
    //}
}