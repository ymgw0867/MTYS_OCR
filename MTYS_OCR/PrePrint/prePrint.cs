using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.Odbc;
using MTYS_OCR.Common;
using LINQtoCSV;

namespace MTYS_OCR.PrePrint
{
    public partial class prePrint : Form
    {
        int ShozokuLen = 6;     // 所属コード桁数
        int ShainLen = 4;       // 社員番号桁数
         
        string _ComNo = string.Empty;               // 会社番号
        string _ComName = string.Empty;             // 会社名
        string _ComDatabeseName = string.Empty;     // 会社データベース名

        string appName = "勤務報告書印刷";             // アプリケーション表題

        // 社員マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 勤怠記号マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public prePrint()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            // ウィンドウズ最大サイズ
            Utility.WindowsMaxSize(this, this.Size.Width, this.Size.Height);

            // キャプション
            this.Text = appName;

            ////////自分自身のバージョン情報を取得する　2011/03/25
            //////System.Diagnostics.FileVersionInfo ver =
            //////    System.Diagnostics.FileVersionInfo.GetVersionInfo(
            //////    System.Reflection.Assembly.GetExecutingAssembly().Location);

            ////////キャプションにバージョンを追加　2011/03/25
            //////this.Text += " ver " + ver.FileMajorPart.ToString() + "." + ver.FileMinorPart.ToString();

            // 帳票区分コンボボックス
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // 開始部門コンボロード
            Utility.ComboBumon.load(cmbBumonS, comboBox1.SelectedIndex);
            cmbBumonS.MaxDropDownItems = 20;

            // 終了部門コンボロード
            Utility.ComboBumon.load(cmbBumonE, comboBox1.SelectedIndex);
            cmbBumonE.MaxDropDownItems = 20;

            // DataGridViewの設定
            GridViewSetting(dg1);

            // 年表示
            txtYear.Text = DateTime.Today.Year.ToString();
            txtYear.Focus();

            // 社員番号
            txtSNo.Text = string.Empty;
            txtENo.Text = string.Empty;

            // チェックボタン
            //btnCheckOn.Enabled = false;     // 2018/10/12
            //btnCheckOff.Enabled = false;    // 2018/10/12

            linkLabel1.Enabled = false;     // 2018/10/12
            linkLabel2.Enabled = false;     // 2018/10/12

            // 印刷ボタン
            btnPrn.Enabled = false;

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
                tempDGV.Height = 532;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.CellSelect;
                tempDGV.MultiSelect = true;

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
                    tempDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// グリッドビューへ社員情報を表示する
        /// </summary>
        /// <param name="tempDGV">DataGridViewオブジェクト名</param>
        /// <param name="tempDate">所属基準日</param>
        /// <param name="sCode">所属範囲開始コード</param>
        /// <param name="eCode">所属範囲終了コード</param>
        /// <param name="sNo">社員範囲開始コード</param>
        /// <param name="eNo">社員範囲終了コード</param>
        private void GridViewShowData(DataGridView tempDGV, int sCode, int eCode, int sNo, int eNo)
        {
            // 基本データ抽出
            var s = dts.社員所属.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                           a.社員番号 >= sNo && a.社員番号 <= eNo && a.所属コード >= sCode &&
                                           a.所属コード <= eCode && a.退職区分 == 0)
                                .OrderBy(a => a.帳票区分).ThenBy(a => a.所属コード).ThenBy(a => a.社員番号);

            // 帳票区分指定
            if (comboBox1.SelectedIndex != 0)
            {
                s = s.Where(a => a.帳票区分 == comboBox1.SelectedIndex)
                     .OrderBy(a => a.帳票区分).ThenBy(a => a.所属コード).ThenBy(a=> a.社員番号);
            }

            try
            {
                // グリッドビューの初期化
                tempDGV.DataSource = null;
                tempDGV.Rows.Clear();
                tempDGV.Columns.Clear();

                //グリッドビューに表示する
                tempDGV.DataSource = s.ToList();

                // 対象者数表示
                if (s.Count() > 0)
                {
                    label10.Text = "対象者：" + s.Count().ToString("#,###") + " 名";
                }
                else
                {
                    label10.Text = string.Empty;
                }

                // チェックボックスカラムを先頭に追加
                DataGridViewCheckBoxColumn clm = new DataGridViewCheckBoxColumn();
                tempDGV.Columns.Insert(0, clm);

                // 全てチェックする
                for (int i = 0; i < dg1.Rows.Count; i++)
                {
                    tempDGV[0, i].Value = true;
                }

                // タイムカード打刻データ印字チェックボックスカラムを２番目に追加：2018/10/12
                DataGridViewCheckBoxColumn clm2 = new DataGridViewCheckBoxColumn();
                tempDGV.Columns.Insert(1, clm2);


                // 各列幅指定
                tempDGV.Columns[0].Width = 40;

                tempDGV.Columns[1].Width = 80;   // 2018/10/12  
                tempDGV.Columns[1].HeaderText = "打刻印字";    // 2018/10/12

                tempDGV.Columns[2].Width = 100;                                         // 社員番号
                tempDGV.Columns[3].Width = 160;                                         // 氏名
                tempDGV.Columns[4].Width = 160;                                         // フリガナ
                tempDGV.Columns[5].Width = 100;                                         // 所属コード
                tempDGV.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // 所属名称
                tempDGV.Columns[7].Visible = false;                                     // 職級
                tempDGV.Columns[8].Visible = false;                                     // 資格
                tempDGV.Columns[9].Visible = false;                                     // 退職区分
                tempDGV.Columns[10].Width = 100;                                         // 帳票区分
                tempDGV.Columns[11].HeaderText = "大阪G";                               // 大阪製造部グループ
                tempDGV.Columns[11].Width = 80;                                         // 大阪製造部グループ
                tempDGV.Columns[12].Visible = false;                                    // 備考
                tempDGV.Columns[13].Visible = false;                                    // 更新年月日
                tempDGV.Columns[14].Visible = false;                                    // タイムカード打刻データ印字
                tempDGV.Columns[15].Visible = false;
                tempDGV.Columns[16].Visible = false;
                tempDGV.Columns[17].Visible = false;
                tempDGV.Columns[18].Visible = false;                

                tempDGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                // 編集可否
                tempDGV.ReadOnly = false;
                tempDGV.Columns[0].ReadOnly = false;
                tempDGV.Columns[1].ReadOnly = false;
                tempDGV.Columns[2].ReadOnly = true;
                tempDGV.Columns[3].ReadOnly = true;
                tempDGV.Columns[4].ReadOnly = true;
                tempDGV.Columns[5].ReadOnly = true;
                tempDGV.Columns[6].ReadOnly = true;
                tempDGV.Columns[10].ReadOnly = true;
                tempDGV.Columns[11].ReadOnly = true;

                tempDGV.CurrentCell = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK);
            }

            //社員情報がないとき
            if (tempDGV.RowCount == 0)
            {
                MessageBox.Show("該当する社員が存在しませんでした", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //btnCheckOn.Enabled = false;   // 2018/10/12
                //btnCheckOff.Enabled = false;  // 2018/10/12

                linkLabel1.Enabled = false; // 2018/10/12
                linkLabel2.Enabled = false; // 2018/10/12
                btnPrn.Enabled = false;
            }
            else
            {
                //btnCheckOn.Enabled = true;    // 2018/10/12
                //btnCheckOff.Enabled = true;   // 2018/10/12
                linkLabel1.Enabled = true;  // 2018/10/12
                linkLabel2.Enabled = true;  // 2018/10/12
                btnPrn.Enabled = true;
            }
        }

        ///-----------------------------------------------------------------
        /// <summary>
        ///     タイムカード打刻データ印字チェック欄にチェック表示：
        ///     社員マスターで対象とされている社員：2018/10/12 </summary>
        ///-----------------------------------------------------------------
        private void setTimeCardChk()
        {
            for (int i = 0; i < dg1.Rows.Count; i++)
            {
                if (dg1[14, i].Value.ToString() == global.FLGON)
                {
                    dg1[1, i].Value = true;
                }
                else
                {
                    dg1[1, i].Value = false;
                }
            }

        }

        private void btnCheckOn_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckOff_Click(object sender, EventArgs e)
        {
        }

        private void btnPrn_Click(object sender, EventArgs e)
        {
            //エラーチェック
            if (ErrCheck() == false) return;

            //件数取得
            int pCnt = PrintRowCount();

            if (pCnt == 0) 
            {
                MessageBox.Show("印刷対象行がありません", "印刷確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // タイムカード打刻データ
            if (txtTMData.Text == string.Empty)
            {
                if (MessageBox.Show("タイムカード打刻データが指定されていませんがよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (MessageBox.Show(pCnt.ToString() + "件の勤務報告書を印刷します。よろしいですか？", "印刷確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            // 2018/11/29
            // タイムカードＣＳＶデータからデータ中のヘッダデータを除去し印刷用データを出力する
            string tmCsv = string.Empty;
            if (txtTMData.Text != string.Empty)
            {
                csvDataOutput(txtTMData.Text, out tmCsv);
            }
            
            // タイムレコーダーCSVファイルパスを渡す 2018/11/29
            sReport(tmCsv);

            MessageBox.Show("印刷が終了しました", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 印刷用タイムカードCSVファイル削除
            if (tmCsv != string.Empty)
            {
                if (System.IO.File.Exists(tmCsv))
                {
                    System.IO.File.Delete(tmCsv);
                }
            }
        }

        ///---------------------------------------------------------------------------------------------
        /// <summary>
        ///     タイムカードＣＳＶデータからデータ中のヘッダデータを除去し印刷用データを出力する </summary>
        /// <param name="sPath">
        ///     画面で指定したタイムカードファイルパス</param>
        /// <param name="tPath">
        ///     印刷用出力ファイル</param>
        /// <returns>
        ///     </returns>
        ///---------------------------------------------------------------------------------------------
        private bool csvDataOutput(string sPath, out string tPath)
        {
            string[] outCsv = null;
            int iX = 0;
            
            try
            {
                Cursor = Cursors.WaitCursor;

                foreach (var str in System.IO.File.ReadAllLines(sPath, Encoding.Default))
                {
                    // カンマ区切りで分割して配列に格納する
                    string[] stCSV = str.Split(',');

                    if (iX > 0)
                    {
                        if (stCSV[0].Contains("カード番号"))
                        {
                            continue;
                        }
                        else
                        {
                            Array.Resize(ref outCsv, iX + 1);
                            outCsv[iX] = str;
                        }
                    }
                    else
                    {
                        Array.Resize(ref outCsv, iX + 1);
                        outCsv[iX] = str;
                    }

                    iX++;
                }
                
                // CSVファイル出力
                string outPath = System.IO.Path.GetDirectoryName(sPath) + @"\timeCard.csv";
                if (txtFileWrite(outPath, outCsv))
                {
                    // 出力ファイル名を返す
                    tPath = outPath;
                    return true;
                }
                else
                {
                    tPath = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tPath = string.Empty;
                return false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        ///----------------------------------------------------------------------------
        /// <summary>
        ///     テキストファイルを出力する</summary>
        /// <param name="outFilePath">
        ///     パスを含む出力ファイル名</param>
        /// <param name="arrayData">
        ///     書き込む配列データ</param>
        ///----------------------------------------------------------------------------
        private bool txtFileWrite(string outFilePath, string[] arrayData)
        {
            try
            {
                //// 出力ファイルが存在するとき
                //if (System.IO.File.Exists(outFilePath))
                //{
                //    // リネーム付加文字列（タイムスタンプ）
                //    string newFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                //                         DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                //                         DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

                //    // リネーム後ファイル名
                //    string reFileName = Path.GetDirectoryName(outFilePath) + @"\" + Path.GetFileNameWithoutExtension(outFilePath) + newFileName + ".csv";

                //    // 確認表示
                //    MessageBox.Show(outFilePath + "は既に存在しています。" + Environment.NewLine + Environment.NewLine +
                //        "登録済みファイルは名前を以下のように変更して保存します。" + Environment.NewLine + Environment.NewLine +
                //    "現）" + outFilePath + Environment.NewLine +
                //    "新）" + reFileName, "既存ファイルの名前変更", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    // 既存のファイルをリネーム
                //    File.Move(outFilePath, reFileName);
                //}

                // テキストファイル出力
                System.IO.File.WriteAllLines(outFilePath, arrayData, System.Text.Encoding.GetEncoding(932));
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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

            // 出勤日数マスター登録チェック
            MTYSDataSetTableAdapters.出勤日数TableAdapter adp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();
            adp.Fill(dts.出勤日数);

            var s = dts.出勤日数.Where(a => a.年 == Utility.StrtoInt(txtYear.Text) && a.月 == Utility.StrtoInt(txtMonth.Text));

            if (s.Count() == 0)
            {
                MessageBox.Show(txtYear.Text + "年" + txtMonth.Text + "月の出勤日数が登録されていません。" + Environment.NewLine + "「マスターメンテナンス」－「出勤日数マスター保守」で出勤日数を登録してください。", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            // 休日マスター登録チェック
            DateTime sDate = DateTime.Today;
            DateTime eDt = DateTime.Today;
            DateTime eDate = DateTime.Today;

            // 1日
            DateTime.TryParse(txtYear.Text + "/" + txtMonth.Text + "/01", out sDate);

            // 月末日
            for (int i = 2; i < 32; i++)
            {
                if (DateTime.TryParse(txtYear.Text + "/" + txtMonth.Text + "/" + i.ToString(), out eDt))
                {
                    eDate = eDt;
                }
                else
                {
                    break;
                }
            }

            MTYSDataSetTableAdapters.休日TableAdapter kadp = new MTYSDataSetTableAdapters.休日TableAdapter();
            kadp.Fill(dts.休日);

            var t = dts.休日.Where(a => a.年月日 >= sDate && a.年月日 <= eDate);

            if (t.Count() == 0)
            {
                MessageBox.Show(txtYear.Text + "年" + txtMonth.Text + "月の休日が登録されていません。" + Environment.NewLine + "「マスターメンテナンス」－「休日設定」で休日と網掛け設定を登録してください。", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            // 社員番号
            if (txtSNo.Text != string.Empty && Utility.NumericCheck(txtSNo.Text) == false)
            {
                MessageBox.Show("開始社員番号は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSNo.Focus();
                return false;
            }

            if (txtENo.Text != string.Empty && Utility.NumericCheck(txtENo.Text) == false)
            {
                MessageBox.Show("終了社員番号は数字で入力してください", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtENo.Focus();
                return false;
            }

            // 2018/10/12
            if (txtTMData.Text.Trim() != string.Empty)
            {
                if (!System.IO.File.Exists(txtTMData.Text))
                {
                    MessageBox.Show("存在しないタイムカード打刻データが指定されています", appName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTMData.Focus();
                    return false;
                }
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

        /// <summary>
        /// 社員出勤簿印刷・シート追加一括印刷
        /// </summary>
        private void sReport(string tmDataPath)
        {
            // 休日テーブル読み込み
            MTYSDataSetTableAdapters.休日TableAdapter adpH = new MTYSDataSetTableAdapters.休日TableAdapter();
            adpH.Fill(dts.休日);

            // 出勤日数テーブル読み込み
            MTYSDataSetTableAdapters.出勤日数TableAdapter adpS = new MTYSDataSetTableAdapters.出勤日数TableAdapter();
            adpS.Fill(dts.出勤日数);

            //エクセルファイル日付明細開始行
            const int S_GYO = 6;        

            string sDate;
            DateTime eDate;

            try
            {
                //マウスポインタを待機にする
                this.Cursor = Cursors.WaitCursor;

                string sAppPath = System.AppDomain.CurrentDomain.BaseDirectory;

                Excel.Application oXls = new Excel.Application();

                // 勤務報告書テンプレートシート
                Excel.Workbook oXlsBook = (Excel.Workbook)(oXls.Workbooks.Open(Properties.Settings.Default.sxlsPath, 
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing));

                // 勤務報告書印刷用シート
                Excel.Workbook oXlsPrintBook = (Excel.Workbook)(oXls.Workbooks.Open(Properties.Settings.Default.wxlsPath,
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                   Type.Missing, Type.Missing));

                Excel.Worksheet oxlsOosaka = (Excel.Worksheet)oXlsBook.Sheets[1];       // 大阪製造部書式
                Excel.Worksheet oxlsShizuoka = (Excel.Worksheet)oXlsBook.Sheets[2];     // 静岡書式
                Excel.Worksheet oxlsHonsha = (Excel.Worksheet)oXlsBook.Sheets[3];       // 本社書式
                Excel.Worksheet oxlsPrintSheet = null;    // 印刷用ワークシート

                Excel.Range[] rng = new Microsoft.Office.Interop.Excel.Range[2];

                string Category = string.Empty;

                try
                {
                    int pCnt = 0;

                    //グリッドを順番に読む
                    for (int i = 0; i < dg1.RowCount; i++)
                    {
                        //チェックがあるものを対象とする
                        if (dg1[0, i].Value.ToString() == "True")
                        {
                            pCnt++;     // ページカウント

                            int sRow = i;   // グリッド行インデックス取得

                            // 帳票区分取得
                            Category = dg1[10, sRow].Value.ToString();

                            // 印刷用BOOKへシートを追加する 
                            if (Category == global.C_HONSHA)        // 本社
                            {                                
                                oxlsHonsha.Copy(Type.Missing, oXlsPrintBook.Sheets[pCnt]);
                            }
                            else if (Category == global.C_SHIZUOKA) // 静岡
                            {
                                oxlsShizuoka.Copy(Type.Missing, oXlsPrintBook.Sheets[pCnt]);
                            }
                            else if (Category == global.C_OOSAKA)   // 大阪
                            {
                                oxlsOosaka.Copy(Type.Missing, oXlsPrintBook.Sheets[pCnt]);
                            }

                            // カレントのシートを設定
                            oxlsPrintSheet = (Excel.Worksheet)oXlsPrintBook.Sheets[pCnt + 1];

                            //// 印刷2件目以降はシートを追加する
                            //if (pCnt > 1)
                            //{
                            //    oxlsSheet.Copy(Type.Missing, oXlsPrintBook.Sheets[pCnt - 1]);
                            //    oxlsSheet = (Excel.Worksheet)oXlsPrintBook.Sheets[pCnt];
                            //}

                            // 社員番号
                            oxlsPrintSheet.Cells[2, 19] = dg1[2, sRow].Value.ToString().PadLeft(ShainLen, '0').Substring(0, 1);
                            oxlsPrintSheet.Cells[2, 20] = dg1[2, sRow].Value.ToString().PadLeft(ShainLen, '0').Substring(1, 1);
                            oxlsPrintSheet.Cells[2, 21] = dg1[2, sRow].Value.ToString().PadLeft(ShainLen, '0').Substring(2, 1);
                            oxlsPrintSheet.Cells[2, 22] = dg1[2, sRow].Value.ToString().PadLeft(ShainLen, '0').Substring(3, 1);

                            // 職級
                            oxlsPrintSheet.Cells[2, 25] = dg1[7, sRow].Value.ToString();

                            // 年
                            oxlsPrintSheet.Cells[2, 11] = string.Format("{0, 2}", int.Parse(txtYear.Text)).Substring(2, 1);
                            oxlsPrintSheet.Cells[2, 12] = string.Format("{0, 2}", int.Parse(txtYear.Text)).Substring(3, 1);

                            // 月
                            oxlsPrintSheet.Cells[2, 14] = string.Format("{0, 2}", int.Parse(txtMonth.Text)).Substring(0, 1);
                            oxlsPrintSheet.Cells[2, 15] = string.Format("{0, 2}", int.Parse(txtMonth.Text)).Substring(1, 1);

                            // 所属コード
                            oxlsPrintSheet.Cells[3, 3] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(0, 1);
                            oxlsPrintSheet.Cells[3, 4] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(1, 1);
                            oxlsPrintSheet.Cells[3, 5] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(2, 1);
                            oxlsPrintSheet.Cells[3, 6] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(3, 1);
                            oxlsPrintSheet.Cells[3, 7] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(4, 1);
                            oxlsPrintSheet.Cells[3, 8] = dg1[5, sRow].Value.ToString().PadLeft(ShozokuLen, '0').Substring(5, 1);

                            // 所属名
                            oxlsPrintSheet.Cells[3, 11] = dg1[6, sRow].Value.ToString();

                            // 氏名
                            oxlsPrintSheet.Cells[3, 19] = dg1[3, sRow].Value.ToString();

                            // 出勤すべき日数を取得
                            var h = dts.出勤日数.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                                        a.年.ToString() == txtYear.Text && a.月.ToString() == txtMonth.Text);

                            string injiNisu = string.Empty;

                            foreach (var t in h)
                            {
                                if (Category == global.C_HONSHA || Category  == global.C_SHIZUOKA)            // 本社のとき
                                {
                                    injiNisu = t.本社静岡印字用.ToString();
                                }
                                else if (Category == global.C_OOSAKA)       // 大阪製造部のとき
                                {

                                    if (dg1[11, sRow].Value.ToString() == global.OOSAKAG_A)         // 大阪製造Aのとき
                                    {
                                        injiNisu = t.大阪A印字用.ToString();
                                    }
                                    else if (dg1[11, sRow].Value.ToString() == global.OOSAKAG_B)    // 大阪製造Bのとき
                                    {
                                        injiNisu = t.大阪B印字用.ToString();
                                    }
                                    else if (dg1[11, sRow].Value.ToString() == global.OOSAKAG_C)    // 大阪製造Cのとき
                                    {
                                        injiNisu = t.大阪C印字用.ToString();
                                    }
                                    else if (dg1[11, sRow].Value.ToString() == global.OOSAKAG_D)    // 大阪製造Dのとき
                                    {
                                        injiNisu = t.大阪D印字用.ToString();
                                    }
                                    else
                                    {
                                        // 大阪製造部のとき
                                        injiNisu = t.大阪印字用.ToString();
                                    }
                                }
                            }

                            oxlsPrintSheet.Cells[3, 28] = injiNisu;

                            // 日別明細処理
                            int addRow = 0;
                            for (int iX = global.MAX_MIN; iX <= global.MAX_GYO; iX++)
                            {
                                if (iX < 7)
                                {
                                    addRow = iX - 1;
                                }
                                else if (iX < 17)
                                {
                                    addRow = iX;
                                }
                                else
                                {
                                    addRow = iX + 1;
                                }

                                sDate = txtYear.Text + "/" + txtMonth.Text + "/" + iX.ToString();

                                // 存在する日付のとき
                                if (DateTime.TryParse(sDate, out eDate))
                                {
                                    // 曜日表示
                                    oxlsPrintSheet.Cells[S_GYO + addRow, 2] = eDate.ToString("ddd");

                                    // タイムカード打刻データ印字：2018/10/13
                                    // タイムカードファイル指定を条件に追加 : 2018/11/29
                                    if (tmDataPath != string.Empty && dg1[1, i].Value.ToString() == "True")
                                    {
                                        string sTime = string.Empty;
                                        string eTime = string.Empty;
                                        getTimeCardData(tmDataPath, Utility.StrtoInt(dg1[2, sRow].Value.ToString()), eDate, out sTime, out eTime);
                                        
                                        // 出勤時刻打刻データ印字
                                        if (sTime != string.Empty)
                                        {
                                            string[] tt = sTime.Split(':');
                                            string gt = string.Empty;

                                            if (tt.Length > 1)
                                            {
                                                // 出勤時刻：時
                                                gt = tt[0].PadLeft(2, ' ');
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 6] = gt.Substring(0, 1);
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 7] = gt.Substring(1, 1);

                                                // 出勤時刻：分
                                                gt = tt[1].PadLeft(2, '0');
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 8] = gt.Substring(0, 1);
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 9] = gt.Substring(1, 1);
                                            }
                                        }

                                        // 退勤時刻打刻データ印字
                                        if (eTime != string.Empty)
                                        {
                                            string[] tt = eTime.Split(':');
                                            string gt = string.Empty;

                                            if (tt.Length > 1)
                                            {
                                                // 退勤時刻：時
                                                gt = tt[0].PadLeft(2, ' ');
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 10] = gt.Substring(0, 1);
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 11] = gt.Substring(1, 1);

                                                // 退勤時刻：分
                                                gt = tt[1].PadLeft(2, '0');
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 12] = gt.Substring(0, 1);
                                                oxlsPrintSheet.Cells[S_GYO + addRow, 13] = gt.Substring(1, 1);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    oxlsPrintSheet.Cells[S_GYO + addRow, 1] = string.Empty;
                                }

                                // 
                                // 網掛け処理
                                //
                                rng[0] = (Excel.Range)oxlsPrintSheet.Cells[S_GYO + addRow, 1];
                                rng[1] = (Excel.Range)oxlsPrintSheet.Cells[S_GYO + addRow, 2];

                                var s = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                                            a.年月日 == eDate);
                                foreach (var t in s)
                                {
                                    if (Category == global.C_HONSHA && t.本社網掛け == global.flgOn)    // 本社のとき
                                    {
                                        oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;                                        
                                    }
                                    else if (Category == global.C_SHIZUOKA && t.静岡網掛け == global.flgOn)  // 静岡のとき
                                    {
                                        oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                    }
                                    else if (Category == global.C_OOSAKA)   // 大阪製造部のとき
                                    {
                                        if (dg1[10, sRow].Value.ToString() == global.OOSAKAG_A && t.大阪A網掛け == global.flgOn)     // 大阪製造Aのとき
                                        {
                                            oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                        }
                                        else if (dg1[10, sRow].Value.ToString() == global.OOSAKAG_B && t.大阪B網掛け == global.flgOn)    // 大阪製造Bのとき
                                        {
                                            oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                        }
                                        else if (dg1[10, sRow].Value.ToString() == global.OOSAKAG_C && t.大阪C網掛け == global.flgOn)    // 大阪製造Cのとき
                                        {
                                            oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                        }
                                        else if (dg1[10, sRow].Value.ToString() == global.OOSAKAG_D && t.大阪D網掛け == global.flgOn)    // 大阪製造Dのとき
                                        {
                                            oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                        }
                                        else if (Utility.NulltoStr(dg1[10, sRow].Value) == string.Empty && t.大阪製造部網掛け == global.flgOn)   // 大阪製造のとき
                                        {
                                            oxlsPrintSheet.get_Range(rng[0], rng[1]).Interior.Color = Color.LightGray;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //マウスポインタを元に戻す
                    this.Cursor = Cursors.Default;

                    // 印刷用BOOKの1番目のシートは削除する
                    ((Excel.Worksheet)oXlsPrintBook.Sheets[1]).Delete();

                    //印刷
                    oXlsPrintBook.PrintOut();

                    // ウィンドウを非表示にする
                    oXls.Visible = false;

                    //保存処理
                    oXls.DisplayAlerts = false;

                    //Bookをクローズ
                    oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);
                    oXlsPrintBook.Close(Type.Missing, Type.Missing, Type.Missing);

                    //Excelを終了
                    oXls.Quit();
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "印刷処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    // ウィンドウを非表示にする
                    oXls.Visible = false;

                    //保存処理
                    oXls.DisplayAlerts = false;

                    //Bookをクローズ
                    oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);

                    //Excelを終了
                    oXls.Quit();
                }

                finally
                {
                    // COM オブジェクトの参照カウントを解放する 
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsHonsha);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsShizuoka);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsOosaka);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsPrintSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oXlsBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oXlsPrintBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oXls);

                    //マウスポインタを元に戻す
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "印刷処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //マウスポインタを元に戻す
            this.Cursor = Cursors.Default;
        }

        ///-------------------------------------------------------------------
        /// <summary>
        ///     タイムカード打刻データ取得：2018/10/13 </summary>
        /// <param name="dPath">
        ///     タイムカード打刻データパス</param>
        /// <param name="sNum">
        ///     社員番号</param>
        /// <param name="sDt">
        ///     日付</param>
        /// <param name="sTime">
        ///     出勤時刻</param>
        /// <param name="eTime">
        ///     退勤時刻</param>
        ///-------------------------------------------------------------------
        private void getTimeCardData(string dPath, int sNum, DateTime sDt, out string sTime, out string eTime)
        {
            sTime = string.Empty;
            eTime = string.Empty;

            var context = new CsvContext();

            // CSVの情報を示すオブジェクトを構築
            var description = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true,
                TextEncoding = Encoding.GetEncoding(932)
            };

            var s = context.Read<Common.clsLinqCsv>(dPath, description)
                .Where(a => a.sNum == sNum && a.sDate.ToShortDateString() == sDt.ToShortDateString());

            foreach (var t in s)
            {
                sTime = Utility.NulltoStr(t.sShukinTime);
                eTime = Utility.NulltoStr(t.sTaikinTime);
            }
        }




        private void txtYear_Enter(object sender, EventArgs e)
        {
            TextBox txtObj = new TextBox();
            
            if (sender == txtYear) txtObj = txtYear;
            if (sender == txtMonth) txtObj = txtMonth;
            if (sender == txtSNo) txtObj = txtSNo;
            if (sender == txtENo) txtObj = txtENo;

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
            int eCode;
            int sNo;
            int eNo;

            //エラーチェック
            if (ErrCheck() == false) return;

            //基準日付
            sDate = (int.Parse(txtYear.Text) + Properties.Settings.Default.RekiHosei).ToString() + "/" + txtMonth.Text + "/01";
            
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

            //終了部門コード取得
            if (cmbBumonE.SelectedIndex == -1)
            {
                eCode = 999999;
            }
            else
            {
                Utility.ComboBumon cmbe = new Utility.ComboBumon();
                cmbe = (Utility.ComboBumon)cmbBumonE.SelectedItem;
                eCode = Utility.StrtoInt(cmbe.ID);
            }

            //開始社員番号取得
            if (txtSNo.Text == string.Empty)
            {
                sNo = 0;
            }
            else
            {
                sNo = Utility.StrtoInt(txtSNo.Text);
            }

            //終了社員番号取得
            if (txtENo.Text == string.Empty)
            {
                eNo = 9999;
            }
            else
            {
                eNo = Utility.StrtoInt(txtENo.Text);
            }

            //データ表示
            GridViewShowData(dg1, sCode, eCode, sNo, eNo);

            // タイムカード打刻データ印字チェックボックス表示：2018/10/12
            setTimeCardChk();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlSTRING = string.Empty;

            OdbcConnection ocn = new OdbcConnection("Dsn=ocrODBC;uid=ocrapp;pwd=ocrpass;");
            //OdbcConnectionStringBuilder oCn = new OdbcConnectionStringBuilder();
            //oCn.Dsn = global.pblUserDsn;
            //OdbcConnection ocn = new OdbcConnection(oCn.ConnectionString);
            ocn.Open();

            OdbcCommand scom = new OdbcCommand();
            OdbcDataReader dR;

            
            sqlSTRING += "SELECT EntityCode,EntityName,DatabaseName FROM tbCorpDatabaseContext ";
            sqlSTRING += "order by EntityCode";
            scom.CommandText = sqlSTRING;
            scom.Connection = ocn;
            dR = scom.ExecuteReader();

            while (dR.Read())
            {
                MessageBox.Show(dR["EntityCode"].ToString() + ":" + dR["EntityName"].ToString() + ":" + dR["DatabaseName"].ToString()); 
            }

            dR.Close();
            ocn.Close();
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

            //終了部門コンボロード
            cmbBumonE.Items.Clear();
            Utility.ComboBumon.load(cmbBumonE, comboBox1.SelectedIndex);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Title = "タイムカード打刻データ選択";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "ＣＳＶファイル(*.csv)|*.csv|全てのファイル(*.*)|*.*";

            //ダイアログボックスを表示し「保存」ボタンが選択されたらファイル名を表示
            string fileName;
            DialogResult ret = openFileDialog1.ShowDialog();

            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                txtTMData.Text = openFileDialog1.FileName;

                //// 勤怠タイムカード打刻データ配列読み込み
                //workArray = System.IO.File.ReadAllLines(txtTMData.Text, Encoding.Default);
                
            }
            else
            {
                fileName = string.Empty;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("表示中の社員全てを印刷対象にします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            for (int i = 0; i < dg1.Rows.Count; i++)
            {
                dg1[0, i].Value = true;
            }

            btnPrn.Enabled = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("表示中の社員全てを印刷対象外にします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            for (int i = 0; i < dg1.Rows.Count; i++)
            {
                dg1[0, i].Value = false;
            }
        }
    }
}
