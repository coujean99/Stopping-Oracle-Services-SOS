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
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btn_windowsStartup.Enabled = true;
            btn_state.Enabled = true;

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

            // I tried to do it using ServiceControllerStatus but it's don't work...
            var t = Task.Run(async delegate { await Task.Delay(TimeSpan.FromSeconds(timeToWait)); });
            t.Wait();


            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(null, EventArgs.Empty);
        }
    }
}