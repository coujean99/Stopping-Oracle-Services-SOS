using System.Diagnostics;
using System.Security.Principal;
using System.ServiceProcess;

namespace ServicesOracle
{
    public partial class Form1 : Form
    {
        private bool serviceActifAuDemarrage;
        private bool serviceActif;

        public Form1()
        {
            InitializeComponent();

            ServiceController serviceOracleAuDemarrage = new ServiceController("OracleServiceXE");

            if (serviceOracleAuDemarrage.StartType == ServiceStartMode.Automatic)
                serviceActifAuDemarrage = true;
            else
                serviceActifAuDemarrage = false;

            if (serviceOracleAuDemarrage.Status == ServiceControllerStatus.Running)
                serviceActif = true;
            else
                serviceActif |= false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (serviceActifAuDemarrage == true)
                btn_auDemarrage.Text = "Désactiver les services au démarrage";
            else
                btn_auDemarrage.Text = "Activer les services au démarrage";

            if (serviceActif == true)
                btn_etat.Text = "Désactiver les services";
            else
                btn_etat.Text = "Activer les services";
        }


        private void btn_auDemarrage_Click(object sender, EventArgs e)
        {
            var servicesOracle =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController serviceOracle in servicesOracle)
            {
                string nomService = serviceOracle.ServiceName;
                string typeDemarrage;
                string serviceController = "C:\\Windows\\System32\\sc.exe";
                string commandLine;

                if (serviceActifAuDemarrage == false)
                    typeDemarrage = "auto";
                else
                    typeDemarrage = "demand";

                commandLine = string.Format("config {0} start= {1}", nomService, typeDemarrage);
                Process.Start(serviceController, commandLine);
            }

            Application.Exit();
        }


        private void btn_etat_Click(object sender, EventArgs e)
        {
            var servicesOracle =
                from sc in ServiceController.GetServices()
                where sc.ServiceName.StartsWith("OracleOra") || sc.ServiceName.Equals("OracleServiceXE")
                select sc;

            foreach (ServiceController service in servicesOracle)
            {
                if (serviceActif == true)
                    service.Stop();
                else
                    service.Start();
            }

            Application.Exit();
        }
    }
}
