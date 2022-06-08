using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleServices
{
    public static class BackgroundRefresh
    {
        public static async Task SearchLoop(CancellationToken token)
        {
            ServicesControl servicesControl = new ServicesControl();

            while (!token.IsCancellationRequested)
            {
                Process[] processName = Process.GetProcessesByName("sqldeveloper64W");

                if (processName.Length == 0)
                {
                    Debug.WriteLine("nothing");
                    if (servicesControl.EnableServices == true)
                    {
                        servicesControl.PresentStates();
                        servicesControl.EnableServices = false;
                    }
                }
                else
                {
                    Debug.WriteLine("run");
                    if (servicesControl.EnableServices == false)
                    {
                        servicesControl.PresentStates();
                        servicesControl.EnableServices = true;
                    }
                }

                await Task.Delay(4000);
            }
        }


        public static void StartSearchLoop()
        {
            var cts = new CancellationTokenSource();
            _ = SearchLoop(cts.Token);
        }

        public static void StopSearchLoop()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            _ = SearchLoop(cts.Token);
        }
    }
}