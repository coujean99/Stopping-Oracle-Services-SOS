using Microsoft.Win32.TaskScheduler;
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
            bool isServicesOnStartup = RunningServicesOnStartup;

            var oracleServices =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in oracleServices)
            {
                string serviceName = service.ServiceName;
                string startupType = String.Empty;
                string serviceController = "sc";
                string commandLine = String.Empty;

                // If OracleServiceXE and OracleOraDB... doesn't have the same StartType or if they just have an other StartType
                if (!this.RunningServicesOnStartup && service.StartType != ServiceStartMode.Automatic)
                {
                    isServicesOnStartup = true;
                    startupType = "auto";
                }

                if (this.RunningServicesOnStartup && service.StartType != ServiceStartMode.Manual)
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
            bool isServiceOn = this.EnableServices;

            var oracleServices =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in oracleServices)
            {
                if (this.EnableServices && service.Status != ServiceControllerStatus.Stopped)
                {
                    isServiceOn = false;
                    service.Stop();
                }
                
                if (!this.EnableServices && service.Status != ServiceControllerStatus.Running)
                {
                    isServiceOn = true;
                    service.Start();
                }
            }

            return this.EnableServices = isServiceOn;
        }


        public void TaskSheduler(bool toAddInTS)
        {
            using (TaskService ts = new TaskService())
            {
                if (toAddInTS)
                {
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Author = "SOS";
                    td.RegistrationInfo.Description = "Will run SOS at Windows startup";
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Triggers.Add(new LogonTrigger());
                    td.Actions.Add(new ExecAction(Application.ExecutablePath));
                    ts.RootFolder.RegisterTaskDefinition(@"StoppingOracleServices", td);
                }
                else
                {
                    ts.RootFolder.DeleteTask("StoppingOracleServices");
                }
            }
        }
    }
}
