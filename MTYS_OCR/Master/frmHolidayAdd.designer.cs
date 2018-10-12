namespace MTYS_OCR.Master
{
    partial class frmHolidayAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHolidayAdd));
            this.btnRtn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTekiyou = new System.Windows.Forms.ComboBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.chkAmiH = new System.Windows.Forms.CheckBox();
            this.chkOskD = new System.Windows.Forms.CheckBox();
            this.chkOskC = new System.Windows.Forms.CheckBox();
            this.chkOskB = new System.Windows.Forms.CheckBox();
            this.chkOskA = new System.Windows.Forms.CheckBox();
            this.chkOsk = new System.Windows.Forms.CheckBox();
            this.chkShiz = new System.Windows.Forms.CheckBox();
            this.chkHon = new System.Windows.Forms.CheckBox();
            this.chkAmiD = new System.Windows.Forms.CheckBox();
            this.chkAmiA = new System.Windows.Forms.CheckBox();
            this.chkAmiB = new System.Windows.Forms.CheckBox();
            this.chkAmiO = new System.Windows.Forms.CheckBox();
            this.chkAmiC = new System.Windows.Forms.CheckBox();
            this.chkAmiS = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDt = new System.Windows.Forms.TextBox();
            this.mTYSDataSet = new MTYS_OCR.MTYSDataSet();
            this.休日BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.休日TableAdapter = new MTYS_OCR.MTYSDataSetTableAdapters.休日TableAdapter();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTYSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.休日BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRtn
            // 
            this.btnRtn.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRtn.Location = new System.Drawing.Point(347, 386);
            this.btnRtn.Name = "btnRtn";
            this.btnRtn.Size = new System.Drawing.Size(89, 36);
            this.btnRtn.TabIndex = 3;
            this.btnRtn.Text = "終了(&E)";
            this.btnRtn.UseVisualStyleBackColor = true;
            this.btnRtn.Click += new System.EventHandler(this.btnRtn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(252, 386);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 36);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "取消(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDel.Location = new System.Drawing.Point(157, 386);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(89, 36);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "削除(&D)";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(62, 386);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(89, 36);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "更新(&U)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(14, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 21);
            this.label4.TabIndex = 54;
            this.label4.Text = "備考：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(14, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 71;
            this.label9.Text = "摘要：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTekiyou
            // 
            this.cmbTekiyou.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbTekiyou.FormattingEnabled = true;
            this.cmbTekiyou.Location = new System.Drawing.Point(93, 46);
            this.cmbTekiyou.Name = "cmbTekiyou";
            this.cmbTekiyou.Size = new System.Drawing.Size(304, 25);
            this.cmbTekiyou.TabIndex = 1;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(93, 298);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(304, 24);
            this.txtMemo.TabIndex = 2;
            // 
            // chkAmiH
            // 
            this.chkAmiH.AutoSize = true;
            this.chkAmiH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiH.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiH.Location = new System.Drawing.Point(183, 4);
            this.chkAmiH.Name = "chkAmiH";
            this.chkAmiH.Size = new System.Drawing.Size(117, 22);
            this.chkAmiH.TabIndex = 1;
            this.chkAmiH.Text = "網掛けする";
            this.chkAmiH.UseVisualStyleBackColor = true;
            // 
            // chkOskD
            // 
            this.chkOskD.AutoSize = true;
            this.chkOskD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOskD.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskD.Location = new System.Drawing.Point(41, 178);
            this.chkOskD.Name = "chkOskD";
            this.chkOskD.Size = new System.Drawing.Size(135, 27);
            this.chkOskD.TabIndex = 12;
            this.chkOskD.Text = "大阪製造部Ｄ班";
            this.chkOskD.UseVisualStyleBackColor = true;
            // 
            // chkOskC
            // 
            this.chkOskC.AutoSize = true;
            this.chkOskC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOskC.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskC.Location = new System.Drawing.Point(41, 149);
            this.chkOskC.Name = "chkOskC";
            this.chkOskC.Size = new System.Drawing.Size(135, 22);
            this.chkOskC.TabIndex = 10;
            this.chkOskC.Text = "大阪製造部Ｃ班";
            this.chkOskC.UseVisualStyleBackColor = true;
            // 
            // chkOskB
            // 
            this.chkOskB.AutoSize = true;
            this.chkOskB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOskB.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskB.Location = new System.Drawing.Point(41, 120);
            this.chkOskB.Name = "chkOskB";
            this.chkOskB.Size = new System.Drawing.Size(135, 22);
            this.chkOskB.TabIndex = 8;
            this.chkOskB.Text = "大阪製造部Ｂ班";
            this.chkOskB.UseVisualStyleBackColor = true;
            // 
            // chkOskA
            // 
            this.chkOskA.AutoSize = true;
            this.chkOskA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOskA.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOskA.Location = new System.Drawing.Point(41, 91);
            this.chkOskA.Name = "chkOskA";
            this.chkOskA.Size = new System.Drawing.Size(135, 22);
            this.chkOskA.TabIndex = 6;
            this.chkOskA.Text = "大阪製造部Ａ班";
            this.chkOskA.UseVisualStyleBackColor = true;
            // 
            // chkOsk
            // 
            this.chkOsk.AutoSize = true;
            this.chkOsk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOsk.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkOsk.Location = new System.Drawing.Point(41, 62);
            this.chkOsk.Name = "chkOsk";
            this.chkOsk.Size = new System.Drawing.Size(135, 22);
            this.chkOsk.TabIndex = 4;
            this.chkOsk.Text = "大阪製造部";
            this.chkOsk.UseVisualStyleBackColor = true;
            // 
            // chkShiz
            // 
            this.chkShiz.AutoSize = true;
            this.chkShiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShiz.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkShiz.Location = new System.Drawing.Point(41, 33);
            this.chkShiz.Name = "chkShiz";
            this.chkShiz.Size = new System.Drawing.Size(135, 22);
            this.chkShiz.TabIndex = 2;
            this.chkShiz.Text = "静岡";
            this.chkShiz.UseVisualStyleBackColor = true;
            // 
            // chkHon
            // 
            this.chkHon.AutoSize = true;
            this.chkHon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkHon.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkHon.Location = new System.Drawing.Point(41, 4);
            this.chkHon.Name = "chkHon";
            this.chkHon.Size = new System.Drawing.Size(135, 22);
            this.chkHon.TabIndex = 0;
            this.chkHon.Text = "本社";
            this.chkHon.UseVisualStyleBackColor = true;
            // 
            // chkAmiD
            // 
            this.chkAmiD.AutoSize = true;
            this.chkAmiD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiD.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiD.Location = new System.Drawing.Point(183, 178);
            this.chkAmiD.Name = "chkAmiD";
            this.chkAmiD.Size = new System.Drawing.Size(117, 27);
            this.chkAmiD.TabIndex = 13;
            this.chkAmiD.Text = "網掛けする";
            this.chkAmiD.UseVisualStyleBackColor = true;
            // 
            // chkAmiA
            // 
            this.chkAmiA.AutoSize = true;
            this.chkAmiA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiA.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiA.Location = new System.Drawing.Point(183, 91);
            this.chkAmiA.Name = "chkAmiA";
            this.chkAmiA.Size = new System.Drawing.Size(117, 22);
            this.chkAmiA.TabIndex = 7;
            this.chkAmiA.Text = "網掛けする";
            this.chkAmiA.UseVisualStyleBackColor = true;
            // 
            // chkAmiB
            // 
            this.chkAmiB.AutoSize = true;
            this.chkAmiB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiB.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiB.Location = new System.Drawing.Point(183, 120);
            this.chkAmiB.Name = "chkAmiB";
            this.chkAmiB.Size = new System.Drawing.Size(117, 22);
            this.chkAmiB.TabIndex = 9;
            this.chkAmiB.Text = "網掛けする";
            this.chkAmiB.UseVisualStyleBackColor = true;
            // 
            // chkAmiO
            // 
            this.chkAmiO.AutoSize = true;
            this.chkAmiO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiO.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiO.Location = new System.Drawing.Point(183, 62);
            this.chkAmiO.Name = "chkAmiO";
            this.chkAmiO.Size = new System.Drawing.Size(117, 22);
            this.chkAmiO.TabIndex = 5;
            this.chkAmiO.Text = "網掛けする";
            this.chkAmiO.UseVisualStyleBackColor = true;
            // 
            // chkAmiC
            // 
            this.chkAmiC.AutoSize = true;
            this.chkAmiC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiC.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiC.Location = new System.Drawing.Point(183, 149);
            this.chkAmiC.Name = "chkAmiC";
            this.chkAmiC.Size = new System.Drawing.Size(117, 22);
            this.chkAmiC.TabIndex = 11;
            this.chkAmiC.Text = "網掛けする";
            this.chkAmiC.UseVisualStyleBackColor = true;
            // 
            // chkAmiS
            // 
            this.chkAmiS.AutoSize = true;
            this.chkAmiS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAmiS.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAmiS.Location = new System.Drawing.Point(183, 33);
            this.chkAmiS.Name = "chkAmiS";
            this.chkAmiS.Size = new System.Drawing.Size(117, 22);
            this.chkAmiS.TabIndex = 3;
            this.chkAmiS.Text = "網掛けする";
            this.chkAmiS.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.61217F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.38783F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiD, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiB, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiC, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiS, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiO, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiA, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkHon, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkShiz, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkOskD, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkOsk, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkOskA, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkAmiH, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkOskB, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.chkOskC, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(93, 83);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 209);
            this.tableLayoutPanel1.TabIndex = 118;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 7);
            this.label1.Size = new System.Drawing.Size(30, 207);
            this.label1.TabIndex = 119;
            this.label1.Text = "休日事業所と勤務報告書";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(14, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "年月日：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtDt);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbTekiyou);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 350);
            this.panel1.TabIndex = 119;
            // 
            // txtDt
            // 
            this.txtDt.Location = new System.Drawing.Point(93, 16);
            this.txtDt.Name = "txtDt";
            this.txtDt.ReadOnly = true;
            this.txtDt.Size = new System.Drawing.Size(300, 24);
            this.txtDt.TabIndex = 119;
            // 
            // mTYSDataSet
            // 
            this.mTYSDataSet.DataSetName = "MTYSDataSet";
            this.mTYSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 休日BindingSource
            // 
            this.休日BindingSource.DataMember = "休日";
            this.休日BindingSource.DataSource = this.mTYSDataSet;
            // 
            // 休日TableAdapter
            // 
            this.休日TableAdapter.ClearBeforeFill = true;
            // 
            // frmHolidayAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 434);
            this.Controls.Add(this.btnRtn);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmHolidayAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "休日マスター保守";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_FormClosing);
            this.Load += new System.EventHandler(this.frm_Load);
            this.Shown += new System.EventHandler(this.frmKintaiKbn_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTYSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.休日BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRtn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbTekiyou;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.CheckBox chkAmiH;
        private System.Windows.Forms.CheckBox chkOskD;
        private System.Windows.Forms.CheckBox chkOskC;
        private System.Windows.Forms.CheckBox chkOskB;
        private System.Windows.Forms.CheckBox chkOskA;
        private System.Windows.Forms.CheckBox chkOsk;
        private System.Windows.Forms.CheckBox chkShiz;
        private System.Windows.Forms.CheckBox chkHon;
        private MTYSDataSet mTYSDataSet;
        private System.Windows.Forms.BindingSource 休日BindingSource;
        private MTYSDataSetTableAdapters.休日TableAdapter 休日TableAdapter;
        private System.Windows.Forms.CheckBox chkAmiD;
        private System.Windows.Forms.CheckBox chkAmiA;
        private System.Windows.Forms.CheckBox chkAmiB;
        private System.Windows.Forms.CheckBox chkAmiO;
        private System.Windows.Forms.CheckBox chkAmiC;
        private System.Windows.Forms.CheckBox chkAmiS;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDt;
    }
}