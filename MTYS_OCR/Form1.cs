using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTYS_OCR.Common;

namespace MTYS_OCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 環境設定データを取得します
            Common.msConfig cnf = new Common.msConfig();
            cnf.GetCommonYearMonth();
        }

        MTYSDataSet dts = new MTYSDataSet();

        private void Form1_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // 社員所属 「出勤簿タイムカード印字」フィールド追加 : 2018/10/12 
            mdbAlter();
            
            //MTYSDataSet dts = new MTYSDataSet();
            MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();
            adp.UpdateQueryTMCardNull();
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Master.frmMasterMenu frm = new Master.frmMasterMenu();
            frm.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Master.frmConfig frm = new Master.frmConfig();
            frm.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PrePrint.prePrint frm = new PrePrint.prePrint();
            frm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmOCR frm = new OCR.frmOCR();
            frm.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 環境設定年月の確認
            string msg = "処理対象年月は " + global.cnfYear.ToString() + "年 " + global.cnfMonth.ToString() + "月です。よろしいですか？";
            if (MessageBox.Show(msg, "勤務データ登録", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
            
            // 出勤日数マスター登録チェック
            MTYSDataSetTableAdapters.出勤日数TableAdapter adp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();
            adp.Fill(dts.出勤日数);

            var s = dts.出勤日数.Where(a => a.年 == Utility.StrtoInt(global.cnfYear.ToString()) && a.月 == Utility.StrtoInt(global.cnfMonth.ToString()));

            if (s.Count() == 0)
            {
                MessageBox.Show(global.cnfYear.ToString() + "年" + global.cnfMonth.ToString() + "月の出勤日数が登録されていません。" + Environment.NewLine + "「マスターメンテナンス」－「出勤日数マスター保守」で出勤日数を登録してください。", "出勤日数未登録", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Hide();
            OCR.frmCorrect frm = new OCR.frmCorrect(string.Empty);
            frm.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmCheckList frm = new OCR.frmCheckList();
            frm.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            OCR.frmPastDataViewer frm = new OCR.frmPastDataViewer();
            frm.ShowDialog();
            this.Show();
        }

        private void mdbAlter()
        {
            SysControl.SetDBConnect dCon = new SysControl.SetDBConnect();
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = dCon.cnOpen();

            StringBuilder sb = new StringBuilder();

            try
            {
                // 社員所属 「出勤簿タイムカード印字」フィールド追加
                sb.Clear();
                sb.Append("ALTER TABLE 社員所属 ");
                sb.Append("ADD COLUMN 出勤簿タイムカード印字 int");
                
                sCom.CommandText = sb.ToString();
                sCom.ExecuteNonQuery();

                // データベース接続解除
                if (sCom.Connection.State == ConnectionState.Open)
                {
                    sCom.Connection.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            finally
            {
                if (sCom.Connection.State == ConnectionState.Open)
                {
                    sCom.Connection.Close();
                }
            }
        }
    }
}
