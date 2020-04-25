using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 血狮2_全球战火启动器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("https://space.bilibili.com/512812524");
        }

        private void linkLabel2_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("http://www.istudio.top");
        }

        private void linkLabel3_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("https://shang.qq.com/wpa/qunwpa?idkey=5b992e4be8fdea5aafea9e424ca39fdba0fd3b793f3e265bc4de40e801a5d53a");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Release")
            {
                Process[] prorelease = Process.GetProcessesByName("GameSDK_Release");
                Process[] dprorelease = Process.GetProcessesByName("DedicatedLauncher_Release");
                if (prorelease.Length == 0) button1.Text = "启动游戏";
                else button1.Text = "强行停止游戏";
                if (dprorelease.Length == 0) button2.Text = "启动服务器";
                else button2.Text = "强行停止服务器";
            }
            else
            {
                Process[] prodebug = Process.GetProcessesByName("GameSDK");
                Process[] dprodebug = Process.GetProcessesByName("DedicatedLauncher");
                if (prodebug.Length == 0) button1.Text = "启动游戏";
                else button1.Text = "强行停止游戏";
                if (dprodebug.Length == 0) button2.Text = "启动服务器";
                else button2.Text = "强行停止服务器";
            }
            Process[] cryeditor = Process.GetProcessesByName("Editor");
            if (cryeditor.Length == 0) button3.Text = "启动编辑器";
            else button3.Text = "强行停止编辑器";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "启动游戏")
            {
                if (comboBox1.Text == "64位")
                {
                    if (comboBox2.Text == "Debug")
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin64\GameSDK.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin64\GameSDK_Release.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                }
                else
                {
                    if (comboBox2.Text == "Debug")
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin32\GameSDK.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin32\GameSDK_Release.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                }
            }
            else
            {
                Process[] _prorelease = Process.GetProcessesByName("GameSDK_Release");
                Process[] _prodebug = Process.GetProcessesByName("GameSDK");
                if (_prorelease.Length > 0) _prorelease[0].Kill();
                if (_prodebug.Length > 0) _prodebug[0].Kill();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "启动服务器")
            {
                if (comboBox1.Text == "64位")
                {
                    if (comboBox2.Text == "Debug")
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin64_Dedicated\DedicatedLauncher.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin64_Dedicated\DedicatedLauncher_Release.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                }
                else
                {
                    if (comboBox2.Text == "Debug")
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin32_Dedicated\DedicatedLauncher.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process p = new Process();
                            p.StartInfo.UseShellExecute = true;
                            p.StartInfo.FileName = @".\Bin32_Dedicated\DedicatedLauncher_Release.exe";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                        }
                    }
                }
            }
            else
            {
                Process[] _dprorelease = Process.GetProcessesByName("DedicatedLauncher_Release");
                Process[] _dprodebug = Process.GetProcessesByName("DedicatedLauncher");
                if (_dprorelease.Length > 0) _dprorelease[0].Kill();
                if (_dprodebug.Length > 0) _dprodebug[0].Kill();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "启动编辑器")
            {
                if (comboBox1.Text == "64位")
                {
                    try
                    {
                        System.Diagnostics.Process p = new Process();
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.FileName = @".\Bin64\Editor.exe";
                        p.Start();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString() + "\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                    }
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process p = new Process();
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.FileName = @".\Bin32\Editor.exe";
                        p.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString()+"\n\n简短解释：\n请检查是否将本程序放在了游戏文件夹根目录；若仍无法解决请进群咨询管理员");
                    }
                }
            }
            else
            {
                Process[] _cryeditor = Process.GetProcessesByName("Editor");
                if (_cryeditor.Length > 0) _cryeditor[0].Kill();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form helpform = new Form2();
            helpform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form upfrm = new Form3();
            upfrm.Show();
        }
    }
}