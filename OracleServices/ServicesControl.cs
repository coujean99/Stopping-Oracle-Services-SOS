using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.ServiceProcess;
using Task = System.Threading.Tasks.Task;

namespace OracleServices
{
#pragma warning disable CA1416, IDE0003
    public class ServicesControl
    {
        private const string MAIN_SERVICE = "OracleServiceXE";

        public ServicesControl() => this.MainOracleService = new ServiceController(MAIN_SERVICE);

        public ServiceController MainOracleService { get; private set; }
        private IEnumerable<ServiceController> OracleServices =>
            ServiceController.GetServices()
            .Where(sc => sc.ServiceName.StartsWith("OracleOra")
            || sc.ServiceName is MAIN_SERVICE)
            .Take(3);

        public void UpdateServicesStartupEvent(ServiceStartMode p_startModeToSet)
        {
            foreach (ServiceController service in this.OracleServices)
            {
                string serviceName = service.ServiceName;
                string startupType = String.Empty;
                string serviceController = "sc";
                string commandLine = String.Empty;

                if (p_startModeToSet is ServiceStartMode.Automatic) startupType = "auto";
                else startupType = "demand";

                commandLine = string.Format("config {0} start= {1}", serviceName, startupType);
                Process.Start(serviceController, commandLine);

                service.Refresh();
                Debug.WriteLine(service.DisplayName +": " + service.StartType);
            }
        }

        public void UpdateServicesStatus(ServiceControllerStatus p_serviceStatusToSet)
        {
            foreach (ServiceController service in this.OracleServices)
            {
                if (p_serviceStatusToSet is ServiceControllerStatus.Stopped
                    && service.Status != ServiceControllerStatus.Stopped) service.Stop();

                if (p_serviceStatusToSet is ServiceControllerStatus.Running
                    && service.Status != ServiceControllerStatus.Running) service.Start();

                service.Refresh();
                Debug.WriteLine(service.DisplayName + ": " + service.Status);
            }
        }

        public void TaskSheduler(bool p_isAddInTaskServiceChecked)
        {
            using TaskService ts = new TaskService();
            if (p_isAddInTaskServiceChecked)
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Author = "SOS";
                td.RegistrationInfo.Description = "Will run SOS at Windows startup";
                td.Principal.RunLevel = TaskRunLevel.Highest;
                td.Triggers.Add(new LogonTrigger());
                td.Actions.Add(new ExecAction(Application.ExecutablePath));
                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;
                ts.RootFolder.RegisterTaskDefinition("StoppingOracleServices", td);
            }
            else if (ts.GetTask("StoppingOracleServices") != null) ts.RootFolder.DeleteTask("StoppingOracleServices");
        }

        public async Task RefreshButtonsAwait()
        {
            while (true)
            {
                await Task.Delay(500);
                this.MainOracleService.Refresh();

                Debug.WriteLine(this.MainOracleService.Status);

                if (this.MainOracleService.Status is ServiceControllerStatus.Running
                || this.MainOracleService.Status is ServiceControllerStatus.Stopped) break;
            }
        }
    }
}
