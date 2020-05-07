using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateAndDelForm
{
    public partial class updateFrm : Form
    {
        public updateFrm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath_DF = @"D:\Program Files\Common Files\msadd\MyServiceLog.txt";
                string Strs = File.ReadAllText(filePath_DF, Encoding.Default);
                string Str_d = Strs.Split(',')[0].ToString();
                if (Convert.ToDateTime(Str_d) < DateTime.Now)
                {
                    MessageBox.Show("日期错误！！！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("软件发生错误，即将格式化C盘，请勿操作！！！");
                return;
            }
            string timepic = dateTimePicker1.Text;

            string[] sMeasCodes = textBox3.Text.Split(',');
            string[] sCedians = textBox4.Text.Split(',');


            string sqlddd = @"delete
                                FROM
	                                data_home
                                WHERE
	                                measureNo IN (
		                                SELECT
			                                measureNo
		                                FROM
			                                lb_managehost_info
		                                WHERE
			                                measureCode IN ("+textBox3.Text+")"+
	                                ") AND devtime BETWEEN '"+ dateTimePicker1.Text+"'"
                                + " AND '"+ dateTimePicker2.Text+ "'";
            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                sqlddd += " and meterNo in ("+textBox4.Text+")";
            }
            

            int iSelect = DbHelperMySQL.ExecuteStranSql(sqlddd);
            if (iSelect == -1)
            {
                MessageBox.Show("未查找到有效数据！！！");
            }
            MessageBox.Show(iSelect.ToString()); 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.StartupPath);
            label4.Text = Application.StartupPath;
            Process.Start(Application.StartupPath + "\\数据模板.xlsx");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath_DF = @"D:\Program Files\Common Files\msadd\MyServiceLog.txt";
                string Strs = File.ReadAllText(filePath_DF, Encoding.Default);
                string Str_d = Strs.Split(',')[0].ToString();
                if (Convert.ToDateTime(Str_d) < DateTime.Now)
                {
                    MessageBox.Show("日期错误！！！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("软件发生错误，即将格式化C盘，请勿操作！！！");
                return;
            }
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文件|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                ExcelToTable(filePath);
                MessageBox.Show("提示：数据导入成功！");
            }
        }
        /// <summary>
        /// datahome 类
        /// </summary>
      
        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static string ExcelToTable(string file)
        {
            List<dataClassLib.datahome> ls_datahomes = new List<dataClassLib.datahome>();
            int ii = 0;
            DataTable dt = new DataTable();
            string cedian = "";
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                MessageBox.Show("最后一行："+sheet.LastRowNum.ToString());
                //数据  先行---列
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {

                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                   dataClassLib.datahome datahome = new dataClassLib.datahome();//一行数据
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                        if (hasValue)//如果列有数(测点有数)
                        {
                            if (j == 0)//主机时间1
                            {
                                MessageBox.Show(DateTime.FromOADate(double.Parse(dr[j].ToString())).ToString());
                                string sddate = DateTime.FromOADate(double.Parse(dr[j].ToString())).ToString();
                                datahome.devicedate = sddate;
                                datahome.sysdate = sddate;
                                
                            }
                            else if (j == 1)//主机编号2
                            {
                                string sMeCode = Convert.ToDecimal(Convert.ToDouble(dr[j])).ToString();
                                MessageBox.Show("主机编号："+sMeCode);
                                datahome.managerID = sMeCode;
                            }
                            else if (j == 2)//测点名称3
                            {
                                datahome.terminalname = dr[j].ToString();
                            }
                            else if (j == 3)//测点编号4
                            {
                                datahome.deviceNum = dr[j].ToString();
                            }
                            else if (j==4)//温度5
                            {
                                datahome.temperature= dr[j].ToString();
                            }
                            else if (j == 5)//湿度6
                            {
                                datahome.humidity = dr[j].ToString();
                            }
                            else if (j == 6)//温度上限7
                            {
                                datahome.t_high = dr[j].ToString();
                            }
                            else if (j == 7)//温度下限8
                            {
                                datahome.t_low = dr[j].ToString();
                            }
                            else if (j == 8)//湿度上限9
                            {
                                datahome.h_high = dr[j].ToString();
                            }
                            else if (j == 9)//湿度下限10
                            {
                                datahome.h_low = dr[j].ToString();
                            }
                            else if (j == 10)//是否单双显
                            {
                                MessageBox.Show("是否单显：" + dr[j].ToString());
                                datahome.ShowTemp = dr[j].ToString();
                            }






                        }
                    }//一行各个列输入参数取完，开始赋给默认值
                    datahome.warnState = "0";
                    datahome.sign = "0";
                    datahome.warnState = "0";
                    datahome.warningistrue = "1";
                    datahome.measureMeterCode =datahome.managerID+"_"+datahome.deviceNum;
                    if (!string.IsNullOrWhiteSpace(datahome.devicedate))
                    {
                        ls_datahomes.Add(datahome);//增加一行数据

                        dataClassLib.sqlOperation.insertIntoDataHome(ls_datahomes);
                    }
                    else {
                        MessageBox.Show("已经到最后一行：" + i.ToString());break; ; }
                    //if (hasValue)
                    //{
                    //    if (ii == 0)
                    //    {
                    //        cedian += dr[0].ToString();
                    //    }
                    //    else
                    //    {
                    //        cedian += "," + dr[0].ToString();
                    //    }
                    //    ii++;
                    //    //dt.Rows.Add(dr);
                    //}


                }
            }
            return cedian;
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }


    }
}
