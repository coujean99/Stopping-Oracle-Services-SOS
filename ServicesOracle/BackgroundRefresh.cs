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
        public static void ThreadDelegation()
        {
            ThreadStart threadDelegation = new ThreadStart(ThreadLoop.SearchLoop);
            Thread newThread = new Thread(threadDelegation);
            newThread.Start();
        }
    }

    public class ThreadLoop
    {
        public static void SearchLoop()
        {
            ServicesControl servicesControl = new ServicesControl();

            while (true)
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

                Thread.Sleep(4000);
            }
        }
    }
}
