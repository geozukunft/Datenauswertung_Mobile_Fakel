using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Windows.Input;
using System.Windows;
using System.Threading;



namespace Datenauswertung_Mobile_Fakel
{
    public partial class Form1 : Form
    {
        string directory; //Directory of Files
        //string configFile = @"config.cfg";
        string AirPressure_AFan_f;
        string AirPressure_BFan_f;
        string Fan_f;
        string AirDamper_f;
        string PropaneValve_f;
        string Temperature_f;

        ArrayList filenames = new ArrayList();
        List<ArrayData> AirPressure_AFan = new List<ArrayData>();
        List<ArrayData> AirPressure_BFan = new List<ArrayData>();
        List<ArrayData> Fan = new List<ArrayData>();
        List<ArrayData> AirDamper = new List<ArrayData>();
        List<ArrayData> PropaneValve = new List<ArrayData>();
        List<ArrayData> Temperature = new List<ArrayData>();

        


        public Form1()
        {
            InitializeComponent();
            prgB.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateFiles();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                
                DialogResult result = fbd.ShowDialog();

                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    directory = fbd.SelectedPath;
                    prgB.Visible = true;


                    BackgroundWorker bW1 = new BackgroundWorker();
                    bW1.WorkerReportsProgress = true;
                    bW1.DoWork += worker_DoWork;
                    bW1.ProgressChanged += worker_ProgressChanged;
                    bW1.RunWorkerCompleted += worker_RunWorkerCompleted;
                    bW1.RunWorkerAsync();




                    //MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }


        /*private void readData(string dir)
        {
            
            

        }*/

        private List<ArrayData> getRawData(string filename, string dir)
        {
            string fullpath = dir + filename;
            //ArrayList rawData = new ArrayList();
            List<ArrayData> data = new List<ArrayData>();
            int i = 0;

            foreach(string line in File.ReadLines(fullpath))
            {
                if (i != 0) //remove First Line
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        //rawData.Add(line);
                        string[] splitLine = line.Split(';');
                        //label1.Text = Convert.ToString(i);
                        try
                        {
                            data.Add(new ArrayData() { Logtime = Convert.ToDateTime(splitLine[1].Trim('"')), Value = Convert.ToDouble(splitLine[2]) });
                        }
                        catch(Exception ex)
                        {
                        }                       
                    }
                }
                i++;
            }

            return data;
        }

        private void updateFiles()
        {
            AirPressure_AFan_f = @"\lnl.csv";
            AirPressure_BFan_f = @"\lvl.csv";
            Fan_f = @"\fan.csv";
            AirDamper_f = @"\aird.csv";
            PropaneValve_f = @"\propv.csv";
            Temperature_f = @"\temp.csv";
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //readData(directory);
            
            AirPressure_AFan = getRawData(AirPressure_AFan_f, directory);
            (sender as BackgroundWorker).ReportProgress(16);
            AirPressure_BFan = getRawData(AirPressure_BFan_f, directory);
            (sender as BackgroundWorker).ReportProgress(32);
            Fan = getRawData(Fan_f, directory);
            (sender as BackgroundWorker).ReportProgress(48);
            AirDamper = getRawData(AirDamper_f, directory);
            (sender as BackgroundWorker).ReportProgress(64);
            PropaneValve = getRawData(PropaneValve_f, directory);
            (sender as BackgroundWorker).ReportProgress(72);
            Temperature = getRawData(Temperature_f, directory);
            (sender as BackgroundWorker).ReportProgress(100);
            Thread.Sleep(150);
            (sender as BackgroundWorker).ReportProgress(0);
            
            (sender as BackgroundWorker).ReportProgress(100);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgB.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Finished Reading Data");
            prgB.Visible = false;
            generateOptimizedData();
        }

        

        private void generateOptimizedData()
        {
            chart_Fan.DataSource = Fan;
            chart_Fan.Series["Series1"].XValueMember = "Logtime";
            chart_Fan.Series["Series1"].YValueMembers = "Value";
            chart_Fan.DataBind();

            chart_AirP_BFan.DataSource = AirPressure_BFan;
            chart_AirP_BFan.Series["Series1"].XValueMember = "Logtime";
            chart_AirP_BFan.Series["Series1"].YValueMembers = "Value";
            chart_AirP_BFan.DataBind();

            chart_AirP_AFan.DataSource = AirPressure_AFan;
            chart_AirP_AFan.Series["Series1"].XValueMember = "Logtime";
            chart_AirP_AFan.Series["Series1"].YValueMembers = "Value";
            chart_AirP_AFan.DataBind();

            chart_AirD.DataSource = AirDamper;
            chart_AirD.Series["Series1"].XValueMember = "Logtime";
            chart_AirD.Series["Series1"].YValueMembers = "Value";
            chart_AirD.DataBind();

            chart_PropV.DataSource = PropaneValve;
            chart_PropV.Series["Series1"].XValueMember = "Logtime";
            chart_PropV.Series["Series1"].YValueMembers = "Value";
            chart_PropV.DataBind();

            chart_Temp.DataSource = Temperature;
            chart_Temp.Series["Series1"].XValueMember = "Logtime";
            chart_Temp.Series["Series1"].YValueMembers = "Value";
            chart_Temp.DataBind();

        }

       
    }

    struct ArrayData
    {
        public DateTime Logtime { get; set; }
        public double Value { get; set; }
    }

    
}
