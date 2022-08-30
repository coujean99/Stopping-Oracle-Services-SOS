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

            systemTray.BalloonTipTitle = "Stopping Oracle Services - SOS";
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            chk_RunInBackground.Checked = Properties.Settings.Default.RunInBackground;
            chk_RunAtStartup.Checked = Properties.Settings.Default.RunAtStartup;
            chk_MinimizeAtStartup.Checked = Properties.Settings.Default.MinimizeAtStartup;

            TextButtonsRefresh();

            if (chk_RunInBackground.Checked) chk_RunInBackground_Click(null, EventArgs.Empty);

            if (chk_MinimizeAtStartup.Checked) this.WindowState = FormWindowState.Minimized;
        }


        public void TextButtonsRefresh()
        {
            btn_ServicesOnStartup.Enabled = true;
            btn_ServicesState.Enabled = true;

            if (servicesControl.RunningServicesOnStartup) btn_ServicesOnStartup.Text = "Disable Oracle services on startup";
            else btn_ServicesOnStartup.Text = "Enable Oracle services on startup";

            if (servicesControl.EnableServices) btn_ServicesState.Text = "Disable Oracle services";
            else btn_ServicesState.Text = "Enable Oracle services";
        }


        private void Btn_ServicesOnStartup_Click(object sender, EventArgs e)
        {
            servicesControl.BootStartingMethod(!servicesControl.RunningServicesOnStartup);
            RefreshingTextButtons(1);
        }


        private void Btn_ServicesState_Click(object sender, EventArgs e)
        {
            servicesControl.StartStopServices(!servicesControl.EnableServices);
            RefreshingTextButtons(2);
        }


        private void chk_RunInBackground_Click(object sender, EventArgs e)
        {
            if (chk_RunInBackground.Checked)
            {
                systemTray.Icon = Properties.Resources.auto_stopped_icon;
                btn_ServicesOnStartup.Enabled = false;
                btn_ServicesState.Enabled = false;

                servicesControl.BootStartingMethod(false);
                BackgroundRefresh.StartSearchLoop(servicesControl, this);

                if (Process.GetProcessesByName("sqldeveloper64W").Length != 0) systemTray.Icon = Properties.Resources.auto_running_icon;
            }

            else
            {
                if (Process.GetProcessesByName("sqldeveloper64W").Length == 0)
                {
					systemTray.Icon = Properties.Resources.default_icon;
                    BackgroundRefresh.StopSearchLoop(servicesControl, this);
                    RefreshingTextButtons(3);
                }
                else chk_RunInBackground.CheckState = CheckState.Checked;
            }
        }


        public void PendingOnOffServicesNotification(bool pending)
        {
            systemTray.Icon = Properties.Resources.auto_pending_icon;

            if (pending) systemTray.BalloonTipText = "Starting Oracle Services. The program will freeze. Please wait";
            else systemTray.BalloonTipText = "Stopping Oracle Services. The program will freeze. Please wait";

            systemTray.ShowBalloonTip(10);
        }


        public void SystemTrayIconAndNotifications(bool run)
        {
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


        public async Task RefreshingTextButtons(int command)
        {
            // 1 - Services on stratup
            // 2 - Service state
            // 3 - Both
            if (command == 1 || command == 3)
            {
                btn_ServicesOnStartup.Enabled = false;
                btn_ServicesOnStartup.Text = "Refreshing...";
            }

            if (command == 2 || command == 3)
            {
                btn_ServicesState.Enabled = false;
                btn_ServicesState.Text = "Refreshing... please wait";
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
                this.Hide();
            }
        }


        private void SystemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            systemTray.Visible = true;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }


        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RunInBackground = chk_RunInBackground.Checked;
            Properties.Settings.Default.RunAtStartup = chk_RunAtStartup.Checked;
            Properties.Settings.Default.MinimizeAtStartup = chk_MinimizeAtStartup.Checked;
            Properties.Settings.Default.Save();

            servicesControl.TaskSheduler(chk_RunAtStartup.Checked);
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            systemTray.Visible = false;
            Environment.Exit(0);
        }
    }
}