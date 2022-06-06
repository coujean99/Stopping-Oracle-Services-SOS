using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;

namespace OracleServices
{
    public partial class MainForm : Form
    {
        public readonly ServiceController runningOracleService = new ServiceController("OracleServiceXE");
        public readonly ServicesControl servicesControl = new ServicesControl();

        public bool runningServicesOnStartup;
        public bool enableServices;

        public MainForm()
        {
            InitializeComponent();

            runningServicesOnStartup = servicesControl.AreActiveOnWindowsStartup(runningOracleService);
            enableServices = servicesControl.AreActiveRightNow(runningOracleService);

            systemTray.BalloonTipTitle = "SOS";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btn_windowsStartup.Enabled = true;
            btn_state.Enabled = true;

            chk_backgroundRun.Checked = Properties.Settings.Default.BackgroundRun;
            chk_runAtStartup.Checked = Properties.Settings.Default.AtStartup;

            if (runningServicesOnStartup)
                btn_windowsStartup.Text = "Disable Oracle services on startup";
            else
                btn_windowsStartup.Text = "Enable Oracle services on startup";

            if (enableServices)
                btn_state.Text = "Disable Oracle services";
            else
                btn_state.Text = "Enable Oracle services";
        }


        private void btn_windowsStartup_Click(object sender, EventArgs e)
        {
            runningServicesOnStartup = servicesControl.StartingMethod();

            ButtonsRefresh("WindowsStartup");
        }


        private void btn_state_Click(object sender, EventArgs e)
        {
            enableServices = servicesControl.PresentStates();

            ButtonsRefresh("BackgroundRun");
        }


        private void chk_backgroundRun_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_backgroundRun.Checked)
            {
                btn_windowsStartup.Enabled = false;
                btn_state.Enabled = false;

                if (runningServicesOnStartup)
                    servicesControl.StartingMethod();

                if (enableServices)
                    servicesControl.PresentStates();

                BackgroundRefresh.ThreadDelegation();
            }
            else
                ButtonsRefresh("BothButtons");
        }


        public void ButtonsRefresh(string command)
        {
            int timeToWait = 0;

            if (command == "WindowsStartup" || command == "BothButtons")
            {
                btn_windowsStartup.Enabled = false;
                btn_windowsStartup.Text = "Refreshing...";

                if (command == "BothButtons")
                    btn_windowsStartup.Text = "Refreshing... please wait";

                timeToWait = 3;
            }

            if (command == "BackgroundRun" || command == "BothButtons")
            {
                btn_state.Enabled = false;
                btn_state.Text = "Refreshing... please wait";

                if (command == "BothButtons")
                    btn_state.Text = "40 seconds to continue";

                timeToWait = 40;
            }

            var t = Task.Run(async delegate { await Task.Delay(TimeSpan.FromSeconds(timeToWait)); });
            t.Wait();


            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(null, EventArgs.Empty);
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
        private void chk_runAtStartup_CheckedChanged(object sender, EventArgs e)
        {
			//https://stackoverflow.com/questions/7394806/creating-scheduled-tasks
            /*RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chk_runAtStartup.Checked)
            {
                registryKey.SetValue("Stopping-Oracle-Services-SOS", Application.ExecutablePath.ToString());
            }
            else
            {
                registryKey.DeleteValue("Stopping-Oracle-Services-SOS");
            }*/
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.BackgroundRun = chk_backgroundRun.Checked;
            Properties.Settings.Default.AtStartup = chk_runAtStartup.Checked;
            Properties.Settings.Default.Save();
            Environment.Exit(0);
        }
    }
}