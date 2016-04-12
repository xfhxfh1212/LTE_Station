using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Plugin
{
    /// <summary>
    /// 全局布局区域
    /// </summary>
    public class GloabeControl
    {
        ///<summary>
        /// 主窗口数据显示区
        /// </summary>
        public static Form RightContent { get; set; }
        /// <summary>
        /// 主窗口数据显示区、小区搜索停留区域
        /// </summary>
        public static DockPanel cellSearchDockPanel { get; set; }
        /// <summary>
        /// 主窗口数据显示区、查找STMSI停留区域
        /// </summary>
        public static DockPanel findSTMSIDockPanel { get; set; }
        /// <summary>
        /// 主窗口数据显示区、测向停留区域
        /// </summary>
        public static DockPanel oriFindingDockPanel { get; set; }
    }
}
