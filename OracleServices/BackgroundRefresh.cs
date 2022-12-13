using System.Diagnostics;
using System.ServiceProcess;

namespace OracleServices
{
    #pragma warning disable CS4014, CS8618
    public static class BackgroundRefresh
    {
        private static CancellationTokenSource _cts;
        private static ServicesControl _servicesControl;
        private static MainForm _mainForm;

        private static async Task SearchLoop()
        {
            while (!_cts.IsCancellationRequested)
            {
                Process[] processName = Process.GetProcessesByName("sqldeveloper64W");

                if (processName.Length is 0)
                {
                    Debug.WriteLine("Nothing");
                    if (_servicesControl.MainOracleService.Status is ServiceControllerStatus.Running)
                    {
                        _mainForm.PendingUpdateNotification(ServiceControllerStatus.Stopped);
                        _servicesControl.UpdateServicesStatus(ServiceControllerStatus.Stopped);
                        await _servicesControl.RefreshButtonsAwait();
                        _mainForm.SystemTrayIconAndNotifications(false);
                    }
                }
                else
                {
                    Debug.WriteLine("Run");
                    if (_servicesControl.MainOracleService.Status is ServiceControllerStatus.Stopped)
                    {
                        _mainForm.PendingUpdateNotification(ServiceControllerStatus.Running);
                        _servicesControl.UpdateServicesStatus(ServiceControllerStatus.Running);
                        await _servicesControl.RefreshButtonsAwait();
                        _mainForm.SystemTrayIconAndNotifications(true);
                    }
                }

                _servicesControl.MainOracleService.Refresh();
                await Task.Delay(4000);
            }
        }


        public static void StartSearchLoop(ServicesControl p_servicesControl, MainForm p_mainForm)
        {
            _cts?.Cancel(); // If the checkbox is checked and unchecked too fast
            _cts = new CancellationTokenSource();
            _servicesControl = p_servicesControl;
            _mainForm = p_mainForm;

            SearchLoop();
        }

        public static void StopSearchLoop()
        {
            _cts.Cancel();
            SearchLoop();
        }
    }
}