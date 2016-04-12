using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace DeviceMangerModule
{
    /// <summary>
    /// 类NewDevice为设备管理的子窗体——新建设备界面
    /// </summary>
    public partial class NewDevice : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewDevice()
        {
            InitializeComponent();
            comboBox_ConnecType.SelectedIndex = 0;
        }
        /// <summary>
        /// 关闭按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 添加新设备按钮单击处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (this.textBox_DeviceName.TextLength != 0)
            {
                bool used = false;
                foreach (Device temp in DeviceManger.deviceList)
                {
                    if ("设备"+this.textBox_DeviceName.Text.Trim() == temp.DeviceName)
                    {
                        MessageBox.Show("This name has been used!");
                        this.textBox_DeviceName.Text = "";
                        used = true;
                        break;
                    }
                }
                if (!used)
                {
                    Device tempDevice = new Device();
                    while (this.textBox_DeviceName.Text.Trim().Length < 4)
                    {
                        this.textBox_DeviceName.Text = "0" + this.textBox_DeviceName.Text.Trim();
                    }
                    tempDevice.DeviceName = "设备" + this.textBox_DeviceName.Text.Trim();
                    if (this.comboBox_ConnecType.Text == "LAN")
                    {
                        try
                        {
                            tempDevice.PortType = (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.LAN;
                            tempDevice.IpAddress = this.textBox_IPAddress.Text.Trim();
                            tempDevice.MessagePortNum = int.Parse(this.textBox_SignalPortNum.Text.Trim());
                            tempDevice.DataPortNum = int.Parse(this.textBox_DataPortNum.Text.Trim()); 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Add instrument exception:" + ex.Message);
                            return;
                        }
                       
                    }
                    else if (this.comboBox_ConnecType.Text == "USB")
                    {
                        tempDevice.PortType = (byte)COM.ZCTT.AGI.Common.Global.PortTypeValue.USB;
                    }
                    DeviceManger.deviceList.Add(tempDevice);
                    if (AGIInterface.Class1.OnChangDeviceListView != null)
                        AGIInterface.Class1.OnChangDeviceListView(tempDevice.DeviceName);
                    this.Close();
                    
                }
            }
        }
        /// <summary>
        /// 连接类型选项改变时改变界面布局和显示的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_ConnecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_ConnecType.SelectedItem.ToString().Trim() == "USB")
            {
                this.label3.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.textBox_IPAddress.Visible = false;
                this.textBox_DataPortNum.Visible = false;
                this.textBox_SignalPortNum.Visible = false;
            }
            else
            {
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
                this.textBox_IPAddress.Visible = true;
                this.textBox_DataPortNum.Visible = true;
                this.textBox_SignalPortNum.Visible = true;
            }
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

 
        private static int GetContentLength(string queryString)
        {
            byte[] bl = System.Text.Encoding.Default.GetBytes(queryString);
            return bl.Length;
        }

        private void textBox_DeviceName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
    }
}
