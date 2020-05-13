using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateAndDelForm.dataClassLib
{
    class sqlOperation
    {
       
        /// <summary>
        /// 获取字符串类型的主键
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {

            string id = DateTime.Now.ToString("yyMMddHHmmssfffffff");
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            id += guid.Substring(0, 10);
            return id;
        }

        /// <summary>
        /// 插入历史和报警
        /// </summary>
        /// <param name="list">历史</param>
        /// <param name="listLishi">报警</param>
        /// <param name="sendStr"></param>

        public static void insertIntoDataHome(List<datahome> list, List<datahome> listLishi)
        {
            string id;
            String sql1 = "";
            //串口、TCP数据报文上传平台记录表插入
            String sql2 = string.Empty;
            string measureCode = "";
            string devtime = "";
           




            if (list != null && list.Count > 0)
            {

                //查询出库房类型
                string mcc = string.Empty;

                //串口、TCP数据报文上传平台记录表插入,数据库表名sendStrToPTRecord
                measureCode = list[0].managerID;
                //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:/kelong521/Debug/log1/" + DateTime.Now.ToString("yy-MM-dd-HH") + "-" + "" + measureCode + "sjk.txt", true))
                //{
                //    sw.WriteLine(DateTime.Now.ToString() + "NEWD__主机：" + measureCode + "------------------START----------------开始执行插入历史数据：" + list.Count);
                //}
                devtime = list[0].devicedate;
                
                

                //插入历史数据表
                sql1 = "insert into data_home(id,measureCode,meterNo,devtime,temperature,humidity,lng,lat,createDate,warnState,sign,measureMeterCode,warningistrue,carinterval,houseinterval,mcc,t_high,t_low,h_high,h_low,measureNo,terminalname) values ";
                for (int j = 0; j < list.Count; j++) 
                {
                    id = GetNewId();

                    string measureCodea = list[j].managerID;//管理主机编号
                    string meterNo = list[j].deviceNum;//管理主机的仪表编号
                    string ShowTemp= list[j].ShowTemp;
                   // String sqlli = "select ShowSingleTemp  FROM lb_device_information where measureCode='" + measureCodea + "' and meterNo='" + meterNo + "' ";//查询语句
                   // object ShowTemp = DbHelperMySQL.GetSingle(sqlli);//返回一个查询对象结果（字段的值）

                    if (ShowTemp.ToString() != "1")
                    {

                        if (j > 0)
                        {
                            sql1 += " , ('" + id + "','" + list[j].managerID + "', '" + list[j].deviceNum + "','"
                                + list[j].devicedate + "','" + list[j].temperature + "','" + list[j].humidity + "','"
                                + list[j].lng + "','" + list[j].lat + "','" + list[j].sysdate + "','" + list[j].warnState + "','0','" + list[j].measureMeterCode + "','"
                                + list[j].warningistrue + "','','" + list[j].houseinterval + "',(select housetype from lb_device_information where measureCode = '"
                                + list[j].managerID + "' and meterNo = '" + list[j].deviceNum + "')," + list[j].t_high + "," + list[j].t_low + ","
                                + list[j].h_high + "," + list[j].h_low + ",(SELECT measureNo from lb_managehost_info where measureCode='"+ list[j].managerID + "'),'" + list[j].terminalname + "')";
                        }
                        else
                        {
                            sql1 += "('" + id + "','" + list[j].managerID + "', '" + list[j].deviceNum + "','" + list[j].devicedate + "','"
                                + list[j].temperature + "','" + list[j].humidity + "','" + list[j].lng + "','" + list[j].lat + "','"
                                + list[j].sysdate + "','" + list[j].warnState + "','0','" + list[j].measureMeterCode + "','"
                                + list[j].warningistrue
                                + "','','" + list[j].houseinterval + "',(select housetype from lb_device_information where measureCode = '"
                                + list[j].managerID + "' and meterNo = '" + list[j].deviceNum + "')," + list[j].t_high + "," + list[j].t_low
                                + "," + list[j].h_high + "," + list[j].h_low + ",(SELECT measureNo from lb_managehost_info where measureCode='" + list[j].managerID + "'),'" + list[j].terminalname + "')";
                        }
                    }
                    else
                    {
                        if (j > 0)
                        {
                            sql1 += " , ('" + id + "','" + list[j].managerID + "', '" + list[j].deviceNum + "','"
                                + list[j].devicedate + "','" + list[j].temperature + "',null,'"
                                + list[j].lng + "','" + list[j].lat + "','" + list[j].sysdate + "','" + list[j].warnState + "','0','" + list[j].measureMeterCode + "','"
                                + list[j].warningistrue + "','','" + list[j].houseinterval + "',(select housetype from lb_device_information where measureCode = '"
                                + list[j].managerID + "' and meterNo = '" + list[j].deviceNum + "')," + list[j].t_high + "," + list[j].t_low + ",null,null,(SELECT measureNo from lb_managehost_info where measureCode='" + list[j].managerID + "'),'" + list[j].terminalname + "')";
                        }
                        else
                        {
                            sql1 += "('" + id + "','" + list[j].managerID + "', '" + list[j].deviceNum + "','" + list[j].devicedate + "','"
                                + list[j].temperature + "',null,'" + list[j].lng + "','" + list[j].lat + "','"
                                + list[j].sysdate + "','" + list[j].warnState + "','0','" + list[j].measureMeterCode + "','"
                                + list[j].warningistrue
                                + "','','" + list[j].houseinterval + "',(select housetype from lb_device_information where measureCode = '"
                                + list[j].managerID + "' and meterNo = '" + list[j].deviceNum + "')," + list[j].t_high + "," + list[j].t_low
                                + ",null,null,(SELECT measureNo from lb_managehost_info where measureCode='" + list[j].managerID + "'),'" + list[j].terminalname + "')";
                        }
                    }
                }



                
            }
            //历史报警
            if (listLishi != null && listLishi.Count > 0)
            {
                sql1 += ";insert into lb_warning_data(id,measureCode,meterNo,devtime,temperature,humidity,lng,lat,createDate,warnState,measureMeterCode,warningistrue,carinterval,houseinterval,mcc,t_high,t_low,h_high,h_low,measureNo,terminalname) values ";
                for (int j = 0; j < listLishi.Count; j++)
                {
                    id = GetNewId();
                    string ShowTemp = list[j].ShowTemp;
                    string measureCodea = listLishi[j].managerID;//管理主机编号
                    string meterNo = listLishi[j].deviceNum;//管理主机的仪表编号
                    //String sqlli = "select ShowSingleTemp FROM lb_device_information where measureCode='" + measureCodea + "' and meterNo='" + meterNo + "' ";//查询语句
                    //object ShowTemp = DbHelperMySQL.GetSingle(sqlli);//返回一个查询对象结果（字段的值）

                    if (ShowTemp.ToString() != "1")
                    {
                        if (j > 0)
                        {
                            sql1 += " , ('" + id + "','" + listLishi[j].managerID + "', '" + listLishi[j].deviceNum + "','" + listLishi[j].devicedate + "','"
                                + listLishi[j].temperature + "','" + listLishi[j].humidity + "','" + listLishi[j].lng + "','" + listLishi[j].lat + "','"
                                + listLishi[j].sysdate + "','" + listLishi[j].warnState + "','" + listLishi[j].measureMeterCode + "','"
                                + listLishi[j].warningistrue + "','','',(select housetype from lb_device_information where measureCode = '"
                                + listLishi[j].managerID + "' and meterNo = '" + listLishi[j].deviceNum + "')," + listLishi[j].t_high + "," + listLishi[j].t_low + ","
                                + listLishi[j].h_high + "," + listLishi[j].h_low + ",'" + listLishi[j].measureNo + "','" + listLishi[j].terminalname + "')";
                        }
                        else
                        {
                            sql1 += "('" + id + "','" + listLishi[j].managerID + "', '" + listLishi[j].deviceNum + "','" + listLishi[j].devicedate + "','"
                                + listLishi[j].temperature + "','" + listLishi[j].humidity + "','" + listLishi[j].lng + "','" + listLishi[j].lat + "','"
                                + listLishi[j].sysdate + "','" + listLishi[j].warnState + "','" + listLishi[j].measureMeterCode + "','"
                                + listLishi[j].warningistrue + "','','',(select housetype from lb_device_information where measureCode = '"
                                + listLishi[j].managerID + "' and meterNo = '" + listLishi[j].deviceNum + "')," + listLishi[j].t_high + "," + listLishi[j].t_low + ","
                                + listLishi[j].h_high + "," + listLishi[j].h_low + ",'" + listLishi[j].measureNo + "','" + listLishi[j].terminalname + "')";
                        }
                    }
                    else
                    {
                        if (j > 0)
                        {
                            sql1 += " , ('" + id + "','" + listLishi[j].managerID + "', '" + listLishi[j].deviceNum + "','" + listLishi[j].devicedate + "','"
                                + listLishi[j].temperature + "',null,'" + listLishi[j].lng + "','" + listLishi[j].lat + "','"
                                + listLishi[j].sysdate + "','" + listLishi[j].warnState + "','" + listLishi[j].measureMeterCode + "','"
                                + listLishi[j].warningistrue + "','','',(select housetype from lb_device_information where measureCode = '"
                                + listLishi[j].managerID + "' and meterNo = '" + listLishi[j].deviceNum + "')," + listLishi[j].t_high + ","
                                + listLishi[j].t_low + ",null,null,'" + listLishi[j].measureNo + "','" + listLishi[j].terminalname + "')";
                        }
                        else
                        {
                            sql1 += "('" + id + "','" + listLishi[j].managerID + "', '" + listLishi[j].deviceNum + "','" + listLishi[j].devicedate + "','"
                                + listLishi[j].temperature + "','" + listLishi[j].humidity + "','" + listLishi[j].lng + "','" + listLishi[j].lat + "','"
                                + listLishi[j].sysdate + "','" + listLishi[j].warnState + "','" + listLishi[j].measureMeterCode + "','"
                                + listLishi[j].warningistrue + "','','',(select housetype from lb_device_information where measureCode = '"
                                + listLishi[j].managerID + "' and meterNo = '" + listLishi[j].deviceNum + "')," + listLishi[j].t_high + "," + listLishi[j].t_low
                                + ",null,null,'" + listLishi[j].measureNo + "','" + listLishi[j].terminalname + "')";
                        }
                    }

                }

            }
        


            

            if (!string.IsNullOrWhiteSpace(sql1))
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:/kelong521/Debug/log1/" + DateTime.Now.ToString("yy-MM-dd-HH") + "-" + "" + measureCode + "sjk.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "即将开始执行插入历史报警数据SQL：" + sql1);
                }

                DbHelperMySQL.ExecuteSql(sql1);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:/kelong521/Debug/log1/" + DateTime.Now.ToString("yy-MM-dd-HH") + "-" + "" + measureCode + "sjk.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "---------------------END---------------------已经执行执行插入历史报警数据SQL------------------------");
                }

            }

        }

    }
}
