using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeviceMangerModule;
using WeifenLuo.WinFormsUI.Docking;
using Plugin;
using COM.ZCTT.AGI.Common;
using AGIInterface;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
namespace LTE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            ParaLoad();
            LTEUnion.DataLoad(this.listView1, System.Windows.Forms.Application.StartupPath + "\\DetectData.bin");
            COM.ZCTT.AGI.Common.Global.tempClass.HeartBeatCountEvent += new Class1.HeartBeatCountHandler(HeartBeatCount);
            COM.ZCTT.AGI.Common.Global.tempClass.SendUeInfoToMainFormEvent += new Class1.SendUeInfoToMainFormHandler(UeInfoReceived);
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LTEUnion.DataSave(this.listView1, System.Windows.Forms.Application.StartupPath + "\\DetectData.bin");
        }
        private void ParaLoad()
        {
            textBox1.Text = "1";
            textBox1.Enabled = false;
            BandTextBox.Text = "40";
            dlEarfcnBox.Text = "38950";
            dlFreqBox.Text = "2330";
            ulEarfcnBox.Text = "38950";
            ulFreqBox.Text = "2330";
            textBox4.Text = "1";
            textBox25.Text = "7";
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 5;
            comboBox10.SelectedIndex = 5;
            comboBox11.SelectedIndex = 1;
            textBox2.Text = "1";
            textBox3.Text = "1";
            checkBox1.Checked = true;
            checkBox2.Checked = true;

            textBox6.Text = "1";
            textBox6.Enabled = false;
            checkBox3.Checked = true;
            checkBox4.Checked = true;

            textBox7.Text = "1";
            textBox7.Enabled = false;
            textBox8.Text = "460";
            textBox8.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textBox9.Text = "336";
            textBox10.Text = "9578";
            textBox11.Text = "75";
            textBox12.Text = "0";

            textBox13.Text = "1";
            textBox14.Text = "5";
            checkBox6.Checked = true;
            comboBox2.SelectedIndex = 6;
            comboBox3.SelectedIndex = 0;

            comboBox6.SelectedIndex = 1;
            textBox15.Text = "63";
            textBox16.Text = "50";
            textBox17.Text = "15";

            comboBox7.SelectedIndex = 0;
            textBox18.Text = "1";
            textBox19.Text = "15";

            textBox23.Text = "1";
            textBox24.Text = "5";
            checkBox7.Checked = true;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        }

        private void BandTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            String dlEarfcn = dlEarfcnBox.Text.Trim();
            String Band = BandTextBox.Text.Trim();
            if (LTEUnion.IsDigitalString(dlEarfcn) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double dlearfcn = Convert.ToDouble(dlEarfcn);
                double dlfreq = COM.ZCTT.AGI.Common.Global.EARFCNToFreq(band, dlearfcn);
                dlFreqBox.Text = dlfreq.ToString();
            }
            String ulEarfcn = ulEarfcnBox.Text.Trim();
            if (LTEUnion.IsDigitalString(ulEarfcn) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double ulearfcn = Convert.ToDouble(ulEarfcn);
                double ulfreq = COM.ZCTT.AGI.Common.Global.EARFCNToFreq(band, ulearfcn);
                ulFreqBox.Text = ulfreq.ToString();
            }
        }

        private void dlFreqBox_KeyUp(object sender, KeyEventArgs e)
        {
            String Freq = dlFreqBox.Text.Trim();
            String Band = BandTextBox.Text.Trim();
            if (LTEUnion.IsDigitalString(Freq) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double freq = Convert.ToDouble(Freq);
                int earfcn = COM.ZCTT.AGI.Common.Global.FreqToEARFCN(band, freq);
                dlEarfcnBox.Text = earfcn.ToString();
            }
        }

        private void dlEarfcnBox_KeyUp(object sender, KeyEventArgs e)
        {
            String Earfcn = dlEarfcnBox.Text.Trim();
            String Band = BandTextBox.Text.Trim();
            if (LTEUnion.IsDigitalString(Earfcn) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double earfcn = Convert.ToDouble(Earfcn);
                double freq = COM.ZCTT.AGI.Common.Global.EARFCNToFreq(band, earfcn);
                dlFreqBox.Text = freq.ToString();
            }
        }
        private void ulFreqBox_KeyUp(object sender, KeyEventArgs e)
        {
            String Freq = ulFreqBox.Text.Trim();
            String Band = BandTextBox.Text.Trim();
            if (LTEUnion.IsDigitalString(Freq) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double freq = Convert.ToDouble(Freq);
                int earfcn = COM.ZCTT.AGI.Common.Global.FreqToEARFCN(band, freq);
                ulEarfcnBox.Text = earfcn.ToString();
            }
        }

        private void ulEarfcnBox_KeyUp(object sender, KeyEventArgs e)
        {
            String Earfcn = ulEarfcnBox.Text.Trim();
            String Band = BandTextBox.Text.Trim();
            if (LTEUnion.IsDigitalString(Earfcn) && LTEUnion.IsDigitalString(Band))
            {
                int band = Convert.ToUInt16(Band);
                double earfcn = Convert.ToDouble(Earfcn);
                double freq = COM.ZCTT.AGI.Common.Global.EARFCNToFreq(band, earfcn);
                ulFreqBox.Text = freq.ToString();
            }
        }
        #region 消息发送
        private void RfParaBtn_Click(object sender, EventArgs e)
        {
            MsgDefine.RecvRfPara RecvRf = new MsgDefine.RecvRfPara();
            RecvRf.head.head = 0xffff;
            RecvRf.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_RF_PARA;
            RecvRf.head.data_length = (uint)(Marshal.SizeOf(RecvRf) - 8);

            RecvRf.rfSysNo = Convert.ToUInt16(textBox1.Text);
            if(checkBox1.Checked)
                RecvRf.rfEnable = 1;
            else
                RecvRf.rfEnable = 0;
            if (checkBox2.Checked)
                RecvRf.fastConfigEarfcn = 0;
            else
                RecvRf.fastConfigEarfcn = 1;
            RecvRf.earfcn_band = Convert.ToUInt16(BandTextBox.Text);
            RecvRf.DlEarfcn = Convert.ToUInt16(dlEarfcnBox.Text);
            RecvRf.UlEarfcn = Convert.ToUInt16(ulEarfcnBox.Text);
            RecvRf.SubframeAssinment = Convert.ToByte(textBox4.Text);
            RecvRf.specialSubframePatterns = Convert.ToByte(textBox25.Text);
            RecvRf.FrameStrucureType = (byte)comboBox8.SelectedIndex;

            switch (comboBox9.SelectedIndex)
            {
                case 0: RecvRf.DlBandWidth = 6; break;
                case 1: RecvRf.DlBandWidth = 15; break;
                case 2: RecvRf.DlBandWidth = 25; break;
                case 3: RecvRf.DlBandWidth = 50; break;
                case 4: RecvRf.DlBandWidth = 75; break;
                case 5: RecvRf.DlBandWidth = 100; break;
            }
            switch (comboBox10.SelectedIndex)
            {
                case 0: RecvRf.UlBandWidth = 6; break;
                case 1: RecvRf.UlBandWidth = 15; break;
                case 2: RecvRf.UlBandWidth = 25; break;
                case 3: RecvRf.UlBandWidth = 50; break;
                case 4: RecvRf.UlBandWidth = 75; break;
                case 5: RecvRf.UlBandWidth = 100; break;
            }
            RecvRf.RFchoice = (byte)comboBox11.SelectedIndex;
            RecvRf.TX1PowerAttenuation = Convert.ToUInt16(textBox2.Text);
            RecvRf.TX2PowerAttenuation = Convert.ToUInt16(textBox3.Text);

            int lengthRecvRf = Marshal.SizeOf(RecvRf);
            byte[] dataRecvRecvRf = new byte[lengthRecvRf];
            IntPtr intptr = Marshal.AllocHGlobal(lengthRecvRf);
            Marshal.StructureToPtr(RecvRf, intptr, true);
            Marshal.Copy(intptr, dataRecvRecvRf, 0, lengthRecvRf);
            Marshal.FreeHGlobal(intptr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.deivceName = "设备0011";//COM.ZCTT.AGI.Common.Global.GCurrentDevice;
            dataEvtArg.data = dataRecvRecvRf;
            for (int i = 0; i < dataRecvRecvRf.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataRecvRecvRf[i]); }
            COM.ZCTT.AGI.Common.Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }

        private void SysOptionBtn_Click(object sender, EventArgs e)
        {
            MsgDefine.RecvSysOption RecvSysOpt = new MsgDefine.RecvSysOption();
            RecvSysOpt.head.head = 0xffff;
            RecvSysOpt.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_SYS_OPTION;
            RecvSysOpt.head.data_length = (uint)(Marshal.SizeOf(RecvSysOpt) - 8);

            RecvSysOpt.opSysNo = Convert.ToUInt16(textBox6.Text);
            if (checkBox3.Checked)
                RecvSysOpt.opLuImei = 1;
            else
                RecvSysOpt.opLuImei = 0;
            if (checkBox4.Checked)
                RecvSysOpt.opLuSTMSI = 1;
            else
                RecvSysOpt.opLuSTMSI = 0;

            int lengthRecvSysOpt = Marshal.SizeOf(RecvSysOpt);
            byte[] dataRecvSysOpt = new byte[lengthRecvSysOpt];
            IntPtr intptr = Marshal.AllocHGlobal(lengthRecvSysOpt);
            Marshal.StructureToPtr(RecvSysOpt, intptr, true);
            Marshal.Copy(intptr, dataRecvSysOpt, 0, lengthRecvSysOpt);
            Marshal.FreeHGlobal(intptr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.deivceName = COM.ZCTT.AGI.Common.Global.GCurrentDevice;
            dataEvtArg.data = dataRecvSysOpt;
            for (int i = 0; i < dataRecvSysOpt.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataRecvSysOpt[i]); }
            COM.ZCTT.AGI.Common.Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }

        private void SysParaBtn_Click(object sender, EventArgs e)
        {
            MsgDefine.RecvSysParaBitStream RecvSysPara = new MsgDefine.RecvSysParaBitStream();
            RecvSysPara.head.head = 0xffff;
            RecvSysPara.head.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_SYS_PARA;
            RecvSysPara.head.data_length = (uint)(Marshal.SizeOf(RecvSysPara) - 8);

            RecvSysPara.paraSysNo = Convert.ToByte(textBox7.Text);           

            if (textBox8.Text.Length == 3)
            {
                byte[] mcc = new byte[3];
                mcc[0] = Convert.ToByte(textBox8.Text.Substring(0, 1));
                mcc[1] = Convert.ToByte(textBox8.Text.Substring(1, 1));
                mcc[2] = Convert.ToByte(textBox8.Text.Substring(2, 1));
                RecvSysPara.paraMcc = mcc;
            }            
            else
            {
                MessageBox.Show("请填入正确的MCC");
                return;
            }
            switch(comboBox1.SelectedIndex)
            {
                case 0: RecvSysPara.paraMnc = new byte[2] { 0, 0 }; break;
                case 1: RecvSysPara.paraMnc = new byte[2] { 0, 2 }; break;
                case 2: RecvSysPara.paraMnc = new byte[2] { 0, 7 }; break;
                case 3: RecvSysPara.paraMnc = new byte[2] { 0, 1 }; break;
                case 4: RecvSysPara.paraMnc = new byte[2] { 0, 6 }; break;
                case 5: RecvSysPara.paraMnc = new byte[2] { 0, 3 }; break;
                case 6: RecvSysPara.paraMnc = new byte[2] { 0, 5 }; break;
                case 7: RecvSysPara.paraMnc = new byte[2] { 1, 1 }; break;
            }
            RecvSysPara.paraPciNo = Convert.ToUInt16(textBox9.Text);
            RecvSysPara.paraTac = Convert.ToUInt16(textBox10.Text);
            RecvSysPara.CellId = Convert.ToUInt32(textBox11.Text);
            RecvSysPara.TaiPeri = Convert.ToUInt32(textBox12.Text);

            RecvSysPara.paraIDReqNumOUT = Convert.ToByte(textBox23.Text);
            RecvSysPara.paraIDReqNumIN = Convert.ToByte(textBox13.Text);

            RecvSysPara.UEcapaEnquiryNumOUT = Convert.ToByte(textBox24.Text);
            RecvSysPara.UEcapaEnquiryNumIN = Convert.ToByte(textBox14.Text);
            if (checkBox7.Checked)
                RecvSysPara.BoolMeasureOUT = 1;
            else
                RecvSysPara.BoolMeasureOUT = 0;
            if (checkBox6.Checked)
                RecvSysPara.BoolMeasureIN = 1;
            else
                RecvSysPara.BoolMeasureIN = 0;

            switch(comboBox4.SelectedIndex)
            {
                case 0: RecvSysPara.paraTAURejCauOUT = 2; break;
                case 1: RecvSysPara.paraTAURejCauOUT = 3; break;
                case 2: RecvSysPara.paraTAURejCauOUT = 5; break;
                case 3: RecvSysPara.paraTAURejCauOUT = 6; break;
                case 4: RecvSysPara.paraTAURejCauOUT = 11; break;
                case 5: RecvSysPara.paraTAURejCauOUT = 12; break;
                case 6: RecvSysPara.paraTAURejCauOUT = 13; break;
                case 7: RecvSysPara.paraTAURejCauOUT = 15; break;
                case 8: RecvSysPara.paraTAURejCauOUT = 17; break;
                case 9: RecvSysPara.paraTAURejCauOUT = 22; break;
            }
            switch (comboBox2.SelectedIndex)
            {
                case 0: RecvSysPara.paraTAURejCauIN = 2; break;
                case 1: RecvSysPara.paraTAURejCauIN = 3; break;
                case 2: RecvSysPara.paraTAURejCauIN = 5; break;
                case 3: RecvSysPara.paraTAURejCauIN = 6; break;
                case 4: RecvSysPara.paraTAURejCauIN = 11; break;
                case 5: RecvSysPara.paraTAURejCauIN = 12; break;
                case 6: RecvSysPara.paraTAURejCauIN = 13; break;
                case 7: RecvSysPara.paraTAURejCauIN = 15; break;
                case 8: RecvSysPara.paraTAURejCauIN = 17; break;
                case 9: RecvSysPara.paraTAURejCauIN = 22; break;
            }
            switch (comboBox5.SelectedIndex)
            {
                case 0: RecvSysPara.paraATTRejCauOUT = 2; break;
                case 1: RecvSysPara.paraATTRejCauOUT = 3; break;
                case 2: RecvSysPara.paraATTRejCauOUT = 5; break;
                case 3: RecvSysPara.paraATTRejCauOUT = 6; break;
                case 4: RecvSysPara.paraATTRejCauOUT = 11; break;
                case 5: RecvSysPara.paraATTRejCauOUT = 12; break;
                case 6: RecvSysPara.paraATTRejCauOUT = 13; break;
                case 7: RecvSysPara.paraATTRejCauOUT = 15; break;
                case 8: RecvSysPara.paraATTRejCauOUT = 17; break;
                case 9: RecvSysPara.paraATTRejCauOUT = 22; break;
            }
            switch (comboBox3.SelectedIndex)
            {
                case 0: RecvSysPara.paraATTRejCauIN = 2; break;
                case 1: RecvSysPara.paraATTRejCauIN = 3; break;
                case 2: RecvSysPara.paraATTRejCauIN = 5; break;
                case 3: RecvSysPara.paraATTRejCauIN = 6; break;
                case 4: RecvSysPara.paraATTRejCauIN = 11; break;
                case 5: RecvSysPara.paraATTRejCauIN = 12; break;
                case 6: RecvSysPara.paraATTRejCauIN = 13; break;
                case 7: RecvSysPara.paraATTRejCauIN = 15; break;
                case 8: RecvSysPara.paraATTRejCauIN = 17; break;
                case 9: RecvSysPara.paraATTRejCauIN = 22; break;
            }
            RecvSysPara.redirectedRATOUT = (byte)(comboBox7.SelectedIndex );
            RecvSysPara.redirectedCarrierOUT = Convert.ToUInt16(textBox18.Text);
            RecvSysPara.PriorityOUT = Convert.ToByte(textBox19.Text);
            RecvSysPara.redirectedRATIN = (byte)(comboBox6.SelectedIndex);
            RecvSysPara.redirectedCarrierIN = Convert.ToUInt16(textBox15.Text);
            RecvSysPara.PriorityIN = Convert.ToByte(textBox16.Text);
            RecvSysPara.redirectedCellIN = Convert.ToByte(textBox17.Text);

            int lengthRecvSysPara = Marshal.SizeOf(RecvSysPara);
            byte[] dataRecvSysPara = new byte[lengthRecvSysPara];
            IntPtr intPtr = Marshal.AllocHGlobal(lengthRecvSysPara);
            Marshal.StructureToPtr(RecvSysPara,intPtr,true);
            Marshal.Copy(intPtr, dataRecvSysPara, 0, lengthRecvSysPara);
            Marshal.FreeHGlobal(intPtr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.data = dataRecvSysPara;
            dataEvtArg.deivceName = COM.ZCTT.AGI.Common.Global.GCurrentDevice;
            for (int i = 0; i < dataRecvSysPara.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataRecvSysPara[i]); }
            COM.ZCTT.AGI.Common.Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }
        #endregion
        #region 消息接收
        private void HeartBeatCount(int count)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.HeartBeatCountHandler(HeartBeatCount), count);
                }
                catch { }

            }
            else
            {
                this.label2.Text = count.ToString();
            }
        }
        private void UeInfoReceived(object sender, CustomDataEvtArg ce)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new Class1.SendUeInfoToMainFormHandler(UeInfoReceived), sender, ce);
                }
                catch { }
            }
            else 
            {
                MsgDefine.SendUeInfo UeInfo = new MsgDefine.SendUeInfo();
                int lengthUeInfo = Marshal.SizeOf(UeInfo);
                if (lengthUeInfo > ce.data.Length)
                    return;
                IntPtr intptr = Marshal.AllocHGlobal(lengthUeInfo);
                Marshal.Copy(ce.data,0,intptr,lengthUeInfo);
                UeInfo = (MsgDefine.SendUeInfo)Marshal.PtrToStructure(intptr,typeof(MsgDefine.SendUeInfo));
                Marshal.FreeHGlobal(intptr);

                String imsi = "";
                for (int i = 0; i < 16; i++)
                {
                    imsi += UeInfo.ueImsi[i];
                }
                String imei = "";
                for (int i = 0; i < 16; i++)
                {
                    imei += UeInfo.ueImei[i];
                }
                String sTmsi = "";
                for (int i = 0; i < 12; i++)
                {
                    sTmsi += UeInfo.ueSTmsi[i];
                }
                String taType = "";
                switch (UeInfo.ueTaType)
                {
                    case 1 :taType = "EpsAtt";break;
                    case 2 :taType = "EPSImsiAtt";break;
                    case 6 :taType = "EpsEmerAtt";break;
                    case 16 :taType = "TAu";break;
                    case 17 :taType = "TaLau";break;
                    case 18 :taType = "TaLauImsi";break;
                    case 19 :taType = "PeriU";break;
                    case 24 :taType = "TaUBr";break;
                    case 25 :taType = "TaLaUBr";break;
                    case 26 :taType = "TaLaImsiBr";break;
                    case 27: taType = "PeriUBr"; break;
                }
                String ueType = "";
                switch (UeInfo.ueType)
                {
                    case 0 :ueType = "LTE";break;
                    case 1 :ueType = "LTE/GSM";break;
                    case 2 :ueType = "LTE/CDMA2000";break;
                    case 3 :ueType = "LTE/CDMA2000/GSM";break;
                    case 4 :ueType = "LTE/TD-S";break;
                    case 5 :ueType = "LTE/TD-S/GSM";break;
                    case 6 :ueType = "LTE/TD-S/CDMA2000";break;
                    case 7 :ueType = "LTE/TD-S/CDMA2000/GSM";break;
                    case 8 :ueType = "LTE/WCDMA";break;
                    case 9 :ueType = "LTE/WCDMA/GSM";break;
                    case 10 :ueType = "LTE/WCDMA/CDMA2000";break;
                    case 11 :ueType = "LTE/WCDMA/CDMA2000/GSM";break;
                    case 12 :ueType = "LTE/WCDMA/TD-S";break;
                    case 13 :ueType = "LTE/WCDMA/TD-S/GSM";break;
                    case 14 :ueType = "LTE/WCDMA/TD-S/CDMA2000";break;
                    case 15: ueType = "LTE/WCDMA/TD-S/CDMA2000/GSM"; break;
                }
                String taTime = DateTime.Now.ToString();
                byte pwrNo = UeInfo.uePwrNo;
                double avePwr = 0;
                for (byte i = 0; i < pwrNo; i++)
                {
                    avePwr += UeInfo.uePwr[i];
                }
                avePwr = avePwr / pwrNo - 135;
                
                //foreach(ListViewItem item in this.listView1.Items)
                //{
                //    if (imsi == item.SubItems[0].Text)
                //    {
                //        item.SubItems[1].Text = imei;
                //        item.SubItems[2].Text = sTmsi;
                //        item.SubItems[3].Text = taType;
                //        item.SubItems[4].Text = ueType;
                //        item.SubItems[5].Text = taTime;
                //        item.SubItems[6].Text = (Convert.ToInt32(item.SubItems[6].Text) + 1).ToString();
                //        this.listView1.Items.Add(item);
                //        return;

                //    }
                ListViewItem item = new ListViewItem(new String[8]);
                    item.SubItems[0].Text = imsi;
                    item.SubItems[1].Text = imei;
                    item.SubItems[2].Text = sTmsi;
                    item.SubItems[3].Text = taType;
                    item.SubItems[4].Text = ueType;
                    item.SubItems[5].Text = taTime;
                    item.SubItems[6].Text = pwrNo.ToString();
                    item.SubItems[7].Text = avePwr.ToString() + "dbm";
                    
                    this.listView1.Items.Add(item);
                //}

            }
        }
        #endregion 
        private void DetectClearBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                listView1.Items.Remove(item);
            }
        }



        public DeviceMangerModule.DeviceManger device;
        private void DeviceBtn_Click(object sender, EventArgs e)
        {
            if (device == null)
                device = new DeviceMangerModule.DeviceManger();
            device.Load();
        }
        public ImsiBankForm imsiBankForm;
        private void IbOperBtn_Click(object sender, EventArgs e)
        {
            if (imsiBankForm == null)
                imsiBankForm = new ImsiBankForm();
            imsiBankForm.ShowDialog();
        }

        private void 寻呼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                if (ctrlCmdForm == null)
                    ctrlCmdForm = new CtrlCmdForm();
                COM.ZCTT.AGI.Common.Global.tempClass.DetectToCtrlCmd(this.listView1.SelectedItems[0].SubItems[0].Text);
                Device.CtrlCmdFormIsClosed = false;
                ctrlCmdForm.ShowDialog();
            }
            else
                return;
        }

        private void 添加到库内ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                if (imsiBankForm == null)
                    imsiBankForm = new ImsiBankForm();
                COM.ZCTT.AGI.Common.Global.tempClass.DetectToImsiBank(this.listView1.SelectedItems[0].SubItems[0].Text, 0);
                imsiBankForm.ShowDialog();
            }
            else
                return;
        }

        private void 删除库内ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                if (imsiBankForm == null)
                    imsiBankForm = new ImsiBankForm();
                COM.ZCTT.AGI.Common.Global.tempClass.DetectToImsiBank(this.listView1.SelectedItems[0].SubItems[0].Text, 1);
                imsiBankForm.ShowDialog();
            }
            else
                return;
        }
        public CtrlCmdForm ctrlCmdForm;
        
        private void CtrlCmdBtn_Click(object sender, EventArgs e)
        {
            if (ctrlCmdForm == null)
                ctrlCmdForm = new CtrlCmdForm();
            Device.CtrlCmdFormIsClosed = false;
            ctrlCmdForm.ShowDialog();
        }

        public NewTacForm newTacForm;
        private void NewTacBtn_Click(object sender, EventArgs e)
        {
            if (newTacForm == null)
                newTacForm = new NewTacForm();
            newTacForm.ShowDialog();
        }

        private void QueryVerBtn_Click(object sender, EventArgs e)
        {
            MsgDefine.Head QueryVer = new MsgDefine.Head();
            QueryVer.head = 0xffff;
            QueryVer.pkt_type = (ushort)MsgDefine.RecvPktType.RECV_QUERY_VER;
            QueryVer.data_length = (uint)(Marshal.SizeOf(QueryVer) - 8);

            int lengthRecvRf = Marshal.SizeOf(QueryVer);
            byte[] dataQueryVer = new byte[lengthRecvRf];
            IntPtr intptr = Marshal.AllocHGlobal(lengthRecvRf);
            Marshal.StructureToPtr(QueryVer, intptr, true);
            Marshal.Copy(intptr, dataQueryVer, 0, lengthRecvRf);
            Marshal.FreeHGlobal(intptr);
            CustomDataEvtArg dataEvtArg = new CustomDataEvtArg();
            dataEvtArg.deivceName = COM.ZCTT.AGI.Common.Global.GCurrentDevice;
            dataEvtArg.data = dataQueryVer;
            for (int i = 0; i < dataQueryVer.Length; i++)
            { System.Diagnostics.Debug.WriteLine(dataQueryVer[i]); }
            COM.ZCTT.AGI.Common.Global.tempClass.SendDataToDevice(sender, dataEvtArg);
        }

        public DlSniffForm dlSniffForm;
        private void DlSniffBtn_Click(object sender, EventArgs e)
        {
            if (dlSniffForm == null)
                dlSniffForm = new DlSniffForm();
            dlSniffForm.ShowDialog();
        }
        public NcParaForm ncParaForm;
        private void NcParaBtn_Click(object sender, EventArgs e)
        {
            if (ncParaForm == null)
                ncParaForm = new NcParaForm();
            ncParaForm.ShowDialog();
        }

        private void SaveLogBtn_Click(object sender, EventArgs e)
        {
            try
            {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //设置文件类型  
            saveFileDialog1.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";

            //设置默认文件类型显示顺序  
            //saveFileDialog1.FilterIndex = 2;

            //保存对话框是否记忆上次打开的目录  
            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.FileName = "DetectData";
            //点了保存按钮进入  
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径  
                String localFilePath = saveFileDialog1.FileName.ToString();

                //获取文件名，不带路径  
                //fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);  

                //获取文件路径，不带文件名  
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));  

                //给文件名前加上时间  
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;  

                //在文件名里加字符  
                //saveFileDialog1.FileName.Insert(1,"dameng");  

                //System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();//输出文件  

                //fs输出带文字或图片的文件，就看需求了  
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                Workbook wBook = excel.Workbooks.Add(true);
                Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
                excel.Application.Workbooks.Add(true);
                for (int i = 0; i < this.listView1.Columns.Count; i++)//为excel添加标题
                {
                    wSheet.Cells[1, i + 1] = listView1.Columns[i].Text;
                }
                for (int i = 0; i < listView1.Items.Count; i++)//添加每一项
                {
                    for (int j = 0; j < listView1.Columns.Count; j++)
                    {
                        wSheet.Cells[i + 2, j + 1] = listView1.Items[i].SubItems[j].Text;
                    }
                }
                excel.DisplayAlerts = false;//和下面这个通常是一起用的
                excel.AlertBeforeOverwriting = false;//设置禁止弹出保存和覆盖的询问提示框
                wSheet.SaveAs(localFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12, null, null, false, false, null, null, null, null);
                wBook.Save();
                excel.ActiveWorkbook.SaveAs(localFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel12, null, null, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
                //excel.Application.Save(localFilePath);//保存路径。可以自定义，也可以通过savedialog对话框获取保存路劲
                //保存excel文件   
                excel.Save(localFilePath);
                excel.SaveWorkspace(localFilePath);
                excel.Quit();
                excel = null;
                MessageBox.Show("保存成功！");
            }
             
            }
            catch (Exception err)
            {
                MessageBox.Show("导出Excel出错！错误原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
        }

        

    }
}
