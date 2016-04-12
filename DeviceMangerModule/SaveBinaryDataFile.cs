using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using COM.ZCTT.AGI.Common;
namespace DeviceMangerModule
{
    /// <summary>
    /// 类SaveBinaryDataFile主要用于保存二进制数据,提供回放使用
    /// 存储的整体思路：建立一个临时文件存数据，用一个配置文件存储开始时间等头信息，最后点结束时
    /// 建立文件，写头，后将临时文件的内容写入文件。
    /// </summary>
    /// 
    public  class SaveBinaryDataFile
    {
        #region 变量及属性定义
        private  UInt32 reserved=0x00000000;
        private  byte source;
        private  byte destination;
        private  UInt16 messageType = 0x0001;
        private  UInt16 transactionID=0x0000;
        private  UInt16 msgLen;
        private  string projectName="BeiJingTest";
        private  string testDeviceName="设备";
        private  string testMode;//ProtocalTrace或者CellScan
        private  string logStartTime;
        private  string logEndTime;
        public UInt32 Reserved
        {
            get { return reserved; }
            set { reserved = value; }
        }
        public byte Source
        {
            get { return source; }
            set { source = value; }
        }
        public byte Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public UInt16 MessageType
        {
            set { messageType = value; }
            get { return messageType; }
        }
        public UInt16 TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        public string TestDeviceName
        {
            get { return testDeviceName; }
            set { testDeviceName = value; }
        }
        public string TestMode
        {
            set { testMode = value; }
            get { return testMode; }
        }
        public string LogStartTime
        {
            get { return logStartTime; }
            set { logStartTime = value; }
        }
        public string LogEndTime
        {
            get { return logEndTime; }
            set { logEndTime = value; }
        }
        #endregion
        private string SaveFileName;

        /// <summary>
        /// 构造函数，委托挂载
        /// </summary>
        public SaveBinaryDataFile()
        {
            AGIInterface.Class1.OnSaveData += new AGIInterface.Class1.SaveDataHandler(SaveData);
            init();
        }

        /// <summary>
        /// 变量初始化操作
        /// </summary>
        private void init()
        {
            reserved = 0x00000000;
            source = 0;
            destination=0;
            messageType = 0x0001;
            transactionID = 0x0000;
            msgLen=0;
        }
        /// <summary>
        /// 新建日志文件和保存数据的临时文件
        /// </summary>     
        /// 
        private string CreateFileName()
        {
            string fileName;
            System.DateTime time = System.DateTime.Parse(logStartTime);
            string str = time.ToString("yyyyMMdd HH_mm_ss");
            DirectoryInfo mydir;
            if (Global.testMode == Global.TestMode.CellScan)
            {
                fileName = "CS_" +Global.GCurrentDevice+"_"+ str + ".bin";
                mydir = new DirectoryInfo(Global.CellScanFileSavePath);
            }
            else
            {
                fileName = "PT_" + Global.GCurrentDevice + "_" + str + ".bin";
                mydir = new DirectoryInfo(Global.binaryFileSavePath);
            }

            if (!mydir.Exists)
            {
                FileDirectoryCheck pFileDirectoryCheck = new FileDirectoryCheck();
                pFileDirectoryCheck.StartPosition = FormStartPosition.CenterParent;
                pFileDirectoryCheck.ShowDialog();

                if (pFileDirectoryCheck.ButtonChose == false)
                {
                    return fileName = "false";
                }
            }

            SaveFileName = fileName;
            //实时停止左边树形菜单显示log文件名(朱涛添加)//////////////
            ShowFileInListView();
            ///////////////////////////////////
            if (Global.testMode == Global.TestMode.CellScan)
            {
                fileName = @"" + Global.CellScanFileSavePath + "\\" + fileName;
            }
            else
            {
                fileName = @"" + Global.binaryFileSavePath + "\\" + fileName;
            }

            return fileName;
        }

        /// <summary>
        /// 在左边文档结构视图中添加新建的日志文件
        /// </summary>
        private void ShowFileInListView()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");
                XmlNode xnode = xmldoc.SelectSingleNode("ProjectTab");
                XmlNode xnode1 = xnode.SelectSingleNode("Parameter");
                XmlElement xelement = xmldoc.CreateElement("LogFile");
                xelement.SetAttribute("Name", SaveFileName);
                if (Global.testMode == Global.TestMode.CellScan)
                {
                    xelement.SetAttribute("FilePath", Global.CellScanFileSavePath + "\\" + SaveFileName);
                    Global.RecentFiles.Add(Global.CellScanFileSavePath + "\\" + SaveFileName);
                }
                else
                {
                    xelement.SetAttribute("FilePath", Global.binaryFileSavePath + "\\" + SaveFileName);
                    Global.RecentFiles.Add(Global.binaryFileSavePath + "\\" + SaveFileName);
                }
                xnode1.AppendChild(xelement);
                xnode.AppendChild(xnode1);
                xmldoc.AppendChild(xnode);
                Global.UpdateRecentFiles();
                xmldoc.Save(System.Windows.Forms.Application.StartupPath + "\\ProjectTab.xml");//保存XMl文件0
                Global.tempClass.RefreshTreeview();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Message=" + ex.Message + "\r\nStacktrace:" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 计算头长度
        /// </summary>
        /// <returns></returns>
        private UInt16 CalMsgLen()
        {
            int i = 0;
        //    i += 12;//消息头长度
            byte[] projectNameLen = Encoding.ASCII.GetBytes("ProjectName=\"" + projectName + "\"\r\n");
            i += projectNameLen.Length;
            byte[] testDeviceNameLen = Encoding.ASCII.GetBytes("TestDeviceNam=\"" +testDeviceName + "\"\r\n");
            i += testDeviceNameLen.Length;
            byte[] testModeLen = Encoding.ASCII.GetBytes("TestMode=\"" + testMode + "\"\r\n");
            i += testModeLen.Length;
            byte[] logStartTimeLen = Encoding.ASCII.GetBytes("LogStartTime=\"" + logStartTime + "\"\r\n");
            i += logStartTimeLen.Length;
            byte[] logEndTimeLen = Encoding.ASCII.GetBytes("LogEndTime=\"" +logEndTime + "\"\r\n");
            i += logEndTimeLen.Length;
            //i += 1;//End位
            return (UInt16)i;
        }
   
        //写日志文件（操作结束时候才建立文件）
        /// <summary>
        /// 写日志文件
        /// </summary>
        public void WriteFile()
        {
            Thread writeFile = new Thread(new ThreadStart(ConsBinaryFile));
            writeFile.IsBackground = true;
            writeFile.Start();
        }
        private void ConsBinaryFile()
        { ;}
        /// <summary>
        /// 写文件加头信息，此中头信息的结束时间是错误信息
        /// </summary>
        public  bool writeHeaderFile()
        { 
            string fileName = CreateFileName();
            if (fileName == "false")
            {
                return false;
            }
            SaveFileName = fileName;
            msgLen = CalMsgLen();
            int yushu = msgLen % 4;
            if (yushu != 0)
            {
                int tempValue = msgLen / 4;
                msgLen =(ushort)tempValue;
                msgLen+=1;
            }
            else
            {
                msgLen /= 4;
            } 
            using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create),Encoding.ASCII))
            {
                //写MsgHeader
                bw.Write(reserved);
                bw.Write(source);
                bw.Write(destination);
                bw.Write(messageType);
                bw.Write(transactionID);
                bw.Write(msgLen);
                string tempStr = "ProjectName=\"" + projectName + "\"\r\n";
                bw.Write(tempStr.ToCharArray());
                tempStr = "TestDeviceNam=\"" + testDeviceName + "\"\r\n";
                bw.Write(tempStr.ToCharArray());
                tempStr="TestMode=\""+testMode+"\"\r\n";
                bw.Write(tempStr.ToCharArray());
                tempStr="LogStartTime=\""+logStartTime+"\"\r\n";
                bw.Write(tempStr.ToCharArray());
                tempStr = "LogEndTime=\"" + logEndTime + "\"\r\n";
                bw.Write(tempStr.ToCharArray());
                //添加00补足4的倍数
                if (yushu != 0)
                {
                    while (4 - yushu != 0)
                    {
                        bw.Write((byte)0x00);
                        yushu++;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 修改头信息，主要是由于头中的结束时间是错误的
        /// </summary>
        public void changeHeader()
        {

            AGIInterface.Class1.OnSaveData -= new AGIInterface.Class1.SaveDataHandler(SaveData);

           // if (File.Exists(SaveFileName))
            //{
                //修改结束时间暂未能实现，等待后来者你的努力
                //using (FileStream fs = File.Open(SaveFileName,FileMode.Open,FileAccess.ReadWrite))
               // {
                    //using (BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII))
                    //{
                    //    Byte[] info = new UTF8Encoding(true).GetBytes(logEndTime);
                     //   bw.Write(info, 117, info.Length);
                     //   bw.Close();
                     //   fs.Close();
                    //}
               // }
           // }
        }
        /// <summary>
        /// 把发送和接收到的数据存储到临时文件中
        /// </summary>
        /// <param name="data"></param>
        private void SaveData(byte[] data)
        {
            //只有设备是工作状态才保存发送和接收的数据
            if (Global.isStart == "true")
            {
                if (File.Exists(SaveFileName))
                {
                    using (FileStream fs = File.Open(SaveFileName, FileMode.Append))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII))
                        {
                            bw.Write(data,0,data.Length);
                            bw.Close();
                            fs.Close();
                        }
                    }
                }
            }
        }
    }
}
