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
    public partial class DlSniffForm : Form
    {
        public DlSniffForm()
        {
            InitializeComponent();
            textBox1.Text = "1";
            textBox1.Enabled = false;

            textBox3.Text = "100";
            comboBox1.SelectedIndex = 0;
            checkBox1.Checked = true;
            Global.tempClass.SendToDlSniffFormEvent += new Class1.SendToDlSniffFormHandler(DlSniffReceived);
            
            LTEUnion.DataLoad(this.listView1, System.Windows.Forms.Application.StartupPath + "\\DlSniffData.bin");
        }
        private void DlSniffForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LTEUnion.DataSave(this.listView1, System.Windows.Forms.Application.StartupPath + "\\DlSniffData.bin");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!LTEUnion.IsDigitalString(textBox2.Text))
            {
                MessageBox.Show("请输入正确的EARFCN！");
            }
            else 
            {
                MsgDefine.RecvDlRxPara DlSniff = new MsgDefine.RecvDlRxPara();
                DlSniff.head.head = 0xffff;
                DlSniff.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_DLRX_PARA;
                DlSniff.head.data_length = (uint)(Marshal.SizeOf(DlSniff) - 8);

                DlSniff.dlSysNo = Convert.ToUInt16(textBox1.Text);
                DlSniff.earfcn = Convert.ToUInt16(textBox2.Text);
                DlSniff.dlFn = Convert.ToUInt32(textBox3.Text);
                DlSniff.dlRxMod = (byte)(comboBox1.SelectedIndex + 1);
                if (checkBox1.Checked)
                    DlSniff.dlEnable = 1;
                else
                    DlSniff.dlEnable = 0;

                int lengthDlSniff = Marshal.SizeOf(DlSniff);
                byte[] dataDlSniff = new byte[lengthDlSniff];
                IntPtr intptr = Marshal.AllocHGlobal(lengthDlSniff);
                Marshal.StructureToPtr(DlSniff, intptr, true);
                Marshal.Copy(intptr, dataDlSniff, 0, lengthDlSniff);
                Marshal.FreeHGlobal(intptr);
                CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
                dataEvtArg.deivceName = Global.GCurrentDevice;
                dataEvtArg.data = dataDlSniff;
                for (int i = 0; i < dataDlSniff.Length; i++)
                { System.Diagnostics.Debug.WriteLine(dataDlSniff[i]); }
                Global.tempClass.SendDataToDevice(sender, dataEvtArg);
            }
        }
        private void DlSniffReceived(object send, CustomDataEvtArg ce)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.SendToDlSniffFormHandler(DlSniffReceived), send, ce);
                }
                catch { }
            }
            else
            {
                MsgDefine.SendDlSniff DlSniff = new MsgDefine.SendDlSniff();
                int lengthDlSinff = Marshal.SizeOf(DlSniff);
                IntPtr intptr = Marshal.AllocHGlobal(lengthDlSinff);
                Marshal.Copy(ce.data, 0, intptr, lengthDlSinff);
                DlSniff = (MsgDefine.SendDlSniff)Marshal.PtrToStructure(intptr,typeof(MsgDefine.SendDlSniff));
                Marshal.FreeHGlobal(intptr);

                byte numberCell = DlSniff.numberCell;
                for (int i = 0; i < numberCell; i++)
                {
                    byte mcc = DlSniff.cellInfomation[i].Mcc;
                    byte mnc = DlSniff.cellInfomation[i].Mnc;
                    ushort tac = DlSniff.cellInfomation[i].Tac;
                    ushort pciNo = DlSniff.cellInfomation[i].PciNo;
                    String ecgi = "";
                    for (int j = 0; j < 7; j++)
                    {
                        ecgi += DlSniff.cellInfomation[i].CellId[j].ToString("X");
                    }
                    ushort earfcn = DlSniff.cellInfomation[i].Earfcn;
                    byte cpType = DlSniff.cellInfomation[i].CP_Type;
                    ushort crsRp = DlSniff.cellInfomation[i].Crs_RP;
                    ushort crsRq = DlSniff.cellInfomation[i].Crs_RQ;

                    ListViewItem item = new ListViewItem(new String[9]);
                    item.SubItems[0].Text = mcc.ToString();
                    item.SubItems[1].Text = mnc.ToString();
                    item.SubItems[2].Text = pciNo.ToString();
                        item.SubItems[3].Text = tac.ToString();
                        item.SubItems[4].Text = ecgi;
                        item.SubItems[5].Text = earfcn.ToString();
                    if(cpType == 0)
                        item.SubItems[6].Text = "常规";
                    else 
                        item.SubItems[6].Text = "扩展";
                        item.SubItems[7].Text = "-" + (crsRp*0.125).ToString() + "dBm";
                        item.SubItems[8].Text = (crsRq * 0.0625).ToString() + "dB";
                        listView1.Items.Add(item);
                }
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
