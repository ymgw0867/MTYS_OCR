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
    public partial class frmCheckList : Form
    {
        int ShozokuLen = 6;     // 所属コード桁数
        int ShainLen = 4;       // 社員番号桁数
         
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "勤怠チェックリスト";       // アプリケーション表題

        // 社員マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 勤怠記号マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public frmCheckList()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);
        }

        #region グリッドカラム定義
        string colNum = "c0";           //　社員番号
        string colName = "c1";          //　社員名
        string colSzCode = "c2";        //　所属コード
        string colSzName = "c3";        //　所属名
        string colShukkinsubeki = "c4"; //　出勤すべき日数
        string colShukkin = "c5";       //　出勤日数
        string colKekkin = "c6";        //　欠勤日数
        string colYukyu = "c7";         //　有給休暇
        string colTokukyu = "c8";       //　特別休暇
        string colSeiri = "c9";         //　生理分娩
        string colChikoku = "c10";      //　遅刻
        string colSoutai = "c11";       //　早退
        string colZan = "c12";          //　普通残業
        string colShinya = "c13";       //　深夜残業
        string colKyujitsu = "c14";     //　休日勤務
        string colSHukuH = "c15";       //　宿日直平日
        string colSHukuK = "c16";       //　宿日直休日
        string colHoanH = "c17";        //　保安平日
        string colHoanK = "c18";        //　保安休日
        string colJisa = "c19";         //　時差出勤
        string col1Lkin = "c20";        //　１L
        string col2kin = "c21";         //　2勤
        string colMaru3kin = "c22";     //　丸3勤
        string col3kin = "c23";         //　3勤
        string colShukujitsu = "c24";   //　日祝日勤務
        string colKoujyo = "c25";       //　控除日数
        string col60H = "c26";          //　60時間超平日
        string col60K = "c27";          //　60時間超休日
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

            // 社員コンボ
            cmbShain.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbShain.Items.Add("全員");
            cmbShain.Items.Add("作成済み社員");
            cmbShain.Items.Add("未作成社員");
            cmbShain.SelectedIndex = 0;

            // CSV出力ボタン
            button1.Enabled = false;

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
                tempDGV.Columns.Add(colNum, "社員番号");
                tempDGV.Columns.Add(colName, "社員名");
                tempDGV.Columns.Add(colSzCode, "コード");
                tempDGV.Columns.Add(colSzName, "所属名");
                tempDGV.Columns.Add(colShukkinsubeki, "出勤すべき日数");
                tempDGV.Columns.Add(colShukkin, "出勤");
                tempDGV.Columns.Add(colKekkin, "欠勤");
                tempDGV.Columns.Add(colYukyu, "有給");
                tempDGV.Columns.Add(colTokukyu, "特休");
                tempDGV.Columns.Add(colSeiri, "生理分娩");
                tempDGV.Columns.Add(colChikoku, "遅刻時間");
                tempDGV.Columns.Add(colSoutai, "早退時間");
                tempDGV.Columns.Add(colZan, "普通残業");
                tempDGV.Columns.Add(colShinya, "深夜残業");
                tempDGV.Columns.Add(colKyujitsu, "休日勤務");
                tempDGV.Columns.Add(colSHukuH, "宿日直平日");
                tempDGV.Columns.Add(colSHukuK, "宿日直休日");
                tempDGV.Columns.Add(colHoanH, "保安休日");
                tempDGV.Columns.Add(colHoanK, "保安平日");
                tempDGV.Columns.Add(colJisa, "時差出勤");
                tempDGV.Columns.Add(col1Lkin, "1L勤");
                tempDGV.Columns.Add(col2kin, "2勤");
                tempDGV.Columns.Add(colMaru3kin, "③勤");
                tempDGV.Columns.Add(col3kin, "3勤");
                tempDGV.Columns.Add(colShukujitsu, "日祝日勤務");
                tempDGV.Columns.Add(colKoujyo, "控除日数");
                tempDGV.Columns.Add(col60H, "60H超平日");
                tempDGV.Columns.Add(col60K, "60H超休日");

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
                    if (i == 0)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                    }

                    if (i > 3)
                    {
                        tempDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    }

                    // ソート機能制限
                    tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                
                // 各列幅指定
                tempDGV.Columns[colNum].Width = 80;
                tempDGV.Columns[colName].Width = 120;
                tempDGV.Columns[colSzCode].Width = 80; 
                tempDGV.Columns[colSzName].Width = 160;
                tempDGV.Columns[colShukkinsubeki].Width = 120;
                tempDGV.Columns[colShukkin].Width = 50;
                tempDGV.Columns[colKekkin].Width = 50;
                tempDGV.Columns[colYukyu].Width = 50;
                tempDGV.Columns[colTokukyu].Width = 50;
                tempDGV.Columns[colSeiri].Width = 80;
                tempDGV.Columns[colChikoku].Width = 80;
                tempDGV.Columns[colSoutai].Width = 80;
                tempDGV.Columns[colZan].Width = 80;
                tempDGV.Columns[colShinya].Width = 80;
                tempDGV.Columns[colKyujitsu].Width = 80;
                tempDGV.Columns[colSHukuH].Width = 100;
                tempDGV.Columns[colSHukuK].Width = 100;
                tempDGV.Columns[colHoanH].Width = 80;
                tempDGV.Columns[colHoanK].Width = 80;
                tempDGV.Columns[colJisa].Width = 80;
                tempDGV.Columns[col1Lkin].Width = 50;
                tempDGV.Columns[col2kin].Width = 50;
                tempDGV.Columns[colMaru3kin].Width = 50;
                tempDGV.Columns[col3kin].Width = 50;
                tempDGV.Columns[colShukujitsu].Width = 100;
                tempDGV.Columns[colKoujyo].Width = 80;
                tempDGV.Columns[col60H].Width = 100;
                tempDGV.Columns[col60K].Width = 100;

                tempDGV.Columns[colName].Frozen = true;

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
        private void GridViewShowData(DataGridView g, int cKbn, int sCode, string sYY, string sMM, int sNull)
        {
            // データの抽出
            SysControl.SetDBConnect dCon = new SysControl.SetDBConnect();
            OleDbCommand sCom = new OleDbCommand();
            sCom.Connection = dCon.cnOpen();

            StringBuilder sb = new StringBuilder();
            sb.Append("select 社員所属.社員番号 as sNum,社員所属.氏名 as sName, 社員所属.所属コード as sSzCode, 社員所属.所属名称 as sSzName, 社員所属.帳票区分 as sCkbn, 社員所属.大阪製造部勤務グループ as sOskg, chk.* ");
            sb.Append("from 社員所属 left join ");
            sb.Append("[select * from 勤怠チェックリスト where 年 = ? and 月 = ?]. as chk ");
            sb.Append("on 社員所属.社員番号 = chk.社員番号 ");
            sb.Append("where 社員所属.社員番号 > 0 ");

            if (cKbn != global.flgOff)
            {
                sb.Append("and 社員所属.帳票区分 = ? ");
            }

            if (sCode != global.flgOff)
            {
                sb.Append("and 社員所属.所属コード = ? ");
            }

            if (sNull == 1)
            {
                sb.Append("and chk.出勤日数 <> null ");
            }
            else if (sNull == 2)
            {
                sb.Append("and chk.出勤日数 is Null ");
            }

            sb.Append("order by 社員所属.社員番号");

            sCom.CommandText = sb.ToString();

            sCom.Parameters.AddWithValue("@year", sYY);
            sCom.Parameters.AddWithValue("@month", sMM);

            if (cKbn != global.flgOff)
            {
                sCom.Parameters.AddWithValue("@cKbn", cKbn);
            }

            if (sCode != global.flgOff)
            {
                sCom.Parameters.AddWithValue("@所属", sCode);
            }

            OleDbDataReader r = sCom.ExecuteReader();

            g.Rows.Clear();

            try
            {
                int i = 0;
                while (r.Read())
                {
                    g.Rows.Add();
                    g[colNum, i].Value = r["sNum"].ToString();
                    g[colName, i].Value = r["sName"];
                    g[colSzCode, i].Value = r["sSzCode"].ToString();
                    g[colSzName, i].Value = r["sSzName"];
                    g[colShukkinsubeki, i].Value = r["出勤すべき日数"];
                    g[colShukkin, i].Value = r["出勤日数"];
                    g[colKekkin, i].Value = r["欠勤日数"];
                    g[colYukyu, i].Value = r["有給休暇"];
                    g[colTokukyu, i].Value = r["特別休暇"];
                    g[colSeiri, i].Value = r["生理分娩休暇"];
                    g[colChikoku, i].Value = r["遅刻時間"];
                    g[colSoutai, i].Value = r["早退時間"];
                    g[colZan, i].Value = r["普通残業"];
                    g[colShinya, i].Value = r["深夜残業"];
                    g[colKyujitsu, i].Value = r["休日勤務"];
                    g[colSHukuH, i].Value = r["宿日直平日"];
                    g[colSHukuK, i].Value = r["宿日直休日"];
                    g[colHoanH, i].Value = r["保安平日"];
                    g[colHoanK, i].Value = r["保安休日"];
                    g[colJisa, i].Value = r["時差出勤"];
                    g[col1Lkin, i].Value = r["1L勤"];
                    g[col2kin, i].Value = r["2勤"];
                    g[colMaru3kin, i].Value = r["丸3勤"];
                    g[col3kin, i].Value = r["3勤"];
                    g[colShukujitsu, i].Value = r["日祝日勤務"];
                    g[colKoujyo, i].Value = r["控除日数"];
                    g[col60H, i].Value = r["残業60H超平日"];
                    g[col60K, i].Value = r["残業60H超休日"];
                    
                    i++;
                }

                r.Close();

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
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }

            sCom.Connection.Close();
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
            GridViewShowData(dg1, comboBox1.SelectedIndex, sCode, txtYear.Text, txtMonth.Text, cmbShain.SelectedIndex);
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

        private void button1_Click(object sender, EventArgs e)
        {
            MyLibrary.CsvOut.GridView(dg1, txtYear.Text + "年" + txtMonth.Text + "月勤怠チェックリスト");
        }

    }
}
