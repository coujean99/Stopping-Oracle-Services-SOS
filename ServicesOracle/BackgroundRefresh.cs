using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
                        mainForm.PendingServicesNotification(false);
                        servicesControl.StartStopServices(false);
                        mainForm.SystemTrayIconAndNotifications(false);
                    }
                }
                else
                {
                    Debug.WriteLine("run");
                    if (!servicesControl.EnableServices)
                    {
                        mainForm.PendingServicesNotification(true);
                        servicesControl.StartStopServices(true);
                        mainForm.SystemTrayIconAndNotifications(true);
                    }
                }

                await Task.Delay(4000);
            }
        }


        public static void StartSearchLoop(ServicesControl servicesControl, MainForm mainForm)
        {
            try
            {
                cts.Cancel();
            }
            catch (NullReferenceException e) { } // If the checkbox is checked and unchecked too fast
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