using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Diverscan.MJP.Utilidades;

namespace ServicioMotorDecisiones
{
    [RunInstaller(true)]
    public partial class SMDInstaller : System.Configuration.Install.Installer
    {
        public SMDInstaller()
        {
            try
            {
                InitializeComponent();
                //string ServiceName = ConfigurationManager.AppSettings["ServiceName"];
                //string ServiceDescription = ConfigurationManager.AppSettings["ServiceDescription"];
                var serviceProcessInstaller = new ServiceProcessInstaller();
                var serviceInstaller = new ServiceInstaller();

                //# Service Account Information
                serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
                serviceProcessInstaller.Username = null;
                serviceProcessInstaller.Password = null;

                //# Service Information
                serviceInstaller.DisplayName = "WS_MotorDecisiones";
                serviceInstaller.StartType = ServiceStartMode.Automatic;

                //# This must be identical to the WindowsService.ServiceBase name
                //# set in the constructor of WindowsService.cs
                
                //SE TIENE QUE QUEMAR EL NOMBRE DEL SERVICIO YA QUE NO REALIZA LA INSTALACIÓN SI NO ESTA PARAMETRIZADO
                serviceInstaller.ServiceName = "WS_MotorDecisiones";
                serviceInstaller.Description = "Servicio Motor de Decisiones";

                this.Installers.Add(serviceProcessInstaller);
                this.Installers.Add(serviceInstaller);
            }
            catch (Exception ex)
            {
                var cl = new clErrores();
                cl.escribirError(ex.Message, ex.StackTrace);
            }
        }
    }
}
