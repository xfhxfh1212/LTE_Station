namespace DeviceMangerModule
{
    partial class NewDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDevice));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_DeviceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_ConnecType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_IPAddress = new System.Windows.Forms.TextBox();
            this.textBox_DataPortNum = new System.Windows.Forms.TextBox();
            this.textBox_SignalPortNum = new System.Windows.Forms.TextBox();
            this.button_Connect = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名称";
            // 
            // textBox_DeviceName
            // 
            this.textBox_DeviceName.Location = new System.Drawing.Point(144, 33);
            this.textBox_DeviceName.MaxLength = 4;
            this.textBox_DeviceName.Name = "textBox_DeviceName";
            this.textBox_DeviceName.Size = new System.Drawing.Size(121, 21);
            this.textBox_DeviceName.TabIndex = 1;
            this.textBox_DeviceName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DeviceName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "连接方式";
            // 
            // comboBox_ConnecType
            // 
            this.comboBox_ConnecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ConnecType.Enabled = false;
            this.comboBox_ConnecType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_ConnecType.FormattingEnabled = true;
            this.comboBox_ConnecType.Items.AddRange(new object[] {
            "LAN",
            "USB"});
            this.comboBox_ConnecType.Location = new System.Drawing.Point(144, 76);
            this.comboBox_ConnecType.Name = "comboBox_ConnecType";
            this.comboBox_ConnecType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ConnecType.TabIndex = 3;
            this.comboBox_ConnecType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ConnecType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "数据端口号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "消息端口号";
            // 
            // textBox_IPAddress
            // 
            this.textBox_IPAddress.Location = new System.Drawing.Point(144, 109);
            this.textBox_IPAddress.MaxLength = 15;
            this.textBox_IPAddress.Name = "textBox_IPAddress";
            this.textBox_IPAddress.Size = new System.Drawing.Size(121, 21);
            this.textBox_IPAddress.TabIndex = 7;
            // 
            // textBox_DataPortNum
            // 
            this.textBox_DataPortNum.Location = new System.Drawing.Point(144, 144);
            this.textBox_DataPortNum.MaxLength = 4;
            this.textBox_DataPortNum.Name = "textBox_DataPortNum";
            this.textBox_DataPortNum.Size = new System.Drawing.Size(121, 21);
            this.textBox_DataPortNum.TabIndex = 8;
            this.textBox_DataPortNum.Text = "3333";
            // 
            // textBox_SignalPortNum
            // 
            this.textBox_SignalPortNum.Location = new System.Drawing.Point(144, 179);
            this.textBox_SignalPortNum.MaxLength = 4;
            this.textBox_SignalPortNum.Name = "textBox_SignalPortNum";
            this.textBox_SignalPortNum.Size = new System.Drawing.Size(121, 21);
            this.textBox_SignalPortNum.TabIndex = 9;
            this.textBox_SignalPortNum.Text = "4444";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(91, 227);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_Connect.TabIndex = 10;
            this.button_Connect.Text = "添加";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(190, 227);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // NewDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.textBox_SignalPortNum);
            this.Controls.Add(this.textBox_DataPortNum);
            this.Controls.Add(this.textBox_IPAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_ConnecType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_DeviceName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "NewDevice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新设备";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_DeviceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_ConnecType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_IPAddress;
        private System.Windows.Forms.TextBox textBox_DataPortNum;
        private System.Windows.Forms.TextBox textBox_SignalPortNum;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Button button_cancel;
    }
}