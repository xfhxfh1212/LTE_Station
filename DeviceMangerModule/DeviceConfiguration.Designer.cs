namespace DeviceMangerModule
{
    partial class DeviceConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceConfiguration));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox_SaveDataType = new System.Windows.Forms.ComboBox();
            this.comboBox_InterfaceType = new System.Windows.Forms.ComboBox();
            this.comboBox_WorkMode = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_DataStoreTime = new System.Windows.Forms.TextBox();
            this.comboBox_DataStoreMode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_OutDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_MsgPortNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_DataPortNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_IpAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_DeviceName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(10, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(293, 272);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.comboBox_SaveDataType);
            this.tabPage1.Controls.Add(this.comboBox_InterfaceType);
            this.tabPage1.Controls.Add(this.comboBox_WorkMode);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBox_DataStoreTime);
            this.tabPage1.Controls.Add(this.comboBox_DataStoreMode);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.comboBox_OutDataType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox_MsgPortNum);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textBox_DataPortNum);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBox_IpAddress);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboBox_DeviceName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(285, 246);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Common";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(131, 210);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "重设IP和Port";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBox_SaveDataType
            // 
            this.comboBox_SaveDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SaveDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_SaveDataType.FormattingEnabled = true;
            this.comboBox_SaveDataType.Items.AddRange(new object[] {
            "CSV",
            "BIN"});
            this.comboBox_SaveDataType.Location = new System.Drawing.Point(391, 157);
            this.comboBox_SaveDataType.Name = "comboBox_SaveDataType";
            this.comboBox_SaveDataType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_SaveDataType.TabIndex = 28;
            this.comboBox_SaveDataType.Visible = false;
            // 
            // comboBox_InterfaceType
            // 
            this.comboBox_InterfaceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_InterfaceType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_InterfaceType.FormattingEnabled = true;
            this.comboBox_InterfaceType.Items.AddRange(new object[] {
            "USB",
            "LAN"});
            this.comboBox_InterfaceType.Location = new System.Drawing.Point(391, 58);
            this.comboBox_InterfaceType.Name = "comboBox_InterfaceType";
            this.comboBox_InterfaceType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_InterfaceType.TabIndex = 27;
            this.comboBox_InterfaceType.Visible = false;
            this.comboBox_InterfaceType.SelectedIndexChanged += new System.EventHandler(this.comboBox_InterfaceType_SelectedIndexChanged);
            // 
            // comboBox_WorkMode
            // 
            this.comboBox_WorkMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_WorkMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_WorkMode.FormattingEnabled = true;
            this.comboBox_WorkMode.Items.AddRange(new object[] {
            "CellScan",
            "ProtocolTrack"});
            this.comboBox_WorkMode.Location = new System.Drawing.Point(391, 40);
            this.comboBox_WorkMode.Name = "comboBox_WorkMode";
            this.comboBox_WorkMode.Size = new System.Drawing.Size(121, 20);
            this.comboBox_WorkMode.TabIndex = 26;
            this.comboBox_WorkMode.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(284, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "Save Data Type";
            this.label11.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "Interface Type";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(284, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "Work Mode";
            this.label6.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(518, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "(ms)";
            this.label10.Visible = false;
            // 
            // textBox_DataStoreTime
            // 
            this.textBox_DataStoreTime.Location = new System.Drawing.Point(391, 129);
            this.textBox_DataStoreTime.Name = "textBox_DataStoreTime";
            this.textBox_DataStoreTime.Size = new System.Drawing.Size(121, 21);
            this.textBox_DataStoreTime.TabIndex = 21;
            this.textBox_DataStoreTime.Visible = false;
            // 
            // comboBox_DataStoreMode
            // 
            this.comboBox_DataStoreMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataStoreMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_DataStoreMode.FormattingEnabled = true;
            this.comboBox_DataStoreMode.Items.AddRange(new object[] {
            "TimingSave",
            "StoredContiguously",
            "TriggerSave"});
            this.comboBox_DataStoreMode.Location = new System.Drawing.Point(391, 103);
            this.comboBox_DataStoreMode.Name = "comboBox_DataStoreMode";
            this.comboBox_DataStoreMode.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DataStoreMode.TabIndex = 20;
            this.comboBox_DataStoreMode.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "Data Store Time";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "Save Mode";
            this.label8.Visible = false;
            // 
            // comboBox_OutDataType
            // 
            this.comboBox_OutDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_OutDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_OutDataType.FormattingEnabled = true;
            this.comboBox_OutDataType.Items.AddRange(new object[] {
            " IQ",
            "TestData",
            "MeasuringData",
            "LayerOutData"});
            this.comboBox_OutDataType.Location = new System.Drawing.Point(391, 80);
            this.comboBox_OutDataType.Name = "comboBox_OutDataType";
            this.comboBox_OutDataType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_OutDataType.TabIndex = 17;
            this.comboBox_OutDataType.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Output Data Type";
            this.label2.Visible = false;
            // 
            // textBox_MsgPortNum
            // 
            this.textBox_MsgPortNum.Location = new System.Drawing.Point(131, 159);
            this.textBox_MsgPortNum.Name = "textBox_MsgPortNum";
            this.textBox_MsgPortNum.Size = new System.Drawing.Size(121, 21);
            this.textBox_MsgPortNum.TabIndex = 8;
            this.textBox_MsgPortNum.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "消息端口";
            this.label5.Visible = false;
            // 
            // textBox_DataPortNum
            // 
            this.textBox_DataPortNum.Location = new System.Drawing.Point(131, 119);
            this.textBox_DataPortNum.Name = "textBox_DataPortNum";
            this.textBox_DataPortNum.Size = new System.Drawing.Size(121, 21);
            this.textBox_DataPortNum.TabIndex = 6;
            this.textBox_DataPortNum.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "数据端口";
            this.label4.Visible = false;
            // 
            // textBox_IpAddress
            // 
            this.textBox_IpAddress.Location = new System.Drawing.Point(131, 79);
            this.textBox_IpAddress.Name = "textBox_IpAddress";
            this.textBox_IpAddress.Size = new System.Drawing.Size(121, 21);
            this.textBox_IpAddress.TabIndex = 4;
            this.textBox_IpAddress.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "IP地址";
            this.label3.Visible = false;
            // 
            // comboBox_DeviceName
            // 
            this.comboBox_DeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DeviceName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_DeviceName.FormattingEnabled = true;
            this.comboBox_DeviceName.Location = new System.Drawing.Point(131, 35);
            this.comboBox_DeviceName.Name = "comboBox_DeviceName";
            this.comboBox_DeviceName.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DeviceName.TabIndex = 1;
            this.comboBox_DeviceName.SelectedIndexChanged += new System.EventHandler(this.comboBox_DeviceName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名称";
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(211, 288);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_CloseClick);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(115, 288);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // DeviceConfiguration
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 327);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(319, 356);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(319, 356);
            this.Name = "DeviceConfiguration";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设备配置";
            this.Load += new System.EventHandler(this.DeviceConfiguration_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeviceConfiguration_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox_MsgPortNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_DataPortNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_IpAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_DeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_DataStoreTime;
        private System.Windows.Forms.ComboBox comboBox_DataStoreMode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_OutDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_SaveDataType;
        private System.Windows.Forms.ComboBox comboBox_InterfaceType;
        private System.Windows.Forms.ComboBox comboBox_WorkMode;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}