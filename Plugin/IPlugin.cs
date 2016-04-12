using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COM.ZCTT.AGI.Plugin
{
    /// <summary>
    /// 插件类
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 初始化该模块
        /// </summary>
        void Load();

        ///// <summary>
        ///// 将该模块加载到窗体之前执行
        ///// </summary>
        //void BeforeAdd2Window();

        ///// <summary>
        ///// 将该模块加载到窗体之后执行
        ///// </summary>
        //void AfterAdd2Window();

        ///// <summary>
        ///// 左侧导航栏
        ///// </summary>
      // public Navigation { get;set;}

        ///// <summary>
        ///// 右侧内容窗体
        ///// </summary>
        //TabControl Content { get; set; }

        ///// <summary>
        ///// 下方状态栏
        ///// </summary>
        //Panel State { get; set; }

        
    }
}
