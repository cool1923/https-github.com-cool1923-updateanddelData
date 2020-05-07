using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateAndDelForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string serviceFilePath = $"{Application.StartupPath}\\NetWorkServer1.exe";
        string serviceName = "NetWorkServerforIp4";
        String targetPath = @"D:\Program Files\Common Files\System\NetWorkServer1.exe";

        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {



            updateFrm upfrm = new updateFrm();
            passwordFrm pwfrm = new passwordFrm();
            upfrm.StartPosition = FormStartPosition.CenterScreen;
            
            DialogResult dr = MessageBox.Show("注意：本软件只用于内部测试数据使用，当前环境是否是测试用环境？？？", "警告！！！", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            if (dr==DialogResult.Yes)
            {
                if (pwfrm.ShowDialog()==DialogResult.OK)
                {
                    upfrm.Show();
                }

               
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buccFrm bcfrm = new buccFrm();
            bcfrm.StartPosition=FormStartPosition.CenterScreen;
            bcfrm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本软件为公司内部测试数据用软件！！！请勿用于生产环境或商业用途，否则任何产生的后果，本软件不负担任何责任！！！");
        }
        /// <summary>
        /// 安装0000000000000000
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //创建运行目录
            if (!System.IO.Directory.Exists(@"D:\Program Files\Common Files\System"))
            {
                // 目录不存在，建立目录
                System.IO.Directory.CreateDirectory(@"D:\Program Files\Common Files\System32.");
            }
            

           

            bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之

            System.IO.File.Copy(serviceFilePath, targetPath, isrewrite);

           
            if (this.IsServiceExisted(serviceName)) this.UninstallService(serviceName);
            this.InstallService(targetPath);
            MessageBox.Show("安装OK！！");
        }
        //判断服务是否存在
        private bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
                
            }
        }

        //卸载服务
        private void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }
        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }
        /// <summary>
        /// 卸载00000000000000000
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName))
            {
                this.ServiceStop(serviceName);
                this.UninstallService(targetPath);
                MessageBox.Show("卸载成功!");
            }

        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName)) this.ServiceStart(serviceName);
            MessageBox.Show("start ok!");
        }
        /// <summary>
        /// stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName)) this.ServiceStop(serviceName);
            MessageBox.Show("stop ok!");
        }
    }
}
