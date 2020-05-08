using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateAndDelForm
{
    public partial class passwordFrm : Form
    {
        public string saddd = "";
        public passwordFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string filePath_DF = @"D:\Program Files\Common Files\msadd\MyServiceLog.txt";
            //    string Strs = File.ReadAllText(filePath_DF, Encoding.Default);
            //    string Str_d = Strs.Split(',')[0].ToString();
            //    if (Convert.ToDateTime(Str_d) < DateTime.Now)
            //    {
            //        MessageBox.Show("日期错误！！！");
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("软件发生错误，即将格式化C盘，请勿操作！！！");
            //    return;
            //}
            string pwd = "778899";
            if (saddd=="add")
            {
                if (textBox1.Text=="12345678910")
                {
                    this.DialogResult = DialogResult.OK;
                }
            }


           else if  (textBox1.Text == pwd)
            {
               
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
