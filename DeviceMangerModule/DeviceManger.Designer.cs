namespace DeviceMangerModule
{
    partial class DeviceManger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceManger));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_deviceList = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DeviceListView = new System.Windows.Forms.ListView();
            this.DeviceInfoListView = new System.Windows.Forms.ListView();
            this.ParameterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_RemoveDevice = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.button_close = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_NewDevice = new System.Windows.Forms.Button();
            this.cmsRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.Configure = new System.Windows.Forms.ToolStripMenuItem();
            this.Reboot = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage_deviceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.cmsRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "connected.ico");
            this.imageList1.Images.SetKeyName(1, "disconnect.ico");
            // 
            // tabPage_deviceList
            // 
            this.tabPage_deviceList.Controls.Add(this.splitContainer1);
            this.tabPage_deviceList.Location = new System.Drawing.Point(4, 22);
            this.tabPage_deviceList.Name = "tabPage_deviceList";
            this.tabPage_deviceList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_deviceList.Size = new System.Drawing.Size(591, 203);
            this.tabPage_deviceList.TabIndex = 0;
            this.tabPage_deviceList.Text = "Instrument";
            this.tabPage_deviceList.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DeviceListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DeviceInfoListView);
            this.splitContainer1.Size = new System.Drawing.Size(585, 197);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 0;
            // 
            // DeviceListView
            // 
            this.DeviceListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeviceListView.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeviceListView.FullRowSelect = true;
            this.DeviceListView.Location = new System.Drawing.Point(0, 0);
            this.DeviceListView.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.DeviceListView.MultiSelect = false;
            this.DeviceListView.Name = "DeviceListView";
            this.DeviceListView.Size = new System.Drawing.Size(170, 198);
            this.DeviceListView.SmallImageList = this.imageList1;
            this.DeviceListView.TabIndex = 0;
            this.DeviceListView.UseCompatibleStateImageBehavior = false;
            this.DeviceListView.View = System.Windows.Forms.View.List;
            this.DeviceListView.SelectedIndexChanged += new System.EventHandler(this.DeviceListView_SelectedIndexChanged);
            this.DeviceListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeviceListView_MouseClick);
            // 
            // DeviceInfoListView
            // 
            this.DeviceInfoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ParameterName,
            this.Value});
            this.DeviceInfoListView.Enabled = false;
            this.DeviceInfoListView.FullRowSelect = true;
            this.DeviceInfoListView.GridLines = true;
            this.DeviceInfoListView.Location = new System.Drawing.Point(0, 0);
            this.DeviceInfoListView.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.DeviceInfoListView.MultiSelect = false;
            this.DeviceInfoListView.Name = "DeviceInfoListView";
            this.DeviceInfoListView.Scrollable = false;
            this.DeviceInfoListView.Size = new System.Drawing.Size(416, 198);
            this.DeviceInfoListView.TabIndex = 0;
            this.DeviceInfoListView.TabStop = false;
            this.DeviceInfoListView.UseCompatibleStateImageBehavior = false;
            this.DeviceInfoListView.View = System.Windows.Forms.View.Details;
            // 
            // ParameterName
            // 
            this.ParameterName.Text = "参数名称";
            this.ParameterName.Width = 148;
            // 
            // Value
            // 
            this.Value.Text = "值";
            this.Value.Width = 436;
            // 
            // button_RemoveDevice
            // 
            this.button_RemoveDevice.Location = new System.Drawing.Point(373, 343);
            this.button_RemoveDevice.Name = "button_RemoveDevice";
            this.button_RemoveDevice.Size = new System.Drawing.Size(108, 24);
            this.button_RemoveDevice.TabIndex = 6;
            this.button_RemoveDevice.Text = "Remove";
            this.button_RemoveDevice.UseVisualStyleBackColor = true;
            this.button_RemoveDevice.Visible = false;
            this.button_RemoveDevice.Click += new System.EventHandler(this.button_RemoveDevice_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_deviceList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(10, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(599, 229);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(501, 256);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(101, 23);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "关闭";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_connect
            // 
            this.button_connect.Enabled = false;
            this.button_connect.Location = new System.Drawing.Point(256, 256);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(101, 23);
            this.button_connect.TabIndex = 2;
            this.button_connect.Text = "连接";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(378, 256);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(101, 23);
            this.button_disconnect.TabIndex = 3;
            this.button_disconnect.Text = "断开";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_NewDevice
            // 
            this.button_NewDevice.Location = new System.Drawing.Point(241, 343);
            this.button_NewDevice.Name = "button_NewDevice";
            this.button_NewDevice.Size = new System.Drawing.Size(101, 23);
            this.button_NewDevice.TabIndex = 5;
            this.button_NewDevice.Text = "New Device";
            this.button_NewDevice.UseVisualStyleBackColor = true;
            this.button_NewDevice.Visible = false;
            this.button_NewDevice.Click += new System.EventHandler(this.button_NewDevice_Click);
            // 
            // cmsRight
            // 
            this.cmsRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.Delete,
            this.Configure,
            this.Reboot});
            this.cmsRight.Name = "cmsRight";
            this.cmsRight.Size = new System.Drawing.Size(134, 92);
            // 
            // Add
            // 
            this.Add.Image = ((System.Drawing.Image)(resources.GetObject("Add.Image")));
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(133, 22);
            this.Add.Text = "Add";
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Delete
            // 
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(133, 22);
            this.Delete.Text = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Configure
            // 
            this.Configure.Image = ((System.Drawing.Image)(resources.GetObject("Configure.Image")));
            this.Configure.Name = "Configure";
            this.Configure.Size = new System.Drawing.Size(133, 22);
            this.Configure.Text = "Configure";
            this.Configure.Click += new System.EventHandler(this.Rename_Click);
            // 
            // Reboot
            // 
            this.Reboot.Image = ((System.Drawing.Image)(resources.GetObject("Reboot.Image")));
            this.Reboot.Name = "Reboot";
            this.Reboot.Size = new System.Drawing.Size(133, 22);
            this.Reboot.Text = "Reboot";
            this.Reboot.Click += new System.EventHandler(this.rebootToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Add_Click);
            // 
            // DeviceManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 284);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_RemoveDevice);
            this.Controls.Add(this.button_NewDevice);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_connect);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(635, 323);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(635, 323);
            this.Name = "DeviceManger";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设备管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceManger_FormClosing);
            this.tabPage_deviceList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.cmsRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage_deviceList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView DeviceListView;
        private System.Windows.Forms.ListView DeviceInfoListView;
        private System.Windows.Forms.ColumnHeader ParameterName;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_NewDevice;
        private System.Windows.Forms.Button button_RemoveDevice;
        private System.Windows.Forms.ContextMenuStrip cmsRight;
        private System.Windows.Forms.ToolStripMenuItem Add;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ToolStripMenuItem Configure;
        private System.Windows.Forms.ToolStripMenuItem Reboot;
        private System.Windows.Forms.Button button1;
    }
}

