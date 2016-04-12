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
    public partial class NewTacForm : Form
    {
        public NewTacForm()
        {
            InitializeComponent();
            textBox1.Text = "1";
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LTEUnion.IsDigitalString(textBox2.Text))
            {
                MessageBox.Show("请输入正确的定时器间隔！");
            }
            else if (Convert.ToUInt32(textBox2.Text) < 0 || Convert.ToUInt32(textBox2.Text) > 500)
            {
                MessageBox.Show("定时器间隔应为0~500");
            }
            else if (Convert.ToUInt32(textBox2.Text) % 10 != 0)
            {
                MessageBox.Show("定时器间隔应为10的整数倍！");
            }
            else if (Global.GCurrentDevice == "")
            {
                MessageBox.Show("设备未连接！");
            }
            else
            {
                MsgDefine.RecvNewTac NewTac = new MsgDefine.RecvNewTac();
                NewTac.head.head = 0xffff;
                NewTac.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_NEW_TAC;
                NewTac.head.data_length = (uint)(Marshal.SizeOf(NewTac) - 8);

                NewTac.newSysNo = Convert.ToUInt16(textBox1.Text);
                NewTac.newTimer = Convert.ToUInt16(textBox2.Text);

                int lengthNewTac = Marshal.SizeOf(NewTac);
                byte[] dataRecvNewTac = new byte[lengthNewTac];
                IntPtr intptr = Marshal.AllocHGlobal(lengthNewTac);
                Marshal.StructureToPtr(NewTac, intptr, true);
                Marshal.Copy(intptr, dataRecvNewTac, 0, lengthNewTac);
                Marshal.FreeHGlobal(intptr);
                CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
                dataEvtArg.deivceName = Global.GCurrentDevice;
                dataEvtArg.data = dataRecvNewTac;
                for (int i = 0; i < dataRecvNewTac.Length; i++)
                { System.Diagnostics.Debug.WriteLine(dataRecvNewTac[i]); }
                Global.tempClass.SendDataToDevice(sender, dataEvtArg);
            }
        }
    }
}
