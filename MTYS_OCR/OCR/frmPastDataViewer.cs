using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MTYS_OCR.Common;

namespace MTYS_OCR.OCR
{
    public partial class frmPastDataViewer : Form
    {
        int ShozokuLen = 6;     // 所属コード桁数
        int ShainLen = 4;       // 社員番号桁数
         
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "過去勤怠データビューワー";    // アプリケーション表題

        // 過去勤務票ヘッダ
        MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter adpHd = new MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter();

        // 社員マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 勤怠記号マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public frmPastDataViewer()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpHd.Fill(dts.過去勤務票ヘッダ);
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);
        }

        #region グリッドカラム定義
        string colNum = "c0";           //　社員番号
        string colName = "c1";          //　社員名
        string colSzCode = "c2";        //　所属コード
        string colSzName = "c3";        //　所属名
        string colCkbn = "c4";          //　帳票区分
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            // ウィンドウズ最大サイズ
            //Utility.WindowsMaxSize(this, this.Size.Width, this.Size.Height);

            // キャプション
            this.Text = appName;

            // 帳票区分コンボボックス
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // 所属コンボロード
            Utility.ComboBumon.load(cmbBumonS, comboBox1.SelectedIndex);
            cmbBumonS.MaxDropDownItems = 20;
            
            // DataGridViewの設定
            GridViewSetting(dg1);

            // 年月表示
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();
            txtYear.Focus();

            //元号表示
            //label5.Text = Properties.Settings.Default.gengou;

            label10.Text = string.Empty;
        }

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        public void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更するe

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 11, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", (float)11, FontStyle.Regular);
                
                // 行の高さ
                tempDGV.ColumnHeadersHeight = 22;
                tempDGV.RowTemplate.Height = 22;

                // 全体の高さ
                tempDGV.Height = 637;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // カラム定義
                tempDGV.Columns.Add(colSzCode, "コード");
                tempDGV.Columns.Add(colCkbn, "事業所");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colNum, "社員番号");
                tempDGV.Columns.Add(colName, "社員名");

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更禁止
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //ソート機能制限
                for (int i = 0; i < tempDGV.Columns.Count; i++)
                {
                    // Alignment
                    if (i == 0 || i == 3)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    }

                    // ソート機能制限
                    //tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                
                // 各列幅指定
                tempDGV.Columns[colNum].Width = 100;
                tempDGV.Columns[colName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tempDGV.Columns[colSzCode].Width = 80; 
                tempDGV.Columns[colSzName].Width = 160;
                tempDGV.Columns[colCkbn].Width = 140;

                // 編集可否
                tempDGV.ReadOnly = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     グリッドビューへ社員情報を表示する </summary>
        /// <param name="tempDGV">
        ///     DataGridViewオブジェクト名</param>
        /// <param name="tempDate">
        ///     所属基準日</param>
        /// <param name="sCode">所属範囲開始コード</param>
        /// <param name="eCode">所属範囲終了コード</param>
        /// <param name="sNo">社員範囲開始コード</param>
        /// <param name="eNo">社員範囲終了コード</param>
        ///-----------------------------------------------------------------------------
        private void GridViewShowData(DataGridView g, int cKbn, string sCode, int sYY, int sMM)
        {

            try
            {
                // データの抽出
                var s = dts.過去勤務票ヘッダ
                        .Where(a => a.年 == sYY && a.月 == sMM)
                        .OrderBy(a => a.所属コード).ThenBy(a => a.個人番号);

                // 帳票番号指定
                if (cKbn > 0)
                {
                    s = s.Where(a => a.帳票番号 == comboBox1.SelectedIndex)
                        .OrderBy(a => a.所属コード).ThenBy(a => a.個人番号);
                }

                // 所属指定
                if (sCode != global.FLGOFF)
                {
                    s = s.Where(a => a.所属コード == sCode)
                        .OrderBy(a => a.所属コード).ThenBy(a => a.個人番号);
                }

                g.Rows.Clear();
                int i = 0;

                global gl = new global();

                foreach (var r in s)
                {
                    g.Rows.Add();
                    g[colSzCode, i].Value = r.所属コード;
                    g[colSzName, i].Value = r.所属名;
                    g[colCkbn, i].Value = gl.arrayChohyoID[r.帳票番号 - 1];
                    g[colNum, i].Value = r.個人番号;
                    g[colName, i].Value = r.氏名;

                    i++;
                }

                // 対象者数表示
                if (i > 0)
                {
                    label10.Text = "対象者：" + i.ToString("#,###") + " 名";
                }
                else
                {
                    label10.Text = string.Empty;
                }

                g.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK);
            }

            //社員情報がないとき
            if (g.RowCount == 0)
            {
                MessageBox.Show("該当する社員が存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCheckOn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("表示中の社員全てを印刷対象にします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            for (int i = 0; i < dg1.Rows.Count; i++)
            {
                dg1[0, i].Value = true;
            }
        }

        private void btnCheckOff_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("表示中の社員全てを印刷対象外にします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            for (int i = 0; i < dg1.Rows.Count; i++)
            {
                dg1[0, i].Value = false;
            }
        }

        private Boolean ErrCheck()
        {
            if (Utility.NumericCheck(txtYear.Text) == false)
            {
                MessageBox.Show("年は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            if (Utility.StrtoInt(txtYear.Text) < 2014)
            {
                MessageBox.Show("年が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            if (Utility.NumericCheck(txtMonth.Text) == false)
            {
                MessageBox.Show("月は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            if (int.Parse(txtMonth.Text) < 1 || int.Parse(txtMonth.Text) > 12)
            {
                MessageBox.Show("月が正しくありません", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// プリント件数取得
        /// </summary>
        /// <returns>印刷件数</returns>
        private int PrintRowCount()
        {
            //int pCnt = 0;

            //for (int i = 0; i < dg1.Rows.Count; i++)
            //{
            //    if (dg1[0, i].Value.ToString() == "True") pCnt++;
            //}
            
            //return pCnt;

            // 2014/07/06 LINQで記述
            var rowList = dg1.Rows.Cast<DataGridViewRow>().Where(a => (bool)a.Cells[0].Value);

            return rowList.Count();

        }

        private void txtYear_Enter(object sender, EventArgs e)
        {
            TextBox txtObj = new TextBox();
            
            if (sender == txtYear) txtObj = txtYear;
            if (sender == txtMonth) txtObj = txtMonth;

            txtObj.SelectAll();
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("終了します。よろしいですか？",appName,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            this.Dispose();
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            string sDate = string.Empty;
            int sCode;

            //エラーチェック
            if (!ErrCheck()) return;

            //開始部門コード取得
            if (cmbBumonS.SelectedIndex == -1)
            {
                sCode = 0;
            }
            else
            {
                Utility.ComboBumon cmbs = new Utility.ComboBumon();
                cmbs = (Utility.ComboBumon)cmbBumonS.SelectedItem;
                sCode = Utility.StrtoInt(cmbs.ID);
            }

            //データ表示
            GridViewShowData(dg1, comboBox1.SelectedIndex, sCode.ToString(), Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!e.Control)
            //    {
            //        this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            //    }
            //}
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    e.Handled = true;
            //}
        }

        private void rBtnPrn_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void prePrint_Shown(object sender, EventArgs e)
        {
            txtYear.Focus();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b') 
                e.Handled = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //開始部門コンボロード
            cmbBumonS.Items.Clear();
            Utility.ComboBumon.load(cmbBumonS, comboBox1.SelectedIndex);
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string rID = string.Empty;

            rID = dg1[colNum, dg1.SelectedRows[0].Index].Value.ToString();

            if (rID != string.Empty)
            {
                this.Hide();
                OCR.frmPastData frm = new OCR.frmPastData(Utility.StrtoInt(rID), Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text));
                frm.ShowDialog();
                this.Show();
            }
        }

    }
}
