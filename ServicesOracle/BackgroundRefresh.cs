using System.Diagnostics;

namespace OracleServices
{
    public static class BackgroundRefresh
    {
        private static CancellationTokenSource cts;

        public static async Task SearchLoop(CancellationToken token, ServicesControl servicesControl, MainForm mainForm)
        {
            while (!token.IsCancellationRequested)
            {
                Process[] processName = Process.GetProcessesByName("sqldeveloper64W");

                if (processName.Length == 0)
                {
                    Debug.WriteLine("nothing");
                    if (servicesControl.EnableServices)
                    {
                        mainForm.PendingOnOffServicesNotification(false);
                        servicesControl.StartStopServices(false);
                        mainForm.SystemTrayIconAndNotifications(false);
                    }
                }

                else
                {
                    Debug.WriteLine("run");
                    if (!servicesControl.EnableServices)
                    {
                        mainForm.PendingOnOffServicesNotification(true);
                        servicesControl.StartStopServices(true);
                        mainForm.SystemTrayIconAndNotifications(true);
                    }
                }

                await Task.Delay(4000);
            }
        }


        public static void StartSearchLoop(ServicesControl servicesControl, MainForm mainForm)
        {
            cts?.Cancel(); // If the checkbox is checked and unchecked too fast
            cts = new CancellationTokenSource();
            SearchLoop(cts.Token, servicesControl, mainForm);
        }

        public static void StopSearchLoop(ServicesControl servicesControl, MainForm mainForm)
        {
            cts.Cancel();
            SearchLoop(cts.Token, servicesControl, mainForm);
        }
    }
}