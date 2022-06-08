using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace OracleServices
{
    public class ServicesControl
    {
        public bool RunningServicesOnStartup { get; set; }
        public bool EnableServices { get; set; }


        public void StateAndStartTypeServices(ServiceController runningOracleService)
        {
            this.RunningServicesOnStartup = runningOracleService.StartType == ServiceStartMode.Automatic;
            this.EnableServices = runningOracleService.Status == ServiceControllerStatus.Running;
        }


        public void StartingMethod()
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

            this.RunningServicesOnStartup = isServicesOnStartup;
        }


        public void PresentStates()
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

            this.EnableServices = isServiceOn;
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
                    ts.RootFolder.DeleteTask("StoppingOracleServices");
            }
        }


        public void RefreshButtonsAwait(ServiceController runningOracleService)
        {
            const int WAITING_TIME_BETWEEN_SCANS = 3;
            Stopwatch waitingTimer = new Stopwatch();

            waitingTimer.Start();
            while (true)
            {
                runningOracleService.Refresh();
                while (waitingTimer.Elapsed.TotalSeconds < WAITING_TIME_BETWEEN_SCANS) { }
                if (runningOracleService.Status == ServiceControllerStatus.Running || runningOracleService.Status == ServiceControllerStatus.Stopped) break;
            }
            waitingTimer.Stop();

            Debug.WriteLine(runningOracleService.Status);
        }
    }
}
