using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Plugin;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using COM.ZCTT.AGI.Common;
using System.IO;
using AGIInterface;
using WugSshLib;
namespace DeviceMangerModule
{
    /// <summary>
    /// 类DevcieManger为设备管理的主窗体，包含多设备的所有管理功能
    /// </summary>
    public partial  class DeviceManger : DockContent, COM.ZCTT.AGI.Plugin.IPlugin
    {
        public static List<Device> deviceList = new List<Device>();

        public static string deviceName = null;
        //设备连接状态显示变化使用线程
   //     Thread deviceConnStateThread;
        private delegate void deviceConnStateChang(Device tempDevice, string state);
        private delegate void deviceInfoListChange(string name);

        private string selectedDeviceName;
        static bool EventLoad = false;
        SshReader sshreader;
        

        //public int flag = 0;//设备被占用，断开重连再次显示设备被占用。
        /// <summary>
        /// 窗体的load方法，窗体加载时运行此方法
        /// </summary>
        public new void Load()
        {
            this.ShowDialog();
            foreach (ToolStripItem item in this.cmsRight.Items)
            { 
                if(item.Name == "Reboot")
                    item.Enabled = Global.RebootButtonEnable;
            }
        }
        /// <summary>
        /// 构造函数，进行一些初始化操作和事件委托的挂载
        /// </summary>
        public DeviceManger()
        {
            //do this only once
            if (!EventLoad)
            {
                Global.tempClass.SendDataToDeviceEvent += new Class1.SendDataToDeviceHandler(send);
                AGIInterface.Class1.OnChangeDeviceInfoList += new Class1.ChangeDeviceInfoList(ChangeDeiveInfoListDisp);
                AGIInterface.Class1.OnConnOk += new AGIInterface.Class1.ConnOKHandler(ConnOkHandler);
                AGIInterface.Class1.OnConnWrong += new AGIInterface.Class1.ConnDiscHandler(ConnDiscHander);
                AGIInterface.Class1.OnChangDeviceListView += new AGIInterface.Class1.ChangeDeviceListViewHandler(DeviceListViewItem);
                EventLoad = true;
                init();
            }
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            DeviceListViewDisplay();

        }
        
      
        /// <summary>
        /// 设备管理模块初始化操作
        /// </summary>
        private void init()
        {
            bool checkOk = true;
            //检查是否存在配置文件和保存文件地址
            if (!File.Exists(COM.ZCTT.AGI.Common.Global.deviceConfigFilePath))
            {
                MessageBox.Show("Instrument config filepath does not exist,pelease config it!");
                checkOk = false;
            }
            DeviceManger.deviceList.Clear();
            if (checkOk)
            {
                ReadDeviceInfoFromXMLToMemory();
            }
        }
        #region 设备状态维护
        /// <summary>
        /// 改变设备状态
        /// </summary>
        /// <param name="tempDevice">设备名称</param>
        /// <param name="state">状态值</param>
        private void ChangeDeviceState(Device tempDevice, string state)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new deviceConnStateChang(DeviceStateDisp),new object[] {tempDevice,state });
            }
            else
            {
                this.DeviceStateDisp(tempDevice,state);
            }
            
        }
        /// <summary>
        /// 根据设备的连接状态变化在设备列76表的显示背景也随之变化
        /// </summary>
        private void DeviceStateDisp(Device tempDevice, string state)
        {
            foreach (ListViewItem tempItem in this.DeviceListView.Items)
            {
                if (tempItem.Text == tempDevice.DeviceName)
                {
                    if (state == "connected")
                        tempItem.ImageKey = "connected.ico";
                    else
                        tempItem.ImageKey = "disconnect.ico";
                    break;
                }
            }
        }
 
        #endregion
        #region 查找设备
        /// <summary>
        /// 查找设备
        /// </summary>
        /// <param name="deviceName"></param>
        /// <returns>查找到的设备</returns>
        public static Device FindDevice(string deviceName)
        {
            Device device = null;

            foreach (Device tempDevice in deviceList)
            {
                if (tempDevice.DeviceName == deviceName)
                {
                    device = tempDevice;
                    break;
                }
            }
            return device;
        }
        #endregion
        #region 指定设备发送信息
        /// <summary>
        /// 指定设备发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void send(object sender, AGIInterface.CustomDataEvtArg e)
        
        {
            string deviceName = e.deivceName;
            byte[] message = e.data;
           Device device = FindDevice(deviceName);
           if (device == null)
           {
               MessageBox.Show("Sorry,Can not find this device!");
               return;
           }
            if (device.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
            {
                device.sendMessage(message);
            }
            else
            {
                MessageBox.Show("Instrument:" + device.DeviceName + "has not been connected!");
                return;
            }
            
        }
        #endregion
        #region 建立设备连接及开始数据接收
        /// <summary>
        /// 当设备连接成功后设置设备的连接状态为已连接并开始数据接收和心跳检测
        /// </summary>
        //TODO this
        private void ConnOkHandler(string deviceName)
        {
            Device connDevice = FindDevice(deviceName);
            connDevice.ConnectionState = (byte)Global.DeviceStateValue.Connecting;

            ChangeDeviceState(connDevice, "connected");
            if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                AGIInterface.Class1.OnChangeDeviceInfoList(deviceName);
            //connDevice.DataRecv();/////////////////////////////////////////////
            if (!connDevice.HeartCheckStart)
            {
                connDevice.HeartCheckStart = true;
                connDevice.timer.Enabled = true;
            }
        }
        //连接断开时改变设备显示为断开状态
        private void ConnDiscHander(String deviceName)
        {
            Device connDevice = FindDevice(deviceName);
            connDevice.ConnectionState = (byte)Global.DeviceStateValue.Disconnection;
            ChangeDeviceState(connDevice, "disconnect");
        }
        #endregion
        #region 设备列表和设备信息列表的显示及事件
        /// <summary>
        /// 设备列表显示方法
        /// </summary>
        private void DeviceListViewDisplay()
        {
            foreach (Device device in deviceList)
            {
                //设备列表显示设备
                ListViewItem tempItem = new ListViewItem();
                tempItem.Text = device.DeviceName;
                tempItem.ImageKey = device.ImageKey;
                this.DeviceListView.Items.Add(tempItem);
            }
        }
        
        /// <summary>
        /// 当设备选择不同时设备信息列表随之改变信息显示方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DeviceListView.SelectedItems.Count != 0)
            {
                this.button_connect.Enabled = true;
                this.button_disconnect.Enabled = true;
                foreach (ToolStripItem item in this.cmsRight.Items)
                {
                    switch (item.Name)
                    {
                        case "Reboot":
                        case "Configure":
                        case "Delete":
                            item.Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
                //this.button_config.Enabled = true;
                selectedDeviceName = this.DeviceListView.SelectedItems[0].Text;

                DeviceInfoListViewDisplay(selectedDeviceName);
                //#####连接linux######
                //try
                //{
                //    string hostname = this.DeviceInfoListView.Items[0].SubItems[1].Text;
                //    sshreader = new SshReader(hostname, 22, "root", "111");
                //    sshreader.OpenConnect();
                //}
                //catch (System.Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine(ex.Message);
                //    MessageBox.Show("Failed to connect the remote server.{0}:22", this.DeviceInfoListView.Items[0].SubItems[1].Text);
                //}
                //###############################
            }
            else
            {
                this.DeviceInfoListView.Items.Clear();
                this.button_connect.Enabled = false;
                this.button_disconnect.Enabled = false;
                foreach (ToolStripItem item in this.cmsRight.Items)
                {
                    switch (item.Name)
                    {
                        case "Reboot":
                        case "Configure":
                        case "Delete":
                            item.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
               //this.button_config.Enabled = false;
              //  this.button_reboot.Enabled = false;
            }
        }
        /// <summary>
        /// 当添加新设备后在DeviceListView上添加新设备的项显示
        /// </summary>
        /// <param name="itemName">要添加的设备名称</param>
        private void DeviceListViewItem(string itemName)
        {
          Device temp = FindDevice(itemName);
          ListViewItem item = new ListViewItem();
          item.Text = temp.DeviceName;
          item.ImageKey = temp.ImageKey;
          this.DeviceListView.Items.Add(item);
        }

        /// <summary>
        /// 设备信息列表显示方法
        /// </summary>
        /// <param name="name"></param>
        private void DeviceInfoListViewDisp(string name)
        {

            if (this.DeviceInfoListView.InvokeRequired)
            {
                this.DeviceInfoListView.Invoke(new deviceInfoListChange(DeviceInfoList), new object[] { name });
            }
            else
            {
                DeviceInfoList(name);
            }   
        }

        /// <summary>
        /// 设备信息列表显示
        /// </summary>
        /// <param name="selectedDeviceName"></param>
        private void DeviceInfoList(string selectedDeviceName)
        {
            Device selectedDevice = FindDevice(selectedDeviceName);
            if (selectedDevice != null)
            {
                this.DeviceInfoListView.Items.Clear();
                this.DeviceInfoListView.ForeColor = Color.Black;
                if (selectedDevice.PortType == (byte)Global.PortTypeValue.LAN)
                {
                    //   itemPortType.SubItems.Add("LAN");
                    ListViewItem item1 = new ListViewItem("IP");
                    item1.SubItems.Add(selectedDevice.IpAddress);
                    ListViewItem item2 = new ListViewItem("Data Port");
                    item2.SubItems.Add(selectedDevice.DataPortNum.ToString());
                    ListViewItem item3 = new ListViewItem("Message Port");
                    item3.SubItems.Add(selectedDevice.MessagePortNum.ToString());
                    //this.DeviceInfoListView.Items.AddRange(new ListViewItem[] {itemPortType, item1, item2, item3 });
                    this.DeviceInfoListView.Items.AddRange(new ListViewItem[] { item1, item2, item3 });
                }
            }
            else
            {
                this.DeviceInfoListView.Items.Clear();
                this.DeviceInfoListView.ForeColor = Color.Red;
                ListViewItem exceptionItem = new ListViewItem("Exception");
                exceptionItem.SubItems.Add("Connect failed!");
                this.DeviceInfoListView.Items.Add(exceptionItem);
            }
        }

        /// <summary>
        /// 设备信息列表显示
        /// </summary>
        /// <param name="selectedDeviceName"></param>
        private void DeviceInfoListViewDisplay(string selectedDeviceName)
        {
            this.DeviceInfoListView.Items.Clear();
            this.DeviceInfoListView.ForeColor = Color.Black;
            Device selectedDevice = FindDevice(selectedDeviceName);
            if (selectedDevice != null)
            {
              //  ListViewItem itemPortType = new ListViewItem("InterfaceType");
                if (selectedDevice.PortType == (byte)Global.PortTypeValue.LAN)
                {
                 //   itemPortType.SubItems.Add("LAN");
                    ListViewItem item1 = new ListViewItem("IP");
                    item1.SubItems.Add(selectedDevice.IpAddress);
                    ListViewItem item2 = new ListViewItem("Data Port");
                    item2.SubItems.Add(selectedDevice.DataPortNum.ToString());
                    ListViewItem item3 = new ListViewItem("Message Port");
                    item3.SubItems.Add(selectedDevice.MessagePortNum.ToString());
                    //this.DeviceInfoListView.Items.AddRange(new ListViewItem[] {itemPortType, item1, item2, item3 });
                    this.DeviceInfoListView.Items.AddRange(new ListViewItem[] {item1, item2, item3 });
                }
                //else if (selectedDevice.PortType == (byte)Global.PortTypeValue.USB)
                //{
                //    itemPortType.SubItems.Add("USB");
                //    this.DeviceInfoListView.Items.Add(itemPortType);
                //}
                //switch (selectedDevice.OutputDataType)
                //{
                //    case 0: ListViewItem item4 = new ListViewItem("OutDataType");
                //        item4.SubItems.Add("IQ");
                //        this.DeviceInfoListView.Items.Add(item4);
                //        break;
                //    case 1: ListViewItem item5 = new ListViewItem("OutDataType");
                //        item5.SubItems.Add("TestData");
                //        this.DeviceInfoListView.Items.Add(item5);
                //        break;
                //    case 2: ListViewItem item6 = new ListViewItem("OutDataType");
                //        item6.SubItems.Add("MeasuringData");
                //        this.DeviceInfoListView.Items.Add(item6);
                //        break;
                //    case 3: ListViewItem item7 = new ListViewItem("OutDataType");
                //        item7.SubItems.Add("LayerOutData");
                //        this.DeviceInfoListView.Items.Add(item7);
                //        break;
                //    default: break;
                //}
            //    switch (selectedDevice.DataStoreMode)
            //    {
            //        case 1: ListViewItem item5 = new ListViewItem("DataStoreMode");
            //            item5.SubItems.Add("TimingSave");
            //            this.DeviceInfoListView.Items.Add(item5);
            //            break;
            //        case 2: ListViewItem item6 = new ListViewItem("DataStoreMode");
            //            item6.SubItems.Add("StoredContiguously");
            //            this.DeviceInfoListView.Items.Add(item6);
            //            break;
            //        case 3: ListViewItem item7 = new ListViewItem("DataStoreMode");
            //            item7.SubItems.Add("TriggerSave");
            //            this.DeviceInfoListView.Items.Add(item7);
            //            break;
            //        default: ListViewItem item8 = new ListViewItem("DataStoreMode");
            //            item8.SubItems.Add("have not been setted");
            //            this.DeviceInfoListView.Items.Add(item8);
            //            break;
            //    }
            }
            else 
            {
                MessageBox.Show("No instrument has been added!");
            }
        }

        /// <summary>
        /// 改变设备信息列表显示信息
        /// </summary>
        /// <param name="name"></param>
        private void ChangeDeiveInfoListDisp(string name)
        {
            DeviceInfoListViewDisp(name);
        }
        #endregion
        #region 按键事件处理
        /// <summary>
        /// 关闭按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 设备配置按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_config_Click(object sender, EventArgs e)
        {
            DeviceConfiguration configForm = new DeviceConfiguration();
            configForm.DeviceName = selectedDeviceName;
            configForm.ShowDialog();
        }
        /// <summary>
        /// 新建设备按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_NewDevice_Click(object sender, EventArgs e)
        {
            NewDevice newDevice = new NewDevice();
            newDevice.ShowDialog();
        }
        /// <summary>
        /// 连接按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_connect_Click(object sender, EventArgs e)
        {
            //Global.flag = 0;
            if (this.DeviceListView.SelectedItems.Count > 0)
            {
                deviceName = this.DeviceListView.SelectedItems[0].Text;
                Device connDevice = FindDevice(deviceName);
                if (connDevice.ConnectionState == (byte)Global.DeviceStateValue.Disconnection)
                {
                    connDevice.ConnectionState =(byte) Global.DeviceStateValue.connState;
                    connDevice.NewMessageConn();
                    foreach (Device device in DeviceManger.deviceList)
                    {
                        if (device.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
                            Global.GCurrentDevice = device.DeviceName;

                    }
                }
                else
                {
                    MessageBox.Show("This instrument is connected!");
                }
            }
            else
            {
                MessageBox.Show("Please choose the device which you want to connect first!");
            }
        }
        /// <summary>
        /// 断开设备连接按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_disconnect_Click(object sender, EventArgs e)
        {
            //if (Global.testStatus== Global.TestStatus.Start)
            //{
            //    MessageBox.Show("Please stop running!");
            //}
            //else
            //{
                Global.connectionStatus = false;
                if (this.DeviceListView.SelectedItems.Count > 0)
                {
                    Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
                    if (connDevice.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
                    {
                        connDevice.CloseDeviceConn();
                        connDevice.timer.Enabled = false;
                        connDevice.HeartCheckStart = false;
                    }
                    else
                    {
                        MessageBox.Show("This instrument is disconnected!");
                    }
                }
                //#######关闭linux连接#######
                try
                {
                    sshreader.CloseConnect();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                }
                //#########################
            //}
            
        }
        /// <summary>
        /// 删除设备按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_RemoveDevice_Click(object sender, EventArgs e)
        {
            if (this.DeviceListView.SelectedItems.Count > 0)
            {
                Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
                if (connDevice.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
                {
                    connDevice.CloseDeviceConn();
                    this.DeviceListView.SelectedItems[0].Remove();
                }
                else
                {
                    this.DeviceListView.SelectedItems[0].Remove();
                }
                deviceList.Remove(connDevice);
            }
        }
        #endregion
        #region XML文件读取修改和保存
        /// <summary>
        /// 把设备配置信息从XML文件中读取到内存中存储
        /// </summary>
        private void ReadDeviceInfoFromXMLToMemory()
        {
            try
            {
                XmlDocument deviceConfigXml = new XmlDocument();
                deviceConfigXml.Load(COM.ZCTT.AGI.Common.Global.deviceConfigFilePath);
                XmlNodeList deviceList = deviceConfigXml.SelectSingleNode("DeviceConfiguration").ChildNodes;
                foreach (XmlNode node in deviceList)
                {
                    XmlElement device = (XmlElement)node;
                    Device tempDevice = new Device();
                    tempDevice.DeviceName = device.GetAttribute("name").ToString();
                    tempDevice.ImageKey = device.GetAttribute("ImageKey").ToString();
                    switch (device.GetAttribute("InterfaceType").ToString())
                    {
                        case "LAN": tempDevice.PortType = (byte)Global.PortTypeValue.LAN; break;
                        case "USB": tempDevice.PortType = (byte)Global.PortTypeValue.USB; break;
                        default: break;
                    }
                    XmlNodeList infoList = device.ChildNodes;
                    foreach (XmlNode node1 in infoList)
                    {
                        XmlElement deviceInfo = (XmlElement)node1;
                        if (deviceInfo.InnerText.Length != 0)
                        {
                            string tempStr = deviceInfo.InnerText.ToString();
                            switch (deviceInfo.Name)
                            {
                                case "IpAddress": tempDevice.IpAddress = tempStr; break;
                                case "DataPortNum": tempDevice.DataPortNum = int.Parse(tempStr); break;
                                case "MessagePortNum": tempDevice.MessagePortNum = int.Parse(tempStr); break;
                                case "OutputDataType":
                                    {
                                        if (tempStr == "IQ")
                                            tempDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.IQ;
                                        else if (tempStr == "TestData")
                                            tempDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.TestData;
                                        else if (tempStr == "MeasuringData")
                                            tempDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.MeasuringData;
                                        else if (tempStr == "LayerOutData")
                                            tempDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.LayerOutData;
                                        break;
                                    }
                                case "DataStoreMode":
                                    {
                                        if (tempStr == "ExternalStore")
                                            tempDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.ExternalStore;
                                        else if (tempStr == "TimingSave")
                                            tempDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TimingSave;
                                        else if (tempStr == "StoredContiguously")
                                            tempDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.StoredContiguously;
                                        else if (tempStr == "TriggerSave")
                                            tempDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TriggerSave;
                                        break;
                                    }
                                case "DataStoreTime": tempDevice.DataStoreTime = ushort.Parse(tempStr); break;
                                case "SaveDataType":
                                    {
                                        if (tempStr == "CSV")
                                            tempDevice.DataStoreType = (byte)Global.DataSaveType.CSV;
                                        else
                                            tempDevice.DataStoreType = (byte)Global.DataSaveType.TXT;
                                        break;
                                    }
                                default: break;
                            }
                        }
                    }
                    DeviceManger.deviceList.Add(tempDevice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Instrument init failed:" + ex.Message);
            }
        }
        /// <summary>
        /// 从内存中把设备信息读取到文件中保存
        /// </summary>
        private void UpdateXMLFromMemory()
        {
            try
            {
                XmlDocument deviceConfig = new XmlDocument();
                deviceConfig.Load(COM.ZCTT.AGI.Common.Global.deviceConfigFilePath);
                XmlNode devices = deviceConfig.SelectSingleNode("DeviceConfiguration");
                devices.RemoveAll();
                foreach (Device device in deviceList)
                {
                    XmlElement xe = deviceConfig.CreateElement("Device");
                    xe.SetAttribute("name", device.DeviceName);
                    xe.SetAttribute("ImageKey", device.ImageKey);
                    if (device.PortType == (byte)Global.PortTypeValue.LAN)
                    {
                        xe.SetAttribute("InterfaceType", "LAN");
                        XmlElement xe2 = deviceConfig.CreateElement("IpAddress");
                        xe2.InnerText = device.IpAddress;
                        XmlElement xe3 = deviceConfig.CreateElement("DataPortNum");
                        xe3.InnerText = device.DataPortNum.ToString();
                        XmlElement xe4 = deviceConfig.CreateElement("MessagePortNum");
                        xe4.InnerText = device.MessagePortNum.ToString();
                        xe.AppendChild(xe2);
                        xe.AppendChild(xe3);
                        xe.AppendChild(xe4);
                    }
                    else
                        xe.SetAttribute("InterfaceType", "USB");
                    XmlElement xe5 = deviceConfig.CreateElement("OutputDataType");
                    if (device.OutputDataType == (byte)Global.OutDataTypeValue.IQ)
                        xe5.InnerText = "IQ";
                    else if (device.OutputDataType == (byte)Global.OutDataTypeValue.LayerOutData)
                        xe5.InnerText = "LayerOutData";
                    else if (device.OutputDataType == (byte)Global.OutDataTypeValue.MeasuringData)
                        xe5.InnerText = "MeasuringData";
                    else if (device.OutputDataType == (byte)Global.OutDataTypeValue.TestData)
                        xe5.InnerText = "TestData";
                    XmlElement xe6 = deviceConfig.CreateElement("DataStoreMode");
                    if (device.DataStoreMode == (byte)Global.DataSaveMode.ExternalStore)
                        xe6.InnerText = "ExternalStroe";
                    else if (device.DataStoreMode == (byte)Global.DataSaveMode.StoredContiguously)
                        xe6.InnerText = "StoredContiguously";
                    else if (device.DataStoreMode == (byte)Global.DataSaveMode.TimingSave)
                        xe6.InnerText = "TimingSave";
                    else if (device.DataStoreMode == (byte)Global.DataSaveMode.TriggerSave)
                        xe6.InnerText = "TriggerSave";
                    XmlElement xe7 = deviceConfig.CreateElement("DataStoreTime");
                    xe7.InnerText = device.DataStoreTime.ToString();
                    XmlElement xe8 = deviceConfig.CreateElement("SaveDataType");
                    if (device.DataStoreType == (byte)Global.DataSaveType.CSV)
                        xe8.InnerText = "CSV";
                    else
                        xe8.InnerText = "TXT";

                    xe.AppendChild(xe5);
                    xe.AppendChild(xe6);
                    xe.AppendChild(xe7);
                    xe.AppendChild(xe8);
                    devices.AppendChild(xe);
                }
                deviceConfig.Save(COM.ZCTT.AGI.Common.Global.deviceConfigFilePath);
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }   
        }
        #endregion
        /// <summary>
        /// 当设备管理界面窗口关闭时的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceManger_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateXMLFromMemory();
           // Global.RebootButtonEnable = this.button_reboot.Enabled;
          //  this = null;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xa1 && (int)m.WParam == 0x3)
            {
                return;
            }
            if (m.Msg == 0xa3 && ((int)m.WParam == 0x3 || (int)m.WParam == 0x2))
            {
                return;
            }
            if (m.Msg == 0xa4 && ((int)m.WParam == 0x2 || (int)m.WParam == 0x3))
            {
                return;
            }
            if (m.Msg == 0x112 && (int)m.WParam == 0xf100)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeviceListView.SelectedItems.Clear();
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    if (this.DeviceListView.SelectedItems.Count > 0)
        //    {
        //        Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
        //        try
        //        {
        //            string hostname = connDevice.IpAddress;
        //            sshreader = new SshReader(hostname, 22, "root", "111");
        //            sshreader.OpenConnect();
        //            sshreader.WaitString("]#");
        //            sshreader.InputCommand("reboot");
        //        }
        //        catch (System.Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine(ex.Message);
        //            MessageBox.Show(string.Format("Failed to connect the remote server.{0}:22", connDevice.IpAddress));
        //            return;     
        //        }
        //        MessageBox.Show(string.Format("Rebooting the server:{0}:22...please click the button 'Connect' later.", connDevice.IpAddress));
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please choose the device which you want to connect first!");
        //    }

        //}
        delegate void buttonRebootinvoke();

        /// <summary>
        /// 改变状态
        /// </summary>
        private void ChangeStatus()
        {
            // if (button_reboot.InvokeRequired)
            //{
            //    buttonRebootinvoke buttonReboot = new buttonRebootinvoke(ChangeStatus);
            //    button_reboot.Invoke(buttonReboot);
            //}
            //else
            //{
            //    button_reboot.Enabled = true;
            //}
        }

        /// <summary>
        /// 重启线程
        /// </summary>
        /// <param name="connDevice"></param>
        public void RebootThreadFun(Device connDevice)
        {
                try
                {
                    string hostname = connDevice.IpAddress;
                    sshreader = new SshReader(hostname, 22, "root", "111");
                    sshreader.OpenConnect();
                    //sshreader.WaitString("]#");
                    sshreader.InputCommand("reboot");
                    ChangeStatus();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message+ "\n"+ ex.StackTrace);
                    ChangeStatus();
                    MessageBox.Show(string.Format("Failed to connect the remote server.{0}:22", connDevice.IpAddress));
                    return;
                }
                MessageBox.Show(string.Format("Rebooting the server:{0}:22...please click the button 'Connect' later.", connDevice.IpAddress));
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure to reboot?", "Warning！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (this.DeviceListView.SelectedItems.Count > 0)
                {
                    foreach (ToolStripItem item in this.cmsRight.Items)
                    {
                        switch (item.Name)
                        {
                            case "Reboot":
                               item.Enabled = false;
                                break;
                            default:
                                break;
                        }
                    }
                    //button_reboot.Enabled = false;
                    Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
                    Thread RebootThread = new Thread(() => RebootThreadFun(connDevice));
                    RebootThread.Start();
                    //Thread checkThread = new Thread(() => checkRebootThreadFun(RebootThread));
                    //checkThread.Start();
                }
                else
                {
                    MessageBox.Show("Please choose the device which you want to reboot first!");
                }
            }
           
        }

        private void DeviceListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsRight.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            NewDevice newdevice = new NewDevice();
            newdevice.Show();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this device from list?", "NOTE",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (this.DeviceListView.SelectedItems.Count > 0)
                {
                    Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
                    if (connDevice.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
                    {
                        connDevice.CloseDeviceConn();
                        this.DeviceListView.SelectedItems[0].Remove();
                    }
                    else
                    {
                        this.DeviceListView.SelectedItems[0].Remove();
                    }
                    deviceList.Remove(connDevice);
                    UpdateXMLFromMemory();
                }
            }
            return;
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            DeviceConfiguration configForm = new DeviceConfiguration();
            configForm.DeviceName = selectedDeviceName;
            configForm.ShowDialog();
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure to reboot?", "Warning！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (this.DeviceListView.SelectedItems.Count > 0)
                {
                    foreach (ToolStripItem item in this.cmsRight.Items)
                    {
                        switch (item.Name)
                        {
                            case "Reboot":
                                item.Enabled = false;
                                break;
                            default:
                                break;
                        }
                    }
                   // button_reboot.Enabled = false;
                    Device connDevice = FindDevice(this.DeviceListView.SelectedItems[0].Text);
                    Thread RebootThread = new Thread(() => RebootThreadFun(connDevice));
                    RebootThread.Start();
                    //Thread checkThread = new Thread(() => checkRebootThreadFun(RebootThread));
                    //checkThread.Start();
                }
                else
                {
                    MessageBox.Show("Please choose the device which you want to reboot first!");
                }
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsRight.Show(MousePosition.X, MousePosition.Y);
            }
        }


        //delegate void buttonRebootinvoke(Thread threads);
        //private void checkRebootThreadFun(Thread threads)
        //{

        //    if (button_reboot.InvokeRequired)
        //    {
        //        buttonRebootinvoke buttonReboot = new buttonRebootinvoke(checkRebootThreadFun);
        //        button_reboot.Invoke(buttonReboot, threads);
        //    }
        //    else
        //    {
        //    while (true)
        //    {
        //        if (threads.IsAlive == false)
        //        {
        //            button_reboot.Enabled = true;
        //            return;
        //        }
        //    }
        //    }
            
        //}
    }
}
