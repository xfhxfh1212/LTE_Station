using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using COM.ZCTT.AGI.Common;
using AGIInterface;
using System.Runtime.InteropServices;
namespace DeviceMangerModule
{
    /// <summary>
    /// 类DeviceConfiguration为设备管理的子界面——设备配置界面
    /// </summary>
    public partial class DeviceConfiguration : Form
    {
        private string deviceName="";
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeviceConfiguration()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体load方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceConfiguration_Load(object sender, EventArgs e)
        {
            Device device = DeviceManger.FindDevice(this.deviceName);
            if (device.ConnectionState == (byte)Global.DeviceStateValue.Connecting)
            {
                comboBox_DeviceName.Enabled = false;
                textBox_IpAddress.Enabled = false;
                textBox_DataPortNum.Enabled = false;
                textBox_MsgPortNum.Enabled = false;
                checkBox1.Enabled = true;                
            }
            foreach (Device tempDevice in DeviceManger.deviceList)
            {
                this.comboBox_DeviceName.Items.Add(tempDevice.DeviceName);
            }
            if (this.deviceName.Length > 0)
            {
                for (int i = 0; i < comboBox_DeviceName.Items.Count; i++)
                {
                    if (comboBox_DeviceName.Items[i].ToString() == deviceName)
                        this.comboBox_DeviceName.SelectedIndex = i;
                    this.comboBox_DeviceName.Enabled = false;
                 }
                
            }
            this.comboBox_InterfaceType.Text = "LAN";
        }
        /// <summary>
        /// 确定按钮单击时的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                Device thisDevice = new Device();
                foreach (Device tempDevice in DeviceManger.deviceList)
                {
                    if (tempDevice.DeviceName == this.comboBox_DeviceName.Text.Trim())
                    {
                        thisDevice = tempDevice;
                        break;
                    }

                }
                if (this.comboBox_WorkMode.Text.Trim() == "CellScan")
                    thisDevice.DeviceWorkModel = (byte)COM.ZCTT.AGI.Common.Global.WorkModeValue.CellScan;
                else if (this.comboBox_WorkMode.Text.Trim() == "ProtocolTrack")
                    thisDevice.DeviceWorkModel = (byte)COM.ZCTT.AGI.Common.Global.WorkModeValue.ProtocolTrack;
                if (this.comboBox_InterfaceType.Text.Trim() == "LAN")
                {
                    thisDevice.PortType = (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.LAN;
                    thisDevice.IpAddress = this.textBox_IpAddress.Text.Trim();
                    thisDevice.DataPortNum = int.Parse(this.textBox_DataPortNum.Text.Trim());
                    thisDevice.MessagePortNum = int.Parse(this.textBox_MsgPortNum.Text.Trim());
                }
                else if (this.comboBox_InterfaceType.Text.Trim() == "USB")
                {
                    thisDevice.PortType = (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.USB;                
                }
                if (this.comboBox_OutDataType.Text.Trim() == "IQ")
                    thisDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.IQ;
                else if (this.comboBox_OutDataType.Text.Trim() == "TestData")
                    thisDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.TestData;
                else if (this.comboBox_OutDataType.Text.Trim() == "MeasuringData")
                    thisDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.MeasuringData;
                else if (this.comboBox_OutDataType.Text.Trim() == "LayerOutData")
                    thisDevice.OutputDataType = (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.LayerOutData;

                if (this.comboBox_DataStoreMode.Text.Trim() == "ExternalStore")
                    thisDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.ExternalStore;
                else if (this.comboBox_DataStoreMode.Text.Trim() == "TimingSave")
                    thisDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TimingSave;
                else if (this.comboBox_DataStoreMode.Text.Trim() == "StoredContiguously")
                    thisDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.StoredContiguously;
                else if (this.comboBox_DataStoreMode.Text.Trim() == "TriggerSave")
                    thisDevice.DataStoreMode = (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TriggerSave;
                if (this.textBox_DataStoreTime.Text.Length != 0)
                    thisDevice.DataStoreTime = UInt16.Parse(this.textBox_DataStoreTime.Text.Trim());

                if (this.comboBox_SaveDataType.Text.Trim() == "CSV")
                    thisDevice.DataStoreType = (byte)COM.ZCTT.AGI.Common.Global.DataSaveType.CSV;
                else if (this.comboBox_SaveDataType.Text.Trim() == "TXT")
                    thisDevice.DataStoreType = (byte)COM.ZCTT.AGI.Common.Global.DataSaveType.TXT;

                if (checkBox1.Enabled && checkBox1.Checked)
                {
                    if (MessageBox.Show("Are you sure to reset IP and Port of this device?", "", MessageBoxButtons.OKCancel)
                        == DialogResult.OK)
                    {
                        #region reset AGT ip and prot ,edit at 20141110
                        PC_AG_RENEW_IP_REQ stu_PC_AG_RENEW_IP_REQ = new PC_AG_RENEW_IP_REQ();
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.Reserved = 0;
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.Source = 3;
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.Destination = 2;
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.MessageType = 0x4008;
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.TransactionID = 0;
                        stu_PC_AG_RENEW_IP_REQ.msgHeader.MsgLen = (UInt16)((Marshal.SizeOf(stu_PC_AG_RENEW_IP_REQ)-12)/4);
                        //stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num = new byte[4];
                        //stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num = new byte[4];
                        stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num = Convert.ToUInt32(thisDevice.DataPortNum);
                        stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num = Convert.ToUInt32(thisDevice.MessagePortNum);
                        stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress = new byte[4];
                        //for (int i = 0; i < 4; i++)
                        //{
                        //    stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num[i] = GetByte(thisDevice.DataPortNum, i);
                        //    stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num[i] = GetByte(thisDevice.MessagePortNum, i);
                        //}
                        string[] s =thisDevice.IpAddress.Split(new char[] { '.' });
                        for (int i = 0; i < 4; i++)
                        {
                            stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress[i] =Convert.ToByte(s[i]);
                        }
                        stu_PC_AG_RENEW_IP_REQ.mu32AgtGateAdress = 0;
                        stu_PC_AG_RENEW_IP_REQ.mu32AgtMacAdress = 0;

                        byte[] configData = new byte[1024];
                        byte[] tempBytes;
                        int dstOffset = 0;

                        //Head
                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.msgHeader.Reserved);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.msgHeader.Source;
                        dstOffset++;

                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.msgHeader.Destination;
                        dstOffset++;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.msgHeader.MessageType);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 2);
                        dstOffset = dstOffset + 2;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.msgHeader.TransactionID);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 2);
                        dstOffset = dstOffset + 2;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.msgHeader.MsgLen);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 2);
                        dstOffset = dstOffset + 2;

                        //Body
                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.mu32ModefyMode);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num[0];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num[1];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num[2];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort0Num[3];
                        //dstOffset++;

                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num[0];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num[1];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num[2];
                        //dstOffset++;
                        //configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtPort1Num[3];
                        //dstOffset++;

                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress[0];
                        dstOffset++;
                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress[1];
                        dstOffset++;
                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress[2];
                        dstOffset++;
                        configData[dstOffset] = stu_PC_AG_RENEW_IP_REQ.mau8AgtIPAdress[3];
                        dstOffset++;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.mu32AgtGateAdress);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        tempBytes = BitConverter.GetBytes(stu_PC_AG_RENEW_IP_REQ.mu32AgtMacAdress);
                        Buffer.BlockCopy(tempBytes, 0, configData, dstOffset, 4);
                        dstOffset = dstOffset + 4;

                        tempBytes = new byte[dstOffset];
                        System.Buffer.BlockCopy(configData, 0, tempBytes, 0, dstOffset);

                        CustomDataEvtArg cusArg = new CustomDataEvtArg();
                        cusArg.data = tempBytes;
                        cusArg.deivceName = comboBox_DeviceName.Text;
                        Global.tempClass.SendDataToDevice(sender, cusArg);
                        ////////////////////////////////////////////
                        #endregion
                    }
                }
                this.Close();

                if (AGIInterface.Class1.OnChangeDeviceInfoList != null)
                    AGIInterface.Class1.OnChangeDeviceInfoList(this.comboBox_DeviceName.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Instrument configuration is exception:" + ex.Message);
            }
        }
        private byte GetByte(int num, int pos)
        {
            return Convert.ToByte(num & (1 << pos));
        }

        /// <summary>
        /// 当设备选择列表中设备选择改变时的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_DeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            foreach (Device device in DeviceManger.deviceList)
            { 
                if (device.DeviceName == this.comboBox_DeviceName.Text.Trim())
                {
                    if (device.DeviceWorkModel == (byte)COM.ZCTT.AGI.Common.Global.WorkModeValue.CellScan)
                        this.comboBox_WorkMode.Text = "CellScan";
                    else if(device.DeviceWorkModel == (byte)COM.ZCTT.AGI.Common.Global.WorkModeValue.ProtocolTrack)
                        this.comboBox_WorkMode.Text = "ProtocolTrack";
                    if (device.PortType == (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.LAN)
                    {
                        this.comboBox_InterfaceType.Text = "LAN";
                        this.textBox_IpAddress.Text = device.IpAddress;
                        this.textBox_DataPortNum.Text = device.DataPortNum.ToString();
                        this.textBox_MsgPortNum.Text = device.MessagePortNum.ToString();
                    }
                    else if (device.PortType == (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.USB)
                        this.comboBox_InterfaceType.Text = "USB";

                    if (device.OutputDataType == (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.IQ)
                        this.comboBox_OutDataType.Text = "IQ";
                    else if (device.OutputDataType == (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.LayerOutData)
                        this.comboBox_OutDataType.Text = "LayerOutData";
                    else if (device.OutputDataType == (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.MeasuringData)
                        this.comboBox_OutDataType.Text = "MeasuringData";
                    else if (device.OutputDataType == (byte)COM.ZCTT.AGI.Common.Global.OutDataTypeValue.TestData)
                        this.comboBox_OutDataType.Text = "TestData";

                    if (device.DataStoreMode == (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.ExternalStore)
                        this.comboBox_DataStoreMode.Text = "ExternalStore";
                    else if (device.DataStoreMode == (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.StoredContiguously)
                        this.comboBox_DataStoreMode.Text = "StoredContiguously";
                    else if (device.DataStoreMode == (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TimingSave)
                        this.comboBox_DataStoreMode.Text = "TimingSave";
                    else if (device.DataStoreMode == (byte)COM.ZCTT.AGI.Common.Global.DataSaveMode.TriggerSave)
                        this.comboBox_DataStoreMode.Text = "TriggerSave";

                    this.textBox_DataStoreTime.Text = device.DataStoreTime.ToString();
                    if (device.DataStoreType == (byte)COM.ZCTT.AGI.Common.Global.DataSaveType.CSV)
                        this.comboBox_SaveDataType.Text = "CSV";
                    else if (device.DataStoreType == (byte)COM.ZCTT.AGI.Common.Global.DataSaveType.TXT)
                        this.comboBox_SaveDataType.Text = "TXT";
                }
            }
        }
        /// <summary>
        /// 当接口类型改变时的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_InterfaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_InterfaceType.Text == "USB")
            {
                this.label3.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.textBox_IpAddress.Visible = false;
                this.textBox_MsgPortNum.Visible = false;
                this.textBox_DataPortNum.Visible = false;
            }
            else
            {
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
                this.textBox_IpAddress.Visible = true;
                this.textBox_MsgPortNum.Visible = true;
                this.textBox_DataPortNum.Visible = true;
            }
        }

        private void button_CloseClick(object sender, EventArgs e)
        {
            this.Close();
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

        /// <summary>
        /// 检测键盘输入操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceConfiguration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button_OK.Focus();
                button_OK_Click(this, new EventArgs());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox_IpAddress.Enabled = true;
                textBox_DataPortNum.Enabled = true;
                textBox_MsgPortNum.Enabled = true;
            }
            else
            {
                textBox_IpAddress.Enabled = false;
                textBox_DataPortNum.Enabled = false;
                textBox_MsgPortNum.Enabled = false;
            }
        }

        
    }
}
