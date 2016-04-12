using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using COM.ZCTT.AGI.Common;
using AGIInterface;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;


namespace DeviceMangerModule
{
    /// <summary>
    /// 类Device为设备类包含设备所有的功能和属性
    /// </summary>
    public class Device
    {
        #region 变量属性定义
        private string deviceName;
        private IPAddress ipAddress;
        private int messagePortNum;
        private int dataPortNum;
        private byte portType;
        private byte outputDataType;
        private byte dataStoreMode;
        private byte dataStoreType;
        private UInt16 dataStoreTime;//单位为ms
        private byte connectionState;
        private string imageKey;
        private string versionNumber;//版本号
        private string remainingPower;//剩余电量
        private byte deviceWorkModel;
        private byte deviceInstrumentState;
        private bool heartCheckStart = false;
        private int connectcount = 0;
        TcpClient tcpClient = null;
        NetworkStream messageStream = null;
        TcpClient dataRecvClient = null;
        NetworkStream dataStrem = null;
        public System.Timers.Timer timer = new System.Timers.Timer();
        static DateTime lastTime;
        private bool isDisconnect = true;
        private byte[] rData;
        static int count = 0;
        //static bool FirstTime = true;// 计算相对时间使用，看是否为第一次
        static uint TimeStampLBase = 0; //基准TimeStampL
        static DateTime TimeBase = new DateTime();  //基准时间
        static DateTime NowTime = new DateTime();//当前时间
        public int heartBeatCount = 0;

        public static bool CtrlCmdFormIsClosed;

        public bool HeartCheckStart
        {
            get { return heartCheckStart; }
            set { heartCheckStart = value; }
        }
        public byte DeviceInstrumentState
        {
            get { return deviceInstrumentState; }
            set { deviceInstrumentState = value; }
        }
        public byte DeviceWorkModel
        {
            get { return deviceWorkModel; }
            set { deviceWorkModel = value; }
        }
        public string RemainingPower
        {
            get { return remainingPower; }
            set { remainingPower = value; }
        }
        public string VersionNumber
        {
            get { return versionNumber; }
            set { versionNumber = value; }
        }
        public string ImageKey
        {
            get { return imageKey; }
            set { imageKey = value; }
        }
        public byte ConnectionState
        {
            get { return connectionState; }
            set
            {
                if ((value == 0) || (value == 1))
                    connectionState = value;
            }
        }
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }
        public string IpAddress
        {
            get { return ipAddress.ToString(); }
            set
            {
                IPAddress ip = null;
                if (IPAddress.TryParse((string)value, out ip))
                    ipAddress = ip;
                else
                    MessageBox.Show("IP is not avalid!");
            }
        }
        public int MessagePortNum
        {
            get { return messagePortNum; }
            set { messagePortNum = value; }
        }
        public int DataPortNum
        {
            get { return dataPortNum; }
            set { dataPortNum = value; }
        }
        public byte PortType
        {
            get { return portType; }
            set { portType = value; }
        }
        public byte OutputDataType
        {
            get { return outputDataType; }
            set { outputDataType = value; }
        }
        public byte DataStoreMode
        {
            get { return dataStoreMode; }
            set { dataStoreMode = value; }
        }
        public byte DataStoreType
        {
            get { return dataStoreType; }
            set { dataStoreType = value; }
        }
        public UInt16 DataStoreTime
        {
            get { return dataStoreTime; }
            set { dataStoreTime = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数，用于初始化设备连接状态和定时器
        /// </summary>
        public Device()
        {
            connectionState = (byte)Global.DeviceStateValue.Disconnection;
            imageKey = "disconnect.ico";
            timer.Interval = 6000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            timer.Enabled = false;
        }
        //int flag = 0;
        #region 建立连接部分
        /// <summary>
        /// 新建设备连接
        /// </summary>
        public void NewMessageConn()
        {
            //timer.Enabled = true;
            try
            {
                heartBeatCount = 0;
                Global.tempClass.HeartBeatCount(heartBeatCount);
                tcpClient = TcpClientConnectorHelp.Connect(ipAddress.ToString(), messagePortNum, 2000);
                tcpClient.ReceiveBufferSize = 30000000;//edit by zhouyt 20140724
                DataRecv();//////////////////////
                //debug
                System.Diagnostics.Debug.WriteLine("Tcp connected>>>>>>>>>>>>>>>>");

                ////如果连接成功，改变设备底色为绿色
                if (AGIInterface.Class1.OnConnOk != null)
                    AGIInterface.Class1.OnConnOk(this.deviceName);
                //SendStatusToMainForm();
                isDisconnect = false;
                lastTime = DateTime.Now;
                messageStream = tcpClient.GetStream();
                //请求获取设备工作状态
                //try
                //{
                //    byte[] getAGTInfo = new byte[] { 0, 0, 0, 0, 0, 0, 0x01, 0x40, 0, 0, 0,0};
                //    sendMessage(getAGTInfo);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Failed to config the remote device:" + ex.Message);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(">>>Message= " + ex.Message + "\r\n StrackTrace: " + ex.StackTrace);
                System.Diagnostics.Debug.WriteLine("Tcp connect failed");
                if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                    AGIInterface.Class1.OnChangeDeviceInfoList("exception");
            }
        }
        #endregion
        #region 发送消息部分以及ACK处理
        /// <summary>
        /// 设备异步发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void sendMessage(object message)
        {
            byte[] sendData = (byte[])message;
            //string SendMsg = sendData.ToString();
            //AsyncCallback writeCallback = new AsyncCallback(WriteCompled);
            try
            {
                if (messageStream != null)
                    //messageStream.BeginWrite(sendData, 0, sendData.Length, writeCallback, sendData);
                    messageStream.Write(sendData, 0, sendData.Length);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(">>>error Message :" + ex.Message + "\r\n" + ex.StackTrace);
            }

        }
        /// <summary>
        /// 异步发送消息回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void WriteCompled(IAsyncResult ar)
        {
            try
            {
                messageStream.EndWrite(ar);
                byte[] sendMessage = (byte[])ar.AsyncState;
                System.Diagnostics.Debug.WriteLine("Send a message success! Send To " + DeviceName);
                //保存二进制数据
                //if (AGIInterface.Class1.OnSaveData != null)
                //    AGIInterface.Class1.OnSaveData(sendMessage);
                byte[] recvByte = new byte[256];
                int byteCount = messageStream.Read(recvByte, 0, recvByte.Length);
                AckDataHander(recvByte, byteCount);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("receive ACK failed! " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// ACK处理
        /// </summary>
        /// <param name="buffer">接收ACK的buffer</param>
        /// <param name="receiveDateLength">接收的数据长度</param>
        /// <returns></returns>
        private int AckDataHander(byte[] buffer, int receiveDateLength)
        {
            if (receiveDateLength >= 12)
            {
                UInt16 msgLength = System.BitConverter.ToUInt16(buffer, 10);
                int bodyCount = msgLength * 4;
                if (bodyCount == 4)//标准ACK响应
                {
                    if (buffer[12] == 1)//ACK失败
                    {
                        MessageBox.Show("Message ACK failed!");
                        return 1;
                    }
                }
                //需要特殊处理的ACK响应
                UInt16 msgType = System.BitConverter.ToUInt16(buffer, 6);
                System.Diagnostics.Debug.WriteLine(" receive a ACK ,msg type :  " + Convert.ToString(msgType, 16));
                ///////////////////////////////////////
                if (Convert.ToString(msgType, 16) == "ffff" && receiveDateLength == 12 && buffer[0] == 0x48 && buffer[1] == 0xc && buffer[2] == 0xf4 && buffer[3] == 0x7e && buffer[4] == 0x2 && buffer[5] == 0x4 && buffer[6] == 0xff && buffer[7] == 0xff && buffer[8] == 0 && buffer[9] == 0 && buffer[10] == 0 && buffer[11] == 0)
                {
                    //######查看收到的0000消息########
                    //AGIInterface.CustomDataEvtArg ce = new CustomDataEvtArg();
                    //ce.data = buffer;
                    //Global.tempClass.SendDataToDebug(this, ce);
                    //#####################
                    MessageBox.Show("The Instrument has been connected by another AGI!");
                    if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                        AGIInterface.Class1.OnChangeDeviceInfoList("exception");
                }
                else
                {
                    if (connectcount == 0)
                    {
                        if (ConnectionState == (byte)Global.DeviceStateValue.Disconnection)
                        {
                            //如果连接成功，改变设备底色为绿色
                            if (AGIInterface.Class1.OnConnOk != null)
                                AGIInterface.Class1.OnConnOk(this.deviceName);
                            //SendStatusToMainForm();
                        }
                    }
                }
                //////////////////////////////////////////

                switch (msgType)
                {
                    case COM.ZCTT.AGI.Common.AGIMsgDefine.AG_PC_PROTOCOL_TRACE_REL_ACK_MSG_TYPE:
                    case COM.ZCTT.AGI.Common.AGIMsgDefine.L1_AG_PROTOCOL_TRACE_REL_ACK_MSG_TYPE:
                        {
                            AGIInterface.CustomDataEvtArg ce = new CustomDataEvtArg();
                            ce.deivceName = Global.GCurrentDevice;
                            Global.tempClass.SendACKToCellSearch(this, ce);
                            break;
                        }
                    //获取设备状态消息的ACK消息
                    case COM.ZCTT.AGI.Common.AGIMsgDefine.AG_PC_GET_AGT_INFO_REQ_ACK_MSG_TYPE:
                        {
                            this.deviceInstrumentState = buffer[12];
                            this.deviceWorkModel = buffer[13];
                            this.PortType = buffer[14];
                            this.OutputDataType = buffer[15];
                            this.DataStoreMode = buffer[16];

                            if (this.deviceInstrumentState == 1)
                            {
                                if (this.deviceWorkModel == 1)
                                {
                                    Global.testMode = Global.TestMode.RealTime;
                                    Global.ReadyModule = "ProtocolTrace";
                                }
                                else if (this.deviceWorkModel == 2)
                                {
                                    //Global.testMode = Global.TestMode.CellScan;
                                    //Global.CRModule = "CellScan";
                                }
                                else if (this.deviceWorkModel == 3)
                                {
                                    //Global.testMode = Global.TestMode.CellScan;
                                    //Global.CRModule = "UnSpCellScan";
                                }
                            }
                            else
                            {

                            }

                            //按照新的接口协议，下面的处理是错误的，如果需要获取值，请参看新接口协议
                            //16 saveTime = System.BitConverter.ToUInt16(buffer,20); 
                            //this.dataStoreTime = saveTime;
                            //this.remainingPower =Convert.ToInt16((byte)buffer[19]).ToString();UInt

                            if (this.deviceInstrumentState == 1)
                                if (Global.isReboot == true)
                                {
                                    byte[] stop = new byte[] { };
                                    if (this.deviceWorkModel == 1)
                                    {
                                        stop = new byte[] { 0, 0, 0, 0, 4, 2, 0x0c, 0x40, 1, 0, 0, 0 };//加消息头。。
                                    }
                                    else if (this.deviceWorkModel == 2)
                                    {
                                        stop = new byte[] { 0, 0, 0, 0,//reserved
                                                   1,         //srdID
                                                   0,         //destID
                                                   0x03,      //msgType
                                                   0x40,      
                                                   0,         //transactionID
                                                   0,      
                                                   0,         //msgLen
                                                   0};
                                    }
                                    else if (this.deviceWorkModel == 3)
                                    {
                                        stop = new byte[] {0, 0, 0, 0,//reserved
                                                   1,         //srdID
                                                   0,         //destID
                                                   0x05,      //msgType
                                                   0x40,      
                                                   0,         //transactionID
                                                   0,      
                                                   0,         //msgLen
                                                   0};
                                    }

                                    sendMessage(stop);
                                    // CloseDeviceConn();
                                }
                            //发送设备状态信息到主程序
                            SendStatusToMainForm();
                            break;
                        }
                    default: break;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ACK is not the right format!");
            }
            return 0;
        }
        #endregion

        #region 数据接收部分
        /// <summary>
        /// 设备数据端口数据接收
        /// </summary>
        Thread dataRecv;
        /// <summary>
        /// 设备数据端口数据接收函数
        /// </summary>
        public void DataRecv()
        {
            dataRecv = new Thread(new ThreadStart(DataRecvThread));
            dataRecv.IsBackground = true;
            //Control.CheckForIllegalCrossThreadCalls = false;
            dataRecv.Start();
        }

        /// <summary>
        /// 数据接收线程
        /// </summary>
        private void DataRecvThread()
        {
            dataRecvClient = new TcpClient();
            try
            {
                dataRecvClient.Connect(ipAddress, dataPortNum);
                dataRecvClient.ReceiveBufferSize = 30000000;//edit by zhouyt 20140723
                dataStrem = dataRecvClient.GetStream();

            }
            catch (Exception ex)
            {
                Console.WriteLine(">>>Message= " + ex.Message + "\r\n StrackTrace: " + ex.StackTrace);
                CloseDeviceConn();
                //////////////////////////////////
                SendStatusToMainForm();
                if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                    AGIInterface.Class1.OnChangeDeviceInfoList("exception");
                ////////////////////////////////////
            }
            while (true)
            {

                try
                {
                    byte[] dataBuffer = new byte[10240];
                    int headerLen = dataStrem.Read(dataBuffer, 0, 8);
                    //先收头，再收数据
                    lastTime = DateTime.Now;//收到任何数据都表示没有断开连接
                    while (headerLen < 8)
                    {
                        headerLen += dataStrem.Read(dataBuffer, headerLen, 8 - headerLen);
                    }
                    if (System.BitConverter.ToUInt16(dataBuffer, 0) != 0xffff)
                        continue;
                    //用于区分消息所属设备，12.10
                    string str2 = "";
                    //byte[] byteArray = new byte[4];
                    for (int i = 0; i < DeviceName.Length; i++)
                    {
                        if ((DeviceName[i] >= '0') && (DeviceName[i] <= '9'))
                            str2 += DeviceName[i];
                    }
                    //byteArray = System.Text.Encoding.Default.GetBytes(str2);
                    //Array.Copy(byteArray, dataBuffer, 4);
                    ////for test
                    string str = "设备" + str2;
                    // string str = "AGT"+System.Text.Encoding.ASCII.GetString(byteArray);
                    Console.WriteLine("消息所属设备：" + str);

                    UInt32 msgLength = System.BitConverter.ToUInt32(dataBuffer, 4);
                    UInt16 msgType = System.BitConverter.ToUInt16(dataBuffer, 2);
                    int bodyCount = (int)msgLength;
                    int bodyReadLen = dataStrem.Read(dataBuffer, 8, bodyCount);
                    lastTime = DateTime.Now;//收到任何数据都表示没有断开连接
                    while (bodyReadLen < bodyCount)//如果一次没有接收完成再继续接收
                    {
                        bodyReadLen += dataStrem.Read(dataBuffer, 8 + bodyReadLen, bodyCount - bodyReadLen);
                    }
                    lastTime = DateTime.Now;//收到任何数据都表示没有断开连接
                    int receivedDataLength = bodyReadLen + 8;
                    byte[] receivedData = new byte[receivedDataLength];
                    Array.Copy(dataBuffer, receivedData, receivedDataLength);
                    //Debug Output
                    System.Diagnostics.Debug.WriteLine("receive a data , msgType : " + Convert.ToString(msgType, 16) + "  dataLength : " + receivedDataLength);

                        switch (msgType)
                        {
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_HEART_BEAT://1
                                {
                                    heartBeatCount++;
                                    Global.tempClass.HeartBeatCount(heartBeatCount);
                                    connectcount = 0;
                                    break;
                                }
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_UE_INFO://2
                                {
                                    CustomDataEvtArg ce = new CustomDataEvtArg();
                                    ce.data = receivedData;
                                    Global.tempClass.SendUeInfoToMainForm(this, ce);
                                    break;
                                }
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_REQ_CNF://3
                                {
                                    ushort cnfType = System.BitConverter.ToUInt16(receivedData,10);
                                    ushort cnfInd = System.BitConverter.ToUInt16(receivedData,12);
                                    switch(cnfType)
                                    {
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_SYS_PARA:			//系统参数，MCC、MNC等
                                            {
                                                if(cnfInd == 0)
                                                    MessageBox.Show("系统参数发送成功");
                                                else
                                                    MessageBox.Show("系统参数发送失败");
                                                break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_SYS_OPTION:				//系统选项，如是否上报IMEI等
                                            {
                                                if (cnfInd == 0)
                                                    MessageBox.Show("系统选项发送成功");
                                                else
                                                    MessageBox.Show("系统选项发送失败");
                                                break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_DLRX_PARA:					//下行接收参数，用于获取公网信息
                                            {
                                                if (cnfInd == 0)
                                                    MessageBox.Show("下行测试参数发送成功");
                                                else
                                                    MessageBox.Show("下行测试参数发送失败");
                                                break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_NC_PARA:						//邻区表参数
                                            {
                                                if (cnfInd == 0)
                                                    Global.tempClass.NcSniffStatus(0);
                                                else if (cnfInd == 1)
                                                    Global.tempClass.NcSniffStatus(1);
                                                break;
                                            }

                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_IB_OPER:					//IMSI库处理
                                            {
                                                if (cnfInd == 0)
                                                    Global.tempClass.IBOPERStatus(0);
                                                else if (cnfInd == 1)
                                                {
                                                    Global.tempClass.IBOPERStatus(1);
                                                    MessageBox.Show("IMSI操作失败，请重新操作！");
                                                }
                                                else if (cnfInd == 2)
                                                {
                                                    Global.tempClass.IBOPERStatus(1);
                                                    MessageBox.Show("IMSI操作失败，请重新操作！");
                                                }
                                                    break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_CTRL_CMD:					//寻呼控制命令
                                            {
                                                if (cnfInd == 0)
                                                    MessageBox.Show("寻呼命令发送成功");
                                                else
                                                    MessageBox.Show("寻呼命令发送失败");
                                                break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_RF_PARA:					//射频参数，如频点、功率等
                                            {
                                                if (cnfInd == 0)
                                                    MessageBox.Show("射频参数发送成功");
                                                else
                                                    MessageBox.Show("射频参数发送失败");
                                                break;
                                            }
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_QUERY_VER:					//查询版本
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_RX_PARA:				//接收参数，如RACH接收功率门限等
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_NEW_TAC:				//重新获取新的MCC、MNC、TAC和Cell ID
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_NOW_PARA:					//获取当前设备内的参数
	
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_HEART_BEAT_CNF:			//心跳包确认
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_DEV_STATE:					//返回设备状态
                                        case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_REDIRECT_R9:
                                        //case (ushort)COM.ZCTT.AGI.Common.MsgDefine.RecvPktType.RECV_REBOOT_ENB:
                                            break;
                                    }
                                    break;
                                }
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_NOW_PARA://4
                                break;
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_PAGING_PWR://5
                                {
                                    if (CtrlCmdFormIsClosed)
                                    {
                                        CustomDataEvtArg ce = new CustomDataEvtArg();
                                        ce.data = receivedData;
                                        Global.tempClass.SendPagingPwrToCtrlForm(this, ce);
                                    }
                                    
                                    break;
                                }
                            case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_DEV_VERSION://7
                                {
                                    MsgDefine.SendDevVersion DevVersion = new MsgDefine.SendDevVersion();
                                    int lengthDevVersion = Marshal.SizeOf(DevVersion);
                                    if (lengthDevVersion > receivedData.Length)
                                        return;
                                    IntPtr intptr = Marshal.AllocHGlobal(lengthDevVersion);
                                    Marshal.Copy(receivedData, 0, intptr, lengthDevVersion);
                                    DevVersion = (MsgDefine.SendDevVersion)Marshal.PtrToStructure(intptr, typeof(MsgDefine.SendDevVersion));
                                    Marshal.FreeHGlobal(intptr);

                                    String fpgaVersion = "";
                                    for (int i = 0; i < 4;i++ )
                                        fpgaVersion += DevVersion.fpgaVersion[i];
                                    String bbuVersion = "";
                                    for (int i = 0; i < 4;i++ )
                                        bbuVersion += DevVersion.BBUVersion[i].ToString();
                                    String SoftWareVersion = "";
                                    for (int i = 0; i < DevVersion.SoftWareVersion.Length; i++)
                                    {
                                        if(DevVersion.SoftWareVersion[i] != '\0')
                                        SoftWareVersion += DevVersion.SoftWareVersion[i].ToString();
                                    }
                                    MessageBox.Show("FPGA版本：" + fpgaVersion + "\n" + "BBU版本 ：" + bbuVersion + "\n" + "软件版本 ：" + SoftWareVersion + "\n");
                                    break;
                                }
                            //case (ushort)COM.ZCTT.AGI.Common.MsgDefine.SendPktType.SEND_DL_SNIFF:

                            default: break;
                        }
                    
                    //if (msgType != 0x8002)//心跳信息
                    //{
                    if (AGIInterface.Class1.OnSaveData != null)
                        AGIInterface.Class1.OnSaveData(receivedData);
                    //}
                

                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>Message= " + ex.Message + "\r\n StrackTrace: " + ex.StackTrace);
                    break;
                }
            }
        }
        #endregion

        /// <summary>
        /// 检测心跳
        /// </summary>
        /// <param name="lastTime"></param>
        private void checkHeart(DateTime lastTime)
        {
            DateTime nowTime = DateTime.Now;
            TimeSpan ts = nowTime - lastTime;
            if (ts.TotalSeconds > 9)
            {
                CloseDeviceConn();
                //////////////////////////
                ConnectionState = (byte)Global.DeviceStateValue.Disconnection;
                //Global.connectionStatus = false;
                SendStatusToMainForm();
                if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                    AGIInterface.Class1.OnChangeDeviceInfoList("exception");
                /////////////////////////////
                isDisconnect = true;
                SendStatusToMainForm();
                NewMessageConn();
                lastTime = DateTime.Now;
                connectcount++;
                if (connectcount > 10)
                {
                    timer.Enabled = false;
                    connectcount = 0;
                    CloseDeviceConn();
                    SendStatusToMainForm();
                    HeartCheckStart = false;
                    if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                        AGIInterface.Class1.OnChangeDeviceInfoList("exception");
                }

            }
        }
        private void SendUERleaseREQ()
        {
            AGIInterface.CustomDataEvtArg ce1 = new CustomDataEvtArg();
            PC_AG_UE_SILENCE_RPT_RSP UeSilenceREQ = new PC_AG_UE_SILENCE_RPT_RSP();
            int reqlength = Marshal.SizeOf(UeSilenceREQ);
            byte[] header = new byte[] { 0, 0, 0, 0, 4, 2, 0x0b, 0x40, 0, 0, 0x0f, 0 };//加消息头
            byte[] tempBytes;
            int dstOffset = 0;
            UeSilenceREQ.mu8ReTrace = 1;
            if (MessageBox.Show("Do you want to release UE?", "NOTE",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                UeSilenceREQ.mu8ReTrace = 0;
            }
            else
            {
                UeSilenceREQ.mu8ReTrace = 1;
            }
            UeSilenceREQ.mau8Pading = new byte[] { 0, 0, 0 };

            ce1.data = new byte[reqlength];
            System.Buffer.BlockCopy(header, 0, ce1.data, dstOffset, 12);
            dstOffset = dstOffset + 12;
            System.Buffer.BlockCopy(rData, 20, ce1.data, dstOffset, 56);
            dstOffset = dstOffset + 56;
            tempBytes = BitConverter.GetBytes(UeSilenceREQ.mu8ReTrace);
            System.Buffer.BlockCopy(tempBytes, 0, ce1.data, dstOffset, 1);
            dstOffset++;
            System.Buffer.BlockCopy(UeSilenceREQ.mau8Pading, 0, ce1.data, dstOffset, 3);
            ce1.deivceName = Global.GCurrentDevice;
            Global.tempClass.SendDataToDevice(null, ce1);
        }
        /// <summary>
        /// 检测心跳，大于6秒断开重连
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            checkHeart(lastTime);
                //if (!isDisconnect)
                //{
                //    byte[] getAGTInfo = new byte[] { 0, 0, 0, 0, 0, 0, 0x01, 0x40, 0, 0, 0, 0 };
                //    sendMessage(getAGTInfo);
                //}
        }

        /// <summary>
        /// 发送设备的状态信息给主窗体
        /// </summary>
        public void SendStatusToMainForm()
        {
            if (Global.GCurrentDevice == deviceName)
            {
                AGIInterface.CustomDataEvtArg ce = new CustomDataEvtArg();
                ce.deviceConnStaus = (this.connectionState == 0) ? "Connected" : "Disconnected";
                Global.connectionStatus = (this.connectionState == 0) ? true : false;
                ce.deivceName = this.deviceName;
                if (ce.deviceConnStaus == "Disconnected")
                {
                    ce.deviceInstumentState = " ";
                    ce.deviceWorkModel = " ";
                }
                else
                {
                    ce.deviceInstumentState = (this.deviceInstrumentState == 0) ? "IDLE" : "Working";
                    //2014.4.15 stop按钮和状态异常
                    if (Global.testMode != Global.TestMode.PlayBack && ce.deviceInstumentState == "IDLE")
                    {
                        Global.testStatus = Global.TestStatus.Stop;
                    }
                    if (Global.testMode != Global.TestMode.PlayBack && ce.deviceInstumentState == "Working")
                    {
                        Global.testStatus = Global.TestStatus.Start;
                    }
                    ce.deviceWorkModel = Global.testMode.ToString(); //(this.deviceWorkModel == 1) ? "Cell Scanning" : "Protocol Tracking";
                }
                ce.remainingPower = this.remainingPower;
                Global.tempClass.SendStatusToMainForm(this, ce);
            }
        }
        /// <summary>
        /// 关闭设备连接，释放资源
        /// </summary>
        public void CloseDeviceConn()
        {
            if (messageStream != null)
                messageStream.Dispose();
            if (tcpClient != null)
                tcpClient.Close();
            if (dataStrem != null)
                dataStrem.Dispose();
            if (dataRecvClient != null)
                dataRecvClient.Close();
            tcpClient = null;
            messageStream = null;
            dataRecvClient = null;
            dataStrem = null;
            if (dataRecv != null)
                dataRecv.Abort();
            this.ConnectionState = (byte)Global.DeviceStateValue.Disconnection;

            if (AGIInterface.Class1.OnConnWrong != null)
                AGIInterface.Class1.OnConnWrong(this.deviceName);
            //debug
            System.Diagnostics.Debug.WriteLine("Disconnected<<<<<<<<<<<<<<<<");
        }
    }
}
