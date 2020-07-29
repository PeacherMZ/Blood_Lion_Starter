using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace BLUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static readonly string CmdPath = @"C:\Windows\System32\cmd.exe";
        public static void Runcmd(string cmd, out string output)
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
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
        }
        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog, System.Windows.Forms.Label label233)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    int mypercent = (int)percent;
                    label233.Text = mypercent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
                this.Text = "血狮2：全球战火启动器 更新完成";
                MessageBox.Show("已经成功升级到 " + NewVersion + " 版本");
                this.Close();
            }
            catch (Exception downee)
            {
                MessageBox.Show(downee.ToString() + "\n\n简短解释：\n下载出错，请检查是否有目录权限；磁盘空间是否已满；若无法解决请进群咨询管理员");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string MyCommand;
            MyCommand = @"taskkill /im 血狮2：全球战火启动器.exe";
            Runcmd(MyCommand, out string output);
        }
        public string NewVersion = "";
        private void Form1_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show("sdd");
            string updateUrl = "http://peacher.sszx.live/BloodLionStarterUpdate.xml";
            string AppName = "BloodLionStarterUpdate";
            string DownloadUrl= "http://peacher.sszx.live/BloodLionStarterUpdate.xml";

            Thread.Sleep(200);

            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(updateUrl);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == AppName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Download") DownloadUrl = xml.InnerText;
                            else if (xml.Name == "Version") NewVersion = xml.InnerText;
                        }
                    }
                }
                DownloadFile(DownloadUrl, @".\血狮2：全球战火启动器.exe", progressBar1, label1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n\n可能是网络出问题了，请检查网络", "提示");
            }
        }
    }
}
