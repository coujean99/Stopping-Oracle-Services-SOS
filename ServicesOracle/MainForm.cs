using System.Diagnostics;
using System.ServiceProcess;

namespace OracleServices
{
    public partial class MainForm : Form
    {
        public readonly ServiceController runningOracleService = new ServiceController("OracleServiceXE");
        public readonly ServicesControl servicesControl = new ServicesControl();

        public MainForm()
        {
            InitializeComponent();

            servicesControl.StateAndStartTypeServices(runningOracleService);

            // Fixed the window size for the title;
            // Now icon in system tra change color when "Run program in background" is checked;
            // A popup shows when the service is running;

            // Fixed the token that was not stopping;
            // Fixed the background loop that was receiving a bad value about if the service was running
            // Fixed a glitch that don't stop the thread if we check and uncheck "Run program in background" too fast
            // Useless code removed from the old structure that was not accurate anymore

            // It still freeze a bit when come the time to generate the popup at the right moment but I think that we can deal with it
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            chk_backgroundRun.Checked = Properties.Settings.Default.BackgroundRun;
            chk_runAtStartup.Checked = Properties.Settings.Default.AtStartup;

            TextButtonsRefresh();

            if (chk_backgroundRun.Checked)
                chk_backgroundRun_CheckedChanged(null, EventArgs.Empty);


            if (chk_runAtStartup.Checked)
                this.WindowState = FormWindowState.Minimized;
        }


        public void TextButtonsRefresh()
        {
            btn_windowsStartup.Enabled = true;
            btn_state.Enabled = true;

            if (servicesControl.RunningServicesOnStartup)
                btn_windowsStartup.Text = "Disable Oracle services on startup";
            else
                btn_windowsStartup.Text = "Enable Oracle services on startup";

            if (servicesControl.EnableServices)
                btn_state.Text = "Disable Oracle services";
            else
                btn_state.Text = "Enable Oracle services";
        }


        private void btn_windowsStartup_Click(object sender, EventArgs e)
        {
            servicesControl.BootStartingMethod(!servicesControl.RunningServicesOnStartup);
            RefreshButtons("WindowsStartup");
        }


        private void btn_state_Click(object sender, EventArgs e)
        {
            servicesControl.StartStopServices(!servicesControl.EnableServices);
            RefreshButtons("BackgroundRun");
        }


        private void chk_backgroundRun_CheckedChanged(object sender, EventArgs e)
        {
            bool eventAllowed = true;

            if (eventAllowed)
            {
                if (chk_backgroundRun.Checked)
                {
                    systemTray.Icon = Properties.Resources.auto_stopped_icon;
                    btn_windowsStartup.Enabled = false;
                    btn_state.Enabled = false;

                    servicesControl.BootStartingMethod(false);
                    BackgroundRefresh.StartSearchLoop(servicesControl);
                }
                else
                {
                    if (Process.GetProcessesByName("sqldeveloper64W").Length != 0)
                    {
                        eventAllowed = false;
                        chk_backgroundRun.Checked = true;
                    }

                    if (Process.GetProcessesByName("sqldeveloper64W").Length == 0)
                    {
                        BackgroundRefresh.StopSearchLoop(servicesControl);
                        RefreshButtons("BothButtons");
                    }
                }
            }
        }


        public void SystemTrayIconAndNotifications(bool run)
        {
            systemTray.BalloonTipTitle = "Stopping Oracle Services - SOS";
            
            if (run)
            {
                systemTray.Icon = Properties.Resources.auto_running_icon;
                systemTray.BalloonTipText = "Oracle services are now running!";
            }
            else
            {
                systemTray.Icon = Properties.Resources.auto_stopped_icon;
                systemTray.BalloonTipText = "Oracle services are now stopped!";
            }

            systemTray.ShowBalloonTip(10);
        }


        public async Task RefreshButtons(string command)
        {
            if (command == "WindowsStartup" || command == "BothButtons")
            {
                btn_windowsStartup.Enabled = false;
                btn_windowsStartup.Text = "Refreshing...";
            }

            if (command == "BackgroundRun" || command == "BothButtons")
            {
                btn_state.Enabled = false;
                btn_state.Text = "Refreshing... please wait";
            }

            await Task.Run(() => servicesControl.RefreshButtonsAwait(runningOracleService));

            servicesControl.StateAndStartTypeServices(runningOracleService);

            TextButtonsRefresh();
        }


        // System Tray
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                systemTray.Visible = true;
            }
        }


        private void systemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            systemTray.Visible = true;
            WindowState = FormWindowState.Normal;
        }


        // On Windows Startup
        private void chk_runAtStartup_CheckedChanged(object sender, EventArgs e) =>
            servicesControl.TaskSheduler(chk_runAtStartup.Checked);


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            systemTray.Visible = false;

            Properties.Settings.Default.BackgroundRun = chk_backgroundRun.Checked;
            Properties.Settings.Default.AtStartup = chk_runAtStartup.Checked;
            Properties.Settings.Default.Save();

            Environment.Exit(0);
        }
    }
}