using System.Diagnostics;
using System.ServiceProcess;

namespace OracleServices
{
    #pragma warning disable CA1416, CS4014, IDE0003
    public partial class MainForm : Form
    {
        private ServicesControl m_servicesControl;

        public MainForm()
        {
            this.InitializeComponent();

            this.m_servicesControl = new ServicesControl();
            this.systemTray.BalloonTipTitle = "Stopping Oracle Services - SOS";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.TextButtonsUpdate();

            this.chk_RunInBackground.Checked = Properties.Settings.Default.RunInBackground;
            this.chk_RunAtStartup.Checked = Properties.Settings.Default.RunAtStartup;
            this.chk_MinimizeAtStartup.Checked = Properties.Settings.Default.MinimizeAtStartup;

            if (this.chk_RunInBackground.Checked) this.chk_RunInBackground_Click(null, EventArgs.Empty);
            if (this.chk_MinimizeAtStartup.Checked) this.WindowState = FormWindowState.Minimized;
        }

        // Buttons
        private async void Btn_ServicesOnStartup_Click(object sender, EventArgs e)
        {
            ServiceStartMode startModeToSet;

            if (this.m_servicesControl.MainOracleService.StartType is ServiceStartMode.Automatic)
            {
                startModeToSet = ServiceStartMode.Manual;
            }
            else startModeToSet = ServiceStartMode.Automatic;

            this.m_servicesControl.UpdateServicesStartupEvent(startModeToSet);
            await this.WaitingTextButtons();
            this.TextButtonsUpdate();
        }

        private async void Btn_ServicesState_Click(object sender, EventArgs e)
        {
            ServiceControllerStatus servicesStatus;

            if (this.m_servicesControl.MainOracleService.Status is ServiceControllerStatus.Running)
            {
                servicesStatus = ServiceControllerStatus.Stopped;
            }
            else servicesStatus = ServiceControllerStatus.Running;

            this.m_servicesControl.UpdateServicesStatus(servicesStatus);
            await this.WaitingTextButtons();
            this.TextButtonsUpdate();
        }

        private async Task WaitingTextButtons()
        {
            this.btn_ServicesOnStartup.Enabled = false;
            this.btn_ServicesState.Enabled = false;
            this.btn_ServicesOnStartup.Text = "Refreshing";
            this.btn_ServicesState.Text = "Please wait...";

            await this.m_servicesControl.RefreshButtonsAwait();
        }

        public void TextButtonsUpdate()
        {
            this.btn_ServicesOnStartup.Enabled = true;
            this.btn_ServicesState.Enabled = true;

            if (this.m_servicesControl.MainOracleService.StartType is ServiceStartMode.Automatic)
            {
                this.btn_ServicesOnStartup.Text = "Disable Oracle services on startup";
            }
            else this.btn_ServicesOnStartup.Text = "Enable Oracle services on startup";

            if (this.m_servicesControl.MainOracleService.Status is ServiceControllerStatus.Running)
            {
                this.btn_ServicesState.Text = "Disable Oracle services";
            }
            else this.btn_ServicesState.Text = "Enable Oracle services";

            this.ActiveControl = null;
        }

        // Background
        private void chk_RunInBackground_Click(object sender, EventArgs e)
        {
            if (this.chk_RunInBackground.Checked)
            {
                this.systemTray.Icon = Properties.Resources.auto_stopped_icon;
                this.btn_ServicesOnStartup.Enabled = false;
                this.btn_ServicesState.Enabled = false;

                this.m_servicesControl.UpdateServicesStartupEvent(ServiceStartMode.Manual);
                BackgroundRefresh.StartSearchLoop(this.m_servicesControl, this);

                if (Process.GetProcessesByName("sqldeveloper64W").Length != 0)
                {
                    this.systemTray.Icon = Properties.Resources.auto_running_icon;
                }
            }

            else
            {
                if (Process.GetProcessesByName("sqldeveloper64W").Length is 0)
                {
                    this.systemTray.Icon = Properties.Resources.default_icon;
                    BackgroundRefresh.StopSearchLoop();
                    this.WaitingTextButtons();
                    this.TextButtonsUpdate();
                }
                else this.chk_RunInBackground.CheckState = CheckState.Checked;
            }
        }

        // System Tray
        public void PendingUpdateNotification(ServiceControllerStatus p_pendingServiceStatus)
        {
            this.systemTray.Icon = Properties.Resources.auto_pending_icon;

            if (p_pendingServiceStatus is ServiceControllerStatus.Running)
            {
                this.systemTray.BalloonTipText = "Starting Oracle Services. Please wait...";
            }
            else this.systemTray.BalloonTipText = "Stopping Oracle Services. Please wait...";

            this.systemTray.ShowBalloonTip(5);
        }

        public void SystemTrayIconAndNotifications(bool p_isRunning)
        {
            if (p_isRunning)
            {
                this.systemTray.Icon = Properties.Resources.auto_running_icon;
                this.systemTray.BalloonTipText = "Oracle services are now running!";
            }
            else
            {
                this.systemTray.Icon = Properties.Resources.auto_stopped_icon;
                this.systemTray.BalloonTipText = "Oracle services are now stopped!";
            }

            this.systemTray.ShowBalloonTip(5);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState is FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.systemTray.Visible = true;
                this.Hide();
            }
        }

        private void SystemTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.systemTray.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        // Save
        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RunInBackground = this.chk_RunInBackground.Checked;
            Properties.Settings.Default.RunAtStartup = this.chk_RunAtStartup.Checked;
            Properties.Settings.Default.MinimizeAtStartup = this.chk_MinimizeAtStartup.Checked;
            Properties.Settings.Default.Save();

            this.m_servicesControl.TaskSheduler(this.chk_RunAtStartup.Checked);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.systemTray.Visible = false;
            Environment.Exit(0);
        }
    }
}