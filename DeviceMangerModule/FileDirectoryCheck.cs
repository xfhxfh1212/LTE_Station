using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using COM.ZCTT.AGI.Common;
namespace DeviceMangerModule
{
    public partial class FileDirectoryCheck : Form
    {
        public bool ButtonChose = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileDirectoryCheck()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 创建路径事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        private void CreateThePath_Click(object sender, EventArgs e)
        {
            if (Global.testMode == Global.TestMode.CellScan)
            {
                Directory.CreateDirectory(Global.CellScanFileSavePath);
            }
            else
            {
                Directory.CreateDirectory(Global.binaryFileSavePath);
            }
            ButtonChose = true;
            this.Close();
        }

        /// <summary>
        /// 关闭当前窗体事件
        /// </summary>
        /// <param name="sender">无</param>
        /// <param name="e">无</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonChose = false;
            this.Close();
        }
    }
}
