namespace MTYS_OCR.Master
{
    partial class frmHolidayBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHolidayBatch));
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSun = new System.Windows.Forms.CheckBox();
            this.chkSat = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chkHon = new System.Windows.Forms.CheckBox();
            this.chkShiz = new System.Windows.Forms.CheckBox();
            this.chkOsk = new System.Windows.Forms.CheckBox();
            this.chkOskA = new System.Windows.Forms.CheckBox();
            this.chkOskB = new System.Windows.Forms.CheckBox();
            this.chkOskC = new System.Windows.Forms.CheckBox();
            this.chkOskD = new System.Windows.Forms.CheckBox();
            this.chkAmiH = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkShuku = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAmiD = new System.Windows.Forms.CheckBox();
            this.chkAmiC = new System.Windows.Forms.CheckBox();
            this.chkAmiB = new System.Windows.Forms.CheckBox();
            this.chkAmiA = new System.Windows.Forms.CheckBox();
            this.chkAmiO = new System.Windows.Forms.CheckBox();
            this.chkAmiS = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(367, 367);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(106, 36);
            this.btnRtn.TabIndex = 4;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(257, 367);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 36);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "一括登録(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 29);
            this.label2.TabIndex = 43;
            this.label2.Text = "設定期間：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dt2
            // 
            this.dt2.CustomFormat = "yyyy/MM/dd (ddd)";
            this.dt2.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt2.Location = new System.Drawing.Point(293, 7);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(140, 28);
            this.dt2.TabIndex = 70;
            // 
            // dt1
            // 
            this.dt1.CustomFormat = "yyyy/MM/dd (dddd)";
            this.dt1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Location = new System.Drawing.Point(123, 7);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(140, 28);
            this.dt1.TabIndex = 82;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(11, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 29);
            this.label7.TabIndex = 83;
            this.label7.Text = "曜日：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSun
            // 
            this.chkSun.AutoSize = true;
            this.chkSun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSun.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkSun.Location = new System.Drawing.Point(167, 10);
            this.chkSun.Name = "chkSun";
            this.chkSun.Size = new System.Drawing.Size(52, 22);
            this.chkSun.TabIndex = 84;
            this.chkSun.Text = "日曜";
            this.chkSun.UseVisualStyleBackColor = true;
            // 
            // chkSat
            // 
            this.chkSat.AutoSize = true;
            this.chkSat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSat.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkSat.Location = new System.Drawing.Point(238, 10);
            this.chkSat.Name = "chkSat";
            this.chkSat.Size = new System.Drawing.Size(52, 22);
            this.chkSat.TabIndex = 90;
            this.chkSat.Text = "土曜";
            this.chkSat.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(269, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 29);
            this.label11.TabIndex = 91;
            this.label11.Text = "～";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(8, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 40);
            this.label13.TabIndex = 92;
            this.label13.Text = "該当事業所と\r\n勤務報告書網掛け：\r\n\r\n";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkHon
            // 
            this.chkHon.AutoSize = true;
            this.chkHon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHon.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkHon.Location = new System.Drawing.Point(167, 10);
            this.chkHon.Name = "chkHon";
            this.chkHon.Size = new System.Drawing.Size(52, 22);
            this.chkHon.TabIndex = 93;
            this.chkHon.Text = "本社";
            this.chkHon.UseVisualStyleBackColor = true;
            // 
            // chkShiz
            // 
            this.chkShiz.AutoSize = true;
            this.chkShiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShiz.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkShiz.Location = new System.Drawing.Point(167, 38);
            this.chkShiz.Name = "chkShiz";
            this.chkShiz.Size = new System.Drawing.Size(52, 22);
            this.chkShiz.TabIndex = 94;
            this.chkShiz.Text = "静岡";
            this.chkShiz.UseVisualStyleBackColor = true;
            // 
            // chkOsk
            // 
            this.chkOsk.AutoSize = true;
            this.chkOsk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOsk.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOsk.Location = new System.Drawing.Point(167, 66);
            this.chkOsk.Name = "chkOsk";
            this.chkOsk.Size = new System.Drawing.Size(94, 22);
            this.chkOsk.TabIndex = 95;
            this.chkOsk.Text = "大阪製造部";
            this.chkOsk.UseVisualStyleBackColor = true;
            // 
            // chkOskA
            // 
            this.chkOskA.AutoSize = true;
            this.chkOskA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskA.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskA.Location = new System.Drawing.Point(167, 94);
            this.chkOskA.Name = "chkOskA";
            this.chkOskA.Size = new System.Drawing.Size(122, 22);
            this.chkOskA.TabIndex = 96;
            this.chkOskA.Text = "大阪製造部Ａ班";
            this.chkOskA.UseVisualStyleBackColor = true;
            // 
            // chkOskB
            // 
            this.chkOskB.AutoSize = true;
            this.chkOskB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskB.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskB.Location = new System.Drawing.Point(167, 122);
            this.chkOskB.Name = "chkOskB";
            this.chkOskB.Size = new System.Drawing.Size(122, 22);
            this.chkOskB.TabIndex = 97;
            this.chkOskB.Text = "大阪製造部Ｂ班";
            this.chkOskB.UseVisualStyleBackColor = true;
            // 
            // chkOskC
            // 
            this.chkOskC.AutoSize = true;
            this.chkOskC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskC.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskC.Location = new System.Drawing.Point(167, 150);
            this.chkOskC.Name = "chkOskC";
            this.chkOskC.Size = new System.Drawing.Size(122, 22);
            this.chkOskC.TabIndex = 98;
            this.chkOskC.Text = "大阪製造部Ｃ班";
            this.chkOskC.UseVisualStyleBackColor = true;
            // 
            // chkOskD
            // 
            this.chkOskD.AutoSize = true;
            this.chkOskD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskD.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskD.Location = new System.Drawing.Point(167, 178);
            this.chkOskD.Name = "chkOskD";
            this.chkOskD.Size = new System.Drawing.Size(122, 22);
            this.chkOskD.TabIndex = 99;
            this.chkOskD.Text = "大阪製造部Ｄ班";
            this.chkOskD.UseVisualStyleBackColor = true;
            // 
            // chkAmiH
            // 
            this.chkAmiH.AutoSize = true;
            this.chkAmiH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiH.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiH.Location = new System.Drawing.Point(309, 10);
            this.chkAmiH.Name = "chkAmiH";
            this.chkAmiH.Size = new System.Drawing.Size(86, 22);
            this.chkAmiH.TabIndex = 101;
            this.chkAmiH.Text = "網掛けする";
            this.chkAmiH.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(20, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 340);
            this.panel1.TabIndex = 102;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkShuku);
            this.panel3.Controls.Add(this.chkSat);
            this.panel3.Controls.Add(this.chkSun);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(11, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 41);
            this.panel3.TabIndex = 110;
            // 
            // chkShuku
            // 
            this.chkShuku.AutoSize = true;
            this.chkShuku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShuku.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkShuku.Location = new System.Drawing.Point(309, 10);
            this.chkShuku.Name = "chkShuku";
            this.chkShuku.Size = new System.Drawing.Size(52, 22);
            this.chkShuku.TabIndex = 102;
            this.chkShuku.Text = "祝日";
            this.chkShuku.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkAmiD);
            this.panel2.Controls.Add(this.chkAmiC);
            this.panel2.Controls.Add(this.chkAmiB);
            this.panel2.Controls.Add(this.chkAmiA);
            this.panel2.Controls.Add(this.chkAmiO);
            this.panel2.Controls.Add(this.chkAmiS);
            this.panel2.Controls.Add(this.chkAmiH);
            this.panel2.Controls.Add(this.chkOskD);
            this.panel2.Controls.Add(this.chkOskC);
            this.panel2.Controls.Add(this.chkOskB);
            this.panel2.Controls.Add(this.chkOskA);
            this.panel2.Controls.Add(this.chkOsk);
            this.panel2.Controls.Add(this.chkShiz);
            this.panel2.Controls.Add(this.chkHon);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Location = new System.Drawing.Point(11, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 212);
            this.panel2.TabIndex = 109;
            // 
            // chkAmiD
            // 
            this.chkAmiD.AutoSize = true;
            this.chkAmiD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiD.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiD.Location = new System.Drawing.Point(309, 178);
            this.chkAmiD.Name = "chkAmiD";
            this.chkAmiD.Size = new System.Drawing.Size(86, 22);
            this.chkAmiD.TabIndex = 108;
            this.chkAmiD.Text = "網掛けする";
            this.chkAmiD.UseVisualStyleBackColor = true;
            // 
            // chkAmiC
            // 
            this.chkAmiC.AutoSize = true;
            this.chkAmiC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiC.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiC.Location = new System.Drawing.Point(309, 150);
            this.chkAmiC.Name = "chkAmiC";
            this.chkAmiC.Size = new System.Drawing.Size(86, 22);
            this.chkAmiC.TabIndex = 107;
            this.chkAmiC.Text = "網掛けする";
            this.chkAmiC.UseVisualStyleBackColor = true;
            // 
            // chkAmiB
            // 
            this.chkAmiB.AutoSize = true;
            this.chkAmiB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiB.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiB.Location = new System.Drawing.Point(309, 122);
            this.chkAmiB.Name = "chkAmiB";
            this.chkAmiB.Size = new System.Drawing.Size(86, 22);
            this.chkAmiB.TabIndex = 106;
            this.chkAmiB.Text = "網掛けする";
            this.chkAmiB.UseVisualStyleBackColor = true;
            // 
            // chkAmiA
            // 
            this.chkAmiA.AutoSize = true;
            this.chkAmiA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiA.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiA.Location = new System.Drawing.Point(309, 94);
            this.chkAmiA.Name = "chkAmiA";
            this.chkAmiA.Size = new System.Drawing.Size(86, 22);
            this.chkAmiA.TabIndex = 105;
            this.chkAmiA.Text = "網掛けする";
            this.chkAmiA.UseVisualStyleBackColor = true;
            // 
            // chkAmiO
            // 
            this.chkAmiO.AutoSize = true;
            this.chkAmiO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiO.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiO.Location = new System.Drawing.Point(309, 66);
            this.chkAmiO.Name = "chkAmiO";
            this.chkAmiO.Size = new System.Drawing.Size(86, 22);
            this.chkAmiO.TabIndex = 104;
            this.chkAmiO.Text = "網掛けする";
            this.chkAmiO.UseVisualStyleBackColor = true;
            // 
            // chkAmiS
            // 
            this.chkAmiS.AutoSize = true;
            this.chkAmiS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiS.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiS.Location = new System.Drawing.Point(309, 38);
            this.chkAmiS.Name = "chkAmiS";
            this.chkAmiS.Size = new System.Drawing.Size(86, 22);
            this.chkAmiS.TabIndex = 103;
            this.chkAmiS.Text = "網掛けする";
            this.chkAmiS.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.dt1);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.dt2);
            this.panel4.Location = new System.Drawing.Point(11, 14);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(444, 44);
            this.panel4.TabIndex = 111;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(30, 375);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(132, 17);
            this.linkLabel1.TabIndex = 103;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "登録される祝日について";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmHolidayBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 415);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnUpdate);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmHolidayBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "休日一括登録";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dt2;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSun;
        private System.Windows.Forms.CheckBox chkSat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkHon;
        private System.Windows.Forms.CheckBox chkShiz;
        private System.Windows.Forms.CheckBox chkOsk;
        private System.Windows.Forms.CheckBox chkOskA;
        private System.Windows.Forms.CheckBox chkOskB;
        private System.Windows.Forms.CheckBox chkOskC;
        private System.Windows.Forms.CheckBox chkOskD;
        private System.Windows.Forms.CheckBox chkAmiH;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkShuku;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkAmiD;
        private System.Windows.Forms.CheckBox chkAmiC;
        private System.Windows.Forms.CheckBox chkAmiB;
        private System.Windows.Forms.CheckBox chkAmiA;
        private System.Windows.Forms.CheckBox chkAmiO;
        private System.Windows.Forms.CheckBox chkAmiS;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}