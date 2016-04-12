using System;
using Renci.SshNet;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Routrek.Crypto;
using Routrek.SSHC;
using Routrek.SSHCV1;
using Routrek.SSHCV2;
using Routrek.Toolkit;
using Routrek.PKI;
using System.Text;
//using System.Timers;

namespace WugSshLib
{
    /// <summary>
    /// ssh协议
    /// </summary>
    public class SshReader : ISSHConnectionEventReceiver, ISSHChannelEventReceiver
    {
        public SSHConnection _conn;
        public SSHChannel _pf;
        public bool _ready;
        public string msg = "";

        private SSHConnectionParameter f = new SSHConnectionParameter();
        private string host, username, password;
        private int port;
        private Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //private System.Timers.Timer timer;
        public SshReader(string host, int port, string username, string password)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        }
        public SshReader()
        {
            // TODO: Complete member initialization
        }
        public void OpenConnect()
        {
            f.UserName = username;
            f.Password = password;
            f.Protocol = SSHProtocol.SSH2;
            f.AuthenticationType = AuthenticationType.Password;
            f.WindowSize = 0x1000;
            s.Connect(new IPEndPoint(IPAddress.Parse(host), port));
            _conn = SSHConnection.Connect(f, this, s);
            this._pf = _conn.OpenShell(this);
            SSHConnectionInfo ci = _conn.ConnectionInfo;
        }
        public void CloseConnect()
        {
            this.msg = "";
            this._pf.Close();
            this._conn.Close();
            this.s.Close();
        }
        public void WaitString(string s,int time)
        {            
            int count = 0;
            Thread.Sleep(100); 
            while (true)
            {
                if (this.msg.IndexOf(s) > 0)
                {                   
                    return;
                }
                if (count > 99999)
                {
                    Thread.Sleep(time);                    
                    return;
                }
                count++;
                //timer = new System.Timers.Timer(10);
                //timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //timer.Enabled = true;
            }           

        }
        //private void timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Thread.CurrentThread.IsBackground = false;
        //    timer.Enabled = false;
        //}
        public void clearMsg()
        {
            int start = this.msg.IndexOf("\n");
            int end = this.msg.LastIndexOf("\r");
            try
            {               
                if (start >= 0 && start <= end)
                {
                    this.msg = this.msg.Remove(end).Remove(0, start + 1);
                    //this.msg = this.msg.Remove(0, start + 1);                   
                }               
            }
            catch { }
        }
        public void InputCommand(string command)
        {
            this.msg = "";
            byte[] data = (new UnicodeEncoding()).GetBytes(command + "\n");
            this._pf.Transmit(data);
        }
        public void OnData(byte[] data, int offset, int length)
        {
            System.Console.Write(Encoding.ASCII.GetString(data, offset, length));
            msg += Encoding.ASCII.GetString(data, offset, length);
        }
        public void OnDebugMessage(bool always_display, byte[] data)
        {
            Debug.WriteLine("DEBUG: " + Encoding.ASCII.GetString(data));
        }
        public void OnIgnoreMessage(byte[] data)
        {
            Debug.WriteLine("Ignore: " + Encoding.ASCII.GetString(data));
        }
        public void OnAuthenticationPrompt(string[] msg)
        {
            Debug.WriteLine("Auth Prompt " + msg[0]);
        }

        public void OnError(Exception error, string msg)
        {
            Debug.WriteLine("ERROR: " + msg);
        }
        public void OnChannelClosed()
        {
            Debug.WriteLine("Channel closed");
            _conn.Disconnect("");
            //_conn.AsyncReceive(this);
        }
        public void OnChannelEOF()
        {
            _pf.Close();
            Debug.WriteLine("Channel EOF");
        }
        public void OnExtendedData(int type, byte[] data)
        {
            Debug.WriteLine("EXTENDED DATA");
        }
        public void OnConnectionClosed()
        {
            Debug.WriteLine("Connection closed");
        }
        public void OnUnknownMessage(byte type, byte[] data)
        {
            Debug.WriteLine("Unknown Message " + type);
        }
        public void OnChannelReady()
        {
            _ready = true;
        }
        public void OnChannelError(Exception error, string msg)
        {
            Debug.WriteLine("Channel ERROR: " + msg);
        }
        public void OnMiscPacket(byte type, byte[] data, int offset, int length)
        {
        }

        public PortForwardingCheckResult CheckPortForwardingRequest(string host, int port, string originator_host, int originator_port)
        {
            PortForwardingCheckResult r = new PortForwardingCheckResult();
            r.allowed = true;
            r.channel = this;
            return r;
        }
        public void EstablishPortforwarding(ISSHChannelEventReceiver rec, SSHChannel channel)
        {
            _pf = channel;
        }
    }
    /// <summary>
    /// 文件传输
    /// </summary>
    public class ScpClient_Ex
    {
        private string dir_upload;
        private string file_upload;
        private string upload_name;
        private string dir_download;
        private string file_download;
        private string download_name;
        private ScpClient scpClient;
        public string msg;

        public string Dir_Upload
        {
            set
            {
                this.dir_upload = value;
            }
            get
            {
                return this.dir_upload;
            }
        }
        public string File_Upload
        {
            set
            {
                this.file_upload = value;
            }
            get
            {
                return this.file_upload;
            }
        }
        public string Dir_Download
        {
            set
            {
                this.dir_download = value;
            }
            get
            {
                return this.dir_download;
            }
        }
        public string File_Download
        {
            set
            {
                this.file_download = value;
            }
            get
            {
                return this.file_download;
            }
        }
        public string Upload_Name
        {
            set
            {
                this.upload_name = value;
            }
            get
            {
                return this.upload_name;
            }
        }
        public string Download_Name
        {
            set
            {
                this.download_name = value;
            }
            get
            {
                return this.download_name;
            }
        }
        public ScpClient_Ex(string host, int port, string username, string password)
        {
            dir_upload = "";
            file_upload = "";
            dir_download = "";
            file_download = "";
            upload_name = "";
            download_name = "";
            scpClient = new ScpClient(host, port, username, password);
            scpClient.BufferSize = 1024;
        }
        public void DownloadFile()
        {
            if (!download_name.Equals(""))
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file_download);
                    this.scpClient.Connect();
                    this.scpClient.Download(download_name, fi);
                    this.scpClient.Disconnect();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.msg = "DownloadFile Error: " + ex.Message;
                }
            }
            else
            {
                this.msg = "下载文件路径错误或者目标路径名不存在！";
            }
        }
        //public void DownloadToDir()
        //{
        //    if (!download_name.Equals("") && System.IO.Directory.Exists(dir_download))
        //    {
        //        try
        //        {
        //            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dir_download);
        //            this.scpClient.Connect();
        //            this.scpClient.Download(download_name, dir);
        //            this.scpClient.Disconnect();
        //            Thread.Sleep(100);
        //        }
        //        catch (Exception ex)
        //        {
        //            this.msg = "DownloadDirectory Error: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        this.msg = "下载目录路径错误或者目标路径名不存在！";
        //    }
        //}
        public void UploadFile()
        {
            if (!upload_name.Equals("") && System.IO.File.Exists(file_upload))
            {
                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file_upload);
                    this.scpClient.Connect();
                    this.scpClient.Upload(fi, upload_name);
                    this.scpClient.Disconnect();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.msg = "UploadFile Error: " + ex.Message;
                }
            }
            else
            {
                this.msg = "上传文件路径不存在或者目标文件名错误！";
            }
        }
        public void UploadFromDir()
        {
            if (!upload_name.Equals("") && System.IO.Directory.Exists(dir_upload))
            {
                try
                {
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dir_upload);
                    this.scpClient.Connect();
                    this.scpClient.Upload(dir, upload_name);
                    this.scpClient.Disconnect();
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.msg = "UploadDirectory Error: " + ex.Message;
                }
            }
            else
            {
                this.msg = "上传目录路径不存在或者目标目录名错误！";
            }
        }
        ~ScpClient_Ex()
        {
            this.scpClient.Dispose();
        }
    }
    ///// <summary>
    ///// 测试类
    ///// </summary>
    //static class Test
    //{
    //    /// <summary>
    //    /// 应用程序的主入口点。
    //    /// </summary>        
    //    // private static SSHConnection _conn;

    //    [STAThread]
    //    static void Main()
    //    {
            //SshReader ssh = new SshReader("10.34.130.44", 22, "root", "vnak@$^");
            //ssh.OpenConnect();
            //ssh.WaitString("]#");
            //ssh.InputCommand("lsb_release");
            //ssh.WaitString("]#");
            //string output1 = ssh.clearMsg();
            //ssh.InputCommand("cat /etc/*release");
            //ssh.WaitString("]#");
            //string output2 = ssh.clearMsg();
            //ssh.WaitString("]#");

            //ssh.WaitString("]#");
            //ssh.InputCommand("rm -fr /home/wugang/upload");

            //ScpClient_Ex sce1 = new ScpClient_Ex("10.34.135.152", 22, "wugang", "2145980");
            //sce1.Dir_Upload = @"D:\TDDOWNLOAD\CY1908";
            //sce1.Upload_Name = "upload";
            //Thread temp = new Thread(sce1.UploadFromDir);
            //temp.Start();

            //ssh.WaitString("]#");
            //ssh.InputCommand("rm -fr /root/wugang/wug.txt");

            //ScpClient_Ex sce = new ScpClient_Ex("10.34.130.44", 22, "root", "vnak@$^");
            //sce.File_Upload = @"E:\wug.txt";
            //sce.Upload_Name = "wug.txt";
            //sce.UploadFile();

            //sce.File_Download = @"E:\wugang.txt";
            //sce.Download_Name = "/home/wugang/wugang.txt";
            //sce.DownloadFile();            

            //ssh.CloseConnect();
    //        //Application.Exit();
    //    }
    //}
}
