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

            systemTray.BalloonTipTitle = "SOS";
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
            servicesControl.StartingMethod();

            _ = RefreshButtons("WindowsStartup");
        }


        private void btn_state_Click(object sender, EventArgs e)
        {
            servicesControl.PresentStates();

            _ = RefreshButtons("BackgroundRun");
        }


        private void chk_backgroundRun_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_backgroundRun.Checked)
            {
                btn_windowsStartup.Enabled = false;
                btn_state.Enabled = false;

                if (servicesControl.RunningServicesOnStartup)
                    servicesControl.StartingMethod();

                if (servicesControl.EnableServices)
                    servicesControl.PresentStates();

                BackgroundRefresh.StartSearchLoop();
            }
            else
            {
                BackgroundRefresh.StopSearchLoop();
                _ = RefreshButtons("BothButtons");
            }
        }


        public async Task RefreshButtons(string command)
        {
            if (command == "WindowsStartup" || command == "BothButtons")
            {
                btn_windowsStartup.Enabled = false;
                btn_windowsStartup.Text = "Refreshing...";

                if (command == "BothButtons")
                    btn_windowsStartup.Text = "Refreshing... please wait";
            }

            if (command == "BackgroundRun" || command == "BothButtons")
            {
                btn_state.Enabled = false;
                btn_state.Text = "Refreshing... please wait";

                if (command == "BothButtons")
                    btn_state.Text = "40 seconds to continue";
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