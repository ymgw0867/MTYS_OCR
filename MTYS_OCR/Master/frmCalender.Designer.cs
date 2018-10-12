namespace MTYS_OCR.Master
{
    partial class frmCalender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalender));
            this.dg = new System.Windows.Forms.DataGridView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkAmiD = new System.Windows.Forms.CheckBox();
            this.chkAmiB = new System.Windows.Forms.CheckBox();
            this.chkAmiC = new System.Windows.Forms.CheckBox();
            this.chkAmiS = new System.Windows.Forms.CheckBox();
            this.chkAmiO = new System.Windows.Forms.CheckBox();
            this.chkAmiA = new System.Windows.Forms.CheckBox();
            this.chkHon = new System.Windows.Forms.CheckBox();
            this.chkOskC = new System.Windows.Forms.CheckBox();
            this.chkShiz = new System.Windows.Forms.CheckBox();
            this.chkOskB = new System.Windows.Forms.CheckBox();
            this.chkOskD = new System.Windows.Forms.CheckBox();
            this.chkAmiH = new System.Windows.Forms.CheckBox();
            this.chkOsk = new System.Windows.Forms.CheckBox();
            this.chkOskA = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDt = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTekiyou = new System.Windows.Forms.ComboBox();
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(11, 28);
            this.dg.Margin = new System.Windows.Forms.Padding(4);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 21;
            this.dg.Size = new System.Drawing.Size(546, 642);
            this.dg.TabIndex = 0;
            this.dg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(448, 6);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(51, 17);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "← 前月";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(505, 6);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(51, 17);
            this.linkLabel2.TabIndex = 2;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "翌月 →";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel2.Click += new System.EventHandler(this.linkLabel2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(201, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(78, 21);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "休日表示";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(286, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(154, 21);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "勤務報告書網掛け表示";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtDt);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbTekiyou);
            this.panel1.Location = new System.Drawing.Point(564, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 567);
            this.panel1.TabIndex = 120;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(384, 18);
            this.label3.TabIndex = 121;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkAmiD);
            this.panel2.Controls.Add(this.chkAmiB);
            this.panel2.Controls.Add(this.chkAmiC);
            this.panel2.Controls.Add(this.chkAmiS);
            this.panel2.Controls.Add(this.chkAmiO);
            this.panel2.Controls.Add(this.chkAmiA);
            this.panel2.Controls.Add(this.chkHon);
            this.panel2.Controls.Add(this.chkOskC);
            this.panel2.Controls.Add(this.chkShiz);
            this.panel2.Controls.Add(this.chkOskB);
            this.panel2.Controls.Add(this.chkOskD);
            this.panel2.Controls.Add(this.chkAmiH);
            this.panel2.Controls.Add(this.chkOsk);
            this.panel2.Controls.Add(this.chkOskA);
            this.panel2.Location = new System.Drawing.Point(72, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(333, 240);
            this.panel2.TabIndex = 120;
            // 
            // chkAmiD
            // 
            this.chkAmiD.AutoSize = true;
            this.chkAmiD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiD.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiD.Location = new System.Drawing.Point(214, 201);
            this.chkAmiD.Name = "chkAmiD";
            this.chkAmiD.Size = new System.Drawing.Size(96, 24);
            this.chkAmiD.TabIndex = 13;
            this.chkAmiD.Text = "網掛けする";
            this.chkAmiD.UseVisualStyleBackColor = true;
            this.chkAmiD.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiB
            // 
            this.chkAmiB.AutoSize = true;
            this.chkAmiB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiB.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiB.Location = new System.Drawing.Point(214, 139);
            this.chkAmiB.Name = "chkAmiB";
            this.chkAmiB.Size = new System.Drawing.Size(96, 24);
            this.chkAmiB.TabIndex = 9;
            this.chkAmiB.Text = "網掛けする";
            this.chkAmiB.UseVisualStyleBackColor = true;
            this.chkAmiB.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiC
            // 
            this.chkAmiC.AutoSize = true;
            this.chkAmiC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiC.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiC.Location = new System.Drawing.Point(214, 170);
            this.chkAmiC.Name = "chkAmiC";
            this.chkAmiC.Size = new System.Drawing.Size(96, 24);
            this.chkAmiC.TabIndex = 11;
            this.chkAmiC.Text = "網掛けする";
            this.chkAmiC.UseVisualStyleBackColor = true;
            this.chkAmiC.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiS
            // 
            this.chkAmiS.AutoSize = true;
            this.chkAmiS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiS.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiS.Location = new System.Drawing.Point(214, 46);
            this.chkAmiS.Name = "chkAmiS";
            this.chkAmiS.Size = new System.Drawing.Size(96, 24);
            this.chkAmiS.TabIndex = 3;
            this.chkAmiS.Text = "網掛けする";
            this.chkAmiS.UseVisualStyleBackColor = true;
            this.chkAmiS.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiO
            // 
            this.chkAmiO.AutoSize = true;
            this.chkAmiO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiO.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiO.Location = new System.Drawing.Point(214, 77);
            this.chkAmiO.Name = "chkAmiO";
            this.chkAmiO.Size = new System.Drawing.Size(96, 24);
            this.chkAmiO.TabIndex = 5;
            this.chkAmiO.Text = "網掛けする";
            this.chkAmiO.UseVisualStyleBackColor = true;
            this.chkAmiO.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiA
            // 
            this.chkAmiA.AutoSize = true;
            this.chkAmiA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiA.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiA.Location = new System.Drawing.Point(214, 108);
            this.chkAmiA.Name = "chkAmiA";
            this.chkAmiA.Size = new System.Drawing.Size(96, 24);
            this.chkAmiA.TabIndex = 7;
            this.chkAmiA.Text = "網掛けする";
            this.chkAmiA.UseVisualStyleBackColor = true;
            this.chkAmiA.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkHon
            // 
            this.chkHon.AutoSize = true;
            this.chkHon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkHon.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkHon.Location = new System.Drawing.Point(33, 15);
            this.chkHon.Name = "chkHon";
            this.chkHon.Size = new System.Drawing.Size(57, 24);
            this.chkHon.TabIndex = 0;
            this.chkHon.Text = "本社";
            this.chkHon.UseVisualStyleBackColor = true;
            this.chkHon.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkOskC
            // 
            this.chkOskC.AutoSize = true;
            this.chkOskC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskC.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskC.Location = new System.Drawing.Point(33, 170);
            this.chkOskC.Name = "chkOskC";
            this.chkOskC.Size = new System.Drawing.Size(137, 24);
            this.chkOskC.TabIndex = 10;
            this.chkOskC.Text = "大阪製造部Ｃ班";
            this.chkOskC.UseVisualStyleBackColor = true;
            this.chkOskC.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkShiz
            // 
            this.chkShiz.AutoSize = true;
            this.chkShiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShiz.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkShiz.Location = new System.Drawing.Point(33, 46);
            this.chkShiz.Name = "chkShiz";
            this.chkShiz.Size = new System.Drawing.Size(57, 24);
            this.chkShiz.TabIndex = 2;
            this.chkShiz.Text = "静岡";
            this.chkShiz.UseVisualStyleBackColor = true;
            this.chkShiz.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkOskB
            // 
            this.chkOskB.AutoSize = true;
            this.chkOskB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskB.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskB.Location = new System.Drawing.Point(33, 139);
            this.chkOskB.Name = "chkOskB";
            this.chkOskB.Size = new System.Drawing.Size(137, 24);
            this.chkOskB.TabIndex = 8;
            this.chkOskB.Text = "大阪製造部Ｂ班";
            this.chkOskB.UseVisualStyleBackColor = true;
            this.chkOskB.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkOskD
            // 
            this.chkOskD.AutoSize = true;
            this.chkOskD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskD.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskD.Location = new System.Drawing.Point(33, 201);
            this.chkOskD.Name = "chkOskD";
            this.chkOskD.Size = new System.Drawing.Size(137, 24);
            this.chkOskD.TabIndex = 12;
            this.chkOskD.Text = "大阪製造部Ｄ班";
            this.chkOskD.UseVisualStyleBackColor = true;
            this.chkOskD.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkAmiH
            // 
            this.chkAmiH.AutoSize = true;
            this.chkAmiH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAmiH.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiH.Location = new System.Drawing.Point(214, 15);
            this.chkAmiH.Name = "chkAmiH";
            this.chkAmiH.Size = new System.Drawing.Size(96, 24);
            this.chkAmiH.TabIndex = 1;
            this.chkAmiH.Text = "網掛けする";
            this.chkAmiH.UseVisualStyleBackColor = true;
            this.chkAmiH.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkOsk
            // 
            this.chkOsk.AutoSize = true;
            this.chkOsk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOsk.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOsk.Location = new System.Drawing.Point(33, 77);
            this.chkOsk.Name = "chkOsk";
            this.chkOsk.Size = new System.Drawing.Size(105, 24);
            this.chkOsk.TabIndex = 4;
            this.chkOsk.Text = "大阪製造部";
            this.chkOsk.UseVisualStyleBackColor = true;
            this.chkOsk.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // chkOskA
            // 
            this.chkOskA.AutoSize = true;
            this.chkOskA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOskA.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskA.Location = new System.Drawing.Point(33, 108);
            this.chkOskA.Name = "chkOskA";
            this.chkOskA.Size = new System.Drawing.Size(137, 24);
            this.chkOskA.TabIndex = 6;
            this.chkOskA.Text = "大阪製造部Ａ班";
            this.chkOskA.UseVisualStyleBackColor = true;
            this.chkOskA.CheckedChanged += new System.EventHandler(this.chkHon_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(17, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 18);
            this.label2.TabIndex = 119;
            this.label2.Text = "休日とする事業所と勤務報告書の網掛けの有無：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDt
            // 
            this.txtDt.Font = new System.Drawing.Font("Meiryo UI", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDt.Location = new System.Drawing.Point(17, 16);
            this.txtDt.Name = "txtDt";
            this.txtDt.ReadOnly = true;
            this.txtDt.Size = new System.Drawing.Size(389, 52);
            this.txtDt.TabIndex = 119;
            this.txtDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtMemo.Location = new System.Drawing.Point(72, 450);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(333, 97);
            this.txtMemo.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(16, 450);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 94);
            this.label4.TabIndex = 54;
            this.label4.Text = "備考：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(17, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 71;
            this.label9.Text = "摘要：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTekiyou
            // 
            this.cmbTekiyou.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbTekiyou.FormattingEnabled = true;
            this.cmbTekiyou.Location = new System.Drawing.Point(72, 110);
            this.cmbTekiyou.Name = "cmbTekiyou";
            this.cmbTekiyou.Size = new System.Drawing.Size(334, 28);
            this.cmbTekiyou.TabIndex = 1;
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(899, 634);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 36);
            this.btnRtn.TabIndex = 124;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(804, 634);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 36);
            this.btnClear.TabIndex = 123;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(709, 634);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 36);
            this.btnDel.TabIndex = 122;
            this.btnDel.Text = "削除(&D)";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(614, 634);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 36);
            this.btnUpdate.TabIndex = 121;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmCalender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 688);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.dg);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalender";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "休日カレンダー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalender_FormClosing);
            this.Load += new System.EventHandler(this.frmCalender_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDt;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAmiD;
        private System.Windows.Forms.CheckBox chkAmiB;
        private System.Windows.Forms.CheckBox chkAmiC;
        private System.Windows.Forms.CheckBox chkAmiS;
        private System.Windows.Forms.CheckBox chkAmiO;
        private System.Windows.Forms.CheckBox chkAmiA;
        private System.Windows.Forms.CheckBox chkHon;
        private System.Windows.Forms.CheckBox chkShiz;
        private System.Windows.Forms.CheckBox chkOskD;
        private System.Windows.Forms.CheckBox chkOsk;
        private System.Windows.Forms.CheckBox chkOskA;
        private System.Windows.Forms.CheckBox chkAmiH;
        private System.Windows.Forms.CheckBox chkOskB;
        private System.Windows.Forms.CheckBox chkOskC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbTekiyou;
        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;

    }
}