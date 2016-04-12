using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COM.ZCTT.AGI.Common;
using System.Runtime.InteropServices;
using AGIInterface;
using DeviceMangerModule;

namespace LTE
{
    public partial class CtrlCmdForm : Form
    {
        public CtrlCmdForm()
        {
            InitializeComponent();            
            textBox1.Text = "1";
            textBox1.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textBox2.Text = "100";
            comboBox2.SelectedIndex = 1;
            LTEUnion.DataSave(this.listView1,System.Windows.Forms.Application.StartupPath + "\\CtrlCmdForm.bin");
            Global.tempClass.SendPagingPwrToCtrlFormEvent += new Class1.SendPagingPwrToCtrlFormHandler(PagingPwrReceived);
            Global.tempClass.DetectToCtrlCmdEvent += new Class1.DetectToCtrlCmdHandler(DetectToCtrlCmd);
        }

        private void CtrlCmdForm_Load(object sender, EventArgs e)
        {

        }
        private void CtrlCmdForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Device.CtrlCmdFormIsClosed = true;
            LTEUnion.DataSave(this.listView1, System.Windows.Forms.Application.StartupPath + "\\CtrlCmdForm.bin");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!LTEUnion.IsDigitalString(textBox2.Text))
            {
                MessageBox.Show("请输入正确的寻呼间隔！");
            }
            else if (Convert.ToInt32(textBox2.Text) < 1 || Convert.ToInt32(textBox2.Text) > 255)
            {
                MessageBox.Show("寻呼间隔范围为1~255秒！");
            }
            else if (comboBox2.SelectedIndex == 1 && !LTEUnion.IsIMSI(textBox3.Text) )
            {
                MessageBox.Show("请输入正确的15位IMSI！");
            }
            else if(comboBox2.SelectedIndex ==0 && !LTEUnion.IsSTMSI(textBox3.Text))
            {
                MessageBox.Show("请输入正确的10位STMSI！");
            }
            else if (Global.GCurrentDevice == "")
            {
                MessageBox.Show("设备未连接！");
            }
            else 
            {
                MsgDefine.RecvCtrlCmd RecvCtrl = new MsgDefine.RecvCtrlCmd();
                RecvCtrl.head.head = 0xffff;
                RecvCtrl.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_CTRL_CMD;
                RecvCtrl.head.data_length = (uint)(Marshal.SizeOf(RecvCtrl) - 8);

                RecvCtrl.ctrlSysNo = Convert.ToByte(textBox1.Text);
                RecvCtrl.ctrlCmdType = (byte)(comboBox1.SelectedIndex + 1);
                RecvCtrl.ctrlCmdPara = Convert.ToByte(textBox2.Text);
                RecvCtrl.ctrlPagingIDType = (byte)(comboBox2.SelectedIndex + 1);
                if (comboBox2.SelectedIndex == 1)
                {
                    String imsi = "0" + textBox3.Text;
                    RecvCtrl.ctrlImsi = new byte[8];
                    for (int i = 0; i < 8; i++)
                        RecvCtrl.ctrlImsi[i] = Convert.ToByte(imsi.Substring(i * 2, 2), 16);
                }
                else if (comboBox2.SelectedIndex == 0)
                {
                    String imsi = "000000" + textBox3.Text;
                    RecvCtrl.ctrlImsi = new byte[8];
                    for (int i = 0; i < 8; i++)
                    RecvCtrl.ctrlImsi[i] = Convert.ToByte(imsi.Substring(i * 2, 2), 16);
                }

                int lengthRecvCtrl = Marshal.SizeOf(RecvCtrl);
                byte[] dataRecvCtrl = new byte[lengthRecvCtrl];
                IntPtr intptr = Marshal.AllocHGlobal(lengthRecvCtrl);
                Marshal.StructureToPtr(RecvCtrl, intptr, true);
                Marshal.Copy(intptr, dataRecvCtrl, 0, lengthRecvCtrl);
                Marshal.FreeHGlobal(intptr);
                CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
                dataEvtArg.deivceName = Global.GCurrentDevice;
                dataEvtArg.data = dataRecvCtrl;
                for (int i = 0; i < dataRecvCtrl.Length; i++)
                { System.Diagnostics.Debug.WriteLine(dataRecvCtrl[i]); }
                Global.tempClass.SendDataToDevice(sender, dataEvtArg);
            }
        }
        private void PagingPwrReceived(object sender, CustomDataEvtArg ce)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.SendUeInfoToMainFormHandler(PagingPwrReceived), sender, ce);
                }
                catch { }
            }
            else
            {
                MsgDefine.SendPagingPwr PagingPwr = new MsgDefine.SendPagingPwr();
                int lengthPagingPwr = Marshal.SizeOf(PagingPwr);
                if (lengthPagingPwr > ce.data.Length)
                    return;
                IntPtr intptr = Marshal.AllocHGlobal(lengthPagingPwr);
                Marshal.Copy(ce.data, 0, intptr, lengthPagingPwr);
                PagingPwr = (MsgDefine.SendPagingPwr)Marshal.PtrToStructure(intptr, typeof(MsgDefine.SendPagingPwr));
                Marshal.FreeHGlobal(intptr);

                String imsi = "";
                for (int i=0;i<8;i++)
                {
                    imsi += PagingPwr.pagingImsi[i].ToString("X");
                }
                imsi = imsi.Substring(1, 15);

                byte pwrNo = PagingPwr.uePwrNo;
                double avePwr = 0;
                for (byte i = 0; i < pwrNo; i++)
                {
                    avePwr += PagingPwr.uePwr[i];
                }
                avePwr = avePwr / pwrNo - 135;
                ListViewItem item = new ListViewItem(new String[3]);
                item.SubItems[0].Text = imsi;
                item.SubItems[1].Text = pwrNo.ToString();
                item.SubItems[2].Text = avePwr.ToString() + "dbm";
                this.listView1.Items.Add(item);
            }
        }
        private void DetectToCtrlCmd(String imsi)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.DetectToCtrlCmdHandler(DetectToCtrlCmd), imsi);
                }
                catch { }
            }
            else
            {
                textBox3.Text = imsi;
            }

        }

        private void ClearUpBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                listView1.Items.Remove(item);
            }
        }
   
    }
}
