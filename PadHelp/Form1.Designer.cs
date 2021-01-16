namespace PadHelp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StartRead_btn = new System.Windows.Forms.Button();
            this.HttpAddrs_txt = new System.Windows.Forms.TextBox();
            this.ShowMSG_txt = new System.Windows.Forms.TextBox();
            this.Test_btn = new System.Windows.Forms.Button();
            this.Awake_btn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartRead_btn
            // 
            this.StartRead_btn.Location = new System.Drawing.Point(26, 112);
            this.StartRead_btn.Name = "StartRead_btn";
            this.StartRead_btn.Size = new System.Drawing.Size(137, 58);
            this.StartRead_btn.TabIndex = 0;
            this.StartRead_btn.Text = "Read";
            this.StartRead_btn.UseVisualStyleBackColor = true;
            this.StartRead_btn.Click += new System.EventHandler(this.Read_ClickAsync);
            // 
            // HttpAddrs_txt
            // 
            this.HttpAddrs_txt.Location = new System.Drawing.Point(26, 58);
            this.HttpAddrs_txt.Name = "HttpAddrs_txt";
            this.HttpAddrs_txt.Size = new System.Drawing.Size(256, 25);
            this.HttpAddrs_txt.TabIndex = 1;
            // 
            // ShowMSG_txt
            // 
            this.ShowMSG_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowMSG_txt.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowMSG_txt.Location = new System.Drawing.Point(300, 12);
            this.ShowMSG_txt.Multiline = true;
            this.ShowMSG_txt.Name = "ShowMSG_txt";
            this.ShowMSG_txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ShowMSG_txt.Size = new System.Drawing.Size(334, 417);
            this.ShowMSG_txt.TabIndex = 2;
            // 
            // Test_btn
            // 
            this.Test_btn.Location = new System.Drawing.Point(26, 222);
            this.Test_btn.Name = "Test_btn";
            this.Test_btn.Size = new System.Drawing.Size(137, 58);
            this.Test_btn.TabIndex = 3;
            this.Test_btn.Text = "Test_BTN";
            this.Test_btn.UseVisualStyleBackColor = true;
            this.Test_btn.Click += new System.EventHandler(this.Test_btn_ClickAsync);
            // 
            // Awake_btn
            // 
            this.Awake_btn.Location = new System.Drawing.Point(26, 286);
            this.Awake_btn.Name = "Awake_btn";
            this.Awake_btn.Size = new System.Drawing.Size(137, 58);
            this.Awake_btn.TabIndex = 4;
            this.Awake_btn.Text = "覺醒資料下載";
            this.Awake_btn.UseVisualStyleBackColor = true;
            this.Awake_btn.Click += new System.EventHandler(this.Awake_btn_ClickAsync);
            this.Awake_btn.Resize += new System.EventHandler(this.Awake_btn_Resize);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "龍族拼圖";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 58);
            this.button1.TabIndex = 5;
            this.button1.Text = "testbtn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 441);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Awake_btn);
            this.Controls.Add(this.Test_btn);
            this.Controls.Add(this.ShowMSG_txt);
            this.Controls.Add(this.HttpAddrs_txt);
            this.Controls.Add(this.StartRead_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartRead_btn;
        private System.Windows.Forms.TextBox HttpAddrs_txt;
        private System.Windows.Forms.TextBox ShowMSG_txt;
        private System.Windows.Forms.Button Test_btn;
        private System.Windows.Forms.Button Awake_btn;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button1;
    }
}

