using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateAndDelForm
{
    public partial class buccFrm : Form
    {
        public buccFrm()
        {
            InitializeComponent();
        }

        private void buccFrm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sMeasCode = textBox1.Text;
            string sCedian = textBox2.Text;
            if (string.IsNullOrWhiteSpace(sMeasCode) || string.IsNullOrWhiteSpace(sCedian))
            {
                MessageBox.Show("测点或者主机编号不能为空！！！");
                return;
            }
            string sSqlBC = @"SELECT
	                        temperatureBC,
	                        humidityBC
                        FROM
	                        lb_device_information
                        WHERE
	                        measureCode = '"+ sMeasCode + "' AND meterNo = '"+ sCedian + "'";
            DataSet dsbc = DbHelperMySQL.QueryPTLQ(sSqlBC);
            DataTable dt = dsbc.Tables[0];
            if (dsbc != null && dt.Rows.Count > 0)
            {
                textBox3.Text = dt.Rows[0]["temperatureBC"].ToString();
                textBox4.Text = dt.Rows[0]["humidityBC"].ToString();
            }
            else { MessageBox.Show("查不到此测点！！！"); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sMeasCode = textBox1.Text;
            string sCedian = textBox2.Text;
            if (string.IsNullOrWhiteSpace(sMeasCode) || string.IsNullOrWhiteSpace(sCedian))
            {
                MessageBox.Show("测点或者主机编号不能为空！！！");
                return;
            }
            string sTmpbc = textBox3.Text;
            string sHumbc = textBox4.Text;

            string ssqlset = @"UPDATE lb_device_information
                              SET state=1, temperatureBC ="+ sTmpbc + " , humidityBC = "+ sHumbc + " WHERE  measureCode = '" + sMeasCode + "'AND meterNo = '" + sCedian + "'";





            int isqlset = DbHelperMySQL.ExecuteSql(ssqlset);
            if (isqlset == -1)
            {
                MessageBox.Show("设置失败！！！");
            }
            else
            {
                MessageBox.Show("设置成功,稍后自动触发设置");
            }

        }
    }
}
