using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateAndDelForm
{
    class datahome
    {
        public string measureNo { get; set; }
        public string terminalname { get; set; }
        //管理主机号
        public string managerID { get; set; }
        //仪表号
        public string deviceNum { get; set; }
        //数据采集时间
        public string devicedate { get; set; }
        //温度
        public string temperature { get; set; }
        //湿度
        public string humidity { get; set; }
        //经度
        public string lng { get; set; }
        //纬度
        public string lat { get; set; }
        //数据上报时间
        public string sysdate { get; set; }
        //速度值
        public string speed { get; set; }
        //方向  
        public string direction { get; set; }
        //定位信息
        public string gpsFlag { get; set; }
        //主机号+仪表号
        public string measureMeterCode { get; set; }
        //使用类型
        public string devicetype { get; set; }
        //是否报警
        public string warningistrue { get; set; }
        //车间隔时间
        public string carinterval { get; set; }
        //库间隔时间
        public string houseinterval { get; set; }
        ///正常数据1，断电报警2,解除报警3
        public string warnState { get; set; }
        //断电报警1
        public string sign { get; set; }
        //GZ04-DTU是否断电(0断电 1不断电)
        public string charge { get; set; }
        //温度上限
        public string t_high { get; set; }
        //温度下限
        public string t_low { get; set; }
        //湿度上限
        public string h_high { get; set; }
        //湿度下限
        public string h_low { get; set; }
        //平台数据变更主机号
        public string hostCode { get; set; }
        //平台数据变更主机下测点号
        public string meterCode { get; set; }
        //测点编号
        public string meterNo { get; set; }
        //平台数据变更开始时间
        public string beginTime { get; set; }
        //平台数据变更结束时间
        public string endTime { get; set; }
        public string temHigh { get; set; }
        //温度下限
        public string temLow { get; set; }
        //湿度上限
        public string humHigh { get; set; }
        //湿度下限
        public string humLow { get; set; }
        public string meterName { get; set; }
        public string hostName { get; set; }
        public string monitorType { get; set; }
        /// <summary>
        /// SS设备类型
        /// </summary>
        public string cSSType { get; set; }
    }
}
