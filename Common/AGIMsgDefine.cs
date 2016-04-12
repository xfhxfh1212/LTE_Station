using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;


using U8 = System.Byte;
using S8 = System.SByte;
using U16 = System.UInt16;
using S16 = System.Int16;
using U32 = System.UInt32;
using S32 = System.Int32;
using F32=System.Single;
using B8=System.Byte;

namespace COM.ZCTT.AGI.Common
{
    public class AGIMsgDefine
    {

        public const uint MAX_SPECIFYCELL_NUM = 64;
        public const uint MAX_FREQUENCYLIST_NUM = 100;

        /// <summary>
        /// 消息类型定义
        /// </summary>
        /// 
        public const UInt16 PC_AG_SET_AGT_INFO_REQ_MSG_TYPE = 0x4000;//设备配置消息
        public const UInt16 PC_AG_GET_AGT_INFO_REQ_MSG_TYPE = 0x4001;//获取设备配置消息
        public const UInt16 PC_AG_SPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4002; //指定小区扫描消息
        public const UInt16 PC_AG_SPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4003; //指定小区扫描结束消息
        public const UInt16 PC_AG_UNSPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4004; //非指定小区扫描消息
        public const UInt16 PC_AG_UNSPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4005; //非指定小区扫描结束消息
        public const UInt16 PC_AG_DEBUG_INFO_REQ_MSG_TYPE = 0x4006; //预留之前消息定义，debug调试使用
        public const UInt16 PC_AG_SET_AGT_IPADD_REQ_MSG_TYPE = 0x4007;  //预留后续开发，暂时没有使用
        public const UInt16 PC_AG_RENEW_IP_REQ_MSG_TYPE = 0x4008;  //IP地址或者端口地址更新消息
        public const UInt16 PC_AG_IQ_STORE_REQ_MSG_TYPE = 0x4009;  //IQ数据存储相关配置消息
        public const UInt16 PC_AG_PROTOCOL_TRACE_REQ_MSG_TYPE = 0x400a;  //协议跟踪相关配置消息
        public const UInt16 PC_AG_UE_SILENCE_RPT_RSP_MSG_TYPE = 0x400b;  //PC机对静默期UE回应消息
        public const UInt16 PC_AG_PROTOCOL_TRACE_REL_MSG_TYPE = 0x400c;  //协议跟踪结束配置消息
        public const UInt16 PC_AG_SOFT_RESET_MSG_TYPE = 0x400d;
        public const UInt16 PC_AG_RENEW_MGC_REQ_MSG_TYPE = 0x400e;//PC机对MGC参数的设置

        /* AGI Agent ==> L1/L2P 主要是Agent转发给L1/L2P的消息 */
        public const UInt16 AG_XX_SET_AGT_INFO_REQ_MSG_TYPE = PC_AG_SET_AGT_INFO_REQ_MSG_TYPE;
        public const UInt16 AG_XX_GET_AGT_INFO_REQ_MSG_TYPE = PC_AG_GET_AGT_INFO_REQ_MSG_TYPE;
        public const UInt16 AG_XX_SPECIFIED_CELL_SCAN_REQ_MSG_TYPE = PC_AG_SPECIFIED_CELL_SCAN_REQ_MSG_TYPE;
        public const UInt16 AG_XX_SPECIFIED_CELL_SCAN_REL_MSG_TYPE = PC_AG_SPECIFIED_CELL_SCAN_REL_MSG_TYPE;
        public const UInt16 AG_XX_UNSPECIFIED_CELL_SCAN_REQ_MSG_TYPE = PC_AG_UNSPECIFIED_CELL_SCAN_REQ_MSG_TYPE;
        public const UInt16 AG_XX_UNSPECIFIED_CELL_SCAN_REL_MSG_TYPE = PC_AG_UNSPECIFIED_CELL_SCAN_REL_MSG_TYPE;
        public const UInt16 AG_XX_DEBUG_INFO_REQ_MSG_TYPE = PC_AG_DEBUG_INFO_REQ_MSG_TYPE;
        public const UInt16 AG_XX_PROTOCOL_TRACE_REQ_MSG_TYPE = PC_AG_PROTOCOL_TRACE_REQ_MSG_TYPE;
        public const UInt16 AG_L2P_UE_SILENCE_RPT_RSP_MSG_TYPE = PC_AG_UE_SILENCE_RPT_RSP_MSG_TYPE;
        public const UInt16 AG_XX_PROTOCOL_TRACE_REL_MSG_TYPE = PC_AG_PROTOCOL_TRACE_REL_MSG_TYPE;
        public const UInt16 AG_XX_IQ_STORE_REQ_MSG_TYPE = PC_AG_IQ_STORE_REQ_MSG_TYPE;

        /* L1 ==> AGI Agent L1给agent发送的ACK消息以及正常数据上报消息 */
        public const UInt16 L1_AG_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x6000; //设备配置消息的反馈
        public const UInt16 L1_AG_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x6001; //获取设备配置消息反馈
        public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x6002;
        public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6003;
        public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x6004;
        public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x6005;
        public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x6006;
        public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6007;
        public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x6008;
        public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x6009;
        public const UInt16 L1_AG_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x600a;
        public const UInt16 L1_AG_DEBUG_INFO_DATA_MSG_TYPE = 0x600b;
        public const UInt16 L1_AG_ERROR_DATA_MSG_TYPE = 0x600c;
        public const UInt16 L1_AG_WARNING_DATA_MSG_TYPE = 0x600d;
        public const UInt16 L1_AG_PROTOCOL_TRACE_REQ_ACK_MSG_TYPE = 0x600e;
        public const UInt16 L1_AG_PROTOCOL_DATA_MSG_TYPE = 0x600f;
        public const UInt16 L1_AG_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x6010;
        public const UInt16 L1_AG_IQ_STORE_REQ_ACK_MSG_TYPE = 0x6011; //L1对IQ数据存储配置消息的反馈
        public const UInt16 L1_AG_PC_IQ_END_MSG_TYPE = 0x6012; //L1上报的IQ数据结束指示消息
        public const UInt16 L1_AG_PHY_COMMEAS_IND_MSG_TYPE = 0x6013; //L1上报公共测量数据消息


        /* L2P ==> AGI Agent */
        public const UInt16 L2P_AG_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x7000;
        public const UInt16 L2P_AG_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x7001;
        public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x7002;
        public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x7003;
        public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x7004;
        public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x7005;
        public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x7006;
        public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x7007;
        public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x7008;
        public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x7009;
        public const UInt16 L2P_AG_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x700a;
        public const UInt16 L2P_AG_DEBUG_INFO_DATA_MSG_TYPE = 0x700b;
        public const UInt16 L2P_AG_ERROR_DATA_MSG_TYPE = 0x700c;
        public const UInt16 L2P_AG_WARNING_DATA_MSG_TYPE = 0x700d;
        public const UInt16 L2P_AG_PROTOCOL_TRACE_REQ_ACK_MSG_TYPE = 0x700e;
        public const UInt16 L2P_AG_CELL_CAPTURE_IND_MSG_TYPE = 0x700f;
        public const UInt16 L2P_AG_CELL_SYSINFO_IND_MSG_TYPE = 0x7010;
        public const UInt16 L2P_AG_CELL_RELEASE_IND_MSG_TYPE = 0x7011;
        public const UInt16 L2P_AG_UE_CAPTURE_IND_MSG_TYPE = 0x7012;
        public const UInt16 L2P_AG_UE_RELEASE_IND_MSG_TYPE = 0x7013;
        public const UInt16 L2P_AG_UE_SILENCE_RPT_IND_MSG_TYPE = 0x7014;
        public const UInt16 L2P_AG_CELL_STATE_IND_MSG_TYPE = 0x7015;
        public const UInt16 L2P_AG_PROTOCOL_DATA_MSG_TYPE = 0x7016;
        public const UInt16 L2P_AG_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x7017;
        public const UInt16 L2P_AG_IQ_STORE_REQ_ACK_MSG_TYPE = 0x7018;
        public const UInt16 L2P_AG_PC_IQ_END_MSG_TYPE = 0x7019;   //L2P发送的IQ数据存储结束标志消息

        /* AGI Agent ==> AGI PC  (透传L1部分) */
        public const UInt16 L1_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = L1_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE;
        public const UInt16 L1_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = L1_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE;
        public const UInt16 L1_DEBUG_INFO_DATA_MSG_TYPE = L1_AG_DEBUG_INFO_DATA_MSG_TYPE;
        public const UInt16 L1_ERROR_DATA_MSG_TYPE = L1_AG_ERROR_DATA_MSG_TYPE;
        public const UInt16 L1_WARNING_DATA_MSG_TYPE = L1_AG_WARNING_DATA_MSG_TYPE;
        public const UInt16 L1_PROTOCOL_DATA_MSG_TYPE = L1_AG_PROTOCOL_DATA_MSG_TYPE;
        public const UInt16 L1_PHY_COMMEAS_IND_MSG_TYPE = L1_AG_PHY_COMMEAS_IND_MSG_TYPE;
        public const UInt16 L1_PC_IQ_END_MSG_TYPE = L1_AG_PC_IQ_END_MSG_TYPE;

        /* AGI Agent ==> PC (透传L2P部分)*/
        public const UInt16 L2P_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = L2P_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE;
        public const UInt16 L2P_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = L2P_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE;
        public const UInt16 L2P_DEBUG_INFO_DATA_MSG_TYPE = L2P_AG_DEBUG_INFO_DATA_MSG_TYPE;
        public const UInt16 L2P_ERROR_DATA_MSG_TYPE = L2P_AG_ERROR_DATA_MSG_TYPE;
        public const UInt16 L2P_WARNING_DATA_MSG_TYPE = L2P_AG_WARNING_DATA_MSG_TYPE;
        public const UInt16 L2P_CELL_CAPTURE_IND_MSG_TYPE = L2P_AG_CELL_CAPTURE_IND_MSG_TYPE;
        public const UInt16 L2P_CELL_SYSINFO_IND_MSG_TYPE = L2P_AG_CELL_SYSINFO_IND_MSG_TYPE;
        public const UInt16 L2P_CELL_RELEASE_IND_MSG_TYPE = L2P_AG_CELL_RELEASE_IND_MSG_TYPE;
        public const UInt16 L2P_UE_CAPTURE_IND_MSG_TYPE = L2P_AG_UE_CAPTURE_IND_MSG_TYPE;
        public const UInt16 L2P_UE_RELEASE_IND_MSG_TYPE = L2P_AG_UE_RELEASE_IND_MSG_TYPE;
        public const UInt16 L2P_UE_SILENCE_RPT_IND_MSG_TYPE = L2P_AG_UE_SILENCE_RPT_IND_MSG_TYPE;
        public const UInt16 L2P_CELL_STATE_IND_MSG_TYPE = L2P_AG_CELL_STATE_IND_MSG_TYPE;
        public const UInt16 L2P_PROTOCOL_DATA_MSG_TYPE = L2P_AG_PROTOCOL_DATA_MSG_TYPE;
        public const UInt16 L2P_PC_IQ_END_MSG_TYPE = L2P_AG_PC_IQ_END_MSG_TYPE;

        /* AGI Agent ==> AGI PC (AGI Agent自己产生部分) */
        public const UInt16 AG_PC_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x4800;
        public const UInt16 AG_PC_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x4801;
        public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x4802;
        public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x4803;
        public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x4804;
        public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x4805;
        public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x4806;
        public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x4807;
        public const UInt16 AG_PC_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x4808;
        public const UInt16 AG_PC_ERRROR_DATA_MSG_TYPE = 0x4809;
        public const UInt16 AG_PC_WARNING_DATA_MSG_TYPE = 0x480a;
        public const UInt16 AG_PC_PROTOCOL_TRACE_REQ_ACK_MSG_TYPE = 0x480b;
        public const UInt16 XX_PROTOCOL_DATA_MSG_TYPE = 0x480c;
        public const UInt16 AG_PC_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x480d;
        public const UInt16 AG_PC_GPS_DATA_MSG_TYPE = 0x480e; //GPS数据上报
        public const UInt16 AG_PC_RENEW_IP_REQ_ACK_MSG_TYPE = 0x480f; //IP地址更新消息反馈
        public const UInt16 AG_PC_IQ_STORE_REQ_ACK_MSG_TYPE = 0x4810; //IQ数据存储配置消息反馈
        public const UInt16 AG_PC_IQ_END_IND_MSG_TYPE = 0x4811; //IQ数据存储结束指示反馈
        public const UInt16 AG_XX_IQ_REL_REQ = 0x4812; //由Agent产生，给L1/L2P
        public const UInt16 PC_AG_RENEW_MGC_REQ_ACK_MSG_TYPE = 0x4813;
        //public const UInt16 PC_AG_SET_AGT_INFO_REQ_MSG_TYPE = 0x4000;
        //public const UInt16 PC_AG_GET_AGT_INFO_REQ_MSG_TYPE = 0x4001;
        //public const UInt16 PC_AG_SPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4002;
        //public const UInt16 PC_AG_SPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4003;
        //public const UInt16 PC_AG_UNSPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4004;
        //public const UInt16 PC_AG_UNSPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4005;
        //public const UInt16 PC_AG_DEBUG_INFO_REQ_MSG_TYPE = 0x4006;
        //public const UInt16 PC_AG_SET_AGT_IPADD_REQ_MSG_TYPE = 0x4007;
        //public const UInt16 PC_AG_PROTOCOL_TRACE_REQ_MSG_TYPE = 0x4008;
        //public const UInt16 PC_AG_UE_SILENCE_RPT_RSP_MSG_TYPE = 0x4009;
        //public const UInt16 PC_AG_PROTOCOL_TRACE_REL_MSG_TYPE = 0x400a;
        //public const UInt16 PC_AG_SOFT_RESET_MSG_TYPE = 0x400b;

        //public const UInt16 AG_XX_SET_AGT_INFO_REQ_MSG_TYPE = 0x4000;
        //public const UInt16 AG_XX_GET_AGT_INFO_REQ_MSG_TYPE = 0x4001;
        //public const UInt16 AG_XX_SPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4002;
        //public const UInt16 AG_XX_SPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4003;
        //public const UInt16 AG_XX_UNSPECIFIED_CELL_SCAN_REQ_MSG_TYPE = 0x4004;
        //public const UInt16 AG_XX_UNSPECIFIED_CELL_SCAN_REL_MSG_TYPE = 0x4005;
        //public const UInt16 AG_XX_DEBUG_INFO_REQ_MSG_TYPE = 0x4006;
        ////public const UInt16 AG_XX_DEBUG_INFO_REL_MSG_TYPE = 0x4007;
        //public const UInt16 AG_XX_PROTOCOL_TRACE_REQ_MSG_TYPE = 0x400a;
        //public const UInt16 AG_XX_PROTOCOL_TRACE_REL_MSG_TYPE = 0x400b;



        //public const UInt16 AG_PC_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x4800;
        //public const UInt16 AG_PC_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x4801;
        //public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x4802;
        //public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x4803;
        //public const UInt16 AG_PC_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x4804;
        //public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x4805;
        //public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x4806;
        //public const UInt16 AG_PC_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x4807;
        //public const UInt16 AG_PC_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x4808;
        //public const UInt16 AG_PC_ERRROR_DATA_MSG_TYPE = 0x4809;
        //public const UInt16 AG_PC_WARNING_DATA_MSG_TYPE = 0x480a;
        //public const UInt16 AG_PC_PROTOCOL_TRACE_REQ_ACK_MSG_TYPE = 0x480b;
        ////public const UInt16 AG_PC_WARNING_DATA_MSG_TYPE = 0x480c;
        //public const UInt16 XX_PROTOCOL_DATA_MSG_TYPE = 0x480c;                         //2013.06.12
        //public const UInt16 AG_PC_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x480d;             //2013.06.13

        //public const UInt16 L1_AG_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x6000;
        //public const UInt16 L1_AG_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x6001;
        //public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x6002;
        //public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6003;
        //public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x6004;
        //public const UInt16 L1_AG_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x6005;
        //public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x6006;
        //public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6007;
        //public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x6008;
        //public const UInt16 L1_AG_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x6009;
        //public const UInt16 L1_AG_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x600a;
        //public const UInt16 L1_AG_DEBUG_INFO_DATA_MSG_TYPE = 0x600b;
        ////public const UInt16 L1_AG_DEBUG_INFO_REL_ACK_MSG_TYPE = 0x600d;
        //public const UInt16 L1_AG_ERRROR_DATA_MSG_TYPE = 0x600c;
        //public const UInt16 L1_AG_WARNING_DATA_MSG_TYPE = 0x600d;
        //public const UInt16 L1_AG_PROTOCOL_TRACE_ACK_MSG_TYPE = 0x600e;
        //public const UInt16 L1_AG_PROTOCOL_DATA_MSG_TYPE = 0x600f;
        ////public const UInt16 L1_AG_CELL_SCAN_DATA_MSG_TYPE = 0x6011;
        //public const UInt16 L1_AG_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x6010;

        //public const UInt16 L1_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6003;
        ////public const UInt16 L1_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x6005;
        //public const UInt16 L1_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x6007;
        //public const UInt16 L1_DEBUG_INFO_DATA_MSG_TYPE = 0x600b;
        //public const UInt16 L1_ERRROR_DATA_MSG_TYPE = 0x600c;
        //public const UInt16 L1_WARNING_DATA_MSG_TYPE = 0x600d;
        //public const UInt16 L1_PROTOCOL_DATA_MSG_TYPE = 0x600f;
        //public const UInt16 L1_AG_PHY_COMMEAS_IND_MSG_TYPE = 0x6013;///* V1.0 */ L1上报公共测量数据消息

        //public const UInt16 L2P_AG_SET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x7000;
        //public const UInt16 L2P_AG_GET_AGT_INFO_REQ_ACK_MSG_TYPE = 0x7001;
        //public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x7002;
        //public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x7003;
        //public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x7004;
        //public const UInt16 L2P_AG_SPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x7005;
        //public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_REQ_ACK_MSG_TYPE = 0x7006;
        //public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x7007;
        //public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_FINISH_IND_MSG_TYPE = 0x7008;
        //public const UInt16 L2P_AG_UNSPECIFIED_CELL_SCAN_REL_ACK_MSG_TYPE = 0x7009;
        //public const UInt16 L2P_AG_DEBUG_INFO_REQ_ACK_MSG_TYPE = 0x700a;
        //public const UInt16 L2P_AG_DEBUG_INFO_DATA_MSG_TYPE = 0x700b;
        ////public const UInt16 L2P_AG_DEBUG_INFO_REL_ACK_MSG_TYPE = 0x700c;
        //public const UInt16 L2P_AG_ERRROR_DATA_MSG_TYPE = 0x700c;
        //public const UInt16 L2P_AG_WARNING_DATA_MSG_TYPE = 0x700d;
        //public const UInt16 L2P_AG_PROTOCOL_TRACE_REQ_ACK_MSG_TYPE = 0x700e;
        //// public const UInt16 L2P_AG_PROTOCOL_TRACE_ACK_MSG_TYPE = 0x700e;
        ////       public const UInt16 L2P_AG_PROTOCOL_TRACE_ACK_MSG_TYPE = 0x7010;
        //public const UInt16 L2P_AG_CELL_CAPTURE_IND_MSG_TYPE = 0x700F;                 //2013.06.12
        //public const UInt16 L2P_AG_CELL_SYSINFO_IND_MSG_TYPE = 0x7010;                 //2013.06.12
        //public const UInt16 L2P_AG_CELL_RELEASE_IND_MSG_TYPE = 0x7011;
        //public const UInt16 L2P_AG_UE_CAPTURE_IND_MSG_TYPE = 0x7012;                   //2013.06.12
        //public const UInt16 L2P_AG_UE_RELEASE_IND_MSG_TYPE = 0x7013;
        //public const UInt16 L2P_AG_UE_SILENCE_RPT_IND_MSG_TYPE = 0x7014;
        //public const UInt16 L2P_AG_CELL_STATE_IND_MSG_TYPE = 0x7015;                   //2013.06.12
        //public const UInt16 L2P_AG_PROTOCOL_DATA_MSG_TYPE = 0x7016;
        //public const UInt16 L2P_AG_PROTOCOL_TRACE_REL_ACK_MSG_TYPE = 0x7017;


        //public const UInt16 L2P_SPECIFIED_CELL_SCAN_DATA_MSG_TYPE_MSG_TYPE = 0x7003;
        //public const UInt16 L2P_UNSPECIFIED_CELL_SCAN_DATA_MSG_TYPE = 0x7007;
        //public const UInt16 L2P_DEBUG_INFO_DATA_MSG_TYPE = 0x700b;
        //public const UInt16 L2P_ERRROR_DATA_MSG_TYPE = 0x700c;
        //public const UInt16 L2P_WARNING_DATA_MSG_TYPE = 0x700d;
        //public const UInt16 L2P_CELL_CAPTURE_IND_MSG_TYPE = 0x700f;
        //public const UInt16 L2P_CELL_SYSINFO_IND_MSG_TYPE = 0x7010;
        //public const UInt16 L2P_CELL_RELEASE_IND_MSG_TYPE = 0x7011;
        //public const UInt16 L2P_UE_CAPTURE_IND_MSG_TYPE = 0x7012;
        //public const UInt16 L2P_UE_RELEASE_IND_MSG_TYPE = 0x7013;
        //public const UInt16 L2P_UE_SILENCE_RPT_IND_MSG_TYPE = 0x7014;
        //public const UInt16 L2P_CELL_STATE_IND_MSG_TYPE = 0x7015;
        //public const UInt16 L2P_PROTOCOL_DATA_MSG_TYPE = 0x7016;
    }

    /////////////////////////////////////////////
    //消息头 与 ACK 标准ACK定义   START
    ///////////////////////////////////////////
    //消息头机构体定义
    /// <summary>
    /// 消息头机构体定义
    /// </summary>
    public struct AGI_MSG_HEADER
    {
        public U32 reserved;        //预留
        public U8 SrcID;          //{L1_SUB_SYS，L2P_SUB_SYS，AGI_AGENT_SUB_SYS }
        public U8 destID;     //消息接收方标识。取值范围：{L1_SUB_SYS，L2P_SUB_SYS，AGI_AGENT_SUB_SYS }
        public U16 msgType;     //消息类型
        public U16 transactionID;//会话标识。当同时发送多条相同消息类型的时候，可以通过此字段来区分。REQ和RSP/IND也可以通过此字段关联。
        public U16 MsgLen;          //消息长度，包括消息头和消息体长度。单位：4 bytes
    }

    //标准ACK消息结构体定义
    /// <summary>
    /// 标准ACK消息结构体定义
    /// </summary>
    public struct AGI_MSG_ACK
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8Cause;               //0-成功 1-失败
    }
    /////////////////////////////////////////////
    //消息头 与 ACK 标准ACK定义   END
    ///////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////////////////
    //设备状态消息定义     START
    /////////////////////////////////////////////////////////////////////////////////////////
    //设备状态控制请求命令


    public struct PC_AG_SET_AGT_INFO_REQ
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8WorkMode;            //工作模式 1-小区扫描 2-协议跟踪（保留以后扩展，对现在不起作用）
        public U8 u8IterfaceType;        //数据输出接口 0—usb 1-lan
        public U8 u8OutDataType;         //输出数据类型 0 -IQ数据 1-调试数据 2-测量数据 3- 协议跟踪各层输出数据
        public U8 u8SaveMode;            //数据保存方式 1- 定时存储 2-连续存储 3-触发存储
        public U32 RptInterval;          //V1.0__AGI上报间隔 0: 表示TTI级，默认使用这个；数值为（1:5000）单位为ms； 其他当做处理
        public U32 SaveType;             /*V1.0__如果存储模式为定时存储的情况下，此值为开始存储时刻，格式为 SaveType[0:7]  秒
                                           SaveType[8:15]  分   SaveType[16:23]  小时
                                           SaveType[24:31] 与当前相隔天数
                                           如果存储模式为事件存储的情况，通过此值的具体值代表具体事件类型 */
        public U32 SaveType2;            /*V1.0__如果存储模式为连续存储的情况下，表示存储时间长度单位为ms */
        public U8 u8RxAntNum;              /* AGT有效接收天线数目。
                                                         //取值：1、2、4、8
                                                          //默认值：2（目前最大也是支持到2）*/
        public U8 u8RxAntStatus;           /* AGT接收天线配置，从低bit到高bit：
                                                            Bit0：Ant0
                                                            Bit1：Ant1
                                                            Bit2~Bit7：Reserved
                                                            各bit位：0--无效;1--有效 */
        public U8 u8Padding1;//填充
        public U8 u8Padding2;
        public U8  mu8GpsValidFlag;         /* GPS_VALID(1) GPS有效标志,只有在GPS有效的情况下上报地图位置才有效
                                               GPS_INVALID(0) GPS无效标志，时间使用系统默认时间 */
        public U8  mu8GpsPosRptFlag;        /* GPS地理位置上报有效标志，GPS_VALID-有效 GPS_INVALID-无效，
                                               上报的信息包括：经纬度，方向，速度，高度等信息 */
        public U16 mu16GpsPosRptPeriod;      /* GPS地理位置上报周期，单位为：s，上报消息的起始时间为收到此消息时开始统计时间 */

    }
    public struct AG_XX_SET_AGT_INFO_REQ
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8WorkMode;            //工作模式 1-小区扫描 2-协议跟踪（保留以后扩展，对现在不起作用）
        public U8 u8IterfaceType;        //数据输出接口 0—usb 1-lan
        public U8 u8OutDataType;         //输出数据类型 0 -IQ数据 1-调试数据 2-测量数据 3- 协议跟踪各层输出数据
        public U8 u8SaveMode;            //数据保存方式 1- 定时存储 2-连续存储 3-触发存储
        public U16 u16Times;             //1- 定时存储 2-连续存储 对应的值 
        public U8 u8SaveDataType;        //数据保存类型 默认cvs格式 保留该字段
        public U8 u8RxAntNum;              /* AGT有效接收天线数目。
                                                         取值：1、2、4、8
                                                          默认值：2（目前最大也是支持到2）*/
        public U8 u8RxAntStatus;           /* AGT接收天线配置，从低bit到高bit：
                                                            Bit0：Ant0
                                                            Bit1：Ant1
                                                            Bit2~Bit7：Reserved
                                                            各bit位：0--无效;1--有效 */
        public U8 u8Padding1;//填充
        public U8 u8Padding2;
        public U8 u8Padding3;
    }

    //AGENT对设备状态控制请求命令的回复ACK
    //typedef AGI_MSG_ACK  AG_PC_GET_GAT_INFO_REQ_ACK;

    //L1对设备状态控制请求命令的回复ACK
    //typedef AGI_MSG_ACK  L1_AG_GET_AGT_INFO_REQ_ACK;

    //L2P对设备状态控制请求命令的回复ACK
    //typedef AGI_MSG_ACK  L2P_AG_GET_AGT_INFO_REQ_ACK;
    //获取设备状态信息消息
    //该消息只有消息头 同过消息类型判断
    //typedef AGI_MSG_HEADER  AG_PC_GET_AGT_INFO_REQ;
    //typedef AGI_MSG_HEADER  AG_XX_GET_AGT_INFO_REQ；
    //获取设备状态消息的ACK消息

    /*
    public struct AG_PC_GET_AGT_INFO_REQ_ACK
    {
        public AGI_MSG_HEADER msgHeader;                //消息头
        public U8 u8InstrumentState;          //0-空闲没有配置工作模式 1-工作  设备正在工作
        public U8 u8WorkModel;                //1-小区扫描 2-协议跟踪
        public U8 u8InterfaceType;            //0-USB 1-LAN
        public U8 u8OutDataType;              //0-IQ数据1-调试数据（工程模式下）2-测量数据(扫频、小区扫描）3-协议跟踪各层数据输出配置：L1，L2，L3，用户面,控制面。
        public U8 u8SaveModel;                // 0-外部存储1-定时存储：时间间隔2-连续存储：时间长度3-触发存储：事件触发，存储上下文数据，
        public U8 u8Time;                     // SaveModel=1 或2时对应的时间值（ms）
        public U8 u8VersionNo;                //版本号
        public U8 u8Battery;                  //剩余电量
    }*/

    public struct AG_PC_GET_AGT_INFO_REQ_ACK
    {
        public AGI_MSG_HEADER msgHeader;    //消息头
        public U8  mu8InstrumentState;      /* IDLE-空闲，没有配置工作模式；WORK-工作，设备正在工作 */
        public U8  mu8WorkModel;            /* CELL_SCAN-小区扫描; PTOTOCOL_TRACE-协议跟踪 */
        public U8  mu8InterfaceType;        /* INTRFACE_USB:USB; INTRFACE_LAN:LAN */
        public U8  mu8OutDataType;          /* OUTDATATYPE_USB         -LOG数据存储通过USB到外设
                                    OUTDATATYPE_FLASH       -LOG数据存储在AGT的FLASH中
                                    OUTDATATYPE_LAN         -LOG数据通过网线存储到PC机存储*/
        public U8  mu8SaveModel;            /* Bit0 SAVEMODE_TIMING    -定时存储：时间间隔
                                    Bit1 SAVEMODE_CONTINUOUS-连续存储：时间长度
                                    Bit2 SAVEMODE_TRIGGER   -触发存储：事件触发，存储上下文数据，峰值触发，实时解码标记触发（保留待实现）*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[]  mu8Padding1;          /* 填充 */
        public U32 mu32SaveType;            /* 如果存储模式为定时存储的情况下，此值为开始存储时刻，格式为 SaveType[0:7]  秒
                                    SaveType[8:15]  分   SaveType[16:23]  小时
                                    SaveType[24:31] 与当前相隔天数 */
        public U32 mu32SaveType2;           /* 如果存储模式为连续存储的情况下，表示存储时间长度单位为ms */
        //edit at 20150113
        public U32  mu32L1VersionNo;          /* L1版本号，前4bit为大版本号后4bit为小版本号 */
        public U32  mu32L2pVersionNo;         /* L2P版本号，前4bit为大版本号后4bit为小版本号 */
        public U32 mu32AgentVersionNo;         /* Agent的版本号 */
        public U32 mu32FPGAVersionNo;         /* FPGA的版本号 */
        public U32 mu32DDVersionNo;         /* 驱动的版本号 */

        public U8  mu8Battery0;             /* 工作电池剩余电量，以百分比表示，50即表示剩余50%电量 */
        public U8  mu8Battery1;             /* 备用电池剩余电量，以百分比表示，50即表示剩余50%电量 */
        public U32 mu32GpsStatus;           /* 目前GPS工作状态， GpsStatus[0:7] GPS星星个数 255：GPS无效
                                    GpsStatus[8:15] 位置上报标志，1：上报，其他： 不上报 */
        public U32 mu32CurrentTime;         /* 当前仪表存储时间，GPS有效的时候上报GPS时间，无效情况下上报仪表默认存储时间
                                    CurrentTime[0:7] 月 范围：1—12
                                    CurrentTime[8:15] 日 范围：1---31
                                    CurrentTime[16:23] 时 范围：0---23
                                    CurrentTime[24:31] 时 范围：0---59 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[]  mau8AgtPort0Num;     /*  AgtPort0Num[0]:端口号0的第一位，比如端口号为8067，则此位是8，
                                    AgtPortNum[1], AgtPortNum[2], AgtPortNum[3]分别是0，6,7 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[]  mau8AgtPort1Num;     /*  AGT端口1的的端口号 */
        public U32 mu32AgtIPAdress;        /* 仪表的IP地址 */
        public U32 mu32AgtGateAdress;      /* 仪表的网关地址 */
        public U32 mu32AgtMacAdress;       /* 仪表的MAC地址 */
        public U16 mu16RemainSpace;        /* Flash的剩余存储空间，单位M,如果是USB输出模式，
                                   则是外设的剩余存储空间，其他方式都是AGT中flash剩余存储空间 */
        public U8  mu8AntStatus;           /* 天线的可用状态  Bit0-天线0  Bit1-天线1 Valid-有效 Invalid-无效 */
        public U8  mu8Pading;
    }


    //l2P，L1层的设备设置请求命令的ACK消息
    public struct L2p_AG_GET_AGT_INFO_REQ_ACK
    {
        public AGI_MSG_HEADER msgHeader;            //消息头
        public U8 u8InstrumentState;                //仪表状态 0-空闲  没有配置工作模式1-工作  设备正在工作
        public U8 u8WorkModel;                      // 1-小区扫描2-协议跟踪
        public U8 u8VersionNo;                      //版本信息
        public U8 u8Reserved;                     //工作模式
    }


    public struct L1_AG_GET_AGT_INFO_REQ_ACK
    {
        public AGI_MSG_HEADER msgHeader;            //消息头
        public U8 u8InstrumentState;                //仪表状态 0-空闲  没有配置工作模式1-工作  设备正在工作
        public U8 u8WorkModel;                      // 1-小区扫描2-协议跟踪
        public U8 u8VersionNo;                      //版本信息
        public U8 u8Reserved;                      //工作模式
    }

    ////////////////////////////////////
    //调试消息定义 START
    ///////////////////////////////////
    //调试请求消息定义

    public struct PC_AG_DEBUG_INFO_REQ
    {
        public AGI_MSG_HEADER msgHeader;
        public UInt16 u16TargetID;         //调试目标标示：0-	AGI Agent；1-	L1；2-	L2P;3-	all subsystem
        public UInt16 u16DebugID;          //调试上报项标示
        public UInt16 u16Para1;       //调试上报参数列表
        public UInt16 u16Para2;
        public UInt16 u16Para3;
        public UInt16 u16Para4;
        public UInt16 u16Para5;
        public UInt16 u16Para6;
    }

    public struct AG_XX_DEBUG_INFO_REQ
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 u16TargetID;         //调试目标标示：0-	AGI Agent；1-	L1；2-	L2P;3-	all subsystem
        public U16 u16DebugID;          //调试上报项标示
        public U16 Para1;    //配置参数1
        public U16 Para2;
        public U16 Para3;
        public U16 Para4;
        public U16 Para5;
        public U16 Para6;
    }


    //调试数据定义
    public struct AG_PC_DEBUG_INFO_DATA
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 mu16DataFlag;          /*0：as32ReportData中存放的是上报数据，1：as32ReportData中存放的是上报数据的首地址*/
        public U16 u16DebugID;          //调试上报项标示
        public U8 mu8WinId;                /* 消息打印窗口ID */
        public U8 mu8ReportType;    /* 消息上报类型：
                                                 0---上报消息经解析函数解析后输出到调试窗口；
                                                 1---上报消息直接打印输出到文件；
                                                 2---上报消息不经解析直接打印到调试窗口 */

        public U16 u16ReportLength;       //调试上报数据的长度，单位：4bytes
        public S32[] s8ReportData;
    }

    public struct L1_AG_DEBUG_INFO_DATA
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 mu16DataFlag;          /*0：as32ReportData中存放的是上报数据，1：as32ReportData中存放的是上报数据的首地址*/
        public U16 u16DebugID;          //调试上报项标示
        public U8 mu8WinId;                /* 消息打印窗口ID */
        public U8 mu8ReportType;    /* 消息上报类型：
                                                 0---上报消息经解析函数解析后输出到调试窗口；
                                                 1---上报消息直接打印输出到文件；
                                                 2---上报消息不经解析直接打印到调试窗口 */

        public U16 u16ReportLength;       //调试上报数据的长度，单位：4bytes
        public S32[] s8ReportData;
    }

    public struct L1_DEBUG_INFO_DATA
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 mu16DataFlag;          /*0：as32ReportData中存放的是上报数据，1：as32ReportData中存放的是上报数据的首地址*/
        public U16 u16DebugID;          //调试上报项标示
        public U8 mu8WinId;                /* 消息打印窗口ID */
        public U8 mu8ReportType;    /* 消息上报类型：
                                                 0---上报消息经解析函数解析后输出到调试窗口；
                                                 1---上报消息直接打印输出到文件；
                                                 2---上报消息不经解析直接打印到调试窗口 */

        public U16 u16ReportLength;       //调试上报数据的长度，单位：4bytes
        public S32[] s8ReportData;
    }

    public struct L2P_AG_DEBUG_INFO_DATA
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 mu16DataFlag;          /*0：as32ReportData中存放的是上报数据，1：as32ReportData中存放的是上报数据的首地址*/
        public U16 u16DebugID;          //调试上报项标示
        public U8 mu8WinId;                /* 消息打印窗口ID */
        public U8 mu8ReportType;    /* 消息上报类型：
                                                 0---上报消息经解析函数解析后输出到调试窗口；
                                                 1---上报消息直接打印输出到文件；
                                                 2---上报消息不经解析直接打印到调试窗口 */

        public U16 u16ReportLength;       //调试上报数据的长度，单位：4bytes
        public S32[] s8ReportData;
    }
    public struct L2P_DEBUG_INFO_DATA
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 mu16DataFlag;          /*0：as32ReportData中存放的是上报数据，1：as32ReportData中存放的是上报数据的首地址*/
        public U16 u16DebugID;          //调试上报项标示
        public U8 mu8WinId;                /* 消息打印窗口ID */
        public U8 mu8ReportType;    /* 消息上报类型：
                                                 0---上报消息经解析函数解析后输出到调试窗口；
                                                 1---上报消息直接打印输出到文件；
                                                 2---上报消息不经解析直接打印到调试窗口 */

        public U16 u16ReportLength;       //调试上报数据的长度，单位：4bytes
        public S32[] s8ReportData;
    }

    //调试释放消息定义
    public struct PC_AG_DEBUG_INFO_REL
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 u16TargetID;         //调试目标标示：0-	AGI Agent；1-	L1；2-	L2P;3-	all subsystem
        public U8[] u8Pading;
    }

    public struct AG_XX_DEBUG_INFO_REL
    {
        public AGI_MSG_HEADER msgHeader;
        public U16 u16TargetID;         //调试目标标示：0-	AGI Agent；1-	L1；2-	L2P;3-	all subsystem
        public U8[] u8Pading;
    }
    //调试释放回复ACK
    public struct AG_PC_DEBUG_INFO_REL_ACK
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8Cause;               //0-成功 1-失败
    }

    public struct L1_AG_DEBUG_INFO_REL_ACK
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8Cause;               //0-成功 1-失败
    }
    public struct L2P_AG_DEBUG_INFO_REL_ACK
    {
        public AGI_MSG_HEADER msgHeader; //消息头
        public U8 u8Cause;               //0-成功 1-失败
    }
    ////////////////////////////////////
    //调试消息定义 END
    ///////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////
    //设备状态消息定义          END
    /////////////////////////////////////////////////////////////////////////////////////////

    #region  协议跟踪Data的各种消息定义


    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Header
    {
        public UInt32 Reserved;
        public byte Source;
        public byte Destination;
        public UInt16 MessageType;
        public UInt16 TransactionID;
        public UInt16 MsgLen;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct XX_PROTOCOL_DATA
    {
        public Header msgHeader;
        public PROTOCOL_DATA_HEADER_STRU mstProtocolDataHeader;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_COMMEAS_IND
    {
        public Header msgHeader;
        public L1_PHY_COMMEAS_IND_HEADER_STRU mstL1PHYComentIndHeader;
    }

    public struct L1_PHY_COMMEAS_IND_HEADER_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] mau8TimeStampH;      /* 时间戳32位（GPS时间）。存储格式为:
                                        TimeStampH[3]为spare;
                                        TimeStampH[2]表示小时，取值范围0~23;
                                        TimeStampH[1]表示分钟，取值范围0~59;
                                        TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 mu32TimeStampL;         /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public U16 mu16EARFCN;             /* 频点 */
        public U16 mu16PCI;                /* 物理层小区ID 0-503  */
        public U32 mu32MeasSelect;/* 使用bitmap表示后续上报的数据结构，如果对应的bit位为0，则相应的数据结构是空的
                                    Bit0----- RSRP/RSRQ/RSSI
                                    Bit1-----SINR
                                    Bit2-----H
                                    Bit3-----Poweroffset
                                    Bit8---PBCH
                                    Bit9---PCFICH
                                    Bit10---PDCCH
                                    Bit11---PHICH
                                    Bit12---PSS/SSS
                                    Bit13---CRS  */
        //若 mu32MeasSelect的bit0和bit12都为1即mu32MeasSelect & 0x1001 == 0x1001，则存在如下数据结构
        //PSSSSS_RSRPQI_INFO  mstPssSssRsRPQIInfo;
        //若 mu32MeasSelect的bit0和bit13都为1即mu32MeasSelect & 0x2001 == 0x2001，则存在如下数据结构
        //CRS_RSRPQI_INFO  mstPssSssRsrPQIInfo;
        //若 mu32MeasSelect的bit0和bit8都为1即mu32MeasSelect & 0x101 == 0x101，则存在如下数据结构
        //PBCH_RSRPQI_INFO  mstPbchRsrPQIInfo;
        //若 mu32MeasSelect的bit0和bit9都为1即mu32MeasSelect & 0x201 == 0x201，则存在如下数据结构
        //PCFICH_RSRPQI_INFO  mstPcfichRsrPQIInfo;
        //若 mu32MeasSelect的bit3和bit8都为1即mu32MeasSelect & 0x104 == 0x104，则存在如下数据结构
        //PBCH_POWER_INFO  mstPbchPowerInfo;
        //若 mu32MeasSelect的bit3和bit8都为1即mu32MeasSelect & 0x204 == 0x204，则存在如下数据结构
        //PCFICH_POWER_INFO  mstPcfichPowerInfo;
        //若 mu32MeasSelect的bit3和bit12都为1即mu32MeasSelect & 0x1004 == 0x1004，则存在如下数据结构
        //PSSSSS_POWER_INFO  mstPssSssPowerInfo;
        //若 mu32MeasSelect的bit1和bit8都为1即mu32MeasSelect & 0x102 == 0x102，则存在如下数据结构
        //PBCH_SINR_INFO  mstPbchSinrInfo;
        //若 mu32MeasSelect的bit1和bit9都为1即mu32MeasSelect & 0x202 == 0x202，则存在如下数据结构
        //PCFICH_SINR_INFO  mstPcfichSinrInfo;
        //若 mu32MeasSelect的bit1和bit12都为1即mu32MeasSelect & 0x1002 == 0x1002，则存在如下数据结构
        //PSSSSS_SINR_INFO  mstPssSssSinrInfo;
        //若 mu32MeasSelect的bit1和bit13都为1即mu32MeasSelect & 0x1002 == 0x1002，则存在如下数据结构
        //CRS_SINR_INFO  mstCrsSinrInfo;
        //若 mu32MeasSelect的bit2为1即mu32MeasSelect & 0x4 == 0x4，则存在如下数据结构
        //H_INFO  mstHInfo;
    }

    public struct PSS_MEASUREMENT
    {
        public S16 ms16PSS_RSSI;            /* 主同步RSSI 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16PSS_RP;              /* 主同步RP 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16PSS_RQ;              /* 主同步RQ 数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB */
        public U16 mu16Padding;
    }

    public struct SSS_MEASUREMENT
    {
        public S16 ms16SSS_RSSI;            /* 辅同步RSSI 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16SSS_RP;              /* 辅同步RP 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16SSS_RQ;              /* 辅同步RQ数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB */
        public U16 mu16Padding;
    }

    public struct PSSSSS_RSRPQI_INFO
    {
        public PSS_MEASUREMENT mstPssRsrpqiInfo;/* 主同步的RSRP/RSRQ/RSSI信息 */
        public SSS_MEASUREMENT mstSssRsrpqiInfo;    /* 辅同步的RSRP.RSRQ/RSSI信息 */
    }

    public struct CRS_MEASUREMENT
    {
        public S16 ms16CRS_RSSI;            /* Cell specific RS RSSI 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16CRS_RP;              /* Cell specific RS RP 数值范围:-1200~ 0, 单位0.125dBm。对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16CRS_RQ;              /* Cell specific RS RQ 数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB */
        public U16 mu16Padding;
    }

    public struct PBCH_MEASUREMENT
    {
        public S16 ms16PBCH_RP;             /* PBCH RP：数值范围:-1200~ 0, 单位0.125dBm。对应实际信号范围为：-150dBm~0dBm在Cell Band=0时，此值无效 */
        public S16 ms16PBCH_RQ;             /* PBCH RQ：数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB 在Cell Band=0时，此值无效 */
    }

    public struct CRS_SINR
    {
        public S16 ms16CRS_SINR;            /* Cell specific RS SINR： 数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB  */
        public U16 mu16Padding;
    }

    public struct OFDM_SYMBOL_POWER
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public OFDM[] ofdmOFDM;
    }

    public struct OFDM
    {
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public S16[] mas16OFDM_Symbol_Power;
    }

    public struct PBCH_EVM
    {
        public U16 mu16PBCH_EVM;            /* PBCH信道的矢量误差测量值： 数值范围:0~ 32768, 单位：1/32768 对应实际信号范围为：0~1 */
        public U16 mu16Padding;
    }

    public struct PBCH_BLER
    {
        public U16 mu16PBCH_BLER;           /* PBCH信道的误块率统计值：数值范围:0~ 32768, 单位：1/32768 对应实际信号范围为：0~1 */
        public U16 mu16Padding;
    }

    public struct SUBFRAME_RSSI
    { 
        /* 对照PC_AG_SPECIFIED_CELL_SCAN_REQ/PC_AG_UNSPECIFIED_CELL_SCAN_REQ中mu16MeasSubFrameInd指示，
       将相应子帧的测量结果填写在mas16SubFrame_RSSI相应索引位置 */
       [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
       public S16[] mas16SubFrame_RSSI;
                                 /* 指定子帧的RSSI。
                                    数值为(-1200 ~ 0)时：单位0.125dBm，对应实际信号范围为：-150dBm~0dBm；
                                    数值为(0x7FFF   )时：表明该子帧没有进行测量，此RSSI值无效。*/   
    }

    public struct SLOT_RSSI
    { 
        /* 对照PC_AG_SPECIFIED_CELL_SCAN_REQ/PC_AG_UNSPECIFIED_CELL_SCAN_REQ中mu32MeasSlotInd指示，
       将相应时隙的测量结果填写在mas16SlotRSSI相应索引位置 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public S16[] mas16SlotRSSI;
                                 /* 指定时隙的RSSI。
                                    数值为(-1200 ~ 0)时：单位0.125dBm，对应实际信号范围为：-150dBm~0dBm
                                    数值为(0x7FFF   )时：表明该时隙上没有进行测量，此RSSI值无效 */
    }
    public struct FRAME_RSSI
    {
        public U16 mu16Pading1;
        public S16 mu16Frame_RSSI;          /* 整个无线帧的RSSI：数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm   */
    }

    public struct RB_RSSI
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public RB[] mas16SlotRSSI;
    }

    public struct RB
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public S16[] mas16Slot;
    }

    public struct CRS_RSRPQI_INFO
    {
        public CRS_MEASUREMENT mstCrs0RsrpqiInfo;   /* CRS0的RSRP/RSRQ/RSSI信息 */
        public CRS_MEASUREMENT mstCrs1RsrpqiInfo;   /* CRS1的RSRP/RSRQ/RSSI信息 */
    }

    public struct PBCH_RSRPQI_INFO
    {
        public S16 ms16_RSSI;            /* Cell specific RS RSSI 数值范围:-1200~ 0, 单位0.125dBm。 对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16_RSRP;            /* Cell specific RS RP 数值范围:-1200~ 0, 单位0.125dBm。对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16_RRSRQ;           /* Cell specific RS RQ 数值范围:-800~ 800, 单位：0.0625dB 对应实际信号范围为：-50dB~50dB */
        public U16 mu16Padding;
    }


    //V1.0版本
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PROTOCOL_DATA
    {
        public Header msgHeader;
        public L1_PROTOCOL_DATA_HEADER_STRU mstL1ProtocolDataHeader;
        public U8  mu8CFINum;              /* 如果是下行的话表示CFI个数 */
        public U8 mu8UeNum;               /* 当前时刻监听的用户个数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Padding;         /* 填充 */
        //!!!消息码流中，如果之前的direction为downlink,则后续紧跟
        //L1_DL_UE_MEAS  mastL1DlUeMeas[mu8UeNum];
        //如果之前的direction为uplink，则后续紧跟
        //S8 mas16Sinr[100];         /* 调度用户的平均SINR，按照PRB上报SINR，没有调度的PRB值填充0x10 */
        //L1_UL_UE_MEAS  mastL1UlUeMeas[mu8UeNum];
    }


    //V1.0版本
    public struct L1_PROTOCOL_DATA_HEADER_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] mau8TimeStampH ;      /* 时间戳32位（GPS时间）。存储格式为:
                                        TimeStampH[3]为spare;
                                        TimeStampH[2]表示小时，取值范围0~23;
                                        TimeStampH[1]表示分钟，取值范围0~59;
                                        TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 mu32TimeStampL;         /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public U16 mu16EARFCN;             /* 频点 */
        public U16 mu16PCI;                /* 物理层小区ID 0-503  */
        public U16 mu16FrameNumber;        /* 空口接收无线帧号  */
        public U8  mu8SubFrameNumber;      /* 空口接收无线子帧号 */
        public U8  mu8Direction;           /* {UPLINK, DOWNLINK} */
    }

    public struct L1_DL_UE_MEAS
    {
        public U8 mu8RNTIType; /* {RA_RNTI,C_RNTI,P_RNTI,SI_RNTI,SPS-RNTI, TPC_PUSCH_RNTI,TPC_PUCCH_RNTI} */
        public U8 mu8MeasSelect;/*  使用bitmap表示
                                     Bit0----- RSRP/RSRQ/RSSI
                                     Bit1-----SINR
                                     Bit2-----H
                                     Bit3-----Poweroffset   */
        public U8 mu8TransMode;/*  UE的传输模式，TM1，TM2，--TM7，TM8 */
        public U8 mu8TransType;/*  0--- SFBC 1---其他  */
        public U32 mu32UeIndValue;/*  RNTI相对应的值 */
        //UE下行消息码流，若mu8MeasSelect & 0x1 = 1,则后续有数据码流如下：
        //UE_RSRPQI_INFO  mstUeRsrPqiInfo;
        //若mu8MeasSelect & 0x2 = 2,则后续有数据码流如下：
        //UE_SINR_INFO  mstUeSinrInfo;
        //若mu8MeasSelect & 0x8 = 8,则后续有数据码流如下：
        //UE_POWER_INFO  mstUePowerInfo;
    }

    public struct UE_RSRPQI_INFO
    {
        public S16 ms16UeRssi;/* 调度用户业务信道的RSSI,数值范围:-1200~ 0, 单位0.125dBm。
                                   对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16UeRsrp;/* 调度用户业务信道的RSRP,数值范围:-1200~ 0, 单位0.125dBm。
                                   对应实际信号范围为：-150dBm~0dBm */
        public S16 ms16UeRsrq; /* 调度用户业务信道的RSSQ,数值范围:-800~ 800, 单位0.0625dB。
                                   对应实际信号范围为：-50dBm~50dBm */
        public S16 ms16DrsRssi;/* 调度用户DRS的RSSI,数值范围:-1200~ 0, 单位0.125dBm。
                                   对应实际信号范围为：-150dBm~0dBm TM7有效 */
        public S16 ms16DrsRssp;/* 调度用户DRS的RSSI,数值范围:-1200~ 0, 单位0.125dBm。
                                   对应实际信号范围为：-150dBm~0dBm TM7有效 */
        public S16 ms16DrsRssq;/* 调度用户DRS的RSSQ,数值范围:-800~ 800, 单位0.0625dB。
                                   对应实际信号范围为：-50dBm~50dBm TM7有效 */
    }

    public struct UE_SINR_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public S16[] mas16UeSinr;/* UE使用PRB的平均SINR,调度用户每个PRB的SINR，如果无效值填充0xfff */
        public S16 ms16DrsSinr;/* 调度用户的DRS的平均SINR， TM7有效 */
        public S16 ms16Padding;/* 填充 */
    }
    public struct UE_POWER_INFO
    {
        public S16 ms16PdschPower_a;/* 没有CRS的PDsCH符号功率相对于CRS的功率差值，单位：DB,负值表示比CRS小 */
        public S16 ms16PdschPower_b;/* 有CRS的PDsCH符号功率相对于CRS的功率差值，单位：DB，负值表示比CRS小  */
    }

//    public struct L1_UL_UE_MEAS
//    {
//        public U8 mu8RNTIType;             /* {RA_RNTI,C_RNTI,P_RNTI,SI_RNTI,SPS-RNTI, TPC_PUSCH_RNTI,TPC_PUCCH_RNTI} */
//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
//        public U8[] mau8Padding;
//        public U32 mu32UeIndValue;/*  RNTI相对应的值 */
//        public S16 ms16Power;/*  调度用户的检测功率值，此处值为monitor检测的结果，
//                                    与实际NodeB结果不一样，只是一个相对值用作观测变化趋势 */
//        public U16 mu16Ta;/* 调度上行用户的TA值，此处值为monitor检测的结果，
//                                   与实际NodeB结果不一样，只是一个相对值用作观测变化趋势 */
//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
//        public U8[] mu8Sinr;/* 调度用户的平均SINR，按照PRB上报SINR，没有调度的PRB值填充0xff */
//    } 

  
    public struct L1_UL_UE_MEAS
    {
        public U8  mu8RNTIType;             /* {RA_RNTI,C_RNTI,P_RNTI,SI_RNTI,SPS-RNTI, TPC_PUSCH_RNTI,TPC_PUCCH_RNTI} */
        public U8  muChType;                /* enum ChTypeOptions{PDCCH, PDSCH, PBCH, PCFICH, PHICH, PUSCH, PRACH, PUCCH, MEASUREMENT}*/
        public U16 mu16UeIndValue;         /*  RNTI相对应的值 */
    
        public S16 ms16Power;              /*  调度用户的检测功率值，此处值为monitor检测的结果，
                                        与实际NodeB结果不一样，只是一个相对值用作观测变化趋势 */
        public U16 mu16Ta;                 /* 调度上行用户的TA值，此处值为monitor检测的结果，
                                       与实际NodeB结果不一样，只是一个相对值用作观测变化趋势 */
        public S8  ms8Sinr;                /*此用户对应的SINR, 单位:dB, 粒度: 0.5*/
        public U8 mu8PucchFormat;                /* enum {FORMAT1 = 0, FORMAT1A, FORMAT1B, FORMAT2, FORMAT2A, FORMAT2B }*/

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[]  mau8Padding;
    
    }


    /* V1.0 版本 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_PROTOCOL_DATA  
    {
        public Header msgHeader;
        public l2P_PROTOCOL_DATA_HEADER_STRU mstProtocolDataHeader;

        public U16 mu16DataType;           /* 协议上报类别标识，
                                    1  ----  L2P_MAC_PRACH
                                    2  ----  L2P_MAC_DCIINFO
                                    3  ----  L2P_MAC_HICHINFO
                                    4  ----  L2P_MAC_CE
                                    5  ----  L2P_MAC_SUBHEAD
                                    6  ----  L2P_RLC
                                    7  ----  L2P_PDCP
                                    8  ----  L2P_RRC */
        public U16 mu16DataLength;         /* 以4 Byte为单位 */
    }

    /* V1.0版本 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct l2P_PROTOCOL_DATA_HEADER_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] mau8TimeStampH;      /* 时间戳32位（GPS时间）。存储格式为:
                                    TimeStampH[3]为spare;
                                    TimeStampH[2]表示小时，取值范围0~23;
                                    TimeStampH[1]表示分钟，取值范围0~59;
                                    TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 mu32TimeStampL;         /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public U16 mu16EARFCN;             /* 频点 */
        public U16 mu16PCI;                /* 物理层小区ID 0-503  */
        public U16 mu16FrameNumber;        /* 空口接收无线帧号  */
        public U8 mu8SubFrameNumber;      /* 空口接收无线子帧号 */
        public U8 mu8Direction;           /* {UPLINK, DOWNLINK} */
        public U8 mu8PhyChType;           /* 物理信道类型  mu16DaraType == 1,2,3时此字段以及后续字段无效 */
        public U8 mu8TrChType;            /* 传输信道类型 */
        public U8 mu8RntiType;            /* {RA_RNTI,C_RNTI,P_RNTI,SI_RNTI,SPS-RNTI, TPC_PUSCH_RNTI,TPC_PUCCH_RNTI} */
        public U8 mau8Padding;
        public U32 mu32RntiValue;          /*  RNTI相对应的值 */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PRACH_INFO_STRU
    {
        public U8 mu8fld;                 /*  检测到的PRACH的在 tId subframe中频率位置的索引。取值范围：（0,…, 5） */
        public U8 mu8NPrid;               /*  在同一tId、fId资源上检测到的preamble id个数；最大64； */
        public U8 mu8Preambled;           /*  共nPRID个此字段，检测到的preamble id值（0~63） */
        public U8 mu8Padding;
        public U16 mu16Ta;                 /*  P序列信道估计的峰值位置 */
        public S16 ms16Power;              /*  P序列的峰值功率  */
    }

    /* V1.0 版本 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_MAC_PRACH  
    {
        public U16 mu16RadioFrameNum;      /* PRACH传输的空口帧号  0 * … 1023 */
        public U8 mu8Prach;               /* 检测到PRACH的频域资源数 */
        public U8 mu8Tld;                 /* 检测到的PRACH的第一个subframe标号。取值范围：（0,…, 9） */
        public U16 mu16DlCarrierFreq;      /* 小区下行链路载波频点。E-UTRA绝对射频信道编号 */
        public U16 mu16Pci;                /*  小区标识 （0--503） */
        public U8 mu8CellIndex;           /*  驻留小区索引 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Padding;
        //紧跟mu8Prach个prach数据结构 即 PRACH_INFO_STRU  mstPrachInfo[mu8Prach]
    }
    //////////////////////////////////////////////
    /// <summary>
    /// 待补充，修改日期10.9
    /// </summary>
    public struct DCI_FORMAT0_Type
    {
        public U8 hoppingFlag;
        public U8 mcsIndex;
        public U8 ndi;
        public U8 tpc;
        public U32 resourceAlloc;
        public U8 shiftDMRS;
        public U8 ulIndex;
        public U8 dai;
        public U8 cqiReq;
    }
    public struct DCI_FORMAT1_Type
    { 
        public U8 mcsIndex;
        public U8 harqProcId;
        public U8 ndi;
        public U8 rv;
        public U8 tpcPucch;
        public U8 dai;
        public U8 padding;
        public U8 resAllocatType;
        public U32 resourceAlloc;
    }
    public struct DCI_FORMAT1A_Type
    {
        public U8 usage;
        public U8 preambleIndex;
        public U8 prachMaskIndex;
        public U8 resAllocatType;
        public U32 resourceAlloc;
        public U8 mcsIndex;
        public U8 harqProcId;
        public U8 ndi;
        public U8 rv;
        public U8 tpcPucch;
        public U8 dai;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =2)]
        public U8[] Padding;
    }

    public struct DCI_FORMAT1B_Type
    {

    }
    public struct DCI_FORMAT1C_Type
    { 
         public U8  Gap;
         public U8 tbSizeIndex;
         public U16 resourceAlloc;
    }
    public struct DCI_FORMAT1D_Type
    {

    }
    public struct DCI_FORMAT2_Type
    { 
    
    }
    public struct DCI_FORMAT2A_Type
    { 
        public U8 tpcPucch;
        public U8 dai;
        public U8 harqProcId;
        public U8 swapFlag;
        public U8 mcsIndex1;
        public U8 ndi1;
        public U8 rv1;
        public U8 mcsIndex2;
        public U8 ndi2;
        public U8 rv2;
        public U8 precodingInfo;
        public U8 resAllocatType;
        public U32 resourceAlloc;
    }
    public struct DCI_FORMAT3_Type
    { 
    
    }
    //////////////////////////////////////////////

    public struct PDCCH_DCI_STRU
    {
        public U16 mu16Rnti;               /* 此DCI对应的RNTI */
        public U16 mu16UserIndex;          /* AGT 内部的用户标识，非用户级为0xff，AGI不用此信息 */
        public U16 mu16CceIndex;           /* 此DCI所在的第一个CCE索引 */
        public U8 mu8AggregationLvl;      /* 聚合等级 {1,2,4,8} */
        public U8 mu8RntiType;            /* RNTI类型 */
        public U8 mu8SpsCtrl;             /* SPS激活和释放控制。取值范围：{SPS_ACTIVE, SPS_REL，SPS_NORMAL_GRANT}
                                   此字段仅当rntiType = SPS_C_RNTI有效 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Padding;
        public U32 mu32DciFormat;          /* DCI_Format0,DCI_Format1,DCI_Format1A,DCI_Format1B,DCI_Format1C,DCI_Format1D,
                                   DCI_Format2,DCI_Format2A,DCI_Format2B,DCI_Format3,DCI_Format3A */
       // public U32 mu32DciBodyLen;         /* DCI原始码流的长度，单位：4字节 */
    // !!!后续为长度mu32DciBodyLen个字的DCI原始数据码流
    //!!! 如果mu8PdcchRptSelect的bit2有效，则后续数据结构 PDCCH_POWER_INFO mstPdcchPowerInfo;
    //!!! 如果mu8PdcchRptSelect的bit0有效,则后续数据结构 PDCCH_RSRPQI_INFO mstPdcchRsrpqiInfo;
    //!!! 如果mu8PdcchRptSelect的bit1有效，则后续数据结构 PDCCH_SINR_INFO mstPdcchSinrInfo;
    }

    public struct L2P_MAC_DCI_STRU
    {
        public U16 mu16RadioFrameNumber;   /* PDCCH空口帧号。取值范围：0 * … 1023 */
        public U8  mu8SubFramNumber;       /* PDCCH空口子帧号。取值范围：0 * … 9  */
        public U8  mu8nDci;                /* 本子帧上报的DCI个数 */
        public U16 mu16DlCarrierFreq;      /* 小区下行链路载波频点。E-UTRA绝对射频信道编号 */
        public U16 mu16Pci;                /*  小区标识 （0--503） */
        public U8  mu8CellIndex;           /*  驻留小区索引 */
        public U8  mu8PdcchRptSelect;      /*  使用bitmap表示后续上报的数据结构，如果对应的bit位为0，则相应的数据结构是空的
                                    Bit0----- RSRP/RSRQ/RSSI
                                    Bit1-----SINR
                                    Bit2-----Poweroffset  */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[]  mau8Padding;         /* 填充 */
        //后续有mu8nDci个DCI数据结构 即 PDCCH_DCI_STRU  mstPdcchDciInfo[mu8nDci]
    }

    public struct L2P_MAC_HICH_STRU
    {
        public U16 mu16RadioFrameNumber;   /* HICH空口帧号。取值范围：0 * … 1023 */
        public U8 mu8SubFramNumber;       /* HICH空口子帧号。取值范围：0 * … 9  */
        public U8 mu8nHi;                 /* 反馈指示的个数 */
        public U16 mu16DlCarrierFreq;      /* 小区下行链路载波频点。E-UTRA绝对射频信道编号 */
        public U16 mu16Pci;                /*  小区标识 （0--503） */
        public U8 mu8CellIndex;           /*  驻留小区索引 */
        public U8 mu8HichRptSelect;       /*  使用bitmap表示后续上报的数据结构，如果对应的bit位为0，则相应的数据结构是空的
                                    Bit0----- RSRP/RSRQ/RSSI
                                    Bit1-----SINR
                                    Bit2-----Poweroffset  */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Padding;         /* 填充 */
    }

    public struct L2P_MAC_TB_INFO_STRU
    {
        public U16 mu16TBLen;              /* TB块的大小 */
        public U16 mu16MacCeNum;           /* TB块中MAC CE个数,如果在协议跟踪配置中，MAC CE的bit位无效，则此值为0 */
        public U16 mu16SubHeaderLen;       /* TB块的MAC子头原始码流的长度，单位4 Byte, 如果在协议跟踪配置中，MACSUB HEAD的bit位无效，则此值为0，后续的BodyHead1数据结构无效 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[]  mau8Padding;
        public L2P_MAC_HARQ_STRU  mstHarqInfo;  /* TB块的HARQ信息 */
    }

    public struct L2P_MAC_DATA_STRU
    {
        public U8  mu8TbNum;               /* TB个数，目前此值只有1和2 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[]  mau8Padding;         /* 填充 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public L2P_MAC_TB_INFO_STRU[]        mastMacTbInfo;    /* mu8TbNum决定有效的TB,如果mu8TbNum为1，则TB2的信息都写为0 */

        //如果TB1的mu16MacCeNum不为0，则后续数据流为mu16MacCeNum个L2P_MAC_CE_STRU
        //如果TB1的mu16SubHeaderLen不为0，则后续的数据为长度mu16SubHeaderLen *4个Byte的MAC子头码流
        //如果TB2的mu16MacCeNum不为0，则后续数据流为mu16MacCeNum个L2P_MAC_CE_STRU
        //如果TB2的mu16SubHeaderLen不为0，则后续的数据为长度mu16SubHeaderLen *4个Byte的MAC子头码流
    }
    public struct L2P_PDCP_DATA_STRU
    {
        public U8  mu8PduNum;              /* 上报PDU的个数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[]  mau8Padding;

    //!!!后续紧跟 mu8PduNum 个L2P_RLC_PDCP_PDU_STRU结构
    };

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PROTOCOL_DATA_HEADER_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] TimeStampH;//[4] ;           /* 时间戳32位（GPS时间）。存储格式为:
        //TimeStampH[3]为spare;
        //TimeStampH[2]表示小时，取值范围0~23;
        //TimeStampH[1]表示分钟，取值范围0~59;
        //TimeStampH[0]表示秒，  取值范围0~59 */
        public UInt32 TimeStampL;              /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public byte mu8RadioType;            /* {TDD,FDD} */
        public byte mu8Pading0;              /* 填充 */
        public UInt16 mu16EARFCN;              /* 频点 */
        public UInt16 mu16PCI;                 /* Grp,Sec    */
        public UInt16 mu16FrameNumber;         /* 空口接收无线帧号  */
        public byte mu8SubFrameNumber;       /* 空口接收无线子帧号 */
        public byte mu8Direction;            /* {UPLINK, DOWNLINK} */
        public byte mu8Pading1;
        public byte mu8RNTIType;             /* {RA_RNTI,C_RNTI,P_RNTI,SI_RNTI,SPS-RNTI, TPC_PUSCH_RNTI,TPC_PUCCH_RNTI} */
        public UInt16 mu16RNTIValue;           /* RA_RNTI:0x0002-0x003b */
        /* C_RNTI: 0x003e-0xFFF2 */
        /* P_RNTI: 0xFFFE */
        /* SI_RNTI: 0xFFFF  */
        public byte mu8ProtocolLayerType;    /* {PHY_CTRL,
                                    PHY_MEAS,
                                    PHY_STAT,
                                    MAC_TB,
                                    MAC_CE,
                                    MAC_RAR,
                                    MAC_HARQ,
                                    RLC_DATA_PDU,
                                    RLC_CTRL_PDU,
                                    PDCP_DATA_PDU,
                                    PDCP_CTRL_PDU,
                                    RRCNAS_MSG} */
        public byte mu8Pading2;
        public UInt16 mu16ProtocolDataLength;  /* Unit: 4Byte(TBD)*/
        public byte mau8Pading3;
        public byte mau8Pading4;

        /* 具体协议结构体定义：参见下述XX_STRU和L1_XX_STRU */
    }

    #region XX_STRU 消息体

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_MAC_TB_STRU
    {
        public U8 mu8PhylChanneltype;      /* 物理信道类型:
                                    {LTE_PHY_CHANNEL_PBCH, LTE_PHY_CHANNEL_PDSCH,  LTE_PHY_CHANNEL_PUSCH,
                                     LTE_PHY_CHANNEL_PMCH, LTE_PHY_CHANNEL_PRACH,  LTE_PHY_CHANNEL_PDCCH,
                                     LTE_PHY_CHANNEL_PUCCH,LTE_PHY_CHANNEL_PHICH,  LTE_PHY_CHANNEL_PCFICH} */
        public U8 mu8TrsChannelType;       /* 传输信道类型:
                                    {LTE_TRANS_CHANNEL_BCH,    LTE_TRANS_CHANNEL_DL_SCH,
                                     LTE_TRANS_CHANNEL_UL_SCH, LTE_TRANS_CHANNEL_PCH,
                                     LTE_TRANS_CHANNEL_MCH,    LTE_TRANS_CHANNEL_RACH} */
        public U16 mu16TBLength;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_MAC_CE_STRU
    {
        public U8 mu8MacCeType;            /* {MAC_CE_TYPE_SHORT_BSR,   MAC_CE_TYPE_TRUNCATED_BSR
                                     MAC_CE_TYPE_LONG_BSR,    MAC_CE_TYPE_CRNTI,
                                     MAC_CE_TYPE_DRX_COMMAND, MAC_CE_TYPE_UE_CR_ID,
                                     MAC_CE_TYPE_TIMING_ADVANCE_COMMADN, MAC_CE_TYPE_PHR} */
        public U8 mu8CeLength;             /* MAC CE 长度 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public U8[] mau8CeData;//[MAX_MAC_CE_LENGTH];
        /* 解码后的MAC CE内容，具体参见下面的结构体:MAC_CE_XX_STRU */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_SHORT_BSR_STRU
    {
        public U8 mu8LCG_ID;               /* The Logical Channel Group ID */
        public U8 mu8BufferSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_TRUNCATED_BSR_STRU
    {
        public U8 mu8LCG_ID;               /* The Logical Channel Group ID */
        public U8 mu8BufferSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_LONG_BSR_STRU
    {
        public U8 mu8Lcg0BufferSize;
        public U8 mu8Lcg1BufferSize;
        public U8 mu8Lcg2BufferSize;
        public U8 mu8Lcg3BufferSize;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_CRNTI_STRU
    {
        public U16 mu16CRNTI;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
    }

    //AC_CE_DRX_COMMAND_STRU:该消息仅有消息头，不存在消息体

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_UE_CR_ID_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public U8[] mau8UeCrID;//[6];            /* UE Contention Resolution Identity */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_TIMING_ADVANCE_COMMAND_STRU
    {
        public U8 mu8TAC;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;//[3];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_PHR_STRU
    {
        public U8 mu8PH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;//[3];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct RAR_STRU
    {
        public U16 mu16TAC;                 /* Timing Advance Command， 11bits */
        public U16 mu16TcRnti;              /* Temporary C-RNTI */
        public U32 mu32UIGrant;             /* UpLink Grant, 20 bits      */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_MAC_RAR_STRU
    {
        public U8 mu8nRAR;                 /* RAR结构的个数。取值范围：1 ~ MAX_RAR_NUM */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mu8Pading;
        public RAR_STRU RarInfo;
        //!!!后续紧跟mu8nRAR个RAR_STRU在消息码流中，相当于存在
        //   RAR_STRU mastRarLisr[mu8nRAR];

    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_MAC_HARQ_STRU
    {
        public U8 mu8HarqID;                /* Harq process ID */
        public U8 mu8TransCnt;              /* RAR结构的个数 */
        public U8 mu8eResult;               /* 0-SUCCESS   1-FAILURE */
        public U8 mu8Pading;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_DATA_PDU_STRU
    {
        public U8 mu8PhylChanneltype;      /* 物理信道类型 */
        public U8 mu8TrsChannelType;       /* 传输信道类型 */
        public U8 mu8LchChannelType;       /* 逻辑信道类型 */
        public U8 mu8Padding;
        public U8 mu8LchChannelId;         /* 逻辑信道ID */
        public U8 mu8DataMode;             /* RLC模式:
                                    {DATAMODE_TM_PDU, DATAMODE_UM_PDU, DATAMODE_AM_PDU, DATAMODE_AM_SEGMENT} */
        public U16 mu16SN;                  /* 序列号，UM/AM有效 */
        public U16 mu16RcvWinRemain;        /* PDU长度 */
        public U16 mu16Pading;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_CTRL_PDU_STRU
    {
        public U8 mu8PhylChanneltype;      /* 物理信道类型 */
        public U8 mu8TrsChannelType;       /* 传输信道类型 */
        public U8 mu8LchChannelType;       /* 逻辑信道类型 */
        public U8 mu8LchChannelId;         /* 逻辑信道ID */
        public U8 mu8CPT;                  /* 控制PDU类型 */
        public U8 mu8ACK_SN;
        public U8 mu8E1;                   /* 0,不存在NACK_SN,E1,E2; 1,存在NACK_SN,E1,E2 */
        public U8 mu8E2;                   /* 0,不存在此NAC K的SOstart和SOend; 1, 存在此NAC K的SOstart和SOend */
        public U16 mu16NACK_SN;
        public U16 mu16SOstart;             /* 丢失PDU的起始位置 */
        public U16 mu16SOend;               /* 丢失PDU的结束位置 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading;//[2];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_PDCP_DATA_PDU_STRU
    {
        public U8 mu8PhylChanneltype;      /* 物理信道类型 */
        public U8 mu8TrsChannelType;       /* 传输信道类型 */
        public U8 mu8LchChannelType;       /* 逻辑信道类型 */
        public U8 mu8LchChannelId;         /* 逻辑信道ID */
        public U8 mu8RbType;               /* {SRB,DRB} */
        public U8 mu8RbId;
        public U16 mu16SN;                  /* 序列号 */
        public U16 mu16PduLength;
        public U16 mu16Pading;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_PDCP_CTRL_PDU_STRU
    {
        public U8 mu8PhylChanneltype;      /* 物理信道类型 */
        public U8 mu8TrsChannelType;       /* 传输信道类型 */
        public U8 mu8LchChannelType;       /* 逻辑信道类型 */
        public U8 mu8LchChannelId;         /* 逻辑信道ID */
        public U8 mu8RbType;               /* {SRB, DRB} */
        public U8 mu8RbId;
        public U16 mu16Pading;
        public U32 mu32FMS;                 /* 第一个丢失的SDU的SN */
    }

    /* V1.0 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_PDCP_PDU_STRU
    {
        public U8  mu8LchType;             /* 逻辑信道类型   0--BCCH   1--CCCH  2--PCCH
                                              3--DCCH  4--MCCH   5--DTCH  6--MTCH  */
        public U8  mu8LchId;               /* 逻辑信道ID，如果两个逻辑信道ID一样的话，按照实际SDU的顺序存放 */
        public U8  mu8TBIndex;             /* TB索引，主要是考虑双流的时候不同TB块的逻辑信道ID一样的情况，此处值为0和1 */
        public U8  mu8PduType;             /* 表示是数据PDU还是状态PDU ,1--数据PDU，后面是原始子头码流
                                   2--状态PDU，后面原始状态PDU码流 */
        public U8  mu8RlcPduType;          /* RLC PDU类型: {DATAMODE_TM_PDU, DATAMODE_UM_PDU, DATAMODE_AM_PDU, DATAMODE_AM_SEGMENT} */
        public U8  mu8RbType;              /* {SRB,DRB} */
        public U8 mu8RlcSnLen;              /*RLC UMD PDU的SN长度，取值为5或10*/
        public U8 mu8PdcpSnLen;              /*PDCP Data PDU的SN长度，取值为7或者12*/
        public U32 mu32BodyHeadLen;        /* 对应后续码流的长度，单位： 4 Byte */
        //后续的消息流为 长度mu32BodyHeadLen *4个Byte的PDU原始码流
    }

    /* V1.0 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RRCNAS_MSG_STRU
    {       
        public U8 mu8LchType;             /* 逻辑信道类型 */
        public U8 mu8LchId;               /* 逻辑信道ID */
        public U8 mu8RbId;
        public U8 mu8TBIndex;             /* TB索引，主要是考虑双流的时候不同TB块的逻辑信道ID一样的情况，此处值为1和2 */
        public U16 mu16MsgType;             /* 消息类型，由HCH提供宏定义TBD */
        public U16 mu16RrcOrNas;            /* 0-rrc; 1-nas */
        public U16 mu16PduLength;           /* 取值范围：1 ~ MAX_PDU_LENGTH */
        public U16 mau16Padding;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public U8[] mu8MessageName;      /* 空口消息名，字符串*/
        //!!!后续紧跟((mu16PduLength + 3) / 4) * 4个Byte的PduData在消息码流中，相当于存在
        //U8  mau8DATA[((mu16PduLength+3)/4)*4];

        /* V0.1 */
        //public U8 mu8PhylChanneltype;      /* 物理信道类型 */
        //public U8 mu8TrsChannelType;       /* 传输信道类型 */
        //public U8 mu8LchChannelType;       /* 逻辑信道类型 */
        //public U8 mu8LchChannelId;         /* 逻辑信道ID */
        //public U8 mu8RbType;
        //public U8 mu8RbId;
        //public U16 mu16MsgType;             /* 消息类型，由HCH提供宏定义TBD */
        //public U16 mu16RrcOrNas;            /* 0-rrc; 1-nas */
        //public U16 mu16PduLength;           /* 取值范围：1 ~ MAX_PDU_LENGTH */

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        //public U8[] mu8MessageName;      /* 空口消息名，字符串*/ 

        //!!!后续紧跟((mu16PduLength + 3) / 4) * 4个Byte的PduData在消息码流中，相当于存在
        //U8  mau8DATA[((mu16PduLength+3)/4)*4];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct TB_INFO_STRU
    {
        public U8 mu8ModulationScheme;      /* 调制类型。{QPSK、16QAM、64QAM} */
        public U8 mu8NDI;                   /* 新数据指示 */
        public U8 mu8RV;                    /* 冗余版本号 */
        public U8 mu8Padding;
        public U16 mu16CodeRate;             /* 编码速率,定标Q(10)。参见TS36.213 Table 7.2.3-1: 4-bit CQI Table */
        public U16 mau8TBSize;               /* 参见TS36.213 Table 7.1.7.2.1-1: Transport block size table (dimension 27×110) */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct LISTEN_UE_INFO_STRU
    {
        public U8 mu8CFI;                   /* 被跟踪UE所在小区当前子帧控制域长度 */
        public U8 mu8TransMode;             /* 传输模式 */
        public U8 mu8RI;                    /* Layer数。可选参数。只对TM3/4/5/6/8存在 */
        public U8 mu8PMI;                   /* 预编码矩阵索引。可选参数。只对TM4/5/6存在 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public U8[] mau8PRB;//[16];              /* PRB资源指示。Bitmap的形式指示：
        //Byte1 Bit 0 – PRB0
        //Byte1 Bit 1 – PRB1
        //……
        //Byte 13  Bit 3 – PRB99
        //各bit位：0---无效， 1 --- 有效 */
        public U8 mu8DCIFormat;             /* DCI格式 */
        public U8 mu8TPC;                   /* 如果DCIFormat是10 --- Format 3A是1个bit
                                     其他DCIFormat，TPC为2bit */
        public U8 mu8DAI;                   /* Downlink Assignment Index */
        public U8 mu8HarqProcId;            /* HARQ进程号 */

        public U8 mu8TBNum;                 /* TB个数，{1,2} */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Padding;//[3];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public TB_INFO_STRU[] mstTbInfo;//[2];    /* 前mu8TBNum个元素有效 */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_CTRL_STRU
    {
        public U8 mu8UeNum;                 /* 当前子帧监听的UE个数。取值范围：1 ~ MAX_LISTEN_UE_NUM */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;//[3];

        //!!!后续紧跟mu8UeNum个LISTEN_UE_INFO_STRU在消息码流中，相当于
        //   LISTEN_UE_INFO_STRU mastUeInfoList[mu8UeNum];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_DL_MEAS_STRU
    {
        public U16 mu16EARFCN;              /* 被跟踪UE所在小区的频点 */
        public U16 mu16PCI;                 /* 被跟踪UE所在小区的物理小区标识 */
        public S32 ms32RSSI;                /* E-UTRA Carrier Received Signal Strength Indicator
                                    数值范围:-1200~ 0, 单位0.125dBm。
                                    对应实际信号范围为：-150dBm~0dBm */
        public S32 ms32RSRQ;                /* Reference Signal Received Quality
                                    数值范围:-800~ 800, 单位：0.0625dB
                                    对应实际信号范围为：-50dB~50dB */
        public U32 mu32UeNum;               /* 监测UE的个数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public S32[] mas32SINR;//[MAX_MONITOR_UE_NUM] = MAX_CELL_NUM * MAX_USER_NUM_PER_CELL 最大4小区*每小区4用户
        /* UE使用PRB的平均SINR
           数值范围:-800~ 800, 单位：0.0625dB
           对应实际信号范围为：-50dB~50dB*/
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_UL_UE_MEAS_STRU
    {
        public U8 mu8TADV;                 /* 定时提前，对应TS36.214 Section5.2.4
                                    数值范围：0~63，单位16TS
                                    对应实际信号范围为：-31~32 */
        public U8 mu8Padding;              /* 补齐4Byte */
        public S16 ms16SNR;                 /* 数值范围:-800~ 800, 单位：0.0625dB
                                    对应实际信号范围为：-50dB~50dB */
        public S32 ms32RSSI;                /* 数值范围:-1200~ 0, 单位0.125dBm。
                                    对应实际信号范围为：-150dBm~0dBm */
        public S32 ms32RSRQ;                /* 数值范围:-800~ 800, 单位：0.0625dB
                                    对应实际信号范围为：-50dB~50dB */
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_UL_MEAS_STRU
    {
        public U16 mu16EARFCN;              /* 被跟踪UE所在小区的频点 */
        public U16 mu16PCI;                 /* 被跟踪UE所在小区的物理小区标识 */
        public U32 ms32RIP;                 /* 接收的干扰功率，对应TS36.214 Section5.2.2
                                    数值范围:-1200~ 0, 单位0.125dBm。
                                    对应实际信号范围为：-150dBm~0dBm */
        public U32 mu32ThermalNoise;        /* 上行热噪声，对应TS36.214 Section5.2.3
                                    数值范围:-1200~ 0, 单位0.125dBm。
                                    对应实际信号范围为：-150dBm~0dBm */
        public U32 mu32UeNum;               /* 监测UE的个数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public L1_PHY_UL_UE_MEAS_STRU[] mastUeMeasData;//[MAX_MONITOR_UE_NUM];
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PHY_UE_INFO_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] mau8RI;//[4];               /* 各RI使用的次数。
        //                                word0（4bytes）--- RI=1
        //                                word1（4bytes）--- RI=2
        //                                …
        //                                Word7（4bytes）--- RI=8 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U16[] mau16Mcs;//[4];             /* 各MCS使用的次数。
        //                                word0（4bytes）--- MCS=0
        //                                word1（4bytes）--- MCS=1
        //                                …
        //                                Word16（4bytes）--- MCS=15
        //                                各种mcs对应的modualtion mode和coderate参见36.213中CQI的定义 */
        public U32 mu32L1Throughout;        /* PHY层吞吐量统计。Unit: kbps */
        public U32 mu32FirstBLER;           /* 首次传输BLER，Q10定标 */
        public U32 mf32RemainBLER;          /* 残余BLER，Q10定标 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U16[] mau16SINR;//[4];            /* 仪表测量获得的UE 下行SINR的使用次数，共将SINR划分为16个等级。
        //word0（4bytes）--- SINR=0
        //word1（4bytes）--- SINR=1
        //…
        //Word16（4bytes）--- SINR=15 ?? */
    }

    /* 目前L1_PHY_STAT_STRU指示的统计量均可由MAC进行统计，故PHY暂不进行该部分统计功能的开发。关于此消息，后续有新需求时再更新 */
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L1_PHY_STAT_STRU
    {
        public U16 mu16EARFCN;              /* 被跟踪UE所在小区的频点 */
        public U16 mu16PCI;                 /* 被跟踪UE所在小区的物理小区标识 */
        public U32 mu32UeNum;               /* 监测UE的个数。取值范围：1 ~ MAX_MONITOR_UE_NUM */

        //!!!后续紧跟mu32UeNum个PHY_UE_INFO_STRU在消息码流中，相当于存在
        //   PHY_UE_INFO_STRU mastUeInfoList[mu32UeNum];
    }

    #endregion

    #endregion

    #region 协议跟踪REQ的各种消息类型

    #region PC_AG_PROTOCOL_TRACE_REQ

    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PC_AG_PROTOCOL_TRACE_REQ
    {
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Header msgHeader;
        public PROTOCOL_TRACE_UE_INFO_STRU mstUeInfo;// = new PROTOCOL_TRACE_UE_INFO_STRU();
        public PROTOCOL_TRACE_CELL_INFO_STRU mstCellInfo;// = new PROTOCOL_TRACE_CELL_INFO_STRU();
    } 

    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PROTOCOL_TRACE_CELL_INFO_STRU
    {
        public UInt32 mu32PlmnNum;             /* 小区所属的运营商标识个数 */

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public PLMNID_STRU[] PlmnIdList;// = new PLMNID_STRU[4];   /* MAX_PLNMID_NUM = 4*/


        public U8 mu8RATType;              /* 无线接入技术类型
                                    0-TDD_LTE
                                    1-FDD_LTE
                                    2-TD_SCDMA
                                    3_WCDMA
                                    4_GSM */
        public U8 mu8CellSelectMode;       /* 小区选择模式：
                                    CELLSELMODE_MANUAL  -手动指定
                                    CELLSELMODE_FREQBAND-仪表自动选择，指定带宽
                                    CELLSELMODE_FREQLIST-仪表自动选择，指定频点
                                    CELLSELMODE_PCI     -仪表指定PCI */
        public U8 mu8CellReSelectMode;     /* 小区重选模式：
                                    0 --- 不进行重选
                                    1 --- 根据UE的测量报告(Measurement report)做小区重选；
                                    2 --- 根据仪表的自己的测量结果做小区重选  */
        public U8 mu8CellReselectionFlag;  /* 仪表做重选时的标识。
                                    mu8CellReSelectMode=2 时有效，为bitmap，含义如下：
                                    Bit0: 启动同频小区重选
                                    Bit1: 启动异频小区重选
                                    Bit2: 启动Inter-RAT小区重选
                                    Other:  Reserved
                                    各bit位，0 --- Disable， 1 --- Enable */
        public U16 mu16ProtolLayerSelect;     /* 跟踪协议层选择。采用bitmap的方式：
                                    Bit 0 （LSB） ---- PHY_COMMEAS
                                    Bit 1  ----  PHY_UEMEAS
                                    Bit 2  ----  L2P_MAC_PRACH
                                    Bit 3  ----  L2P_MAC_DCIINFO
                                    Bit 4  ----  L2P_MAC_HICHINFO
                                    Bit 5  ----  L2P_MAC_CE
                                    Bit 6  ----  L2P_MAC_SUBHEAD
                                    Bit 7  ----  L2P_RLC
                                    Bit 8  ----  L2P_PDCP
                                    Bit 9  ----  L2P_RRC
                                    Bit 10 ----  L2P_CELLINFO
                                    Bit 11 ----  L2P_PAGINGINFO
                                    各bit位，0 --- Disable， 1 --- Enable */
        public U16 mu16L1MeasSelect;       /* L1的测量项选择。采用bitmap的方式，未选中的可以不测量。
                                   前4个bit在上述参数的bit0或者bit1有效的时此参数有效
                                    Bit 0 --- RSRP/RDRQ/RSSI        Bit 1  --- SINR
                                    Bit 2 --- H                     Bit 3  --- POWER_OFFSET
                                    Bit 8 --- PBCH                  Bit 9  --- PCFICH
                                    Bit 10 --- PDCCH                Bit 11 --- PHICH
                                    Bit 12 --- PSS/SSS              Bit 13 --- CRS
                                    各bit位，0 --- Disable， 1 --- Enable */
        public U8 mu8PhyTrackInfoSelect;   /* 物理层子带测量的粒度， 单位：PRB  */
        public U8 mu8PhyMeasRptPeriod;     /* 物理层公共测量上报的周期 */
        public U16 mu16AverageFrameNum;     /* 物理层功率测量平均帧数。可选参数。*/
        public U16 mu16statisticalInfoReportPeriod;
        /* 统计信息上报周期 */
        public U16 mu16CtrlInfoReportPeriod;/* 控制信息上报周期 */

        public U16 mu16CellNumber;          /* 指定追踪的小区个数。取值范围为1 ~ MAX_TRACECELL_NUM
                                     CellSelectMode为仪表自动选择时，CellInfoStru结构无效 */
        public U16 mu16FreqBandNum;          /* 搜索的频段/频点个数。
                                    当CellSelectMode=CELLSELMODE_FREQBAND或CELLSELMODE_FREQLIST时，数值有效，u16FreqBandNum取值范围为1~ MAX_FREQBAND_NUM
                                    当CellSelectMode=CELLSELMODE_MANUAL时，u16FreqBandNum = 0，下述EARFCN_INFO_STRU不存在 */
        public U16 mu16RadioParaGetFlag;   /* 暂时保留此字段用作系统信息修改标志，代码暂不实现 */
        public S8 ms8AgcPara;             /* 初始的AGC因子，表示最初上行信号用的AGC因子，如果值为0 则认为此参数无效 */
        public U8 mu8Padding;             /* 填充 */

        //!!!若(mu8CellSelectMode == CELLSELMODE_MANUAL)，后续紧跟mu16CellNumber个CELL_INFO_STRU的小区列表在消息码流中，相当于存在
        //   CELL_INFO_STRU mastCellInfoList[mu16CellNumber];

        //!!!若(mu8CellSelectMode != CELLSELMODE_MANUAL)，后续紧跟u16FreqBandNum个EARFCN_INFO_STRU的小区列表在消息码流中，相当于存在
        //   EARFCN_INFO_STRU mastEarfcnInfoList[u16FreqBandNum];
    }


    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PLMNID_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] mau8AucMcc;// = new byte[3] { 0, 6, 4 };            /* 移动国家码，MCC的资源由国际电联（ITU）统一分配和管理，唯一识别移动用户所属的国家，共3位，中国为460 */
        public byte mu8Pading1;// = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] mau8AucMnc;// = new byte[2] { 0, 1 };            /* 移动网络码，2位，中国移动的MNC为00和02[1]，中国联通的MNC为01，中国电信的MNC为03 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] mau8Pading2;// = new byte[2] { 0, 0};           /* 物理层小区标识 */
    }

    #region PROTOCOL_TRACE_UE_INFO_STRU
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PROTOCOL_TRACE_UE_INFO_STRU
    {
        public byte mu8UESelectMode;// = 0;         /* UE选择模式：UESELMODE_MANUAL-手动指定；UESELMODE_AUTO-仪表自动选择 */
        public byte mu8TraceUENum;// = 0;           /* 跟踪UE的个数。取值范围：0 ~ MAX_TRACEUE_NUM */
        public byte mu8UEIDListCount;// = 0;        /* UE ID 个数。取值范围：0 ~ MAX_UEID_LIST_NUM */
        public byte mu8KeyGetMode;// = 0;           /* UE的KEY获取方式：
        //KEY_GET_MODE_MANUAL: 手动指定; 
        //KEY_GET_MODE_USIM  : USIM卡获得，只能指定1个UE */
        public UInt32 mu32UeSilenceCheckTimer;// = 0; /* 取值为0：表示L2P不启动对沉默UE的异常保护 
        //取值>0：表示L2P判别、上报沉默UE的计时门限 */
        //此处需要最后转换成List然后再添加
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public UE_INFO_STRU[] mastUEInfoList;// = new UE_INFO_STRU[16];                         //[MAX_UEID_LIST_NUM]= 16??
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public USIM_INFO_STRU[] mastUsimInfoList;// = new USIM_INFO_STRU[16];                     //[MAX_UEID_LIST_NUM]= 16??
    }

    #region UE_INFO_STRU
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct UE_INFO_STRU
    {
        public byte mu8CellInfoFlag;         /* V1.0是否指定小区信息。0--None，不指定；1--指定 
                                          * Note：只有当PROTOCOL_TRACE_CELL_INFO_STRU中CellSelectMode为“CELLSELMODE_MANUAL 手动指定”时才能指定cellInfoPresent=1.*/
        public byte mu8UEIDTYPE;             /* V1.0 */
        public byte mu8UEIDLength;           /* V1.0 */
        public byte mu8UeCategory;           /* INTEGER (1..5)*/

        public CELL_INFO_STRU mstCellInfo;  /* UE所在小区的信息。当mu8CellInfoFlag=1时，此字段数值有效 */
        public UEID_DATA_STRU muUeIdData;   /* UE ID数据,对于IMSI为BCD编码前数据 */
    }
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct CELL_INFO_STRU
    {
        public UInt16 u16EARFCN;
        public UInt16 u16PCI;
    }
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct CELL_INFO_STRU2
    {
        public UInt16 u16EARFCN;
        public UInt16 u16PCI;
        public int index;
    }
    //[Serializable]
    /*[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct UEID_DATA_STRU  // V1.0 
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21 + 3)]
        public byte[] mau8IMSI;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] mau8GUTI;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] mau8IMEI;
        public UInt16 mu16CRNTI;
        public byte mu8PRID;
    }*/
    [StructLayout(LayoutKind.Explicit,Size = 24)]
    public struct UEID_DATA_STRU  // V1.0 
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21 + 3)]
        public byte[] mau8IMSI;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] mau8GUTI;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] mau8IMEI;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public UInt16[] mu16CRNTI;
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] mu8PRID;
        [FieldOffset(0)]                                                 //0916xxr             
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]             //0916xxr
        public byte[] mau8MTMSI;                                          //0916xxr
    }
    #endregion

    #region USIM_INFO_STRU
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct USIM_INFO_STRU
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21 + 3)]
        public byte[] mau8IMSI;// = new byte[21 + 3];
        public UInt16 mau16SimAC;                   /* Access Classes bitmap, MSB is ACC15, LSB is ACC0 */
        //20141106如果下发CK/IK，暂通过该字段下发RRC/NAS加密方式，完保不影响解消息内容，所以暂时没有添加 
        //   0Bxxxxxxxxxxxxxx00 RRC EEA0 NAS EEA0
        //   0Bxxxxxxxxxxxxxx01  RRC EEA1
        //   0Bxxxxxxxxxxxxxx10  RRC EEA2
        //   0Bxxxxxxxxxxxxxx11  RRC EEA3
        //   0Bxxxxxxxxxxxx01xx  NAS EEA1 
        //   0Bxxxxxxxxxxxx10xx  NAS EEA2
        //   0Bxxxxxxxxxxxx11xx  NAS EEA3
        public byte mu8KLengthIndex;                /* K length index, 0: 128 bits, 1: 256 bits */
        public byte mu8Pading; //20141106如果下发CK/IK，通过该字节下发NAS UL count
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] maU8KDATA;// = new byte[32];     /* 若mu8KLengthIndex=0, 则maU8KDATA中从低地址起前16byte数据有效  */
    }
    #endregion

    #endregion

    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct EARFCN_INFO_STRU
    {
        public UInt16 mu16StartEARFCN;         /* 起始频点。当Scan Mode = SCANMODE_FRBANDLIST时，指定要扫描频段的起始频点；当Scan Mode = SCANMODE_FREQLIST时，指定要扫描的频点 */
        public UInt16 mu16EndEARFCN;           /* 截止频点。当Scan Mode = SCANMODE_FRBANDLIST时，指定要扫描频段的截止频点；当Scan Mode = SCANMODE_FREQLIST时，此字段无效，0xFFFFFFFF */
        public UInt16 mu16Step;                /* 步长。    当Scan Mode = SCANMODE_FRBANDLIST时，指定步长，单位：KHz；当Scan Mode = 2时，此字段无效，0xFFFFFFFF */
        //0705WDW
        public U16 mu16PCI;                           /* PCI。     
                                            当取值为0xFFFF时，表示当前频段/频点扫描中不指定PCI 
                                            当取值为0~503 时，表示当前频段/频点扫描中按照此指定PCI进行扫描 */
    }
    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct EARFCN_INFO_STRU2
    {
        public UInt16 mu16StartEARFCN;         /* 起始频点。当Scan Mode = SCANMODE_FRBANDLIST时，指定要扫描频段的起始频点；当Scan Mode = SCANMODE_FREQLIST时，指定要扫描的频点 */
        public UInt16 mu16EndEARFCN;           /* 截止频点。当Scan Mode = SCANMODE_FRBANDLIST时，指定要扫描频段的截止频点；当Scan Mode = SCANMODE_FREQLIST时，此字段无效，0xFFFFFFFF */
        public UInt16 mu16Step;                /* 步长。    当Scan Mode = SCANMODE_FRBANDLIST时，指定步长，单位：KHz；当Scan Mode = 2时，此字段无效，0xFFFFFFFF */
        //0705WDW
        public U16 mu16PCI;                           /* PCI。     
                                            当取值为0xFFFF时，表示当前频段/频点扫描中不指定PCI 
                                            当取值为0~503 时，表示当前频段/频点扫描中按照此指定PCI进行扫描 */
        public int index;
    }
    #endregion 
    
    #region PC_AG_UE_SILENCE_RPT_RSP

    //[Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PC_AG_UE_SILENCE_RPT_RSP
    {
        /* 该消息是pc机的AGI接收到L2P_UE_SILENCE_RPT_IND消息后，AGI对L2P_UE_SILENCE_RPT_IND消息的回应，
   消息中决定哪些UE释放哪些不释放继续跟踪    */
        public AGI_MSG_HEADER msgHeader;
        public AG_UE_SILENCE_INFO_STRU mstUECaptureInfo;
        public U8 mu8ReTrace;/* 0-释放; 1-继续跟踪 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] mau8Pading;// = new byte[3]
    }

    #endregion

    #endregion

    #region 协议跟踪的其他消息类型
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_AG_CELL_CAPTURE_IND
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;           /* 时间戳32位（GPS时间）。存储格式为:
                                    TimeStampH[3]为spare;
                                    TimeStampH[2]表示小时，取值范围0~23;
                                    TimeStampH[1]表示分钟，取值范围0~59;
                                    TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 TimeStampL;              /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public U16 mu16CellStatus;          /* CELL_STATUS_SUITABLE   - suitable cell  提供Normal service
                                    CELL_STATUS_ACCEOTABLE - acceptable cell  提供Limited service
                                    CELL_STATUS_RESERVED   - reserved cell 提供Operator service
                                    CELL_STATUS_BARRED     - barred cell 无法提供服务
                                    CELL_STATUS_CAPTUREFAIL- CaptureFail 指定小区跟踪失败上报此原因值，此条件下只有EARFCN、PCI有效 */

        public U16 mu16PCI;                 /* 物理层小区ID，0-503 */
        public U16 mu16EARFCN;              /* 指定小区的频点 */
        public U16 mu16TAC;                 /* Trace Area Code. 协议中定义TAC ::= BIT STRING (SIZE (16)), 高比特位对应于比特流中的低位*/
        public U32 mu32CellID;              /* CGI:小区GlobalID。协议中定义CellIdentity ::= BIT STRING (SIZE (28)) */
        public S16 mu16Rsrp;                /* The value of RSRP */
        public S16 mu16Rsrq;                /* The value of RSRQ */
        public U8 mu8Dlbandwidth;          /* {DL_BANDWIDTH_N6, DL_BANDWIDTH_N15, DL_BANDWIDTH_N25,
                                     DL_BANDWIDTH_N50,DL_BANDWIDTH_N70, DL_BANDWIDTH_N100} */
        public U8 mu8PhichDuration;        /* PHICH_DURATION_NORMAL-normal;
                                    PHICH_DURATION_EXTEND-extended */
        public U8 mu8PhichResource;        /* PHICH_RESOURCE_ONESIXTH-oneSixth;
                                    PHICH_RESOURCE_HALF    -half;
                                    PHICH_RESOURCE_ONE     -one;
                                    PHICH_RESOURCE_TWO     -two */
        public U8 mu8SpecialSubframePatterns;
        /* Indicates Configuration as in TS 36.211 [21, table 4.2.1] :
           {SSP0,SSP1,SSP2,...,SSP8} */
        public U8 mu8UplinkDownlinkConfiguration;
        /* Indicates DL/UL subframe configuration as specified in TS 36.211 [21, table 4.2.2].
           {UPLINK_DOWNLINK_CFG0, UPLINK_DOWNLINK_CFG1, UPLINK_DOWNLINK_CFG2
            UPLINK_DOWNLINK_CFG3, UPLINK_DOWNLINK_CFG4, UPLINK_DOWNLINK_CFG5
            UPLINK_DOWNLINK_CFG6 }*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_AG_CELL_STATE_IND
    {
        /* 在指定小区跟踪的场景，通过此接口上报小区状态 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;           /* 时间戳32位（GPS时间）。存储格式为:
                                    TimeStampH[3]为spare;
                                    TimeStampH[2]表示小时，取值范围0~23;
                                    TimeStampH[1]表示分钟，取值范围0~59;
                                    TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 TimeStampL;              /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public U16 mu16CellStatus;          /* CELL_STATUS_OUTAREA    -out of service area
                                    CELL_STATUS_INAREA     -in service area
                                    CELL_STATUS_LINKFAIL   -Radio Link failure
                                    CELL_STATUS_NOSUITABLE -No suitable cell 此条件下以下参数无效。*/
        public U16 mu16PCI;                 /* 物理层小区ID，0-503 */
        public U16 mu16EARFCN;              /* 指定小区的频点 */
        public U16 mu16Pading;
    }
    

#endregion 

    /* == MGC参数修改请求 == */
    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi,Pack=1)]
    public struct PC_AG_RENEW_MGC_REQ
    {
        public AGI_MSG_HEADER msgHeader; //消息头

        public U32 mu32ModefyMode;     /* 使用bitmap表示需要修改的字段：
								Bit0： AGCMode 字段有效
								Bit1：MGCEnable 字段有效
								Bit2：EvaluateMode 字段有效
								Bit3：AgcFactor 字段有效
								Bit4：AdjustStep 字段有效
								Bit5：GainFactor字段有效
								Bit6：UpTargetPower 字段有效
								Bit7：DownTargetPower 字段有效
								如果对应的bit位为0，则对应的字段无效 */
        public U8 mu8AgcMode;     /*AGC/MGC切换的标志位，0:AGC  1:MGC   */
        public U8 mu8MgcEnable;     /* MGC上行手动增益使能标志，1代表使能，0表示关闭 */
        public U8 mu8EvaluateMode; /* 使用'功率'评估或'功率与幅度'共同评估，0为功率评估，1为功率与幅度共同评估 */
        public U8 mu8AgcFactor; /* 遗忘因子配置,取值范围：0:3 */
        public U8 mu8AdjustStep;/* MGC调整步长，取值范围：0:3 */
        public U8 mu8GainFactor; /*  MGC上行手动增益值取值范围:0;76  默认值为:64 */
        public U8 mu8LNAEnabe; /* 外置低噪放使能标志，	00：全关，
						01：外置低噪放1打开
						10：外置低噪放2打开
						11：全开 */
        public U8 mu8Reserved; /* 保留字段 */
        public U16 mu16UpTargetPower; /* MGC上行目标功率，默认值为2048 */
        public U16 mu16DownTargetPower;    /* MGC下行目标功率，默认值为：768 */
    };

    #region PC_AG_RENEW_IP_REQ
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PC_AG_RENEW_IP_REQ
    {
        public Header msgHeader;
        public U32 mu32ModefyMode;         /* 使用bitmap表示需要修改的字段：
                                    Bit0： 端口0  Bit1：端口1
                                    Bit2：IP       Bit3：Gate
                                    Bit4：MAC 如果相应字段为0，则下面的值无效 */
        public U32 mau8AgtPort0Num;     /* AgtPort0Num[0]:端口号0的第一位，比如端口号为8067，则此位是8，
                                   AgtPortNum[1], AgtPortNum[2], AgtPortNum[3]分别是0，6,7 */
        public U32 mau8AgtPort1Num;     /* 端口1的端口号 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] mau8AgtIPAdress;        /* 仪表的IP地址 */
        public U32 mu32AgtGateAdress;      /* 仪表的网关地址 */
        public U32 mu32AgtMacAdress;       /* 仪表的MAC地址 */
    }
    #endregion
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum CELL_STATECellStatus
    {
        CELL_STATUS_OUTAREA = 0,
        CELL_STATUS_INAREA,
        CELL_STATUS_LINKFAIL,
        CELL_STATUS_NOSUITABLE
    }
    public enum CELL_CAPTURECellStatus
    {
        CELL_STATUS_SUITABLE = 0,
        CELL_STATUS_ACCEOTABLE,
        CELL_STATUS_RESERVED,
        CELL_STATUS_BARRED,
        CELL_STATUS_CAPTUREFAIL
    }
    public enum Dlbandwidth
    {
        DL_BANDWIDTH_N6 = 0,
        DL_BANDWIDTH_N15,
        DL_BANDWIDTH_N25,
        DL_BANDWIDTH_N50,
        DL_BANDWIDTH_N70,
        DL_BANDWIDTH_N100
    }
    public enum PhichDuration
    {
        PHICH_DURATION_NORMAL = 0,
        PHICH_DURATION_EXTEND
    }
    public enum PhichResource
    {
        PHICH_RESOURCE_ONESIXTH = 0,
        PHICH_RESOURCE_HALF,
        PHICH_RESOURCE_ONE,
        PHICH_RESOURCE_TWO
    }
    public enum SpecialSubframePatterns
    {
        SSP0 = 0,
        SSP1,
        SSP2,
        SSP3,
        SSP4,
        SSP5,
        SSP6,
        SSP7,
        SSP8
    }
    public enum UplinkDownlinkConfiguration
    {
        UPLINK_DOWNLINK_CFG0 = 0,
        UPLINK_DOWNLINK_CFG1,
        UPLINK_DOWNLINK_CFG2,
        UPLINK_DOWNLINK_CFG3,
        UPLINK_DOWNLINK_CFG4,
        UPLINK_DOWNLINK_CFG5,
        UPLINK_DOWNLINK_CFG6
    }
    #region 协议跟踪的其他消息类型

    public struct L2P_AG_UE_CAPTURE_IND
    {
        public Header msgHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;           /* 时间戳32位（GPS时间）。存储格式为:
                                    TimeStampH[3]为spare;
                                    TimeStampH[2]表示小时，取值范围0~23;
                                    TimeStampH[1]表示分钟，取值范围0~59;
                                    TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 TimeStampL;              /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public AG_UE_CAPTURE_INFO_STRU mstUECaptureInfo;
    }

    public struct L2P_AG_UE_SILENCE_RPT_IND
    {
        public Header msgHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;           /* 时间戳32位（GPS时间）。存储格式为:
                                    TimeStampH[3]为spare;
                                    TimeStampH[2]表示小时，取值范围0~23;
                                    TimeStampH[1]表示分钟，取值范围0~59;
                                    TimeStampH[0]表示秒，  取值范围0~59 */
        public U32 TimeStampL;              /* 时间戳低32位（ms为单位）， 取值范围0 ~ 999 */

        public AG_UE_SILENCE_INFO_STRU mstUESilenceInfo;
    }

    public struct AG_UE_CAPTURE_INFO_STRU   /* V1.0 增加了TimeStampH 和 TimeStampL */
    {
        public U16 mu16EARFCN;              /* 指定小区的频点 */
        public U16 mu16PCI;                 /* 物理层小区ID，0-503 */
        public U8 mu8UEIDTypeFlg;          /* Bitmap UE ID存在标志
                                    Bit0: IMSI
                                    Bit1: GUTI
                                    Bit2: IMEI
                                    Bit3: CRNTI
                                    Bit4: PRID
                                    BIT5: S-TMSI
                                    注：Bit位值为0时，对应的data信息无效 */
        public U8 mu8ImsiDigitCnt;         /* IMSI位数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public U8[] mau8IMSI;              //21
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public U8[] mau8GUTIDATA;          //10
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public U8[] mau8IMEI;              //15
        /* 2+MAX_SIZE_IMSI(21)+MAX_SIZE_GUTI(10)+MAX_SIZE_IMEI(15)=48个byte，满足4字节对齐 */
        public U16 mu16CRNTIDATA;
        public U8 mu8PRIDDATA;
        public U8 mu8Pading1;
    }
    public struct AG_UE_SILENCE_INFO_STRU   /* V1.0 增加了TimeStampH 和 TimeStampL */
    {
        public U16 mu16EARFCN;              /* 指定小区的频点 */
        public U16 mu16PCI;                 /* 物理层小区ID，0-503 */
        public U8 mu8UEIDTypeFlg;          /* Bitmap UE ID存在标志
                                    Bit0: IMSI
                                    Bit1: GUTI
                                    Bit2: IMEI
                                    Bit3: CRNTI
                                    Bit4: PRID
                                    注：Bit位值为0时，对应的data信息无效 */
        public U8 mu8ImsiDigitCnt;         /* IMSI位数 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public U8[] mau8IMSI;              //21
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public U8[] mau8GUTIDATA;          //10
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public U8[] mau8IMEI;              //15
        /* 2+MAX_SIZE_IMSI(21)+MAX_SIZE_GUTI(10)+MAX_SIZE_IMEI(15)=48个byte，满足4字节对齐 */
        public U16 mu16CRNTIDATA;
        public U8 mu8PRIDDATA;
        public U8 mu8Pading1;
    }

    public struct L2P_AG_UE_RELEASE_IND
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;
        public U32 TimeStampL;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;
        public U8 mu8UeNUM;
    }

    public struct L2P_AG_CELL_RELEASE_IND
    {
        public U8 mu8CellNUM;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public U8[] mau8Pading;
    }

    public struct XX_L2P_AG_UE_RELEASE_IND
    {
        public Header msgHeader;
        public L2P_AG_UE_RELEASE_IND mstUEReleaseHeader;
    }

    public struct XX_L2P_AG_CELL_RELEASE_IND
    {
        public Header msgHeader;
        public L2P_AG_CELL_RELEASE_IND mstCellReleaseHeader;
    }

    public struct XX_L2P_AG_CELL_CAPTURE_IND
    {
        public Header msgHeader;
        public L2P_AG_CELL_CAPTURE_IND mstCellCaptureHeader;
    }
    

    public struct XX_L2P_AG_CELL_STATE_IND
    {
        public Header msgHeader;
        public L2P_AG_CELL_STATE_IND mstCellStateHeader;
    }
   
    #endregion

    #region 小区扫描消息

    public struct PC_AG_SPECIFIED_CELL_SCAN_REQ
    {
        public U8 mu8RATType;   /* 无T线?接入?技?术?类型
                                    0-TDD_LTE
                                    1-FDD_LTE
                                    2-TD_SCDMA
                                    3_WCDMA
                                    4_GSM */
        public U8 mu8GpsControl;
        public U16 mu16MeasureMask;
        public U16 mu16MeasSubFrameInd;
        public U16 mu16MMeasOFDMSymInd;
        public U32 mu32MeasSlotInd;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U32[] mau32MeasRBInd;
        public U8 mu8SystemBand;
        public U8 mu8AntPort1PresentFlag;
        public U16 mu16Padding;
        public U32 mu32SIPresentFlag;
        public U16 mu16AvergeFrames;
        public U16 mu16ScanMode;
        public U16 mu16ScanCycle;
        public U16 mu16CellNum;
    }

    public struct XX_PC_AG_SPECIFIED_CELL_SCAN_REQ
    {
        public Header msgHeader;
        public PC_AG_SPECIFIED_CELL_SCAN_REQ mstSpeCellScanHead;
    }

    public struct PC_AG_UNSPECIFIED_CELL_SCAN_REQ
    {
        public U8 mu8RATType;   /* 制式{RAT_TDD, RAT_FDD, RAT_SCDMA, RAT_WCDMA, RAT_GSM} */
        public U8 mu8ScanMode;/* SCANMODE_FRBANDLIST：Frequency Band List模式 （缺省值） SCANMODE_FREQLIST：Frequency List扫描模式 */
        public U8 mu8GpsControl;/* 控制扫频仪GPS上报，默认为0 。
                                    GPSCONTRL_REPORT_YES(0)-使用内置GPS，上报
                                    GPSCONTRL_REPORT_NO (1)-使用外置GPS，不上报 */
        public U8 mu8Pading;/* 填充 */
        public U16 mu16MeasureMask;/* 指定测量类型（可任意组合如下BITMASK）
                                    0x0001: PSS RSSI/RP/RQ
                                    0x0002: SSS RSSI/RP/RQ
                                    0x0004: CRS RSSI/RP/RQ
                                    0x0008: PBCH RP/RQ
                                    0x0010: CRS SINR
                                    0x0020: OFDM Symbol Power
                                    0x0040: PBCH EVM
                                    0x0080: PBCH BLER
                                    0x0100: SubFrame：子帧信号强度测量
                                    0x0200: TS: 时隙信号强度测量
                                    0x0400: Frame: 无线帧信号强度测量
                                    0x0800: 指定RB或RB集合上的信号强度测量

                                    各测量量的指定方式为：
                                    “子帧信号强度测量”通过MeasSubFrameInd字段指示；
                                    “时隙信号强度测量”通过MeasSlotInd字段指示；
                                    “指定RB或RB集合上的信号强度测量”通过MeasSlotInd+ MeasRBInd指示；
                                    “OFDM Symbol Power”通过MeasSubFrameInd+ MeasOFDMSymInd指示。*/
        public U16 mu16MeasSubFrameInd;/* 待测量的subframe指示。采用bitmap的方式指示：
                                    Bit0 --- SubFrame 0
                                    Bit1 --- SubFrame 1
                                    …
                                    Bit9 --- Subframe 9
                                    其它比特位保留。
                                    各bit位0表示无效，1表示有效 */
        public U16 mu16MMeasOFDMSymInd;/* 待测量的OFDM符号的索引。采用bitmap的方式指示：
                                    Bit0 --- OFDM Symbol 0
                                    Bit1 --- OFDM Symbol 1
                                    …
                                    Bit13 --- OFDM Symbol 13
                                    其它比特位保留。
                                    各bit位0表示无效，1表示有效 */
        public U16 mu16Padding;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U32[] mu32MeasRBInd;/* 待测量的RB，为BitMap形式：
                                     mu32MeasRBInd[0]的bit0~bit31：nPRB0~nPRB31
                                     mu32MeasRBInd[1]的bit0~bit31：nPRB32~nPRB63
                                     mu32MeasRBInd[2]的bit0~bit31：nPRB64~nPRB95
                                     mu32MeasRBInd[3]的bit0~bit3： nPRB96~nPRB99 */
        public U32 mu32MeasSlotInd;/* 待测量的时隙索引。采用bitmap的方式指示：
                                    Bit0 --- Slot 0
                                    Bit1 --- Slot 1
                                    …
                                    Bit19 --- Slot 19
                                    各bit位 0表示无效，1表示有效 */
        public U32 mu32SIPresentFlag;/* 需要解调的L3系统信息的类型 Bit0: MIB Bit1: SIB1 Bit2: SIB2…Bit13: SIB13 Bit14 ~ Bit31  Reserved */
        public U8 mu8SystemBand;/* 系统带宽设置0: None，用户没有指定1: 20M 2: 15M 3: 10M 4: 5M 5: 3M 6: 1.4M */
        public U8 mu8AntPort1PresentFlag;/* 基站天线端口1是否存在标识：0：用户没有指定 1；天线端口1存在 2：天线端口1不存在 */
        public U16 mu16AvergeFrames;/* 测量时用来做平均的帧数。
                                    取值范围：1-256，默认值为1。 */
        public U16 mu16ScanPattern;/* 0-单次扫描; 1-按照ScanCycle设置进行扫描 */
        public U16 mu16ScanPeriod;/* 扫描周期 ，单位是ms  */
        public U16 mu16EarfcnNum;/* 用户指定小区列表数目。取值范围：1 ~ MAX_FREQUENCYLIST_NUM */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] mau8Pading1;

        //!!!后续紧跟mu16EarfcnNum个EARFCN_INFO_STRU的小区列表在消息码流中，相当于存在
        //   EARFCN_INFO_STRU mastEARFCNList[mu16EarfcnNum];
    }

    public struct XX_PC_AG_UNSPECIFIED_CELL_SCAN_REQ
    {
        public Header msgHeader;
        public PC_AG_UNSPECIFIED_CELL_SCAN_REQ mstUnSpeCellScanHead;
    }

    public struct L1_CELL_SCAN_DATA
    {
        public Header msgHeader;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public U8[] TimeStampH;
        public U32 TimeStampL;
        public U16 EARFCN;
        public U16 PCI;
        public U8 SynStatus;
        public U8 AntennaPortNumber;
        public U8 CP_Type;
        public U8 gpsValidFlag;
        public S32 Timing_offset;
        public U16 MeasureMask;
    }
    #endregion


    #region MAC,RLC,PDCP解码结构体定义
    public struct L2P_MAC_RAR_SUBHEADER_INFO_STRU 
    {
        public U8 u8BI;							/* 36.321协议，6.2.2 */
	    public U8 u8RAPID;						/* 36.321协议，6.2.2 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	    public U8[] u8Padding;
    }
   
    public struct L2P_MAC_RAR_SUBHEADER_STRU
    {
        public U32 u32SubHeaderNum;
	    public U32	u32HeadSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	    public L2P_MAC_RAR_SUBHEADER_INFO_STRU[] rarSubHeaderInfo;
    }

    public struct L2P_MAC_PDU_SUBHEADER_INFO_STRU
    {
        public U8 u8Lcid;
        public U8 u8F;
        public U16 u16L;
    }

    public struct L2P_MAC_PDU_SUBHEADER_STRU
    {
        public U32 u32SubHeaderNum;
        public U32 u32HeadSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public L2P_MAC_PDU_SUBHEADER_INFO_STRU[] pduSubHeaderInfo;
    }


    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MAC_CE_C_RNTI_STRU
    {
        public U16 u16CRnti;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public U8[] u8Padding;
    }

    public struct L2P_MAC_RAR_INFO_STRU
    {
        public U16 u16TaCommand;
        public U16 u16TcRnti;
        public U32 u32UlGrant;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct L2P_MAC_CE
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] data;
    }
    //[StructLayout(LayoutKind.Explicit, Size = 8)]
    /*public struct L2P_MAC_CE
    {
        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_SHORT_BSR_STRU shortBsr;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_TRUNCATED_BSR_STRU truncatedBsr;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_LONG_BSR_STRU longBsr;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_C_RNTI_STRU cRnti;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_UE_CR_ID_STRU CRId;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_TIMING_ADVANCE_COMMAND_STRU TACommand;

        [FieldOffset(0)]
        //[MarshalAs(UnmanagedType.LPStruct)]
        public MAC_CE_PHR_STRU PHR;
    }*/

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_EXTENSION_STRU
    {
        public U16 u16LI;
        public U16 u8Padding;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_UMD_PDU_HEADER_STRU
    {
        public U8 u8FI;
        public U8 u8E;
        public U16 u16SN;
        public U16 u32HeadSize;
        public  U16 u16LiNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public L2P_RLC_EXTENSION_STRU[] extensionInfo;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_STATUS_PDU_EXTENSION_STRU
    {
	    public U16 u16NackSn;
	    public U16 u16SOstart;
	    public U16 u16SOend;
	    public U8	u8E1;
	    public U8	u8E2;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_RLC_AMD_PDU_HEADER_STRU
    {
	    public U8 u8DC;	/* 0--Control PDU; 1--Data PDU */
	                                    /* data pdu 内容，u8DC 为0 时有效  */
	    public U8 u8RF;	/* 0--AMD PDU; 1--AMD PDU segment */
	    public U8 u8P;	/* 0--Status report not requested;
	 	 	 	 	 	 	 	 	1--Status report is requested */
	    public U8	u8FI;		/* Framing Info */
	    public U8	u8LSF;
	    public U8	u8E;
	    public U16	u16SN;
	    public U16	u16SO; 	/* 15bit, u8RF为1有效 */
	    public U16	u16HeadSize;
        public U16 u16LiNum;
	    /* end */
	    /* status pdu 内容，u8DC 为0 时有效 */
	    public U8	u8CPT;
	    public U8	u8ExtenNum;
	    public U16	u16AckSN;
        public U16 u16Padding;
	/* end */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	    public L2P_RLC_EXTENSION_STRU[] dataPduExten; /* data pdu 扩展内容， u8E为1有效 */
	    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public L2P_RLC_STATUS_PDU_EXTENSION_STRU[] statusPduExten; /* status pdu扩展内容，u8ExtenNum不为0xff时有效 */
    }
/* PDCP */

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct L2P_PDCP_PDU_DRB_STRU
    {
	    public U32 u32DC;
	    public U16	u16PdcpSn;
	    public U16	u16Fms;
	    public U32	u32PduType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	    public U32[] rohcFeedbackPacket;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	    public U32[]	Bitmap;
    }

    /*GPS*/
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MSG_AG_PC_GPS_DATA
    {
        public Header msgHeader;
        public AG_PC_GPS_DATA mstAG_PC_GPS_DATA;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct AG_PC_GPS_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
         public  U8[]  GpsDate	;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
         public U8[] GpsTime;
         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
         public U8[] GpsLatitude;
         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
         public U8[] GpsLongtitude;
         public U8 GpsLongtitudeAngle;
         public U8 GpsLatitudeAngle;
         public U8 Padding;
         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
         public U8[] GpsSpeed;
         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
         public U8[] GpsCourse;

         public U32 mu32TimeStampL;       /*AGT 1ms 计数*/
    }

    /// <summary>
    /// 用于STMSI统计
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct STMSIStruct
    {

        public UInt32 iNum;
        public DateTime dTime;
        public B8 mMec;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public B8[] sTMSI;
        public U8 mu8EstCause;
    }

    public struct CountStruct
    {
        public UInt32 iNum;
        public DateTime dTime;
        public string MsgType;
    }

    public struct MulCountStruct
    {
        public UInt32 iNum;
        public DateTime dTime;
        public string MsgType;
        public UInt32 iCount;
    }
    #endregion
}
