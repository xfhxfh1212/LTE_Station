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

namespace LTE
{
    public partial class ImsiBankForm : Form
    {
        public ImsiBankForm()
        {
            InitializeComponent();
            LTEUnion.DataLoad(this.listView1, System.Windows.Forms.Application.StartupPath + "\\ImsiBankData.bin");
            textBox1.Text = "1";
            textBox1.Enabled = false;
            Global.tempClass.IBOPERStatusEvent += new Class1.IBOPERStatusHandler(IbOperStatus);
            Global.tempClass.DetectToImsiBankEvent += new Class1.DetectToImsiBankHandler(DetectToImsiBank);
        }

        private void ImsiBankForm_Load(object sender, EventArgs e)
        {
            
        }

        private void ImsiBankForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LTEUnion.DataSave(this.listView1, System.Windows.Forms.Application.StartupPath + "\\ImsiBankData.bin");
            //this.Dispose(true);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.textBox2.Text = this.listView1.SelectedItems[0].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LTEUnion.IsIMSI(textBox2.Text))
            {
                MessageBox.Show("请输入正确的15位IMSI！");
                
            }
            else if (comboBox1.SelectedIndex < 0 || comboBox1.SelectedIndex > 2)
            {
                MessageBox.Show("请选择操作命令！");
                
            }
            else if (Global.GCurrentDevice == "")
            { 
                MessageBox.Show("设备未连接！"); 
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0: 
                        {
                            foreach (ListViewItem item in this.listView1.Items)
                            {
                                if (item.SubItems[0].Text == textBox2.Text)
                                {
                                    MessageBox.Show("该IMSI已在库内！");
                                    return;
                                }
                            }
                            ListViewItem Item = new ListViewItem(new String[2]);
                            Item.SubItems[0].Text = textBox2.Text;
                            Item.SubItems[1].Text = "正在添加";
                            listView1.Items.Add(Item);
                            IBOperMsg(sender, e);
                            break;
                        }
                    case 1:
                        {
                            foreach (ListViewItem item in this.listView1.Items)
                            {
                                if (item.SubItems[0].Text == textBox2.Text)
                                {
                                    item.SubItems[1].Text = "正在删除";
                                    IBOperMsg(sender, e);
                                    return;
                                }
                            }
                            MessageBox.Show("该IMSI不在库内");
                            break;
                        }
                    case 2:
                        {
                            
                            foreach (ListViewItem item in this.listView1.Items)
                            {
                                item.SubItems[1].Text = "正在清空";
                                
                            }
                            IBOperMsg(sender, e);
                            break;
                        }
                }
                
            }
            
        }
        private void IBOperMsg(object sender, EventArgs e)
        {
            MsgDefine.RecvIbOper RecvIbOp = new MsgDefine.RecvIbOper();
            RecvIbOp.head.head = 0xffff;
            RecvIbOp.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_IB_OPER;
            RecvIbOp.head.data_length = (uint)(Marshal.SizeOf(RecvIbOp) - 8);

            RecvIbOp.ibSysNo = Convert.ToByte(textBox1.Text);
            RecvIbOp.ibCmdType = (byte)(comboBox1.SelectedIndex + 1);

            String imsi = "0" + textBox2.Text;
            RecvIbOp.ibImsi = new byte[8];
            for (int i = 0; i < 8; i++)
                RecvIbOp.ibImsi[i] = Convert.ToByte(imsi.Substring(i*2, 2), 16);

            int lengthRecvIbOp = Marshal.SizeOf(RecvIbOp);
            byte[] dataRecvRecvRf = new byte[lengthRecvIbOp];
            IntPtr intptr = Marshal.AllocHGlobal(lengthRecvIbOp);
            Marshal.StructureToPtr(RecvIbOp, intptr, true);
            Marshal.Copy(intptr, dataRecvRecvRf, 0, lengthRecvIbOp);
            Marshal.FreeHGlobal(intptr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.deivceName = Global.GCurrentDevice;
            dataEvtArg.data = dataRecvRecvRf;
            for (int i = 0; i < dataRecvRecvRf.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataRecvRecvRf[i]); }
            Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }
        private void IbOperStatus(int i)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.IBOPERStatusHandler(IbOperStatus), i);
                }
                catch { }
            }
            else 
            {
                if (i == 0)
                {
                    foreach (ListViewItem item in this.listView1.Items)
                    {
                        switch (item.SubItems[1].Text)
                        {
                            case "正在添加":
                                item.SubItems[1].Text = "已添加";
                                break;
                            case "正在删除":
                                this.listView1.Items.Remove(item);
                                break;
                            case "正在清空":
                                this.listView1.Items.Remove(item);
                                break;
                            case "已添加":
                                break;
                        }
                    }
                }
                else if (i == 1)
                {
                    foreach (ListViewItem item in this.listView1.Items)
                    {
                        switch (item.SubItems[1].Text)
                        {
                            case "正在添加":
                                
                                break;
                            case "正在删除":
                                item.SubItems[1].Text = "已添加";
                                break;
                            case "正在清空":
                                item.SubItems[1].Text = "已添加";
                                break;
                            case "已添加":
                                break;
                        }
                    }
                }
            }
        }
        private void DetectToImsiBank(String imsi,int i)
        {
            if (this.InvokeRequired)
            {
                try

                {
                    this.BeginInvoke(new Class1.DetectToImsiBankHandler(DetectToImsiBank),imsi,i);
                }
                catch { }
            }
            else
            {
                this.textBox2.Text = imsi;
                this.comboBox1.SelectedIndex = i; 
            }
        }
    }
}
