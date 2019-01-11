using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4NXCI_GUI
{


    public partial class Form1 : Form
    {
        string xciFile;
        string nspName;
        string saveLoc;
        string tmpPath;
        private bool eventHandled = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            eventHandled = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdb = new OpenFileDialog();
            ofdb.ShowDialog();
            xciFile = ofdb.FileName;
            nspName = Path.GetFileNameWithoutExtension(ofdb.FileName);
            textBox1.Text = xciFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdb = new FolderBrowserDialog();
            fbdb.ShowDialog();
            saveLoc = fbdb.SelectedPath;
            tmpPath = saveLoc + @"\4nxci working folder";
            textBox2.Text = saveLoc;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process xciConvert = new Process();
            xciConvert.StartInfo.FileName = "4nxci.exe";
            xciConvert.StartInfo.CreateNoWindow = false;
            xciConvert.EnableRaisingEvents = true;
            xciConvert.Exited += new EventHandler(myProcess_Exited);

            if (!String.IsNullOrEmpty(xciFile) && !String.IsNullOrEmpty(saveLoc))
            {
                label5.ForeColor = Color.Blue;
                label5.Text = @"XCI File valid.";
                label6.ForeColor = Color.Blue;
                label6.Text = @"Save Location valid.";
                timer1.Start();
                label4.Text = @"Starting XCI Conversion.... Window may be unresponsive during process.....";

                DirectoryInfo createTempDir = Directory.CreateDirectory(tmpPath);
                xciConvert.StartInfo.Arguments = @" --outdir " + @"""" + tmpPath + @"""" + @" " + @"""" + xciFile + @"""";
                Thread.Sleep(2000);
                xciConvert.Start();

                while (!eventHandled)
                {
                    Thread.Sleep(1000);
                }

                xciConvert.WaitForExit();

                string[] tmpNSPname = Directory.GetFiles(tmpPath, "*.nsp");
                string tmpNSPname2 = tmpNSPname[0];

                if (saveLoc.EndsWith(@"\"))
                {
                    File.Move(tmpNSPname2, saveLoc + nspName + @".nsp");
                }
                else
                {
                    File.Move(tmpNSPname2, saveLoc + @"\" + nspName + @".nsp");
                }

                createTempDir.Delete();
                label4.ForeColor = Color.Green;
                label4.Text = @"XCI Conversion Complete!!!";

            }
            else if (string.IsNullOrEmpty(xciFile))
            {
                label5.ForeColor = Color.Red;
                label5.Text = @"XCI File invalid, please select your XCI file to convert!!";
                if (String.IsNullOrEmpty(saveLoc))
                {
                    label6.ForeColor = Color.Red;
                    label6.Text = @"Save Location invalid, please select your save to location!!";
                }
                else
                {

                    label6.ForeColor = Color.Blue;
                    label6.Text = @"Save Location valid.";
                }

            }
            else if (String.IsNullOrEmpty(saveLoc))
            {
                label6.Text = @"Save Location invalid, please select your save to location!!";
                if (String.IsNullOrEmpty(xciFile))
                {
                    label5.ForeColor = Color.Red;
                    label5.Text = @"XCI File invalid, please select your XCI file to convert!!";
                }
                else
                {
                    label5.ForeColor = Color.Blue;
                    label5.Text = @"XCI File valid.";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random labelcolor = new Random();
            int R = labelcolor.Next(0, 255);
            int G = labelcolor.Next(0, 255);
            int B = labelcolor.Next(0, 255);
            int A = labelcolor.Next(0, 255);

            label4.ForeColor = Color.FromArgb(A, R, G, B);
        }
    }
}
