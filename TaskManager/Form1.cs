using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            progressBarCPU.Value = (int)fcpu;
            lblCPU.Text = string.Format("{0:0.00}%", fcpu);

            float fram = pRAM.NextValue();
            progressBarRAM.Value = (int)fram;
            lblRAM.Text = string.Format("{0:0.00}%", fram);


        }
    }
}
