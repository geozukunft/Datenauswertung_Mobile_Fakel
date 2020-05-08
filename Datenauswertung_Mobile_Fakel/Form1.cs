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

namespace Datenauswertung_Mobile_Fakel
{
    public partial class Form1 : Form
    {
        string directory; //Directory of Files
        string configFile = @"config.cfg";
        string AirPressure_AFan_f;
        string AirPressure_BFan_f;
        string Fan_f;
        string AirDamper_f;
        string PropaneValve_f;
        string Temperature_f;

        ArrayList filenames = new ArrayList();
        ArrayList AirPressure_AFan = new ArrayList();
        ArrayList AirPressure_BFan = new ArrayList();
        ArrayList Fan = new ArrayList();
        ArrayList AirDamper = new ArrayList();
        ArrayList PropaneValve = new ArrayList();
        ArrayList Temperature = new ArrayList();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
