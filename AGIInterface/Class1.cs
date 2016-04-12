using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using AGIInterface;
using System.Runtime.InteropServices;

namespace AGIInterface
{
    #region 自定义事件参数定义
    /// <summary>
    /// 自定义事件参数定义
    /// </summary>
    public class CustomDataEvtArg : EventArgs
    {
        //状态信息
        /// <summary>
        /// 连接状态
        /// </summary>
        public string deviceConnStaus = "";
        /// <summary>
        /// 设备状态
        /// </summary>
        public string deviceInstumentState = "";
        /// <summary>
        /// 工作模式
        /// </summary>
        public string deviceWorkModel = "";
        /// <summary>
        /// 电池状态
        /// </summary>
        public string remainingPower = "";

        /// <summary>
        /// 接收数据
        /// </summary>
        public byte[] data;
        /// <summary>
        /// 设备名称
        /// </summary>
        public string deivceName = null;
        /// <summary>
        /// 用来区别接收“开始”和“结束”事件的模块：ProtocolTrace、CellSacn
        /// </summary>
        public string moduleName = null;
        /// <summary>
        /// 用来区别准备好的模块：ProtocolTrace、CellScan
        /// </summary>
        public string readyModule = null;
        /// <summary>
        /// 用来发送“小区扫描”和“协议跟踪”配置文件的文件名
        /// </summary>
        public string configFile = null;
        /// <summary>
        ///协议跟踪过滤发送事件的参数
        /// </summary>
        public List<byte> filter = new List<byte> { };
        /// <summary>
        /// MessageType和LayerType都包含在里面
        /// </summary>
        public List<string> listFilter = new List<string> { };
        /// <summary>
        /// 发生改变的filter
        /// </summary>
        public string changedFilter = null;
        /// <summary>
        /// 发生改变的filter是否选中
        /// </summary>
        public bool changedFilterVisible;

        /// <summary>
        /// 区分开始的是实时模式还是回放模式。
        /// true：实时
        /// false：回放
        /// </summary>
        public bool isRealTime = true;
        
    }
    #endregion
    /// <summary>
    /// 主程序模块
    /// </summary>
    public class Class1
    {
        #region 关闭程序时


        #endregion 

        #region 设备模块事件定义
        /// <summary>
        /// 更改设备信息列表
        /// </summary>
        public delegate void ChangeDeviceInfoList(string name);
        /// <summary>
        /// 更改设备信息列表事件
        /// </summary>
        public static ChangeDeviceInfoList OnChangeDeviceInfoList;

        /// <summary>
        /// 保存数据到二进制logFile中
        /// </summary>
        public delegate void SaveDataHandler(byte[] data);
        /// <summary>
        /// 保存数据到二进制logFile中事件
        /// </summary>
        public static SaveDataHandler OnSaveData;

        /// <summary>
        ///设备连接成功后调用此委托通知DeviceManger连接成功
        /// </summary>
        public delegate void ConnOKHandler(string deviceName);
        /// <summary>
        ///点击连接OK按钮事件
        /// </summary>
        public static ConnOKHandler OnConnOk;

        /// <summary>
        ///设备连接成功后调用此委托通知DeviceManger连接成功
        /// </summary>
        public delegate void ConnDiscHandler(string deviceName);
        /// <summary>
        ///点击断开连接按钮事件
        /// </summary>
        public static ConnDiscHandler OnConnWrong;

        /// <summary>
        ///添加设备后改变设备显示列表
        /// </summary>
        public delegate void ChangeDeviceListViewHandler(string deviceName);
        /// <summary>
        ///添加设备后改变设备显示列表事件
        /// </summary>
        public static ChangeDeviceListViewHandler OnChangDeviceListView;

        /// <summary>
        ///把通信模块接收到的数据发送给小区扫描模块委托
        /// </summary>
        public delegate void DeviceSendDataToCellScanHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        ///把通信模块接收到的数据发送给小区扫描模块事件
        /// </summary>
        public event DeviceSendDataToCellScanHandler SendDataToCellScanEvent;

        /// <summary>
        ///把通信模块接收到的数据发送给协议跟踪模块委托
        /// </summary>
        public delegate void DeviceSendDataToProTrackHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event DeviceSendDataToProTrackHandler SendDataToProTrackEvent;

        

        /// <summary>
        /// 把ProtocolTracking中接收到的UE上下行数据发送给RSRPGraph模块
        /// </summary>
        public delegate void ProTrackSendDataToRSRPGraphHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event ProTrackSendDataToRSRPGraphHandler SendDataToRSRPGraphEvent;

        /// <summary>
        /// 把ProtocolTracking中接收到的STMSI数据发送给STMSIGraph模块
        /// </summary>
        public delegate void ProTrackSendDataToSTMSIGraphHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event ProTrackSendDataToSTMSIGraphHandler SendDataToSTMSIGraphEvent;

        /// <summary>
        ///把通信模块接收到的数据发送给调试模块委托
        /// </summary>
        public delegate void DeviceSendDataToDebugHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event DeviceSendDataToDebugHandler SendDataToDebugEvent;

        /// <summary>
        ///其他模块向设备模块发送消息数据使用事件委托
        /// </summary>
        public delegate void SendDataToDeviceHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event SendDataToDeviceHandler SendDataToDeviceEvent;

        /// <summary>
        ///Cell_SysInfo拆分委托
        /// </summary>
        public delegate void SendDataToCellSysSplitHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event SendDataToCellSysSplitHandler SendDataToCellSysSplitEvent;

        /// <summary>
        ///把设备的工作状态等信息发送给主程序委托
        /// </summary>
        public delegate void DeviceSendStatusToMainForm(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event DeviceSendStatusToMainForm SendStatusToMainFormEvent;

        /// <summary>
        ///把设备的ACK信息发送给搜索窗口
        /// </summary>
        public delegate void DeviceSendACKToCellSearch(object sender, CustomDataEvtArg e);
        /// <summary>
        ///事件
        /// </summary>
        public event DeviceSendACKToCellSearch SendACKToCellSearchEvent;

        /// <summary>
        ///关闭解码窗口事件委托
        /// </summary>
        public delegate void CloseDecodeHandler();
        /// <summary>
        ///事件
        /// </summary>
        public event CloseDecodeHandler CloseDecodeEvent;

        /// <summary>
        ///协议跟踪stop刷新treeview
        /// </summary>
        public delegate void RefreshTreeviewHandler();
        /// <summary>
        ///事件
        /// </summary>
        public event RefreshTreeviewHandler RefreshTreeviewEvent;

        /// <summary>
        ///关闭协议跟踪窗口
        /// </summary>
        public delegate void CloseDisplayFormHander();
        /// <summary>
        ///事件
        /// </summary>
        public event CloseDisplayFormHander CloseDisplayFormEvent;

        /// <summary>
        /// 关闭Display窗口
        /// </summary>
        /// 

        public void CloseDisplayForm()
        {
            if (CloseDisplayFormEvent != null)
            {
                CloseDisplayFormEvent();
            }
        }

        public delegate void CSAMStartHander();
        public event CSAMStartHander CSAMStartEvent;
        public void CSAMStart()
        {
            if (CSAMStartEvent != null)
            {
                CSAMStartEvent();
            }
        }

        public delegate void CSAMStopHander();
        public event CSAMStopHander CSAMStopEvent;
        public void CSAMStop()
        {
            if (CSAMStopEvent != null)
                CSAMStopEvent();
        }

        public delegate void FTSTMSIStartHander();
        public event FTSTMSIStartHander FTSTMSIStartEvent;
        public void FTSTMISIStart()
        {
            if (FTSTMSIStartEvent != null)
            {
                FTSTMSIStartEvent();
            }
        }

        public delegate void FTSTMSIStopHander();
        public event FTSTMSIStopHander FTSTMSIStopEvent;
        public void FTSTMSIStop()
        {
            if (FTSTMSIStopEvent != null)
                FTSTMSIStopEvent();
        }

        public delegate void OrtFStartHander();
        public event OrtFStartHander OrtFStartEvent;
        public void OrtFStart()
        {
            if (OrtFStartEvent != null)
                OrtFStartEvent();
        }

        public delegate void OrtFStopHander();
        public event OrtFStopHander OrtFStopEvent;
        public void OrtFStop()
        {
            if (OrtFStopEvent != null)
                OrtFStopEvent();
        }

        /// <summary>
        /// 刷新日志列表TREE
        /// </summary>
        public void RefreshTreeview()
        {
            if (RefreshTreeviewEvent != null)
                RefreshTreeviewEvent();
        }

        /// <summary>
        /// 关闭解码窗口
        /// </summary>
        public void CloseDecode()
        {
            if (CloseDecodeEvent != null)
                CloseDecodeEvent();
        }

        /// <summary>
        /// 发送数据到主窗口
        /// </summary>
        public void SendStatusToMainForm(object sender, CustomDataEvtArg e)
        {
            if (SendStatusToMainFormEvent != null)
                SendStatusToMainFormEvent(sender, e);
        }

        /// <summary>
        /// 发送ACK到搜索窗口
        /// </summary>
        public void SendACKToCellSearch(object sender, CustomDataEvtArg e)
        {
            if (SendACKToCellSearchEvent != null)
                SendACKToCellSearchEvent(sender, e);
        }

        /// <summary>
        /// 发送数据到小区扫描模块
        /// </summary>
        public int SendDataToCellScan(object sender, CustomDataEvtArg e)
        {
            if (SendDataToCellScanEvent != null)
            {
                SendDataToCellScanEvent(sender, e);
                return 0;
            }
            else
                return -1;
        }


        /// <summary>
        /// 发送数据到协议跟踪模块
        /// </summary>
        public void SendDataToProTrack(object sender, CustomDataEvtArg e)
        {
            if(SendDataToProTrackEvent!=null)
            SendDataToProTrackEvent(sender, e);
        }

       
        
        /// <summary>
        /// 发送数据到RSRPGraph模块
        /// </summary>
        public void SendDataToRSRPGraph(object sender, CustomDataEvtArg e)
        {
            if (SendDataToRSRPGraphEvent != null)
                SendDataToRSRPGraphEvent(sender, e);
        }

        /// <summary>
        /// 发送数据到STMSIGraph模块
        /// </summary>
        public void SendDataToSTMSIGraph(object sender,CustomDataEvtArg e)
        {
            if (SendDataToSTMSIGraphEvent != null)
                SendDataToSTMSIGraphEvent(sender, e);
        }

        /// <summary>
        /// 发送数据到Debug模块
        /// </summary>
        public void SendDataToDebug(object sender, CustomDataEvtArg e)
        {
            if (SendDataToDebugEvent != null)
                SendDataToDebugEvent(sender, e);
        }

        /// <summary>
        /// 发送数据到设备模块
        /// </summary>
        public int SendDataToDevice(object sender, CustomDataEvtArg e)
        {
            if (SendDataToDeviceEvent != null)
            {
                SendDataToDeviceEvent(sender, e);
                return 0;
            }
            else
                return -1;
        }
        /// <summary>
        /// 发送数据到小区消息分离模块
        /// </summary>
        public void SendDataToCellSysSplit(object sender, CustomDataEvtArg e)
        {
            if (SendDataToCellSysSplitEvent != null)
            {
                SendDataToCellSysSplitEvent(sender, e);
            }
        }
        #endregion

        #region 准备、开始、停止测试
        /// <summary>
        /// 准备、开始、停止测试
        /// </summary>
        public delegate void ReadyHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event ReadyHandler ReadyEvent;
        /// <summary>
        /// 准备测试事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public void Ready(object sender, CustomDataEvtArg e)
        {
            if (ReadyEvent != null)
            {
                ReadyEvent(this, e);
            }
        }
        /// <summary>
        /// 启动事件委托
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public delegate void StartHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 启动事件
        /// </summary>
        public event StartHandler StartEvent;
        /// <summary>
        /// 清屏
        /// </summary>
        public StartHandler ClearForm = null;
        /// <summary>
        /// 开始测试事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Start(object sender, CustomDataEvtArg e)
        {
            if (StartEvent != null)
            {
                StartEvent(this, e);
            }
        }

        #region 测向开始
        public delegate void OrientationFidingStartHandler(object sender);
        public event OrientationFidingStartHandler OrientationFidingStartEvent;
        public void OrientationFidingStart(object sender)
        {
            if (OrientationFidingStartEvent != null)
                OrientationFidingStartEvent(sender);
        }
        #endregion
        /// <summary>
        /// 停止测试事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void StopHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 停止事件
        /// </summary>
        public event StopHandler StopEvent;
        /// <summary>
        /// 停止测试事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool Stop(object sender, CustomDataEvtArg e)
        {
            if (StopEvent != null)
            {
                StopEvent(this, e);
                return true;
            }
            else
            {
                return false;
            }
        } 

        #endregion 

        #region 将小区扫描和协议跟踪数据发送到详细解析模 
        /// <summary>
        /// 发送事件到MIB解析界面委托
        /// </summary>
        public delegate void SendDataToDecodeHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event SendDataToDecodeHandler SendDataToDecodeEvent;
        /// <summary>
        /// 发送事件到MIB解析界面
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public void SendDataToDecode(object sender, CustomDataEvtArg e)
        {
            if (SendDataToDecodeEvent != null)
            {
                SendDataToDecodeEvent(sender, e);
            }
        }

        #endregion 

        #region Filter改变的事件
        /// <summary>
        ///发送事件到MIB解析界面
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public delegate void FilterChangedHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event FilterChangedHandler FilterChangedEvent;
        /// <summary>
        /// 点击Filter按钮，改变Filter事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public void FilterChanged(object sender, CustomDataEvtArg e)
        {
            if (FilterChangedEvent != null)
            {
                FilterChangedEvent(sender, e);
            }
        }
        #endregion 

        #region 调试
        /// <summary>
        ///配置“调试”完成的事件。调试配置界面中点击“OK”按钮，通知主程序
        /// </summary>
        public delegate void DebugConfigOK(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event DebugConfigOK DebugConfigOKEvent;
        /// <summary>
        /// 配置“调试”完成的事件。调试配置界面中点击“OK”按钮，通知主程序
        /// </summary>
        /// <param name="e">无</param>
        public void OnDebugConfigOK(CustomDataEvtArg e)
        {
            if (DebugConfigOKEvent != null)
            {
                DebugConfigOKEvent(this, e);
            }
        }

       
        /// <summary>
        /// 开始“调试”的事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public delegate void StartDebugHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event StartDebugHandler StartDebugEvent;
        /// <summary>
        /// 开始“调试”的事件
        /// </summary>
        /// <param name="e">无</param>
        public void StartDebug(CustomDataEvtArg e)
        {
            if (StartDebugEvent != null)
            {

                StartDebugEvent(this, e);
            }
        }

        

        /// <summary>
        /// 停止调试事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        public delegate void StopDebugHandler(object sender, CustomDataEvtArg e);
        /// <summary>
        /// 事件
        /// </summary>
        public event StopDebugHandler StopDebugEvent;
        /// <summary>
        /// 停止调试事件
        /// </summary>
        /// <param name="e">无</param>
        public void StopDebug(CustomDataEvtArg e)
        {
            if (StopDebugEvent != null)
            {
                StopDebugEvent(this, e);
            }
        }
        #endregion

        public delegate void HeartBeatCountHandler(int count);
        public event HeartBeatCountHandler HeartBeatCountEvent;
        public void HeartBeatCount(int count)
        {
            if (HeartBeatCountEvent != null)
            {
                HeartBeatCountEvent(count);
            }
        }
        public delegate void SendUeInfoToMainFormHandler(object sender, CustomDataEvtArg ce);
        public event SendUeInfoToMainFormHandler SendUeInfoToMainFormEvent;
        public void SendUeInfoToMainForm(object sender, CustomDataEvtArg ce)
        {
            if (SendUeInfoToMainFormEvent != null)
            {
                SendUeInfoToMainFormEvent( sender,  ce);
            }
        }
        public delegate void IBOPERStatusHandler(int i);
        public event IBOPERStatusHandler IBOPERStatusEvent;
        public void IBOPERStatus(int i )
        {
            if (IBOPERStatusEvent != null)
                IBOPERStatusEvent(i);
        }

        public delegate void DetectToImsiBankHandler(String imsi,int i);
        public event DetectToImsiBankHandler DetectToImsiBankEvent;
        public void DetectToImsiBank(String imsi, int i)
        {
            if (DetectToImsiBankEvent != null)
                DetectToImsiBankEvent(imsi,i);
        }
        public delegate void SendPagingPwrToCtrlFormHandler(object sender, CustomDataEvtArg ce);
        public event SendPagingPwrToCtrlFormHandler SendPagingPwrToCtrlFormEvent;
        public void SendPagingPwrToCtrlForm(object sender, CustomDataEvtArg ce)
        {
            if (SendPagingPwrToCtrlFormEvent != null)
            {
                SendPagingPwrToCtrlFormEvent( sender,  ce);
            }
        }
        public delegate void DetectToCtrlCmdHandler(String imsi);
        public event DetectToCtrlCmdHandler DetectToCtrlCmdEvent;
        public void DetectToCtrlCmd(String imsi)
        {
            if (DetectToCtrlCmdEvent != null)
                DetectToCtrlCmdEvent(imsi);
        }
        public delegate void SendToDlSniffFormHandler(object sender, CustomDataEvtArg ce);
        public event SendToDlSniffFormHandler SendToDlSniffFormEvent;
        public void SendToDlSniffForm(object sender, CustomDataEvtArg ce)
        {
            if (SendToDlSniffFormEvent != null)
            {
                SendToDlSniffFormEvent(sender, ce);
            }
        }
        public delegate void NcSniffStatusHandler(int i);
        public event NcSniffStatusHandler NcSniffStatusEvent;
        public void NcSniffStatus(int i)
        {
            if (NcSniffStatusEvent != null)
                NcSniffStatusEvent(i);
        }
    }
}
