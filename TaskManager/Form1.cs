using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            ListAllProcesses();
            timer.Start();
        }
        public void ListAllProcesses()
        {
            Process[] allProcess = Process.GetProcesses();

            foreach (Process pl in allProcess)
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", pl.ProcessName);
                PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", pl.ProcessName);
                PerformanceCounter networkUsageCounter = new PerformanceCounter("Process", "IO Other Bytes/sec", pl.ProcessName);
                PerformanceCounter totalRamCounter = new PerformanceCounter("Memory", "Committed Bytes");

                cpuCounter.NextValue();
                ramCounter.NextValue();
                networkUsageCounter.NextValue();
                               
                System.Threading.Thread.Sleep(100);

                float ramUsage = ramCounter.NextValue() / (1024 * 1024); 
                float networkUsage = networkUsageCounter.NextValue() / (1024 * 1024); 
                float totalRam = totalRamCounter.NextValue() / (1024 * 1024); 
                float ramUsagePercentage = (ramUsage / totalRam) * 100;
                float cpuUsage = cpuCounter.NextValue()/10;

                string cpuUsageFormatted = cpuUsage.ToString("N2") + "%";
                string ramUsageFormatted = ramUsagePercentage.ToString("N2") + "%";
                            
                tabeladados.Rows.Add(pl.ProcessName, pl.Id, ramUsageFormatted, cpuUsageFormatted, networkUsage.ToString("N2") + " MB/s");
            }    
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            progressBarCPU.Value = (int)fcpu;
            lblCPU.Text = string.Format("{0:0.00}%", fcpu);
            float fram = pRAM.NextValue();
            progressBarRAM.Value = (int)fram;
            lblRAM.Text = string.Format("{0:0.00}%", fram);
         
            DateTime currentTime = DateTime.Now;
            string formattedDateTime = currentTime.ToString("HH:mm");
       
            chart1.Series["CPU"].Points.AddXY(formattedDateTime, fcpu);
            chart1.Series["RAM"].Points.AddXY(formattedDateTime, fram);

        }
    }
}
