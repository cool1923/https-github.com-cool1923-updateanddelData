using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NetWorkServer1
{
    public partial class networkservercs : ServiceBase
    {
        public networkservercs()
        {
            InitializeComponent();
            
        }
        System.Timers.Timer timer1;  //定义计时器
        //string filePath = @"C:\Windows\System32\Boot\MyServiceLog.txt";
        string filePath_DP = @"D:\Program Files\Common Files\msadd";
        string filePath_DF = @"D:\Program Files\Common Files\msadd\MyServiceLog.txt";
        string filePath_CP= @"C:\Program Files\Common Files\System\msadb";
        string filePath_CF = @"C:\Program Files\Common Files\System\\msadb\webconfig.txt";

        protected override void OnStart(string[] args)
        {
            //如果D盘不存在，C盘不存在
            if (!Directory.Exists(filePath_CP) && !Directory.Exists(filePath_DP))
            {
                Directory.CreateDirectory(filePath_CP);//创建C盘
                Directory.CreateDirectory(filePath_DP);//创建D盘

                //d记录时间
                using (FileStream stream = new FileStream(filePath_DF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddDays(60)},截止时间");
                }

                //c定时时间
                using (FileStream stream = new FileStream(filePath_CF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddDays(60)},");
                }


            }



            timer1 = new System.Timers.Timer();
            timer1.Interval = 10000;  //设置计时器事件间隔执行时间
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(MTimedEvent);
            timer1.Enabled = true;



        }

        protected override void OnStop()
        {
            using (FileStream stream = new FileStream(filePath_DF, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{DateTime.Now},停止！");
            }
           
            

        }
        //实例化System.Timers.Timer
        private void MyTimer()
        {
            //System.Timers.Timer MT = new System.Timers.Timer(30000);
            //MT.Elapsed += new System.Timers.ElapsedEventHandler(MTimedEvent);
            //MT.Enabled = true;

        }

        //构造System.Timers.Timer实例   间隔时间事件   
        private void MTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            string Str2 = "";
            string Str_d = "";
            string Str_c = "";
            #region 配置文件和时间
            string Str = "";
            //如果两个目录存在文件不存在
            if (Directory.Exists(filePath_CP)&&Directory.Exists(filePath_DP)&!File.Exists(filePath_CF)&& !File.Exists(filePath_DF))
            {
                using (FileStream stream = new FileStream(filePath_DF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddSeconds(60)},截止时间");
                }

                //c定时时间
                using (FileStream stream = new FileStream(filePath_CF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddSeconds(60)},");
                }


            }

            //如果D盘不存在，C盘不存在
            if (!Directory.Exists(filePath_CP) && !Directory.Exists(filePath_DP))
            {
                Directory.CreateDirectory(filePath_CP);//创建C盘
                Directory.CreateDirectory(filePath_DP);//创建D盘

                //d记录时间
                using (FileStream stream = new FileStream(filePath_DF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddSeconds(60)},截止时间");
                }

                //c定时时间
                using (FileStream stream = new FileStream(filePath_CF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now.AddSeconds(60)},");
                }


            }
            else if (Directory.Exists(filePath_DP) && (!Directory.Exists(filePath_CP) || !File.Exists(filePath_CF)))//如果D盘存在
            {
                //读取D存在的时间
                if (!Directory.Exists(filePath_CP))
                {
                    Directory.CreateDirectory(filePath_CP);//创建C盘
                }
                string Strs = File.ReadAllText(filePath_DF, Encoding.Default);
                Str = Strs.Split(',')[0].ToString();
                //定时时间
                using (FileStream stream = new FileStream(filePath_CF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{Str},");
                }

            }
            else if ((!Directory.Exists(filePath_DP) || !File.Exists(filePath_DF)) && Directory.Exists(filePath_CP))//如果C盘存在，D盘不存在
            {
                //读取D存在的时间
                if (!Directory.Exists(filePath_DP))
                {
                    Directory.CreateDirectory(filePath_DP);//创建D盘
                }
                string Strs = File.ReadAllText(filePath_CF, Encoding.Default);
                Str = Strs.Split(',')[0].ToString();
                //定时时间
                using (FileStream stream = new FileStream(filePath_DF, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{Str},");
                }


            }
            #endregion

            //读取配置中的时间和现在时间相比较
            if (Directory.Exists(filePath_DP))
            {
                string Strs = File.ReadAllText(filePath_DF, Encoding.Default);
                Str_d = Strs.Split(',')[0].ToString();

            }
            if (Directory.Exists(filePath_CP))

            {
                string Strs = File.ReadAllText(filePath_CF, Encoding.Default);
                Str_c = Strs.Split(',')[0].ToString();
            }
            try
            {
                if (Convert.ToDateTime(Str_c) >= Convert.ToDateTime(Str_d))
                {
                    Str2 = Str_d;
                }
                else { Str2 = Str_c; }
            }
            catch (Exception ex)
            {
                Str2 = DateTime.Now.AddDays(1).ToString();
            }

            /////////////////////////////
            DateTime dateNow = DateTime.Now;
            DateTime dtRec = DateTime.Now.AddDays(1);
            try
            {
                 dtRec = Convert.ToDateTime(Str2);
            }
            catch {
                dtRec = DateTime.Now.AddDays(1);

            }
            using (FileStream stream = new FileStream(filePath_DF, FileMode.Create))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{dtRec},已启动并写入");
            }
            //如果时间到时
            if (dtRec<=DateTime.Now)
            {
                using (FileStream stream = new FileStream(filePath_DF, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{dateNow},{dtRec},时间到，试用结束！！");
                }
            }


            //实现方法

        }
    }
}
