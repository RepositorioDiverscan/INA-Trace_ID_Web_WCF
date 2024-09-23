using Diverscan.MJP.Utilidades;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Diverscan.MJP.AccesoDatos.Despacho
{
    public class NDespacho
    {
        private IFileExceptionWriter _fileExceptionWriter;
        private DDespacho dDespacho;

        public NDespacho(IFileExceptionWriter fileExceptionWriter)
        {
            _fileExceptionWriter = fileExceptionWriter;
            dDespacho = new DDespacho();
        }
        public string AsignarPedidoEncargado(EAsignarDespacho input)
        {
            //Si se le asigna a un Encargado de Bodega un Pedido proveniente de un Traslado, no se envia correo con PIN, regla de negocio.
            if (input.CorreoEnvioPIN.Equals("Traslado"))
            {
                try
                {
                    return dDespacho.AsignarPedidoEncargado(input, "NA");
                }
                catch (Exception ex)
                {

                    _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
                    return ex.Message;
                }
            }
            else
            {
                string PIN = GenerateRandomPIN();

                try
                {
                    string res = dDespacho.AsignarPedidoEncargado(input, PIN);

                    if (res.Equals("SSCC Asignado para Despachar con éxito."))
                    {
                        SendEmailWithPin(PIN, input.CorreoEnvioPIN);
                    }

                    return res;
                }
                catch (Exception ex)
                {
                    _fileExceptionWriter.WriteException(ex, PathFileConfig.VEHICLEFILEPATHEXCEPTION);
                    return ex.Message;
                }
            }
            
        }

        public string GenerateRandomPIN()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var pin = new char[4];

            for (int i = 0; i < 4; i++)
            {
                pin[i] = chars[random.Next(chars.Length)];
            }

            return new string(pin);
        }

        public void SendEmailWithPin(string PIN, string toEmail)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(ConfigurationManager.AppSettings["fromEmail"].ToString(), ConfigurationManager.AppSettings["tittle"].ToString());
                mail.To.Add(toEmail);
                mail.Subject = ConfigurationManager.AppSettings["subject"].ToString();
                mail.Body = string.Format(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Resources/EmailTempates/PinEmailTemplate.html"), PIN);
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = ConfigurationManager.AppSettings["smtpHost"].ToString();
                    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpEnableSsl"]);
                    smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpUseDefaultCredentials"]);
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpCredentialsUser"].ToString(), ConfigurationManager.AppSettings["smtpCredentialsPass"].ToString());

                    smtp.Send(mail);
                }
            }
        }

        public byte PedidoOTraslado(string NumeroTransaccion)
        {
            return dDespacho.PedidoOTraslado(NumeroTransaccion);
        }
    }
}
