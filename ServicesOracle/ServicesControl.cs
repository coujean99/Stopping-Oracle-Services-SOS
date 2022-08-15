using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.ServiceProcess;

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


        public void BootStartingMethod(bool startupRun)
        {
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

                if (startupRun)
                {
                    this.RunningServicesOnStartup = true;
                    startupType = "auto";
                }

                if (!startupRun)
                {
                    this.RunningServicesOnStartup = false;
                    startupType = "demand";
                }

                commandLine = string.Format("config {0} start= {1}", serviceName, startupType);
                Process.Start(serviceController, commandLine);
            }
        }


        public void StartStopServices(bool toRunServices)
        {
            var oracleServices =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in oracleServices)
            {
                if (!toRunServices && service.Status != ServiceControllerStatus.Stopped)
                {
                    this.EnableServices = false;
                    service.Stop();
                }

                if (toRunServices && service.Status != ServiceControllerStatus.Running)
                {
                    this.EnableServices = true;
                    service.Start();
                }

                if (service.DisplayName.Equals("OracleServiceXE")) RefreshButtonsAwait(service);
            }
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
                else ts.RootFolder.DeleteTask("StoppingOracleServices");
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
