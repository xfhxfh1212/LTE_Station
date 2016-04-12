using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Xml;

namespace COM.ZCTT.AGI.Common
{
    public class Global
    {
        public static AGIInterface.Class1 tempClass = new AGIInterface.Class1();

        public static TestStatus testStatus;
        public static TestMode testMode;
        public static ScanMode scanMode;
        public static string playbackemode = "";
        public static string CurrentSender = "";
        public Global()
        {
            ProTracReq = new byte[0] { };
            CellScanReq = new byte[0] { };
            UnSpeCellScanReq = new byte[0] { };
            DebugReq = new byte[0] { };

            testStatus = TestStatus.Stop;
            testMode = TestMode.RealTime;
            scanMode = ScanMode.Single;
            connectionStatus = false;
        }

        #region 与协议跟踪和小区扫描有关的变量
        
        //测试用,指定小区扫描
        public static List<CELL_INFO_STRU2> GCellInfoList = new List<CELL_INFO_STRU2>();
        public static int GCellNum = 0;
        public static List<EARFCN_INFO_STRU2> GEARFCNList = new List<EARFCN_INFO_STRU2>();//FOR BANDlist
        public static int GEarfcnNum = 0;
        public static List<EARFCN_INFO_STRU2> GEList = new List<EARFCN_INFO_STRU2>();//FOR EarfcnLIST
        public static int GENum = 0;
        public static byte GScanMode =1;
        /// <summary>
        /// 开始“小区扫描”还是开始“协议跟踪”
        /// </summary>
        public static string ReadyModule = "";//“开始”按钮点击时，判断该属性.确认协议跟踪相关配置是否完成
        public static string CRModule = "";

        /// <summary>
        /// “协议跟踪”配置信息的byte数组
        /// </summary>
        public static byte[] ProTracReq;

        /// <summary>
        /// “小区扫描”配置信息的byte数组
        /// </summary>
        public static byte[] CellScanReq;
        public static byte[] UnSpeCellScanReq;
        /// <summary>
        /// “调试”配置信息的byte数组
        /// </summary>
        public static byte[] DebugReq;
        /// <summary>
        /// “小区扫描”修改频点  guoyunjun
        /// </summary>
        public static String strPEACN;

        /// <summary>
        /// 协议跟踪过滤需要显示的MessageType 和 LayerType
        /// </summary>
        public static List<string> listFilter = new List<string> { };

        /// <summary>
        /// 存放最近打开文件路径
        /// </summary>
        public static List<string> RecentFiles = new List<string> { };
        /// <summary>
        /// STMSI统计数据
        /// </summary>
        public static List<STMSIStruct> GStmsiList = new List<STMSIStruct> { };

        /// <summary>
        /// Count统计数据
        /// </summary>
        public static List<CountStruct> UeCaputreList = new List<CountStruct> { };
        public static List<MulCountStruct> RARCountList = new List<MulCountStruct> { };
        #endregion

        public static bool FirstTime = true;//计算相对时间使用

        #region 跟设备有关的变量定义
        public static bool isReboot = true;
        public static string isStart = "false";//此变量用于标示AGI是首次打开，还是意外关闭后打开状态，用于log文件的持续保存判断
        public static string logFileConfigPath = System.Windows.Forms.Application.StartupPath + "\\LogFileConfig.xml";
        public static string binaryFileSavePath = "";
        public static string CellScanFileSavePath = "";
        public static string deviceConfigFilePath =System.Windows.Forms.Application.StartupPath +"\\DeviceConfig.xml";
        public static bool RebootButtonEnable = true;
        public static string GCurrentDevice = "";
        public enum DeviceInstrumentStateValue
        {
            Idle,
            Working
        }
        public enum PortTypeValue
        {
            USB,
            LAN
        }
        public enum DeviceStateValue
        {
            Connecting,
            Disconnection,
            connState
        }
        public enum WorkModeValue
        {
            CellScan = 1,
            ProtocolTrack
        }
        public enum OutDataTypeValue
        {
            IQ,
            TestData,
            MeasuringData,
            LayerOutData
        }
        public enum DataSaveMode
        {
            ExternalStore,
            TimingSave,
            StoredContiguously,
            TriggerSave
        }
        public enum DataSaveType
        {
            CSV,
            TXT
        }
        #endregion

        #region 与主程序有关的变量
        /// <summary>
        /// 控制状态：播放、暂停、停止
        /// "Play","Pause","Stop"
        /// </summary>
        public enum TestStatus
        {
            Start,
            Pause,
            Stop,
        };//

        /// <summary>
        /// 测试模式：实时、回放
        /// "Realtime","Playback"
        /// </summary>
        public enum TestMode
        {
            RealTime,
            PlayBack,
            CellScan,
        };

        /// <summary>
        /// 小区扫描模式：单词扫描、循环扫描
        /// "Single","Cycle"
        /// </summary>
        public enum ScanMode
        {
            Single,
            Cycle,
        };

        /// <summary>
        /// 连接状态：连接、断开
        /// Connect--True、Disconnect--False
        /// </summary>
        public static bool connectionStatus; //

        /// <summary>
        /// 回放文件的完整路径
        /// </summary>
        public static string replayFileName;  //

        /// <summary>
        /// 回放文件是否被选中
        /// </summary>
        public static bool replayFileSelected; //True:被选中；False：未选中
        //设备被占用，断开重连再次显示设备被占用
        //public static int flag

        //用户名和密码
        public struct userInfo
        {
            public string UserName;
            public string PassWord;
        }

        public static Dictionary<String, String> userInfoDic = new Dictionary<String, String> { };
        #endregion 

        /// <summary>
        /// 频点转换EARFCN
        /// </summary>
        /// <param name="dFreq"></param>
        /// <param name="iUDLink 0=downlink,1=uplink"></param>
        /// <param name="iLowBand lowband number"></param>
        /// <param name="iHighBand high band number"></param>
        /// <returns></returns>
        public static int FreqToEARFCN(int band, double dFreq, int iUDLink = 0, int iLowBand = 0, int iHighBand = 50)
        {
            //E-UTRA Operating Band	FDL_low (MHz)	NOffs-DL	Range of NDL	FUL_low (MHz)	NOffs-UL	Range of NUL

            int[,] FreqEARFCNTable = new int[,]{
                                                {37,1910,37550,37550,37749,1910,37550,37550,37749},
                                                {38,2570,37750,37750,38249,2570,37750,37750,38249},
                                                {39,1880,38250,38250,38649,1880,38250,38250,38649},
                                                {40,2300,38650,38650,39649,2300,38650,38650,39649},
                                                {41,2496,39650,39650,41589,2496,39650,39650,41589},
                                                {42,3400,41590,41590,43589,3400,41590,41590,43589},
                                                {43,3600,43590,43590,45589,3600,43590,43590,45589},
                                                { 1,2110,    0,    0,  599,1920,18000,18000,18599},  //FDD, band1
                                                { 3,1805, 1200, 1200, 1949,1710,19200,19200,19949}   //FDD, band3
                                                };

            int iFreq10 = System.Convert.ToInt32(dFreq * 10);
            int rowOffset = 1;

            switch (band)
            {
                case 38:
                    return (iFreq10 - FreqEARFCNTable[1, rowOffset] * 10 + FreqEARFCNTable[1, rowOffset + 1]);
                case 39:
                    return (iFreq10 - FreqEARFCNTable[2, rowOffset] * 10 + FreqEARFCNTable[2, rowOffset + 1]);
                case 40:
                    return (iFreq10 - FreqEARFCNTable[3, rowOffset] * 10 + FreqEARFCNTable[3, rowOffset + 1]);
                case 41:
                    return (iFreq10 - FreqEARFCNTable[4, rowOffset] * 10 + FreqEARFCNTable[4, rowOffset + 1]);
                case 42:
                    return (iFreq10 - FreqEARFCNTable[5, rowOffset] * 10 + FreqEARFCNTable[5, rowOffset + 1]);  //FDD, band1
                case 43:
                    return (iFreq10 - FreqEARFCNTable[6, rowOffset] * 10 + FreqEARFCNTable[6, rowOffset + 1]);  //FDD, band3
                case 1:
                    return (iFreq10 - FreqEARFCNTable[7, rowOffset] * 10 + FreqEARFCNTable[7, rowOffset + 1]);
                case 3:
                    return (iFreq10 - FreqEARFCNTable[8, rowOffset] * 10 + FreqEARFCNTable[8, rowOffset + 1]);
                default:
                    break;
            }
            /* for (int i = 0; i < FreqEARFCNTable.GetLength(0); i++)
             {
                 if (FreqEARFCNTable[i, 0] >= iLowBand && FreqEARFCNTable[i, 0] <= iHighBand)//判断频带范围
                 {
                     if (iUDLink == 1)//判断频率范围
                         rowOffset = 5;
                     lowFreq = FreqEARFCNTable[i, rowOffset]*10;
                     highFreq = FreqEARFCNTable[i, rowOffset]*10 + FreqEARFCNTable[i, rowOffset + 3] - FreqEARFCNTable[i, rowOffset + 1];
                     if (iFreq10 >= lowFreq && iFreq10 <= highFreq)
                         return (iFreq10 - FreqEARFCNTable[i, rowOffset] * 10 + FreqEARFCNTable[i, rowOffset + 1]);
                    
                
                 }
             }*/

            return -1;
            //return 39650 + ((int)dFreq - 2496) * 10;
        }
  
  
        /// <summary>
        /// EARFCN转换频点
        /// </summary>
        /// <param name="freqIndex"></param>
        /// <param name="iEARFCN"></param>
        /// <returns></returns>
        public static double EARFCNToFreq(int band, double iEARFCN)
        {
            //E-UTRA Operating Band	FDL_low (MHz)	NOffs-DL	Range of NDL	FUL_low (MHz)	NOffs-UL	Range of NUL

            int[,] FreqEARFCNTable = new int[,]{
                                                {37,1910,37550,37550,37749,1910,37550,37550,37749},
                                                {38,2570,37750,37750,38249,2570,37750,37750,38249},
                                                {39,1880,38250,38250,38649,1880,38250,38250,38649},
                                                {40,2300,38650,38650,39649,2300,38650,38650,39649},
                                                {41,2496,39650,39650,41589,2496,39650,39650,41589},
                                                {42,3400,41590,41590,43589,3400,41590,41590,43589},
                                                {43,3600,43590,43590,45589,3600,43590,43590,45589},
                                                { 1,2110,    0,    0,  599,1920,18000,18000,18599},  //FDD, band1
                                                { 3,1805, 1200, 1200, 1949,1710,19200,19200,19949}   //FDD, band3
                                                };
            int rowOffset = 1;
            switch (band)
            {
                case 38:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[1, rowOffset + 1] + FreqEARFCNTable[1, rowOffset] * 10) / 10);
                case 39:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[2, rowOffset + 1] + FreqEARFCNTable[2, rowOffset] * 10) / 10);
                case 40:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[3, rowOffset + 1] + FreqEARFCNTable[3, rowOffset] * 10) / 10);
                case 41:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[4, rowOffset + 1] + FreqEARFCNTable[4, rowOffset] * 10) / 10);
                case 42:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[5, rowOffset + 1] + FreqEARFCNTable[5, rowOffset] * 10) / 10);
                case 43:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[6, rowOffset + 1] + FreqEARFCNTable[6, rowOffset] * 10) / 10);
                case 1:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[7, rowOffset + 1] + FreqEARFCNTable[7, rowOffset] * 10) / 10);
                case 3:
                    return Convert.ToDouble((iEARFCN - FreqEARFCNTable[8, rowOffset + 1] + FreqEARFCNTable[8, rowOffset] * 10) / 10);
                default:
                    break;
            }
            return -1;
        }

        /// <summary>
        /// 更新最近打开列表
        /// </summary>
        public static void UpdateRecentFiles()
        {
            try
            {  
                CleanUpRecentFile();

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");
                XmlNode xnode = xmldoc.SelectSingleNode("ProjectTab");
                XmlNode xnode1 = xnode.SelectSingleNode("RencentFiles");

                XmlElement xelement;
                int pfile = Global.RecentFiles.Count() - 1;
                int p_count = 0;
                for (int i =  Global.RecentFiles.Count() - 1; i >=0;i-- )
                {
                    if (Global.RecentFiles[pfile] != null && Global.RecentFiles[pfile] != "")
                    {
                        xelement = xmldoc.CreateElement("RecentLogFile");
                        xelement.SetAttribute("FilePath", Global.RecentFiles[pfile]);
                        xnode1.AppendChild(xelement);
                        pfile--;
                        p_count++;
                    }
                    if (p_count > 9)
                        break;
                }
                xnode.AppendChild(xnode1);
                xmldoc.AppendChild(xnode);

                xmldoc.Save(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");//保存XMl文件0
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\nStacktrace:" +ex.StackTrace);
            }
        }

        /// <summary>
        /// 清空最近打开列表
        /// </summary>
        public static void CleanUpRecentFile()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");
            XmlNode xnode = xmldoc.SelectSingleNode("ProjectTab");
            XmlNode xnode1 = xnode.SelectSingleNode("RencentFiles");
            //清空treeview节点
            while (xnode1.ChildNodes.Count > 0)
            {
                foreach (XmlNode nd in xnode1.ChildNodes)
                {
                    xnode1.RemoveChild(nd);
                }
            }
            xmldoc.Save(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");//保存XMl文件0
        }
    }

    public static class FormPositionHelper
    {
        // 在HKEY_CURRENT_USER 设置注册表的路径
        public static string RegPath = @"Software\App\";
        public static void SaveSize(System.Windows.Forms.Form frm)
        {
            //Create or retrieve a reference to a key where the settings
            //will be stored.
            RegistryKey key;
            key = Registry.LocalMachine.CreateSubKey(RegPath + frm.Name);
            key.SetValue("Height", frm.Height);
            key.SetValue("Width", frm.Width);
            key.SetValue("Left", frm.Left);
            key.SetValue("Top", frm.Top);
        }
        public static void SetSize(System.Windows.Forms.Form frm)
        {
            RegistryKey key;
            key = Registry.LocalMachine.OpenSubKey(RegPath + frm.Name);
            if (key != null)
            {
                frm.Height = (int)key.GetValue("Height");
                frm.Width = (int)key.GetValue("Width");
                frm.Left = (int)key.GetValue("Left");
                frm.Top = (int)key.GetValue("Top");
            }
        }
    }

}
