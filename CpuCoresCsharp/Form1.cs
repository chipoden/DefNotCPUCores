using Microsoft.Win32;
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

namespace CpuCoresCsharp
{
    public partial class Form1 : Form

    {
        int i = 2;
        int capa = 1;
        string[] processos = new string[100];
        int count = 0;
        int sim = 1;
        int b = 2;
        int ativar = 0;
        string go;

        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {


            while (b > 1)
            {


                for (i = 0; i < count; i++)
                {


                    int ativar = 0;

                    sim = 1;
                    //it checks if the processes are running
                    System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName(processos[i]);
                    if (proc.Length > 0)
                    {
                      //  System.Threading.Thread.Sleep(5000);

                        do
                        {
                            //System.Threading.Thread.Sleep(5000);
                            if (ativar == 0) { 
                            Process[] pa = Process.GetProcessesByName(processos[i]);

                            foreach (Process process in pa)

                            {
                               // System.Threading.Thread.Sleep(10000);


                                process.PriorityClass = ProcessPriorityClass.High;

                            }

                            Process[] allProc = Process.GetProcesses();
                            Process me = Process.GetCurrentProcess();
                           // System.Threading.Thread.Sleep(5000);
                            foreach (Process p in Process.GetProcesses())
                            {

                                try
                                {
                                    if (p.Id != me.Id
                                        && p.ProcessName != "winlogon"
                                        && p.ProcessName != "explorer"
                                        && p.ProcessName != "wininit"
                                        && p.ProcessName != "System"
                                        && p.ProcessName != "fontdrvhost"
                                        && p.ProcessName != "winlogon"
                                        && p.ProcessName != "SecurityHealthService"
                                        && p.ProcessName != "Registry"
                                        && p.ProcessName != "MsMpEng"
                                        && p.ProcessName != "System Idle Process"
                                        && p.ProcessName != "Processo inativo do sistema"
                                        && p.ProcessName != "ShellExperienceHost"
                                        && p.ProcessName != "RuntimeBroker"
                                        && p.ProcessName != "taskmgr"
                                        && p.ProcessName != "spoolsv"
                                        && p.ProcessName != "csrss"
                                        && p.ProcessName != "smss"
                                        && p.ProcessName != "svchost"
                                        && p.ProcessName != "services"
                                        && p.ProcessName != "chrome"
                                        && p.ProcessName != "discord"
                                        && p.ProcessName != "Taskmgr"
                                        && p.ProcessName != processos[i]
                                    )
                                    {
                                        p.PriorityClass = ProcessPriorityClass.BelowNormal;
                                        p.ProcessorAffinity = (IntPtr)0x0001;

                                    }
                                 //   System.Threading.Thread.Sleep(500);
                                }
                                catch (Exception ex)
                                {
                                    //logging goes here
                                    continue;
                                }
                            }
                            }
                            ativar = 1;

                            System.Diagnostics.Process[] skrr = System.Diagnostics.Process.GetProcessesByName(processos[i]);
                            // if checks when the program is closed
                            if (ativar == 1)
                            { 
                                if (skrr.Length > 0)
                                {
                                }
                                else
                                {

                                    Process[] pa = Process.GetProcessesByName(processos[i]);
                                    Process me = Process.GetCurrentProcess();



                                    foreach (Process process in pa)

                                    {

                                        process.PriorityClass = ProcessPriorityClass.Normal;

                                    }
                                    foreach (Process p in Process.GetProcesses())
                                    {
                                        try
                                        {

                                            if (p.Id != me.Id
                                                && p.ProcessName != "winlogon"
                                                && p.ProcessName != "explorer"
                                                && p.ProcessName != "System Idle Process"
                                                && p.ProcessName != "taskmgr"
                                                && p.ProcessName != "spoolsv"
                                                && p.ProcessName != "csrss"
                                                && p.ProcessName != "smss"
                                                && p.ProcessName != "svchost "
                                                && p.ProcessName != "services"                          
                                                && p.ProcessName != processos[i]
                                            )
                                            {
                                                p.PriorityClass = ProcessPriorityClass.Normal;
                                                p.ProcessorAffinity = (IntPtr)0x000F;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            //logging goes here
                                            continue;
                                        }
                                    }

                                    //  Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\CPUCores\cpucores.exe");
                                    sim = 0;
                                            ativar = 0;


                                }
                                }
                            

                            System.Threading.Thread.Sleep(5000);

                        } while (sim > 0);


                    }
                    else
                    {
                        // start your process
                    }
                    System.Threading.Thread.Sleep(5000);

                }



            }



        }

        private void ShowWindow(object mainWindowHandle, int v)
        {
            throw new NotImplementedException();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {   //reads the items that are in the txt file
            //this.WindowState = FormWindowState.Minimized;

            label1.ForeColor = System.Drawing.Color.Green;



            go = System.AppDomain.CurrentDomain.BaseDirectory + "processos.txt";
            //  System.Windows.Forms.MessageBox.Show(go);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

          //  this.Size = Properties.Settings.Default.FormSize;
            this.Text = "CPUCORES Fixer";

            string[] lines = File.ReadAllLines(go);
            listBox1.Items.AddRange(lines);


            //counts and reads the processes to the background worker

            do
            {
                processos[count] = listBox1.Items[count].ToString();

                count = count + 1;
            } while (count < listBox1.Items.Count);


            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
            label1.Text = "Working...";



        }

        private void button2_Click(object sender, EventArgs e)
        {    //button to stop and start the backgroundworker
            if (button2.Text == "STOP")
            {


                b = 1;
                backgroundWorker1.CancelAsync();

                button2.Text = "START";
                label1.Text = "Stopped";
                backgroundWorker1.CancelAsync();
                foreach (Process p in Process.GetProcesses())
                {
                    try
                    {
                        if (p.ProcessName != "winlogon"
                            && p.ProcessName != "explorer"
                            && p.ProcessName != "System Idle Process"
                            && p.ProcessName != "taskmgr"
                            && p.ProcessName != "spoolsv"
                            && p.ProcessName != "csrss"
                            && p.ProcessName != "smss"
                            && p.ProcessName != "svchost "
                            && p.ProcessName != "services"
                            && p.ProcessName != processos[i]
                        )
                        {
                            p.PriorityClass = ProcessPriorityClass.Normal;
                            p.ProcessorAffinity = (IntPtr)0x000F;
                        }
                    }
                    catch (Exception ex)
                    {
                        //logging goes here
                        continue;
                    }
                }
                label1.ForeColor = System.Drawing.Color.Red;


            }
            else
            {
                b = 2;

                Application.Restart();





            }


        }

        private void button1_Click(object sender, EventArgs e)
        {   //adding items to the list box
            listBox1.Items.Add(textBox1.Text);

            //saving the list box items in the txt file
            string sPath = go;

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item);
            }

            SaveFile.Close();





            do
            {
                processos[count] = listBox1.Items[count].ToString();

                count = count + 1;
            } while (count < listBox1.Items.Count);

            Application.Restart();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {   //removing items from listbox
            listBox1.Items.Remove(listBox1.Items[listBox1.SelectedIndex]);

            //saving to the txt file
            string sPath = go;
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item);
            }

            SaveFile.Close();

            Application.Restart();


        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.ProcessName != "winlogon"
                        && p.ProcessName != "explorer"
                        && p.ProcessName != "System Idle Process"
                        && p.ProcessName != "taskmgr"
                        && p.ProcessName != "spoolsv"
                        && p.ProcessName != "csrss"
                        && p.ProcessName != "smss"
                        && p.ProcessName != "svchost "
                        && p.ProcessName != "services"
                    )
                    {
                        p.PriorityClass = ProcessPriorityClass.Normal;
                        p.ProcessorAffinity = (IntPtr)0x000F;
                    }
                }
                catch (Exception ex)
                {
                    //logging goes here
                    continue;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //add
            rk.SetValue("CpuCoresCsharpMELHORA", Application.ExecutablePath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            //delete
            rk.DeleteValue("CpuCoresCsharpMELHORA", false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*    Process[] pa = Process.GetProcessesByName("Overwolf");


                    foreach (Process process in pa)

                    {
                 process.ProcessorAffinity = (IntPtr)0x0002;
           
            }*/
            backgroundWorker1.CancelAsync();

            Process[] allProc = Process.GetProcesses();
            Process me = Process.GetCurrentProcess();
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.Id != me.Id
                        && p.ProcessName != "winlogon"
                        && p.ProcessName != "explorer"
                        && p.ProcessName != "System Idle Process"
                        && p.ProcessName != "taskmgr"
                        && p.ProcessName != "spoolsv"
                        && p.ProcessName != "csrss"
                        && p.ProcessName != "smss"
                        && p.ProcessName != "svchost "
                        && p.ProcessName != "services"
                        && p.ProcessName != "chrome"
                    )
                    {
                        p.PriorityClass = ProcessPriorityClass.BelowNormal;
                        p.ProcessorAffinity = (IntPtr)0x0001;

                    }
                }
                catch (Exception ex)
                {
                    //logging goes here
                    continue;
                }
            }





        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //

            }

        private void button7_Click(object sender, EventArgs e)
        {

            Process[] allProc = Process.GetProcesses();
            Process me = Process.GetCurrentProcess();
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    if (p.Id != me.Id
                        && p.ProcessName != "winlogon"
                        && p.ProcessName != "explorer"
                        && p.ProcessName != "System Idle Process"
                        && p.ProcessName != "taskmgr.exe"
                        && p.ProcessName != "spoolsv.exe"
                        && p.ProcessName != "csrss.exe"
                        && p.ProcessName != "smss.exe"
                        && p.ProcessName != "svchost.exe "
                        && p.ProcessName != "services.exe"                    
                    )
                    {
                        p.PriorityClass = ProcessPriorityClass.Normal;
                        p.ProcessorAffinity = (IntPtr)0x000F;
                    }
                }
                catch (Exception ex)
                {
                    //logging goes here
                    continue;
                }
            }

        }
    }
    }

