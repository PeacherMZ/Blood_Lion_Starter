using System;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace 血狮2_全球战火启动器
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static void SetText(string strtrt)
        {
            try
            {
                Clipboard.SetDataObject(strtrt ?? "");
            }
            catch(Exception fwcpee)
            {
                MessageBox.Show(fwcpee.ToString() + "\n\n简短解释：\n剪贴板访问出错，请检查是否有后台程序占用；若无法解决请进群咨询管理员");
            }
        }

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception delee)
            {
                MessageBox.Show(delee.ToString() + "\n\n简短解释：\n删除出错，请检查是否有权限；若无法解决请进群咨询管理员");
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
                    label233.Text = "下载进度" + mypercent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
            }
            catch (Exception downee)
            {
                MessageBox.Show(downee.ToString()+ "\n\n简短解释：\n下载出错，请检查是否有目录权限；磁盘空间是否已满；若无法解决请进群咨询管理员");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string now = "None";
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    now = (string)iData.GetData(DataFormats.Text);
                }
                if (now == null) now = "1";
                //MessageBox.Show(now);
                if (now.Length > 8)
                {
                    if (now[0] == 'h' && now[1] == 't' && now[7] == 'p' && now[now.Length - 1] == 'p')
                    {
                        SetText("iStudio");
                        label2.Text = now;
                        button1.Enabled = true;
                        button2.Enabled = true;
                        label1.Text = "选择了地图";
                        int st = now.Length - 1;
                        for (st = now.Length - 1; st > 0; st--)
                        {
                            if (now[st] == '/')
                            {
                                st++;
                                break;
                            }
                        }
                        for (int i = st; i <= now.Length - 5; i++)
                        {
                            label1.Text = label1.Text + now[i];
                        }
                        label1.Text = label1.Text + "！请选择类型：";
                        label3.Text = "等待开始";
                    }
                }
            }
            catch(Exception cpee)
            {
                MessageBox.Show(cpee.ToString() + "\n\n简短解释：\n剪贴板访问出错，请检查是否有后台程序占用；若无法解决请进群咨询管理员");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                string downto = Application.StartupPath + @"\GameSDK\Levels\Singleplayer\";
                string exto = downto;
                string sourcefile = label2.Text;
                int _st = sourcefile.Length - 1;
                for (_st = sourcefile.Length - 1; _st > 0; _st--)
                {
                    if (sourcefile[_st] == '/')
                    {
                        _st++;
                        break;
                    }
                }
                label1.Text = "";
                for (int i = _st; i <= sourcefile.Length - 1; i++)
                {
                    downto = downto + sourcefile[i];
                    label1.Text = label1.Text + sourcefile[i];
                }
                label1.Text += "下载中....";
                DownloadFile(label2.Text, downto, progressBar1, label3);
                string deletefile1 = downto;
                int j = deletefile1.Length - 1;
                for (j = deletefile1.Length - 1; j > 0; j--) if (deletefile1[j] == '.') break;
                j--;
                string deletefile = "";
                for (int k = 0; k <= j; k++) deletefile = deletefile + deletefile1[k];
                if (Directory.Exists(deletefile)) DelectDir(deletefile);
                label1.Text = "安装到地图文件夹....";
                label3.Text = "安装中....";
                ZipFile.ExtractToDirectory(downto, exto);
                MessageBox.Show("安装/修复完成");
                if (File.Exists(downto)) File.Delete(downto);
                label1.Text = "请在上方选择地图！";
                label3.Text = "等待开始";
                progressBar1.Value = 0;
            }
            catch(Exception ee1)
            {
                MessageBox.Show(ee1.ToString() + "\n\n简短解释：\n请检查本程序是否正确放在了游戏文件夹根目录；请检查游戏文件夹\\GameSDK\\Levels下Singleplayer文件夹是否存在；若问题无法解决，请进群咨询管理员");
                label1.Text = "下载出错，请重试或阅读帮助";
                label3.Text = "安装出错";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                string downto = Application.StartupPath + @"\GameSDK\Levels\Multiplayer\";
                string exto = downto;
                string sourcefile = label2.Text;
                int _st = sourcefile.Length - 1;
                for (_st = sourcefile.Length - 1; _st > 0; _st--)
                {
                    if (sourcefile[_st] == '/')
                    {
                        _st++;
                        break;
                    }
                }
                label1.Text = "";
                for (int i = _st; i <= sourcefile.Length - 1; i++)
                {
                    downto = downto + sourcefile[i];
                    label1.Text = label1.Text + sourcefile[i];
                }
                label1.Text += "下载中....";
                DownloadFile(label2.Text, downto, progressBar1, label3);
                string deletefile1 = downto;
                int j = deletefile1.Length - 1;
                for (j = deletefile1.Length - 1; j > 0; j--) if (deletefile1[j] == '.') break;
                j--;
                string deletefile = "";
                for (int k = 0; k <= j; k++) deletefile = deletefile + deletefile1[k];
                if (Directory.Exists(deletefile)) DelectDir(deletefile);
                label1.Text = "安装到地图文件夹....";
                label3.Text = "安装中....";
                ZipFile.ExtractToDirectory(downto, exto);
                MessageBox.Show("安装/修复完成");
                if (File.Exists(downto)) File.Delete(downto);
                label1.Text = "请在上方选择地图！";
                label3.Text = "等待开始";
                progressBar1.Value = 0;
            }
            catch(Exception ee2)
            {
                MessageBox.Show(ee2.ToString() + "\n\n简短解释：\n请检查本程序是否正确放在了游戏文件夹根目录；请检查游戏文件夹\\GameSDK\\Levels下Multiplayer文件夹是否存在；若问题无法解决，请进群咨询管理员");
                label1.Text = "下载出错，请重试或阅读帮助";
                label3.Text = "安装出错";
            }
        }
    }
}