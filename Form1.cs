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
                if (prorelease.Length == 0)
                {
                    //checkBox1.Enabled = true;
                    checkBox1.Text = "同时启动VPN联机";
                    if (checkBox1.Checked == false)
                    {
                        button1.Text = "启动游戏";
                    }
                    else
                    {
                        button1.Text = "启动游戏和VPN";
                    }
                }
                else
                {
                    checkBox1.Text = "同时关闭VPN联机";
                    if (checkBox1.Checked == false)
                    {
                        button1.Text = "强行停止游戏";
                    }
                    else
                    {
                        button1.Text = "强行停止游戏和VPN";
                    }
                }
                if (dprorelease.Length == 0) button2.Text = "启动服务器";
                else button2.Text = "强行停止服务器";
            }
            else
            {
                Process[] prodebug = Process.GetProcessesByName("GameSDK");
                Process[] dprodebug = Process.GetProcessesByName("DedicatedLauncher");
                if (prodebug.Length == 0){
                    //checkBox1.Enabled = true;
                    checkBox1.Text = "同时启动VPN联机";
                    if (checkBox1.Checked == false)
                    {
                        button1.Text = "启动游戏";
                    }
                    else
                    {
                        button1.Text = "启动游戏和VPN";
                    }
                }
                else
                {
                    checkBox1.Text = "同时关闭VPN联机";
                    if (checkBox1.Checked == false)
                    {
                        button1.Text = "强行停止游戏";
                    }
                    else
                    {
                        button1.Text = "强行停止游戏和VPN";
                    }
                }
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
        private static readonly string CmdPath = @"C:\Windows\System32\cmd.exe";
        public static void Runcmd(string cmd)
        {
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;
                //p.StandardInput.WriteLine("exit");
                //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
                //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

                //获取cmd窗口的输出信息
                //output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "强行停止游戏" && button1.Text != "强行停止游戏和VPN")
            {
                if (checkBox1.Checked == true)
                {
                    try
                    {
                        Process p = new Process();
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.FileName = @"C:\Program Files\SoftEther VPN Client\vpncmgr_x64.exe";
                        p.Start();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString()+"\n\n简短解释：可能是VPN客户端没有安装好，请安装SoftEther VPN到默认C盘目录");
                    }
                }
                if (comboBox1.Text == "64位")
                {
                    if (comboBox2.Text == "Debug")
                    {
                        try
                        {
                            Process p = new Process();
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
                try
                {
                    Process[] _prorelease = Process.GetProcessesByName("GameSDK_Release");
                    Process[] _prodebug = Process.GetProcessesByName("GameSDK");
                    //Process[] _proVPN = Process.GetProcessesByName("vpnclient_x64");
                    //Process[] _proVPNmgr = Process.GetProcessesByName("vpncmgr_x64");
                    if (_prorelease.Length > 0) _prorelease[0].Kill();
                    if (_prodebug.Length > 0) _prodebug[0].Kill();
                    Runcmd(@"taskkill /im vpnclient_x64.exe");
                    Runcmd(@"taskkill /im vpncmgr_x64.exe");

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString()+"\n\n简短解释：结束进程失败，可能是没有权限或已经结束\n也可能是因为VPN程序不是由本启动器启动");
                }
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
                        Process p = new Process();
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
                        Process p = new Process();
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

        private void button5_Click(object sender, EventArgs e)
        {
            Form aboutform = new AboutBox1();
            aboutform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form frm3 = new Form3();
            frm3.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (button1.Text == "强行停止游戏"||button1.Text=="强行停止游戏和VPN")
            {
                if (checkBox1.Checked == true) button1.Text = "强行停止游戏和VPN";
                else button1.Text = "强行停止游戏";
            }
            else
            {
                if (checkBox1.Checked == true) button1.Text = "启动游戏和VPN";
                else button1.Text = "启动游戏";
            }
        }
    }
}