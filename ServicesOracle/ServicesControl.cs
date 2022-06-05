using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OracleServices
{
    public class ServicesControl
    {
        public bool RunningServicesOnStartup { get; set; }
        public bool EnableServices { get; set; }

        public bool AreActiveOnWindowsStartup(ServiceController runningOracleService)
        {
            return this.RunningServicesOnStartup = runningOracleService.StartType == ServiceStartMode.Automatic;
        }

        public bool AreActiveRightNow(ServiceController runningOracleService)
        {
            return this.EnableServices = runningOracleService.Status == ServiceControllerStatus.Running;
        }

        public bool StartingMethod()
        {
            bool isServicesOnStartup = true;

            var oracleServices =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in oracleServices)
            {
                string serviceName = service.ServiceName;
                string startupType;
                string serviceController = "sc";
                string commandLine;

                if (!this.RunningServicesOnStartup)
                {
                    isServicesOnStartup = true;
                    startupType = "auto";
                }
                else
                {
                    isServicesOnStartup = false;
                    startupType = "demand";
                }

                commandLine = string.Format("config {0} start= {1}", serviceName, startupType);
                Process.Start(serviceController, commandLine);
            }

            return this.RunningServicesOnStartup = isServicesOnStartup;
        }

        public bool PresentStates()
        {
            bool isServiceOn = true;

            var oracleServices =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in oracleServices)
            {
                if (this.EnableServices)
                {
                    isServiceOn = false;
                    service.Stop();
                }
                else
                {
                    isServiceOn = true;
                    service.Start();
                }
            }

            return this.EnableServices = isServiceOn;
        }
    }
}
