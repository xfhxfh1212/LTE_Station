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
    public class MsgDefine
    {
/*****************************************msg_struct***************************************************/
        public enum RecvPktType
        {
            RECV_SYS_PARA		= 0x1,				//系统参数，MCC、MNC等
	        RECV_SYS_OPTION		= 0x2,				//系统选项，如是否上报IMEI等
	        RECV_DLRX_PARA		= 0x3,				//下行接收参数，用于获取公网信息
	        RECV_NC_PARA		= 0x4,				//邻区表参数
	        RECV_IB_OPER		= 0x5,				//IMSI库处理
	        RECV_CTRL_CMD		= 0x6,				//寻呼控制命令
	        RECV_RF_PARA		= 0x7,				//射频参数，如频点、功率等
	        RECV_QUERY_VER		= 0x8,				//查询版本
	        RECV_COUNT_ZERO		= 0x9,				//上报IMSI计数清零
	        RECV_RX_PARA		= 0xA,				//接收参数，如RACH接收功率门限等 10
	        RECV_NEW_TAC		= 0xB,				//重新配置新的MCC、MNC、TAC和Cell ID
	        RECV_NOW_PARA		= 0xC,				//获取当前设备内的参数
	        RECV_HEART_BEAT_CNF	= 0xD,				//心跳包确认
	        RECV_DEV_STATE		= 0xE,				//返回设备状态
	        RECV_REDIRECT_R9	= 0xF,
            RECV_REBOOT_ENB     = 0x10,

        };
        public struct Head
        {
	        public U16			head;				//头部标识 0xffff
            public U16			pkt_type;			//消息类型
            public U32			data_length;		//消息长度
        }


        /*****************************************socket stream***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvSysParaBitStream     //系统参数数据结构
        {
             public Head        head;
             public U8    		paraSysNo;
             [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
             public U8[]        paraMcc;
             [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
             public U8[]    	paraMnc;
             public U16   		paraPciNo;
             public U16   		paraTac;
             [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
             public U8[]    	padding1;
             public U32   		CellId;
             public U32   		TaiPeri;
             public U8    		paraIDReqNumOUT;
             public U8    		paraIDReqNumIN;
             public U8    		UEcapaEnquiryNumOUT;
             public U8    		UEcapaEnquiryNumIN;
             public U8    		BoolMeasureOUT;
             public U8    		BoolMeasureIN;
             public U8    		paraTAURejCauOUT;
             public U8    		paraTAURejCauIN;
             public U8    		paraATTRejCauOUT;
             public U8    		paraATTRejCauIN;
             public U8    		redirectedRATOUT;
             public U8    		padding2;
             public U16   		redirectedCarrierOUT;
             public U8    		PriorityOUT;
             public U8    		redirectedRATIN;
             public U16    		redirectedCarrierIN;/*TODO:16*/
             public U8    		PriorityIN;
             public U8    		redirectedCellIN;/*BSIC*/
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RedirectedR9						//rf参数数据结构
        {
            public Head         head;
	        public U8    		BoolR9;
	        public U8    		MsgType;
	        public U16    	    CellID;
	        public U16    	    LAC;
	        public U8    		MSCR;
	        public U8    		ba_ag_blks_res;
	        public U8    		ccch_conf;
	        public U8    		bs_pa_mfrms;
	        public U8    		T3412_value;
	        public U8    		pwrc;
	        public U8    		DTX;
	        public U8    		radio_link_timeout;
	        public U8    		MsTxPwrMaxCch;
	        public U8    		Neci;
	        public U8    		RxlevAccessMin;
	        public U8    		RachMax_retrans;
	        public U8    		RachTx_int;
	        public U8    		CelllReselOffset;
        }

        /*****************************************RecvSysPara***************************************************/



        public enum redirectedRAT_Type
        {
	        RAT_Type_EUTRAN		= 0x1,
	        RAT_Type_UTRANTDD	= 0x2,
	        RAT_Type_GERAN		= 0x3,
	        RAT_Type_UTRANFDD	= 0x4,
        };


        /*****************************************RecvSysOption***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvSysOption					//系统选项数据结构
        {
            public Head         head;
	        public U16		   opSysNo;
	        public U8		   opLuImei;
	        public U8		   opLuSTMSI;
        }
        /*****************************************RecvDlRxPara***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvDlRxPara					//下行接收参数数据结构
        {
            public Head         head;
	        public U16		   dlSysNo;
	        public U16		   earfcn;
	        public U32		   dlFn;					//帧数
	        public U8		   dlRxMod;
	        public U8		   dlEnable;
        }
        /*****************************************RecvNcPara***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct	interFreqList					//异频邻区列表
        {
            public Head         head;
	        public U8		   NumInterFreqCell;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	        public U8[]		   PCI;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct interFreqCellList				//异频列表
        {
            public Head         head;
	        public U8		   NumInterFreq;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	        public interFreqList[]	   interFreqList;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct intraFreqCell					//同频邻区
        {
            public Head         head;
	        public U8		   NumIntraFreqCell;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	        public U8[]		   PCI;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvNcPara						//邻区参数数据结构
        {
            public Head         head;
	        public U16		   ncSysNo;
	        public U8		   ncCmdType;				//设置为0x01  清除为0x02
            public intraFreqCell intraFreqCell;			//同频邻区列表
            public interFreqCellList interFreqCellList;		//异频邻区列表 Uint16	interncList[6]
        }

        /*****************************************RecvIbOper***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvIbOper						//IMSI库参数数据结构
        {
            public Head         head;
	        public U8		   ibSysNo;
	        public U8		   ibCmdType;				//添加为1，删除为2，清空为3
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	        public U8[]		   ibImsi;
        }
        /*****************************************RecvCtrlCmd***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvCtrlCmd						//控制命令参数数据结构
        {
            public Head         head;
	        public U8		   ctrlSysNo;
	        public U8		   ctrlCmdType;			//连续寻呼为0x01
	        public U8		   ctrlCmdPara;			//对连续寻呼而言，该参数为寻呼间隔
	        public U8		   ctrlPagingIDType;		//寻呼ID类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	        public U8[]		   ctrlImsi;
        }
        /*****************************************RecvRfPara***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvRfPara						//rf参数数据结构
        {
            public Head         head;
            public U16          rfSysNo;
            public U8           rfEnable;
            public U8           fastConfigEarfcn;
            public U16          earfcn_band;
            public U16          DlEarfcn;
            public U16          UlEarfcn;
            public U8           FrameStrucureType;
            public U8           SubframeAssinment;
            public U8           specialSubframePatterns;
            public U8           DlBandWidth;
            public U8           UlBandWidth;
            public U8           RFchoice;
            public U16          TX1PowerAttenuation;
            public U16          TX2PowerAttenuation;
            //public U32          rfPwr;
        }
        /*****************************************RecvNewTac***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvNewTac						//重新设置位置区TAI相关信息
        {
            public Head         head;
	        public U16		   newSysNo;
	        public U16		   newTimer;
        }
        /*****************************************RecvNowPara***************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RecvNowPara						//获取当前参数
        {
            public Head         head;
	        public U16		   nowSysNo;
	        public U16		   nowType;				//返回的请求类型，同enum RecvPktType
        }


        /*****************************************SendPktType***************************************************/

        public enum SendPktType
        {
	        SEND_HEART_BEAT				=1,					//心跳包，每3s发送一次
	        SEND_UE_INFO				=2,					//用户设备信息，包括IMSI、IMEI等
	        SEND_REQ_CNF				=3,					//确认接收到的请求
	        SEND_NOW_PARA				=4,					//返回设备的当前参数
	        SEND_PAGING_PWR				=5,					//上报寻呼UE发射功率
	        SEND_FREQ					=6,					//返回频点信息
	        SEND_DEV_VERSION			=7,					//打印版本信息
	        SEND_NEW_TAC				=8,					//报告新的PLMN\TAC号生成
	        SEND_DEV_STATE				=9,					//上报设备状态数据
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SendUeInfo							//UE信息数据结构
        {
            public Head                 head;
	        public U16					ueSysNo;
	        public U16					ueSendNo;			//发送序号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	        public U8[]					CurrentMcc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	        public U8[]					CurrentMnc;
	        public U8					padding;
	        public U16					CurrentTac;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	        public char[]				ueImsi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[]               ueImei;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[]               ueSTmsi;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	        public U8[]					ueMsisdn;		//手机号码号段
	        public U8					ueType;				//手机类型（如LTE单模数据卡,多模LTE终端）
	        public U8					ueTaType;			//位置更新类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
	        public U8[]					ueTaTime;		//设备端取号的时间戳
	        public U8					uePwrNo;			//有效功率值的数量
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
	        public U8[]					uePwr;			//交互过程中接收的手机信号功率值
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SendReqCnf							//请求确认数据结构
        {
            public Head                 head;
	        public U16					cnfSysNo;
	        public U16					cnfType;			//确认的请求类型，同enum RecvPktType
	        public U16					cnfInd;				//0表示正确接收，1表示参数错误...
	        public U16					padding;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SendNowPara							//获取当前参数
        {
            public Head                 head;
	        public U8					nowSysNo;
	        public U16					nowType;			//返回的请求类型，同enum RecvPktType
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
	        public U8[]					nowPara;		//返回的参数
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SendPagingPwr
        {
            public Head                 head;
	        public U8					pagingSysNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	        public U8[]					pagingImsi;
	        public U8					uePwrNo;
	        public U8					padding;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
	        public U8[]					uePwr;			//交互过程中接收的手机信号功率值
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SendFreq							//返回频点值
        {
            public Head                 head;
	        public U8					sysNo;
	        public U16					earfcn;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct  SendDevVersion						//上报操作管理信息
        {
            public Head                 head;
	        public U8					type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	        public char[]						fpgaVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	        public char[]					    BBUVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
	        public char[]						SoftWareVersion;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct  SendDlSniff
        {
            public Head                 head;
	        public U8			        numberCell;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public cellInfomation[] cellInfomation;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct  cellInfomation
        {
            public Head                 head;
	        public U8		            SysNo ;
	        public U8		            Mcc;
	        public U8		            Mnc;
	        public U16	                Tac;
	        public U16	                PciNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
	        public U8[]		            CellId;
	        public U16	                Earfcn;
	        public U8		            CP_Type;
	        public U16	                Crs_RP;
	        public U16	                Crs_RQ;
        }
    }
}
