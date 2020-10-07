using AlertTester.Process.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlertTester.Interfaces;
using AlertTester.DTO;

namespace AlertAlertTester.WinForm
{
    public partial class frmTester : Form
    {
        int Steps = 0;
        public frmTester()
        {
            InitializeComponent();
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "MM/dd/yyyy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "MM/dd/yyyy";
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IConfiguration configurations = new AlertTester.Process.LocalConfiguration();
            configurations.ReadConfig();

            int Interval = Convert.ToInt32(txtInterval.Text);
            int Window = Convert.ToInt32(txtWindow.Text);
                        DateTime dtStart = dtpStart.Value.Date +
                    dtpStartTime.Value.TimeOfDay;
            DateTime dtEnd = dtpEnd.Value.Date +
                    dtpEndTime.Value.TimeOfDay;
           

            //configurations.QueryConfig = new QueryDetails();
            //configurations.QueryConfig.StartDate = dtStart;
            //configurations.QueryConfig.EndDate = dtEnd;
            //configurations.QueryConfig.Interval = Interval;
            //configurations.QueryConfig.Window = Window;
            //configurations.QueryConfig.Query = txtQuery.Text;

            System.TimeSpan diff = dtEnd.Subtract(dtStart);
            Steps = Convert.ToInt32(diff.TotalMinutes / Interval);

            List<string> lstresult = new List<string>();
            lstresult.Add("DateTime,Count,Success");
            //string logAnalyticsQuery = txtQuery.Text;

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();

            AlertTester.Process.QueryProcessor processor = new AlertTester.Process.QueryProcessor();
            //processor.ExecuteQuery(configurations.LogAnalyticsProviderConfig,
            //    configurations.PathConfig,
            //    configurations.QueryConfig);


            if (lstresult.Count > 1)
                File.WriteAllLines(configurations.ApplicationConfig.OutputPath, lstresult);

            MessageBox.Show("Test Completed. The result file is available at " + configurations.ApplicationConfig.OutputPath);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker1 = sender as BackgroundWorker;
            for (int j = 0; j <= Steps; j++)
            {
                Calculate(j);
                backgroundWorker1.ReportProgress((j * 100) / Steps);

            }
        }

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            MethodInvoker invoker = delegate
            {
                progressBar1.Value = e.ProgressPercentage;
            };
            progressBar1.BeginInvoke(invoker);
        }
    }
}
