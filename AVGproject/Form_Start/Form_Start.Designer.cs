namespace AGVproject
{
    partial class Form_Start
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.xSpeed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ySpeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.aSpeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.urgBaudRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.urgPortName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.currKeyPos = new System.Windows.Forms.Button();
            this.CorrectPos = new System.Windows.Forms.Button();
            this.CurrKeyPosMSG = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "PortName";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(102, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baudrate";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(45, 95);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(157, 223);
            this.textBox3.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Open Control Port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xSpeed
            // 
            this.xSpeed.Location = new System.Drawing.Point(102, 324);
            this.xSpeed.Name = "xSpeed";
            this.xSpeed.Size = new System.Drawing.Size(100, 21);
            this.xSpeed.TabIndex = 7;
            this.xSpeed.TextChanged += new System.EventHandler(this.xSpeed_TextChanged);
            this.xSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xSpeed_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "xSpeed";
            // 
            // ySpeed
            // 
            this.ySpeed.Location = new System.Drawing.Point(102, 351);
            this.ySpeed.Name = "ySpeed";
            this.ySpeed.Size = new System.Drawing.Size(100, 21);
            this.ySpeed.TabIndex = 9;
            this.ySpeed.TextChanged += new System.EventHandler(this.ySpeed_TextChanged);
            this.ySpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ySpeed_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "ySpeed";
            // 
            // aSpeed
            // 
            this.aSpeed.Location = new System.Drawing.Point(102, 378);
            this.aSpeed.Name = "aSpeed";
            this.aSpeed.Size = new System.Drawing.Size(100, 21);
            this.aSpeed.TabIndex = 11;
            this.aSpeed.TextChanged += new System.EventHandler(this.aSpeed_TextChanged);
            this.aSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.aSpeed_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 381);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "aSpeed";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(256, 66);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(157, 23);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Open URG Port";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // urgBaudRate
            // 
            this.urgBaudRate.Location = new System.Drawing.Point(313, 39);
            this.urgBaudRate.Name = "urgBaudRate";
            this.urgBaudRate.Size = new System.Drawing.Size(100, 21);
            this.urgBaudRate.TabIndex = 16;
            this.urgBaudRate.TextChanged += new System.EventHandler(this.urgBaudRate_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "Baudrate";
            // 
            // urgPortName
            // 
            this.urgPortName.Location = new System.Drawing.Point(313, 12);
            this.urgPortName.Name = "urgPortName";
            this.urgPortName.Size = new System.Drawing.Size(100, 21);
            this.urgPortName.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(254, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "PortName";
            // 
            // currKeyPos
            // 
            this.currKeyPos.Location = new System.Drawing.Point(256, 110);
            this.currKeyPos.Name = "currKeyPos";
            this.currKeyPos.Size = new System.Drawing.Size(157, 23);
            this.currKeyPos.TabIndex = 17;
            this.currKeyPos.Text = "Get Current Point";
            this.currKeyPos.UseVisualStyleBackColor = true;
            this.currKeyPos.Click += new System.EventHandler(this.currKeyPos_Click);
            // 
            // CorrectPos
            // 
            this.CorrectPos.Location = new System.Drawing.Point(256, 376);
            this.CorrectPos.Name = "CorrectPos";
            this.CorrectPos.Size = new System.Drawing.Size(157, 23);
            this.CorrectPos.TabIndex = 18;
            this.CorrectPos.Text = "Correct Position";
            this.CorrectPos.UseVisualStyleBackColor = true;
            // 
            // CurrKeyPosMSG
            // 
            this.CurrKeyPosMSG.Location = new System.Drawing.Point(256, 139);
            this.CurrKeyPosMSG.Multiline = true;
            this.CurrKeyPosMSG.Name = "CurrKeyPosMSG";
            this.CurrKeyPosMSG.ReadOnly = true;
            this.CurrKeyPosMSG.Size = new System.Drawing.Size(157, 223);
            this.CurrKeyPosMSG.TabIndex = 19;
            // 
            // Form_Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 433);
            this.Controls.Add(this.CurrKeyPosMSG);
            this.Controls.Add(this.CorrectPos);
            this.Controls.Add(this.currKeyPos);
            this.Controls.Add(this.urgBaudRate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.urgPortName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.aSpeed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ySpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xSpeed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form_Start";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Start_FormClosed);
            this.Load += new System.EventHandler(this.Form_Start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox xSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ySpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox aSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox urgBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox urgPortName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button currKeyPos;
        private System.Windows.Forms.Button CorrectPos;
        private System.Windows.Forms.TextBox CurrKeyPosMSG;
    }
}

