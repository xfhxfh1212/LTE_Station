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
    public partial class NcParaForm : Form
    {
        public NcParaForm()
        {
            InitializeComponent();
            textBox0.Text = "1";
            textBox0.Enabled = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            Global.tempClass.NcSniffStatusEvent += new Class1.NcSniffStatusHandler(NcSniffStatus);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] pci = new String[6];
            pci[0] = textBox1.Text;
            pci[1] = textBox2.Text;
            pci[2] = textBox3.Text;
            pci[3] = textBox4.Text;
            pci[4] = textBox5.Text;
            pci[5] = textBox6.Text;
            
            if (comboBox1.SelectedIndex == 0)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[1].Text == comboBox2.Text && item.SubItems[0].Text == "待添加")
                    {
                        item.SubItems[2].Text = pci[0];
                        item.SubItems[3].Text = pci[1];
                        item.SubItems[4].Text = pci[2];
                        item.SubItems[5].Text = pci[3];
                        item.SubItems[6].Text = pci[4];
                        item.SubItems[7].Text = pci[5];
                        return;
                    }
                }
                ListViewItem Item = new ListViewItem(new String[8]);
                Item.SubItems[0].Text = "待添加";
                Item.SubItems[1].Text = comboBox2.Text;
                Item.SubItems[2].Text = pci[0];
                Item.SubItems[3].Text = pci[1];
                Item.SubItems[4].Text = pci[2];
                Item.SubItems[5].Text = pci[3];
                Item.SubItems[6].Text = pci[4];
                Item.SubItems[7].Text = pci[5];
                listView1.Items.Add(Item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MsgDefine.RecvNcPara NcPara = new MsgDefine.RecvNcPara();
            NcPara.head.head = 0xffff;
            NcPara.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_NC_PARA;
            NcPara.head.data_length = (uint)(Marshal.SizeOf(NcPara) - 8);

            NcPara.ncSysNo = Convert.ToUInt16(textBox0.Text);
            NcPara.ncCmdType = (byte)(comboBox1.SelectedIndex + 1);
            NcPara.intraFreqCell.PCI = new byte[6];
            NcPara.interFreqCellList.interFreqList = new MsgDefine.interFreqList[6];

            byte NumInterFreq = 0;
            foreach(ListViewItem item in listView1.Items)
            {
                if (item.SubItems[0].Text == "待添加")
                {
                    switch (item.SubItems[1].Text)
                    {
                        case "同频邻区表":
                        {

                            for (int i = 0; i < 6; i++)
                            {
                                if (item.SubItems[i + 2].Text == "")
                                {
                                    NcPara.intraFreqCell.NumIntraFreqCell = (byte)i;
                                    break;
                                }
                                else
                                {
                                    NcPara.intraFreqCell.PCI[i] = Convert.ToByte(item.SubItems[i + 2].Text);
                                }
                            }
                            break;
                        }
                        case "异频邻区表1":
                        case "异频邻区表2":
                        case "异频邻区表3":
                        case "异频邻区表4":
                        case "异频邻区表5":
                        case "异频邻区表6":
                        {
                            NcPara.interFreqCellList.interFreqList[NumInterFreq].PCI = new byte[6];
                            for (int i = 0; i < 6; i++)
                            {
                                if (item.SubItems[i + 2].Text == "")
                                {
                                    NcPara.interFreqCellList.interFreqList[NumInterFreq].NumInterFreqCell = (byte)i;
                                    break;
                                }
                                else
                                {
                                    NcPara.interFreqCellList.interFreqList[NumInterFreq].PCI[i] = Convert.ToByte(item.SubItems[i + 2].Text);
                                }
                            }
                            NumInterFreq++;
                            break;
                        }
                    }
                }
            }
            NcPara.interFreqCellList.NumInterFreq = NumInterFreq;


            int lengthNcPara = Marshal.SizeOf(NcPara);
            byte[] dataNcPara = new byte[lengthNcPara];
            IntPtr intptr = Marshal.AllocHGlobal(lengthNcPara);
            Marshal.StructureToPtr(NcPara, intptr, true);
            Marshal.Copy(intptr, dataNcPara, 0, lengthNcPara);
            Marshal.FreeHGlobal(intptr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.deivceName = Global.GCurrentDevice;
            dataEvtArg.data = dataNcPara;
            for (int i = 0; i < dataNcPara.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataNcPara[i]); }
            Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }
        private void NcSniffStatus(int i)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.NcSniffStatusHandler(NcSniffStatus), i);
                }
                catch { }
            }
            else
            {
                if (i == 0)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[0].Text == "待添加")
                        {
                            item.SubItems[0].Text = "已添加";
                        }

                    }
                }
                else if (i == 1)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.SubItems[0].Text == "待添加")
                        {
                            MessageBox.Show("添加失败，请重新添加！");
                        }

                    }
                }
            }
        }
    }
}
