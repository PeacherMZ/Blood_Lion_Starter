using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace 血狮2_全球战火启动器
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
        }

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void logoPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {

        }
        public const string updateUrl = "http://peacher.sszx.live/BloodLionStarterUpdate.xml";
        public const string AppName = "BloodLionStarterUpdate";
        public string Version;
        public string NewVersion;
        public string DownloadUrl;
        public string NewDescription;

        public void checkUpdate()
        {
            try
            {
                Version = label1.Text;
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
                            if (xml.Name == "Version") NewVersion = xml.InnerText;
                            else if (xml.Name == "Download") DownloadUrl = xml.InnerText;
                            else if (xml.Name == "NewDescription") NewDescription = xml.InnerText;
                        }
                    }
                }
                int VersionNum;
                int NewVersionNum;
                int.TryParse(Version, out VersionNum);
                int.TryParse(NewVersion, out NewVersionNum);
                if (NewVersionNum > VersionNum)
                {
                    if (MessageBox.Show("当前版本 " + Version + "  检测到新版本 " + NewVersion + "\n\n" + NewDescription+ "\n\n是否更新？", "发现新版本",MessageBoxButtons.YesNo,MessageBoxIcon.Information).ToString()=="Yes")
                    {
                        //下载并替换
                        try
                        {
                            Process UpdateProgram = new Process();
                            UpdateProgram.StartInfo.UseShellExecute = true;
                            UpdateProgram.StartInfo.FileName = @".\BLUpdate.exe";
                            UpdateProgram.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() + "\n\n简短解释：\n可能是更新程序丢失，请检查BLUpdate.exe程序是否在游戏根目录下");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("当前版本 " + Version + " 已是最新版本", "已是最新版本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新出现错误，请确认网络连接无误后重试！" + ex.ToString());
            }
        }

        public void DownloadFile(string URL, string filename)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
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
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    int mypercent = (int)percent;
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
            }
            catch (Exception downee)
            {
                MessageBox.Show(downee.ToString() + "\n\n简短解释：\n下载出错，请检查是否有目录权限；磁盘空间是否已满；若无法解决请进群咨询管理员");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadFile("http://peacher.sszx.live/istudioupdate/BLUpdate.exe",@".\BLUpdate.exe");
            checkUpdate();
        }
    }
}