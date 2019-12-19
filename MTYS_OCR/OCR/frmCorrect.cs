﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using MTYS_OCR.Common;

namespace MTYS_OCR.OCR
{
    public partial class frmCorrect : Form
    {
        /// ------------------------------------------------------------
        /// <summary>
        ///     コンストラクタ </summary>
        /// <param name="sID">
        ///     処理モード</param>
        /// ------------------------------------------------------------
        public frmCorrect(string sID)
        {
            InitializeComponent();

            dID = sID;              // 処理モード

            // 処理モードの判断
            if (dID == string.Empty)    // データ編集モード
            {
                // 画像パス取得
                global.pblImagePath = Properties.Settings.Default.dataPath;
            }
            else // 過去データ表示モード
            {
                // 画像パス取得
                global.pblImagePath = Properties.Settings.Default.tifPath;

                // 過去勤務票ヘッダデータテーブル取得
                MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter adp = new MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
                adp.Fill(dts.過去勤務票ヘッダ);

                // 過去勤務票明細データテーブル取得
                MTYSDataSetTableAdapters.過去勤務票明細TableAdapter mAdp = new MTYSDataSetTableAdapters.過去勤務票明細TableAdapter();
                mAdp.Fill(dts.過去勤務票明細);
            }

            // テーブルアダプターマネージャーに勤務票ヘッダ、明細テーブルアダプターを割り付ける
            adpMn.勤務票ヘッダTableAdapter = hAdp;
            adpMn.勤務票明細TableAdapter = iAdp;

            // 社員番号テキストボックス：2018/10/15
            txtNo.AutoSize = false;
            txtNo.Height = 21;
        }

        // データアダプターオブジェクト
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();
        MTYSDataSetTableAdapters.勤務票ヘッダTableAdapter hAdp = new MTYSDataSetTableAdapters.勤務票ヘッダTableAdapter();
        MTYSDataSetTableAdapters.勤務票明細TableAdapter iAdp = new MTYSDataSetTableAdapters.勤務票明細TableAdapter();

        MTYSDataSetTableAdapters.社員所属TableAdapter sAdp = new MTYSDataSetTableAdapters.社員所属TableAdapter();
        MTYSDataSetTableAdapters.勤怠記号TableAdapter kAdp = new MTYSDataSetTableAdapters.勤怠記号TableAdapter();
        MTYSDataSetTableAdapters.休日TableAdapter yAdp = new MTYSDataSetTableAdapters.休日TableAdapter();
        MTYSDataSetTableAdapters.出勤形態TableAdapter eAdp = new MTYSDataSetTableAdapters.出勤形態TableAdapter();
        MTYSDataSetTableAdapters.出勤日数TableAdapter nAdp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();

        // データセットオブジェクト
        MTYSDataSet dts = new MTYSDataSet();

        // カレント社員情報
        MTYSDataSet.社員所属Row cSR = null;
        
        /// <summary>
        ///     カレントデータRowsインデックス</summary>
        int cI = 0;

        // 処理テーブルキー配列  2015/10/30
        string [] sKey = null;

        // 社員マスターより取得した所属コード
        string mSzCode = string.Empty;

        #region 終了ステータス定数
        const string END_BUTTON = "btn";
        const string END_MAKEDATA = "data";
        const string END_CONTOROL = "close";
        const string END_NODATA = "non Data";
        #endregion

        string dID = string.Empty;                  // 表示する過去データのID
        string sDBNM = string.Empty;                // データベース名

        string _PCADBName = string.Empty;           // 会社領域データベース識別番号
        string _PCAComNo = string.Empty;            // 会社番号
        string _PCAComName = string.Empty;          // 会社名

        // dataGridView1_CellEnterステータス
        bool gridViewCellEnterStatus = true;

        private void frmCorrect_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // フォーム最大値
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小値
            Utility.WindowsMinSize(this, this.Width, this.Height);

            //元号を取得
            //label1.Text = Properties.Settings.Default.gengou;

            // OCRDATAクラスインスタンスを生成
            OCRData ocr = new OCRData();
            
            // 勤務データ登録
            if (dID == string.Empty)
            {
                // 参照テーブルデータセット読込み
                sAdp.Fill(dts.社員所属);
                kAdp.Fill(dts.勤怠記号);
                yAdp.Fill(dts.休日);
                eAdp.Fill(dts.出勤形態);
                nAdp.Fill(dts.出勤日数);

                // CSVデータをMDBへ読み込みます
                GetCsvDataToMDB();

                // データセットへデータを読み込みます
                getDataSet();

                // データテーブル件数カウント
                if (dts.勤務票ヘッダ.Count == 0)
                {
                    MessageBox.Show("対象となる勤務報告書データがありません", "勤務報告書データ登録", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    //終了処理
                    Environment.Exit(0);
                }
            }

            // キャプション
            this.Text = "勤務報告書データ表示";

            // グリッドビュー定義
            GridviewSet gs = new GridviewSet();
            gs.Setting_Shain(dGV);

            // 出勤簿データキー配列作成 2015/10/30
            createKeyArray();

            // 編集作業、過去データ表示の判断
            if (dID == string.Empty) // パラメータのヘッダIDがないときは編集作業
            {
                // 最初のレコードを表示
                cI = 0;
                showOcrData(cI);
            }

            // tagを初期化
            this.Tag = string.Empty;
        }

        #region データグリッドビューカラム定義
        private static string cDay = "col1";        // 日付
        private static string cWeek = "col2";       // 曜日
        private static string cKintai1 = "col3";    // 勤怠記号1
        private static string cKintai2 = "col4";    // 勤怠記号2
        private static string cSH = "col5";         // 開始時
        private static string cSE = "col6";
        private static string cSM = "col7";         // 開始分
        private static string cEH = "col8";         // 終了時
        private static string cEE = "col9";
        private static string cEM = "col10";        // 終了分
        private static string cZH = "col11";        // 残業時
        private static string cZE = "col12";
        private static string cZM = "col13";        // 残業分
        private static string cSIH = "col14";       // 深夜時
        private static string cSIE = "col15";
        private static string cSIM = "col16";       // 深夜分
        private static string cKSH = "col17";       // 休日出勤時
        private static string cKSE = "col18";
        private static string cKSM = "col19";       // 休日出勤分
        private static string cSKEITAI = "col20";   // 出勤形態
        private static string cID = "colID";
        #endregion

        // 勤務票データキー配列作成
        private void createKeyArray()
        {
            int i = 0;

            foreach (var t in dts.勤務票ヘッダ.OrderBy(a => a.個人番号))
            {
                Array.Resize(ref sKey, i + 1);
                sKey[i] = t.ID;

                i++;
            }
        }

        ///----------------------------------------------------------------------------
        /// <summary>
        ///     データグリッドビュークラス </summary>
        ///----------------------------------------------------------------------------
        private class GridviewSet
        {
            ///----------------------------------------------------------------------------
            /// <summary>
            ///     社員用データグリッドビューの定義を行います</summary> 
            /// <param name="gv">
            ///     データグリッドビューオブジェクト</param>
            ///----------------------------------------------------------------------------
            public void Setting_Shain(DataGridView gv)
            {
                try
                {
                    // データグリッドビューの基本設定
                    setGridView_Properties(gv);

                    // Tagをセット
                    gv.Tag = global.SHAIN_ID;

                    // カラムコレクションを空にします
                    gv.Columns.Clear();

                    // 行数をクリア            
                    gv.Rows.Clear();                                       

                    //各列幅指定
                    gv.Columns.Add(cDay, "日");
                    gv.Columns.Add(cWeek, "曜");
                    gv.Columns.Add(cKintai1, "記");
                    gv.Columns.Add(cKintai2, "号");
                    gv.Columns.Add(cSH, "開");
                    gv.Columns.Add(cSE, "");
                    gv.Columns.Add(cSM, "始");
                    gv.Columns.Add(cEH, "終");
                    gv.Columns.Add(cEE, "");
                    gv.Columns.Add(cEM, "了");
                    gv.Columns.Add(cZH, "普");
                    gv.Columns.Add(cZE, "");
                    gv.Columns.Add(cZM, "通");
                    gv.Columns.Add(cSIH, "深");
                    gv.Columns.Add(cSIE, "");
                    gv.Columns.Add(cSIM, "夜");
                    gv.Columns.Add(cKSH, "休");
                    gv.Columns.Add(cKSE, "");
                    gv.Columns.Add(cKSM, "出");
                    gv.Columns.Add(cSKEITAI, "形態");

                    gv.Columns.Add(cID, "");   // 明細ID
                    gv.Columns[cID].Visible = false;

                    foreach (DataGridViewColumn c in gv.Columns)
                    {
                        // 幅                       
                        if (c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE || c.Name == cSIE) 
                            c.Width = 10;
                        else c.Width = 26;
                                                
                        // 表示位置
                        if (c.Index < 4 || c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE || 
                            c.Name == cSIE)
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                        else c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                        if (c.Name == cSH || c.Name == cEH || c.Name == cZH || c.Name == cKSH || c.Name == cSIH) 
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                        if (c.Name == cSM || c.Name == cEM || c.Name == cZM || c.Name == cKSM || c.Name == cSIM) 
                            c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;

                        // 編集可否
                        if (c.Index < 2 || c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE ||
                            c.Name == cSIE)
                            c.ReadOnly = true;
                        else c.ReadOnly = false;

                        // 区切り文字
                        if (c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE || c.Name == cSIE)
                            c.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", 8, FontStyle.Regular);

                        // 入力可能桁数
                        // 普通残業・時2桁 2015/04/21
                        // 深夜残業・時1桁 2015/04/21
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
                        if (c.Name == cKintai1 || c.Name == cKintai2 || c.Name == cSIH || c.Name == cSKEITAI)
                        {
                            col.MaxInputLength = 1;
                        }
                        else
                        {
                            col.MaxInputLength = 2;
                        }

                        // ソート禁止
                        c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ///----------------------------------------------------------------------------
            /// <summary>
            ///     データグリッドビュー基本設定</summary>
            /// <param name="gv">
            ///     データグリッドビューオブジェクト</param>
            ///----------------------------------------------------------------------------
            private void setGridView_Properties(DataGridView gv)
            {
                // 列スタイルを変更する
                gv.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                gv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                gv.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // データフォント指定
                gv.DefaultCellStyle.Font = new Font("Meiryo UI", (Single)9.5, FontStyle.Regular);

                // 行の高さ
                gv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                gv.ColumnHeadersHeight = 19;
                gv.RowTemplate.Height = 19;

                // 全体の高さ
                gv.Height = 610;

                // 全体の幅
                gv.Width = 443;

                // 奇数行の色
                //gv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;

                //テキストカラーの設定
                gv.RowsDefaultCellStyle.ForeColor = Color.Navy;       
                gv.DefaultCellStyle.SelectionBackColor = Color.Empty;
                gv.DefaultCellStyle.SelectionForeColor = Color.Navy;

                // 行ヘッダを表示しない
                gv.RowHeadersVisible = false;

                // 選択モード
                gv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                gv.MultiSelect = false;

                // データグリッドビュー編集可
                gv.ReadOnly = false;

                // 追加行表示しない
                gv.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                gv.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                gv.AllowUserToOrderColumns = false;

                // 列サイズ変更不可
                gv.AllowUserToResizeColumns = false;

                // 行サイズ変更禁止
                gv.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //gv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                gv.StandardTab = false;

                // 編集モード
                gv.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }


        ///----------------------------------------------------------------------------
        /// <summary>
        ///     CSVデータをMDBへインサートする</summary>
        ///----------------------------------------------------------------------------
        private void GetCsvDataToMDB()
        {
            // CSVファイル数をカウント
            string[] inCsv = System.IO.Directory.GetFiles(Properties.Settings.Default.dataPath, "*.csv");

            // CSVファイルがなければ終了
            if (inCsv.Length == 0) return;

            // オーナーフォームを無効にする
            this.Enabled = false;

            //プログレスバーを表示する
            frmPrg frmP = new frmPrg();
            frmP.Owner = this;
            frmP.Show();

            // OCRのCSVデータをMDBへ取り込む
            OCRData ocr = new OCRData();
            ocr.CsvToMdb(Properties.Settings.Default.dataPath, frmP, dts);

            // いったんオーナーをアクティブにする
            this.Activate();

            // 進行状況ダイアログを閉じる
            frmP.Close();

            // オーナーのフォームを有効に戻す
            this.Enabled = true;
        }
 
        private void txtNo_TextChanged(object sender, EventArgs e)
        {
            // 過去データ表示のときは何もしない
            if (dID != string.Empty) return;

            // チェンジバリューステータス
            if (!global.ChangeValueStatus) return;

            // 社員番号のとき
            lblName.Text = string.Empty;

            if (txtNo.Text != string.Empty)
            {
                OCRData ocr = new OCRData();

                // 勤務票ヘッダテーブル行を取得
                //MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[cI];
                
                // 2015/10/30
                MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Single(a => a.ID == sKey[cI]);

                // 社員情報を参照します
                if (ocr.errCheckShainCode(dts, Utility.StrtoInt(txtNo.Text), out cSR))
                {
                    lblName.Text = cSR.氏名;
                    lblFuri.Text = cSR.フリガナ;
                    lblShokukyu.Text = cSR.職級.ToString();
                    lblSzCode.Text = cSR.所属コード.ToString();
                    label1.Text = cSR.所属名称;
                    label4.Text = cSR.帳票区分.ToString();
                    global gl = new global();
                    label27.Text = gl.arrayChohyoID[cSR.帳票区分 - 1];
                }
                else
                {
                    lblName.Text = string.Empty;
                    lblFuri.Text = string.Empty;
                    lblShokukyu.Text = string.Empty;
                    lblSzCode.Text = string.Empty;
                    label1.Text = string.Empty;
                    label4.Text = string.Empty;
                    label27.Text = string.Empty;
                }
            }
        }


        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == txtShukkin || sender == txtYukyu)
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            else if (sender == txtZangyoTL || sender == txtShinyaTL || sender == txtKyujitsuTL || 
                     sender == txtChikokuTL || sender == txtSoutaiTL)
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != ':')
                {
                    e.Handled = true;
                }
            }
            else
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                // 勤怠記号カラム
                // 出勤形態も"A","B","C"あり：2019/12/19
                if (dGV.CurrentCell.ColumnIndex == 2 || dGV.CurrentCell.ColumnIndex == 3　||
                    dGV.CurrentCell.ColumnIndex == 19)
                {
                    // 大文字英字入力とする（'A','B'等）
                    TextBox tb = (TextBox)(e.Control);
                    if (tb != null)
                        tb.CharacterCasing = CharacterCasing.Upper;
 
                    //イベントハンドラを削除する
                    e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                    e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress2);

                    //イベントハンドラを追加する
                    e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress2);
                }
                else
                {   
                    // 勤怠記号以外のカラムに適用
                    //イベントハンドラが複数回追加されてしまうので最初に削除する
                    e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                    e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress2);

                    //イベントハンドラを追加する
                    e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                }
            }
        }

        void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '\t')
                e.Handled = true;
        }

        void Control_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                e.KeyChar == '\b' || e.KeyChar == '\t')
                e.Handled = false;
            else e.Handled = true;
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     曜日をセットする </summary>
        /// <param name="tempRow">
        ///     MultiRowのindex</param>
        /// -------------------------------------------------------------------
        private void YoubiSet(int tempRow)
        {
            string sDate;
            DateTime eDate;
            Boolean bYear = false;
            Boolean bMonth = false;

            //年月を確認
            if (txtYear.Text != string.Empty)
            {
                if (Utility.NumericCheck(txtYear.Text))
                {
                    if (int.Parse(txtYear.Text) > 0)
                    {
                        bYear = true;
                    }
                }
            }

            if (txtMonth.Text != string.Empty)
            {
                if (Utility.NumericCheck(txtMonth.Text))
                {
                    if (int.Parse(txtMonth.Text) >= 1 && int.Parse(txtMonth.Text) <= 12)
                    {
                        for (int i = 0; i < global._MULTIGYO; i++)
                        {
                            bMonth = true;
                        }
                    }
                }
            }

            //年月の値がfalseのときは曜日セットは行わずに終了する
            if (bYear == false || bMonth == false) return;

            //行の色を初期化
            dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.Empty;

            //Nullか？
            dGV[cWeek, tempRow].Value = string.Empty;
            if (dGV[cDay, tempRow].Value != null) 
            {
                if (dGV[cDay, tempRow].Value.ToString() != string.Empty)
                {
                    if (Utility.NumericCheck(dGV[cDay, tempRow].Value.ToString()))
                    {
                        {
                            sDate = Utility.EmptytoZero(txtYear.Text) + "/" + 
                                    Utility.EmptytoZero(txtMonth.Text) + "/" +
                                    Utility.EmptytoZero(dGV[cDay, tempRow].Value.ToString());
                            
                            // 存在する日付と認識された場合、曜日を表示する
                            if (DateTime.TryParse(sDate, out eDate))
                            {
                                dGV[cWeek, tempRow].Value = ("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1);

                                //// 休日背景色設定・土曜日、日曜日
                                //if (dGV[cWeek, tempRow].Value.ToString() == "日" || dGV[cWeek, tempRow].Value.ToString() == "土")
                                //    dGV.Rows[tempRow].DefaultCellStyle.BackColor = Color.MistyRose;

                                // 時刻区切り文字
                                dGV[cSE, tempRow].Value = ":";
                                dGV[cEE, tempRow].Value = ":";
                                dGV[cSIE, tempRow].Value = ":";
                                dGV[cZE, tempRow].Value = ":";
                                dGV[cKSE, tempRow].Value = ":";
                            }
                        }
                    }
                }
             }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!global.ChangeValueStatus) return;

            if (e.RowIndex < 0) return;

            string colName = dGV.Columns[e.ColumnIndex].Name;

            // 日付
            if (colName == cDay) YoubiSet(e.RowIndex);  
            
            // 出勤日数
            //txtShukkinTl.Text = getWorkDays(_YakushokuType);

            //// 休日チェック
            //if (colName == cKyuka || colName == cCheck)
            //{
            //    // 休日行背景色設定
            //    if (dGV[cKyuka, e.RowIndex].Value.ToString() == "True")
            //        dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            //    else dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
            //}

            // 過去データ表示のときは終了
            if (dID != string.Empty) return;

            // 勤怠記号
            if (colName == cKintai1) 
            {
                //txtYukyuHiTl.Text = getYukyuTotal(0);
                //txtYukyuTmTl.Text = getYukyuTotal(1);
            }

            // 出勤時刻、退勤時刻
            if (colName == cSH || colName == cSM || colName == cEH || colName == cEM)
            {
                // 実働時間を計算して表示する
                if (dGV[cSH, e.RowIndex].Value != null && dGV[cSM, e.RowIndex].Value != null &&
                    dGV[cEH, e.RowIndex].Value != null && dGV[cEM, e.RowIndex].Value != null)
                {
                    if (dGV[cSH, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cSM, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cEH, e.RowIndex].Value.ToString() != string.Empty &&
                        dGV[cEM, e.RowIndex].Value.ToString() != string.Empty)
                    {
                        // 出勤時刻、退勤時刻、休憩時間から実働時間を取得する
                        OCRData ocr = new OCRData();
                        //double wTime = ocr.getWorkTime(dGV[cSH, e.RowIndex].Value.ToString(),
                        //                dGV[cSM, e.RowIndex].Value.ToString(),
                        //                dGV[cEH, e.RowIndex].Value.ToString(),
                        //                dGV[cEM, e.RowIndex].Value.ToString(),
                        //                Properties.Settings.Default.restTime);

                        //// 遅刻早退外出時間を差し引く
                        //double cTime = Utility.StrtoDouble(Utility.NulltoStr(dGV[cCSGH, e.RowIndex].Value)) * 60 +
                        //               Utility.StrtoDouble(Utility.NulltoStr(dGV[cCSGM, e.RowIndex].Value));
                        //wTime = wTime - cTime;

                        // 実働時間
                        //double wTimeH = Math.Floor(wTime / 60);
                        //double wTimeM = wTime % 60;

                        // ChangeValueイベントを発生させない
                        global.ChangeValueStatus = false;
                        //dGV[cWH, e.RowIndex].Value = wTimeH.ToString();
                        //dGV[cWM, e.RowIndex].Value = wTimeM.ToString();
                        global.ChangeValueStatus = true;
                    }
                    else
                    {
                        // ChangeValueイベントを発生させない
                        global.ChangeValueStatus = false;
                        //dGV[cWH, e.RowIndex].Value = string.Empty;
                        //dGV[cWM, e.RowIndex].Value = string.Empty;
                        global.ChangeValueStatus = true;
                    }
                }
                else
                {
                    // ChangeValueイベントを発生させない
                    global.ChangeValueStatus = false;
                    //dGV[cWH, e.RowIndex].Value = string.Empty;
                    //dGV[cWM, e.RowIndex].Value = string.Empty;
                    global.ChangeValueStatus = true;
                }
            }

        }

        /// <summary>
        /// 与えられた休暇記号に該当する休暇日数取得
        /// </summary>
        /// <param name="kigou">休暇記号</param>
        /// <returns>休暇日数</returns>
        private string getKyukaTotal(string kigou)
        {
            int days = 0;
            //for (int i = 0; i < dGV.RowCount; i++)
            //{
            //    if (dGV[cKyuka, i].Value != null)
            //    {
            //        if (dGV[cKyuka, i].Value.ToString() == kigou)
            //            days++;
            //    }
            //}

            return days.ToString();
        }

        /// <summary>
        /// 有給休暇日数・時間取得
        /// </summary>
        /// <returns></returns>
        /// <param name="Status">0:日数、1:時間</param>
        private string getYukyuTotal(int Status)
        {
            int days = 0;
            for (int i = 0; i < dGV.RowCount; i++)
            {
                if (dGV[cKintai1, i].Value != null)
                {
                    if (Status == 0)    // 有給日数
                    {
                        if (dGV[cKintai1, i].Value.ToString() == global.K_YUKYU)
                        days++;
                    }
                    else if (Status == 1)   // 有給時間
                    {
                        if (dGV[cKintai1, i].Value.ToString() != global.K_YUKYU &&
                            dGV[cKintai1, i].Value.ToString() != string.Empty)
                            days += int.Parse(dGV[cKintai1, i].Value.ToString());
                    }
                }
            }

            return days.ToString();
        }


        /// <summary>
        /// 出勤日数取得
        /// </summary>
        /// <returns>出勤日数</returns>
        private string getWorkDays(int yaku)
        {
            // 出勤日数
            int sDays = 0;
            for (int i = 0; i < dGV.RowCount; i++)
            {
                // 勤務時間が記入されている行
                if ((dGV[cSH, i].Value != null && dGV[cSH, i].Value.ToString() != string.Empty) && 
                    (dGV[cSM, i].Value != null && dGV[cSM, i].Value.ToString() != string.Empty))
                {
                    // 開始時間が24時台以外のもの（24時台は前日からの通し勤務とみなし出勤日数に加えない）
                    if (dGV[cSH, i].Value.ToString() != "24")
                    {
                        // 社員
                        if (yaku != global.flgOn) sDays++;
                        else if (dGV[cKintai1, i].Value != null)    // パート：終日有休以外のときは出勤日数としてカウントする
                        {
                            if (dGV[cKintai1, i].Value.ToString() != global.K_YUKYU)
                                sDays++;
                        }
                    }
                }
            }

            return sDays.ToString();
        }

        /// <summary>
        /// 深夜勤務時間取得(22:00～05:00)
        /// </summary>
        /// <returns>深夜勤務時間・分</returns>
        private double getShinyaTime()
        {
            int wHour = 0;
            int wMin = 0;
            int wHourk = 0;
            int wMink = 0;
            int sKyukei = 0;

            int sHour = 0;
            int sMin = 0;

            DateTime stTM;
            DateTime edTM;
            double spanMin = 0;

            for (int i = 0; i < dGV.RowCount; i++)
            {
                // 開始が５：００以前のとき
                if (Utility.NulltoStr(dGV[cSH, i].Value) != string.Empty && 
                    Utility.NulltoStr(dGV[cSM, i].Value) != string.Empty)
                {
                    wHour = Utility.StrtoInt(Utility.NulltoStr(dGV[cSH, i].Value));
                    wMin = Utility.StrtoInt(Utility.NulltoStr(dGV[cSM, i].Value));

                    if (wHour == 24) wHour = 0;

                    if (wHour < 5 && wMin < 60)
                    {
                        // 深夜勤務時間
                        stTM = DateTime.Parse(wHour.ToString() + ":" + wMin.ToString());
                        spanMin += Utility.GetTimeSpan(stTM, global.dt0500).TotalMinutes;
                    }
                }

                // 終了が２２：００以降のとき
                if (Utility.NulltoStr(dGV[cEH, i].Value) != string.Empty && 
                    Utility.NulltoStr(dGV[cEM, i].Value) != string.Empty)
                {
                    wHour = Utility.StrtoInt(Utility.NulltoStr(dGV[cEH, i].Value));
                    wMin = Utility.StrtoInt(Utility.NulltoStr(dGV[cEM, i].Value));

                    if (wHour >= 22)
                    {
                        // 深夜勤務時間
                        //sHour = (wHour - 22) * 60 + wMin;

                        if (wHour < 25 && wMin < 60)
                        {
                            if (wHour < 24)
                            {
                                edTM = DateTime.Parse(wHour.ToString() + ":" + wMin.ToString());
                                spanMin += Utility.GetTimeSpan(global.dt2200, edTM).TotalMinutes;
                            }
                            // 24:00のときは23:59まで計算して1分加算する
                            else if (wMin == 0)
                            {
                                edTM = DateTime.Parse("23:59");
                                spanMin += Utility.GetTimeSpan(global.dt2200, edTM).TotalMinutes + 1;
                            }
                        }
                    }
                }

                // 深夜勤務時間
                spanMin -= sKyukei;
            }

            return spanMin;
        }

        private void frmCorrect_Shown(object sender, EventArgs e)
        {
            if (dID != string.Empty) btnRtn.Focus();
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //イベントハンドラが複数回追加されてしまうので最初に削除する
                e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                //イベントハンドラを追加する
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
        }

        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //イベントハンドラが複数回追加されてしまうので最初に削除する
                e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
                //イベントハンドラを追加する
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            if (cI + 1 < dts.勤務票ヘッダ.Rows.Count)
            {
                cI++;
                showOcrData(cI);
            }   
        }

        ///-----------------------------------------------------------------------------------
        /// <summary>
        ///     カレントデータを更新する</summary>
        /// <param name="iX">
        ///     カレントレコードのインデックス</param>
        ///-----------------------------------------------------------------------------------
        private void CurDataUpDate(int iX)
        {
            // エラーメッセージ
            string errMsg = "出勤簿テーブル更新";

            try
            {
                // 勤務票ヘッダテーブル行を取得
                //MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[iX];

                // 2015/10/30
                MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Single(a => a.ID == sKey[iX]);

                // 勤務票ヘッダテーブルセット更新
                r.年 = Utility.StrtoInt(txtYear.Text);
                r.月 = Utility.StrtoInt(txtMonth.Text);
                r.個人番号 = Utility.StrtoInt(txtNo.Text);
                r.氏名 = Utility.NulltoStr(lblName.Text);
                r.フリガナ = Utility.NulltoStr(lblFuri.Text);
                r.所属コード = lblSzCode.Text;
                r.所属名 = label1.Text;
                r.帳票番号 = Utility.StrtoInt(label4.Text); 

                r.出勤すべき日数 = Utility.StrtoInt(lblNissu.Text);
                r.計算用出勤日数 = Utility.StrtoInt(txtKeisanDays.Text);
                r.出勤日数合計 = (int)System.Math.Floor(Utility.StrtoDouble(txtShukkin.Text));
                r.出勤日数2 = (int)((Utility.StrtoDouble(txtShukkin.Text) * 10) % 10);
                r.有休日数合計 = (int)System.Math.Floor(Utility.StrtoDouble(txtYukyu.Text));
                r.有休日数2 = (int)((Utility.StrtoDouble(txtYukyu.Text) * 10) % 10);
                r.特休日数合計 = Utility.StrtoInt(txtTokukyu.Text);
                r.欠勤日数合計 = Utility.StrtoInt(txtKekkin.Text);
                r.生理分娩日数 = Utility.StrtoInt(txtSeiri.Text);
                r.時差出勤日数 = Utility.StrtoInt(txtJisa.Text);
                r.平日保安回数 = Utility.StrtoInt(txtHoanH.Text);
                r.休日保安回数 = Utility.StrtoInt(txtHoanK.Text);
                r.平日宿日直回数 = Utility.StrtoInt(txtShukuH.Text);
                r.休日宿日直回数 = Utility.StrtoInt(txtShukuK.Text);
                r._1L勤回数 = Utility.StrtoInt(txt1L.Text);
                r._2勤回数 = Utility.StrtoInt(txt2Kin.Text);
                r.丸3勤回数 = Utility.StrtoInt(txtMaru3Kin.Text);
                r._3勤回数 = Utility.StrtoInt(txt3Kin.Text);
                r.日祝日勤務回数 = Utility.StrtoInt(txtshukujitsu.Text);
                r.控除日数 = Utility.StrtoInt(txtKoujyoNissu.Text);

                int hh = 0;
                int mm = 0;

                Utility.getHHMM(txtZangyoTL.Text, out hh, out mm);
                r.残業時 = hh;
                r.残業分 = mm;

                Utility.getHHMM(txtShinyaTL.Text, out hh, out mm);
                r.深夜時 = hh;
                r.深夜分 = mm;

                Utility.getHHMM(txtKyujitsuTL.Text, out hh, out mm);
                r.休日勤務時 = hh;
                r.休日勤務分 = mm;

                Utility.getHHMM(txtChikokuTL.Text, out hh, out mm);
                r.遅刻時間時 = hh;
                r.遅刻時間分 = mm;

                Utility.getHHMM(txtSoutaiTL.Text, out hh, out mm);
                r.早退時間時 = hh;
                r.早退時間分 = mm;

                r.更新年月日 = DateTime.Now;

                // 勤務票明細テーブルセット更新
                for (int i = 0; i < global._MULTIGYO; i++)
                {
                    // 存在する日付か検証
                    if (Utility.NulltoStr(dGV[cWeek, i].Value) != string.Empty)
                    {
                        MTYSDataSet.勤務票明細Row m = (MTYSDataSet.勤務票明細Row)dts.勤務票明細.FindByID(int.Parse(dGV[cID, i].Value.ToString()));

                        m.勤怠記号1 = Utility.NulltoStr(dGV[cKintai1, i].Value);
                        m.勤怠記号2 = Utility.NulltoStr(dGV[cKintai2, i].Value);
                        m.開始時 = timeValH(dGV[cSH, i].Value);
                        m.開始分 = timeVal(dGV[cSM, i].Value, 2);
                        m.終了時 = timeValH(dGV[cEH, i].Value);
                        m.終了分 = timeVal(dGV[cEM, i].Value, 2);
                        m.深夜時 = timeVal(dGV[cSIH, i].Value, 1);
                        m.深夜分 = timeVal(dGV[cSIM, i].Value, 2);
                        m.時間外時 = timeValH(dGV[cZH, i].Value);
                        m.時間外分 = timeVal(dGV[cZM, i].Value, 2);
                        m.休日出勤時 = timeValH(dGV[cKSH, i].Value);
                        m.休日出勤分 = timeVal(dGV[cKSM, i].Value, 2);
                        m.出勤形態 = Utility.NulltoStr(dGV[cSKEITAI, i].Value);
                        m.更新年月日 = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errMsg, MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        /// ----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     空白以外のとき、指定された文字数になるまで左側に０を埋めこみ、右寄せした文字列を返す
        /// </summary>
        /// <param name="tm">
        ///     文字列</param>
        /// <param name="len">
        ///     文字列の長さ</param>
        /// <returns>
        ///     文字列</returns>
        /// ----------------------------------------------------------------------------------------------------
        private string timeVal(object tm, int len)
        {
            string t = Utility.NulltoStr(tm);
            if (t != string.Empty) return t.PadLeft(len, '0');
            else return t;
        }

        /// ----------------------------------------------------------------------------------------------------
        /// <summary>
        ///     空白以外のとき、先頭文字が０のとき先頭文字を削除した文字列を返す　
        ///     先頭文字が０以外のときはそのまま返す
        /// </summary>
        /// <param name="tm">
        ///     文字列</param>
        /// <returns>
        ///     文字列</returns>
        /// ----------------------------------------------------------------------------------------------------
        private string timeValH(object tm)
        {
            string t = Utility.NulltoStr(tm);

            if (t != string.Empty)
            {
                t = t.PadLeft(2, '0');
                if (t.Substring(0, 1) == "0")
                {
                    t = t.Substring(1, 1);
                }
            }

            return t;
        }

        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     Bool値を数値に変換する </summary>
        /// <param name="b">
        ///     True or False</param>
        /// <returns>
        ///     true:1, false:0</returns>
        /// ------------------------------------------------------------------------------------
        private int booltoFlg(string b)
        {
            if (b == "True") return global.flgOn;
            else return global.flgOff;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI =  dts.勤務票ヘッダ.Rows.Count - 1;
            showOcrData(cI);
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            if (cI > 0)
            {
                cI--;
                showOcrData(cI);
            }   
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI = 0;
            showOcrData(cI);
        }

        /// <summary>
        /// エラーチェックボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrCheck_Click(object sender, EventArgs e)
        {
            // OCRDataクラス生成
            OCRData ocr = new OCRData();

            // エラーチェックを実行
            if (getErrData(cI, ocr))
            {
                MessageBox.Show("エラーはありませんでした", "エラーチェック", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dGV.CurrentCell = null;

                // データ表示
                showOcrData(cI);
            }
            else
            {
                // カレントインデックスをエラーありインデックスで更新
                cI = ocr._errHeaderIndex;

                // データ表示
                showOcrData(cI);

                // エラー表示
                ErrShow(ocr);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //カレントデータの更新
            CurDataUpDate(cI);

            //レコードの移動
            cI = hScrollBar1.Value;
            showOcrData(cI);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("表示中の勤務管理表データを削除します。よろしいですか", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // レコードと画像ファイルを削除する
            DataDelete(cI);

            // 勤務票ヘッダテーブル件数カウント
            if (dts.勤務票ヘッダ.Count() > 0)
            {
                //// カレントレコードインデックスを再設定
                //if (dts.勤務票ヘッダ.Count() - 1 < cI) cI = dts.勤務票ヘッダ.Count() - 1;

                
                // データ表示 2015/10/30
                if (cI > (sKey.Length - 1))
                {
                    cI = sKey.Length - 1;
                }

                // データ画面表示
                showOcrData(cI);
            }
            else
            {
                // ゼロならばプログラム終了
                MessageBox.Show("全ての勤務票データが削除されました。処理を終了します。", "勤務票削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //終了処理
                this.Tag = END_NODATA;
                this.Close();
            }
        }

        ///-------------------------------------------------------------------------------
        /// <summary>
        ///     １．指定した勤務票ヘッダデータと勤務票明細データを削除する　
        ///     ２．該当する画像データを削除する</summary>
        /// <param name="i">
        ///     勤務票ヘッダRow インデックス</param>
        ///-------------------------------------------------------------------------------
        private void DataDelete(int i)
        {
            string sImgNm = string.Empty;
            string errMsg = string.Empty;

            // 勤務票データ削除
            try
            {
                // ヘッダIDを取得します
                //MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[i];

                // 2015/10/30
                MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Single(a => a.ID == sKey[i]);

                // 画像ファイル名を取得します
                sImgNm = r.画像名;

                // データテーブルからヘッダIDが一致する勤務票明細データを削除します。
                errMsg = "勤務票明細データ";
                foreach (MTYSDataSet.勤務票明細Row item in dts.勤務票明細.Rows)
                {
                    if (item.RowState != DataRowState.Deleted && item.ヘッダID == r.ID)
                    {
                        item.Delete();
                    }
                }

                // データテーブルから勤務票ヘッダデータを削除します
                errMsg = "勤務票ヘッダデータ";
                //dts.勤務票ヘッダ.Rows[i].Delete();

                // 2015/10/30
                r.Delete();

                // データベース更新
                adpMn.UpdateAll(dts);

                // 画像ファイルを削除します
                errMsg = "勤務管理表画像";
                if (sImgNm != string.Empty)
                {
                    if (System.IO.File.Exists(Properties.Settings.Default.dataPath + sImgNm))
                    {
                        System.IO.File.Delete(Properties.Settings.Default.dataPath + sImgNm);
                    }
                }

                // 出勤簿キー配列再作成 2015/10/30
                createKeyArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(errMsg + "の削除に失敗しました" + Environment.NewLine + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
            }

        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じる
            this.Tag = END_BUTTON;
            this.Close();
        }

        private void frmCorrect_FormClosing(object sender, FormClosingEventArgs e)
        {
            //「受入データ作成終了」「勤務票データなし」以外での終了のとき
            if (this.Tag.ToString() != END_MAKEDATA && this.Tag.ToString() != END_NODATA)
            {
                if (MessageBox.Show("終了します。よろしいですか", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // カレントデータ更新
                if (dID == string.Empty) CurDataUpDate(cI);
            }

            // データベース更新
            adpMn.UpdateAll(dts);

            // 解放する
            this.Dispose();
        }

        private void btnDataMake_Click(object sender, EventArgs e)
        {
            // 給与計算用テキストデータ出力
            textDataMake();
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        ///     給与計算・受入テキストデータ出力 </summary>
        /// -----------------------------------------------------------------------
        private void textDataMake()
        {
            if (MessageBox.Show("給与計算用受け渡しデータを作成します。よろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            // OCRDataクラス生成
            OCRData ocr = new OCRData();

            // エラーチェックを実行する
            if (getErrData(cI, ocr)) // エラーがなかったとき
            {
                // OCROutputクラス インスタンス生成
                OCROutput kd = new OCROutput(this, dts);
                kd.SaveData();          // 汎用データ作成
                kd.writeChecklist();    // 勤怠チェックリストテーブル作成

                // 画像ファイル退避
                tifFileMove();

                // 過去データ作成
                saveLastData();

                // 設定月数分経過した過去画像と過去勤務データを削除する
                deleteArchived();

                // 勤務票データ削除
                deleteDataAll();

                // MDBファイル最適化
                mdbCompact();

                //終了
                MessageBox.Show("終了しました。給与計算システムで勤務データ受け入れを行ってください。", "給与計算受け入れデータ作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Tag = END_MAKEDATA;
                this.Close();
            }
            else
            {
                // カレントインデックスをエラーありインデックスで更新
                cI = ocr._errHeaderIndex;

                // エラーあり
                showOcrData(cI);    // データ表示
                ErrShow(ocr);   // エラー表示
            }
        }

        /// -----------------------------------------------------------------------------------
        /// <summary>
        ///     エラーチェックを実行する</summary>
        /// <param name="cIdx">
        ///     現在表示中の勤務票ヘッダデータインデックス</param>
        /// <param name="ocr">
        ///     OCRDATAクラスインスタンス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        /// -----------------------------------------------------------------------------------
        private bool getErrData(int cIdx, OCRData ocr)
        {
            // カレントレコード更新
            CurDataUpDate(cIdx);

            // エラー番号初期化
            ocr._errNumber = ocr.eNothing;

            // エラーメッセージクリーン
            ocr._errMsg = string.Empty;         

            // エラーチェック実行①:カレントレコードから最終レコードまで
            if (!ocr.errCheckMain(cIdx, (dts.勤務票ヘッダ.Rows.Count - 1), this, dts, sKey))
            {
                return false;
            }

            // エラーチェック実行②:最初のレコードからカレントレコードの前のレコードまで
            if (cIdx > 0)
            {
                if (!ocr.errCheckMain(0, (cIdx - 1), this, dts, sKey))
                {
                    return false;
                }
            }

            // エラーなし
            lblErrMsg.Text = string.Empty;

            return true;
        }
        
        ///----------------------------------------------------------------------------------
        /// <summary>
        ///     画像ファイル退避処理 </summary>
        ///----------------------------------------------------------------------------------
        private void tifFileMove()
        {
            Cursor = Cursors.WaitCursor;

            // 移動先フォルダがあるか？なければ作成する（TIFフォルダ）
            if (!System.IO.Directory.Exists(Properties.Settings.Default.tifPath))
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.tifPath);

            // 出勤簿ヘッダデータを取得する
            var s = dts.勤務票ヘッダ.OrderBy(a => a.ID);

            foreach (var t in s)
            {
                string NewFilenameYearMonth = t.年.ToString() + t.月.ToString().PadLeft(2, '0');

                // 画像ファイルパスを取得する
                string fromImg = Properties.Settings.Default.dataPath + t.画像名;

                // ファイル名を「会社番号_対象年月・帳票ID・個人番号」に変えて退避先フォルダへ移動する
                string NewFilename = _PCAComNo.PadLeft(4, '0') + "_" + NewFilenameYearMonth + t.帳票番号.ToString() + t.個人番号.ToString().PadLeft(global.ShainMaxLength, '0') + ".tif";
                string toImg = Properties.Settings.Default.tifPath + NewFilename;

                // 同名ファイルが既に登録済みのときは削除する
                if (System.IO.File.Exists(toImg)) System.IO.File.Delete(toImg);

                // ファイルを移動する
                if (System.IO.File.Exists(fromImg)) System.IO.File.Move(fromImg, toImg);

                // 出勤簿ヘッダレコードの画像ファイル名を書き換える
                MTYSDataSet.勤務票ヘッダRow r = dts.勤務票ヘッダ.FindByID(t.ID);
                r.画像名 = NewFilename;
            }

            Cursor = Cursors.Default;
        }

        /// ---------------------------------------------------------------------
        /// <summary>
        ///     MDBファイルを最適化する </summary>
        /// ---------------------------------------------------------------------
        private void mdbCompact()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                JRO.JetEngine jro = new JRO.JetEngine();
                string OldDb = Properties.Settings.Default.mdbOlePath;
                string NewDb = Properties.Settings.Default.mdbPathTemp;

                jro.CompactDatabase(OldDb, NewDb);

                //今までのバックアップファイルを削除する
                System.IO.File.Delete(Properties.Settings.Default.mdbPath + global.MDBBACK);

                //今までのファイルをバックアップとする
                System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBFILE, Properties.Settings.Default.mdbPath + global.MDBBACK);

                //一時ファイルをMDBファイルとする
                System.IO.File.Move(Properties.Settings.Default.mdbPath + global.MDBTEMP, Properties.Settings.Default.mdbPath + global.MDBFILE);
            }
            catch (Exception e)
            {
                MessageBox.Show("MDB最適化中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        
        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (leadImg.ScaleFactor < global.ZOOM_MAX)
            {
                leadImg.ScaleFactor += global.ZOOM_STEP;
            }
            global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (leadImg.ScaleFactor > global.ZOOM_MIN)
            {
                leadImg.ScaleFactor -= global.ZOOM_STEP;
            }
            global.miMdlZoomRate = (float)leadImg.ScaleFactor;
        }

        /// ---------------------------------------------------------------------------------
        /// <summary>
        ///     設定月数分経過した過去画像と過去勤務データを削除する </summary>        ///     
        /// ---------------------------------------------------------------------------------
        private void deleteArchived()
        {
            // 削除月設定が0のとき、「過去画像削除しない」とみなし終了する
            if (global.cnfArchived == global.flgOff) return;

            Cursor = Cursors.WaitCursor;

            try
            {
                // 削除年月の取得
                DateTime dt = DateTime.Parse(DateTime.Today.Year.ToString() + "/" + DateTime.Today.Month.ToString() + "/01");
                DateTime delDate = dt.AddMonths(global.cnfArchived * (-1));
                int _dYY = delDate.Year;            //基準年
                int _dMM = delDate.Month;           //基準月
                int _dYYMM = _dYY * 100 + _dMM;     //基準年月
                int _waYYMM = (delDate.Year - Properties.Settings.Default.RekiHosei) * 100 + _dMM;   //基準年月(和暦）
                
                // 設定月数分経過した過去画像を削除する
                deleteImageArchived(_dYYMM);

                // 過去勤務票データを削除する
                deleteLastDataArchived(_dYYMM);

                // 勤務チェックリストデータを削除する
                deleteCheckArchived(_dYYMM);
            }
            catch (Exception e)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("過去画像・過去勤務票データ削除中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                //if (ocr.sCom.Connection.State == ConnectionState.Open) ocr.sCom.Connection.Close();
            }
        }

        /// ---------------------------------------------------------------------------
        /// <summary>
        ///     過去勤務票データ登録 </summary>
        /// ---------------------------------------------------------------------------
        private void saveLastData()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                // 過去勤務票ヘッダテーブルデータ読み込み
                MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter lsh = new MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter();
                adpMn.過去勤務票ヘッダTableAdapter = lsh;
                adpMn.過去勤務票ヘッダTableAdapter.Fill(dts.過去勤務票ヘッダ);
                
                // 過去勤務票明細テーブルデータ読み込み
                MTYSDataSetTableAdapters.過去勤務票明細TableAdapter lsm = new MTYSDataSetTableAdapters.過去勤務票明細TableAdapter();
                adpMn.過去勤務票明細TableAdapter = lsm;
                adpMn.過去勤務票明細TableAdapter.Fill(dts.過去勤務票明細);
                
                // -------------------------------------------------------------------------
                //      帳票番号、データ領域名、年月、個人番号が一致する
                //      過去勤務票ヘッダデータとその明細データを削除します
                // -------------------------------------------------------------------------
                deleteLastData();

                // -------------------------------------------------------------------------
                //      過去勤務票ヘッダデータと過去勤務票明細データを作成します
                // -------------------------------------------------------------------------
                addLastdata();

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "過去勤務票データ作成エラー", MessageBoxButtons.OK);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        /// -------------------------------------------------------------------------
        /// <summary>
        ///     勤務票データの帳票番号、データ領域名、年月、個人番号が一致する
        ///     過去勤務票ヘッダデータとその明細データを削除します</summary>    
        ///     
        /// -------------------------------------------------------------------------
        private void deleteLastData()
        {
            // 勤務票ヘッダを取得します
            var s = dts.勤務票ヘッダ
                .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached)
                .OrderBy(a => a.ID);

            foreach (var r in s)
            {
                // 帳票番号、データ領域名、年月、個人番号が一致する過去勤務票ヘッダデータを取得します
                var h = dts.過去勤務票ヘッダ
                        .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                    a.帳票番号 == r.帳票番号 && a.年 == r.年 && a.月 == r.月 &&
                                    a.個人番号 == r.個人番号 && a.データ領域名 == r.データ領域名);

                // foreach用の配列を作成
                var hLst = h.ToList();

                foreach (var lh in hLst)
                {
                    // ヘッダIDが一致する過去勤務票明細を取得します
                    var m = dts.過去勤務票明細
                        .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                    a.ヘッダID == lh.ID);

                    // foreach用の配列を作成
                    var list = m.ToList();

                    // 該当過去勤務票明細を削除します
                    foreach (var lm in list)
                    {
                        MTYSDataSet.過去勤務票明細Row lRow = (MTYSDataSet.過去勤務票明細Row)dts.過去勤務票明細.Rows.Find(lm.ID);
                        lRow.Delete();
                    }

                    // 過去勤務票ヘッダを削除します
                    lh.Delete();
                }
            }

            // データベース更新
            adpMn.UpdateAll(dts);
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     過去勤務票ヘッダデータと過去勤務票明細データを作成します</summary>
        ///     
        /// -------------------------------------------------------------------------
        private void addLastdata()
        {
            for (int i = 0; i < dts.勤務票ヘッダ.Rows.Count; i++)
            {
                // -------------------------------------------------------------------------
                //      過去勤務票ヘッダレコードを作成します
                // -------------------------------------------------------------------------
                MTYSDataSet.勤務票ヘッダRow hr = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[i];
                MTYSDataSet.過去勤務票ヘッダRow nr = dts.過去勤務票ヘッダ.New過去勤務票ヘッダRow();

                #region テーブルカラム名比較～データコピー

                // 勤務票ヘッダのカラムを順番に読む
                for (int j = 0; j < dts.勤務票ヘッダ.Columns.Count; j++)
                {
                    // 過去勤務票ヘッダのカラムを順番に読む
                    for (int k = 0; k < dts.過去勤務票ヘッダ.Columns.Count; k++)
                    {
                        // フィールド名が同じであること
                        if (dts.勤務票ヘッダ.Columns[j].ColumnName == dts.過去勤務票ヘッダ.Columns[k].ColumnName)
                        {
                            if (dts.過去勤務票ヘッダ.Columns[k].ColumnName == "更新年月日")
                            {
                                nr[k] = DateTime.Now;   // 更新年月日はこの時点のタイムスタンプを登録
                            }
                            else
                            {
                                nr[k] = hr[j];          // データをコピー
                            }
                            break;
                        }
                    }
                }
                #endregion

                // 過去勤務票ヘッダデータテーブルに追加
                dts.過去勤務票ヘッダ.Add過去勤務票ヘッダRow(nr);

                // -------------------------------------------------------------------------
                //      過去勤務票明細レコードを作成します
                // -------------------------------------------------------------------------
                var mm = dts.勤務票明細
                    .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                           a.ヘッダID == hr.ID)
                    .OrderBy(a => a.ID);

                foreach (var item in mm)
                {
                    MTYSDataSet.勤務票明細Row m = (MTYSDataSet.勤務票明細Row)dts.勤務票明細.Rows.Find(item.ID);
                    MTYSDataSet.過去勤務票明細Row nm = dts.過去勤務票明細.New過去勤務票明細Row();

                    #region  テーブルカラム名比較～データコピー

                    // 勤務票明細のカラムを順番に読む
                    for (int j = 0; j < dts.勤務票明細.Columns.Count; j++)
                    {
                        // IDはオートナンバーのため値はコピーしない
                        if (dts.勤務票明細.Columns[j].ColumnName != "ID")
                        {
                            // 過去勤務票ヘッダのカラムを順番に読む
                            for (int k = 0; k < dts.過去勤務票明細.Columns.Count; k++)
                            {
                                // フィールド名が同じであること
                                if (dts.勤務票明細.Columns[j].ColumnName == dts.過去勤務票明細.Columns[k].ColumnName)
                                {
                                    if (dts.過去勤務票明細.Columns[k].ColumnName == "更新年月日")
                                    {
                                        nm[k] = DateTime.Now;   // 更新年月日はこの時点のタイムスタンプを登録
                                    }
                                    else
                                    {
                                        nm[k] = m[j];          // データをコピー
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    #endregion

                    // 過去勤務票明細データテーブルに追加
                    dts.過去勤務票明細.Add過去勤務票明細Row(nm);
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            string colName = dGV.Columns[e.ColumnIndex].Name;

            if (colName == cSH || colName == cSE || colName == cEH || colName == cEE ||
                colName == cZH || colName == cZE || colName == cSIH || colName == cSIE || 
                colName == cKSH || colName == cKSE)
            {
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            string colName = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;
            //if (colName == cKyuka || colName == cCheck)
            //{
            //    if (dGV.IsCurrentCellDirty)
            //    {
            //        dGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //        dGV.RefreshEdit();
            //    }
            //}
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            // エラー表示時には処理を行わない
            if (!gridViewCellEnterStatus) return;
 
            string ColH = string.Empty;
            string ColM = dGV.Columns[dGV.CurrentCell.ColumnIndex].Name;

            // 開始時間または終了時間を判断
            if (ColM == cSM)            // 開始時刻
            {
                ColH = cSH;
            }
            else if (ColM == cEM)       // 終了時刻
            {
                ColH = cEH;
            }
            else if (ColM == cZM)       // 時間外
            {
                ColH = cZH;
            }
            else if (ColM == cKSM)      // 休日出勤
            {
                ColH = cKSH;
            }
            else if (ColM == cSIM)      // 深夜
            {
                ColH = cSIH;
            }
            else
            {
                return;
            }

            // 時が入力済みで分が未入力のとき分に"00"を表示します
            if (dGV[ColH, dGV.CurrentRow.Index].Value != null)
            {
                if (dGV[ColH, dGV.CurrentRow.Index].Value.ToString().Trim() != string.Empty)
                {
                    if (dGV[ColM, dGV.CurrentRow.Index].Value == null)
                    {
                        dGV[ColM, dGV.CurrentRow.Index].Value = "00";
                    }
                    else if (dGV[ColM, dGV.CurrentRow.Index].Value.ToString().Trim() == string.Empty)
                    {
                        dGV[ColM, dGV.CurrentRow.Index].Value = "00";
                    }
                }
            }
        }

        /// ------------------------------------------------------------------------------
        /// <summary>
        ///     伝票画像表示 </summary>
        /// <param name="iX">
        ///     現在の伝票</param>
        /// <param name="tempImgName">
        ///     画像名</param>
        /// ------------------------------------------------------------------------------
        public void ShowImage(string tempImgName)
        {
            //修正画面へ組み入れた画像フォームの表示    
            //画像の出力が無い場合は、画像表示をしない。
            if (tempImgName == string.Empty)
            {
                leadImg.Visible = false;
                lblNoImage.Visible = false;
                global.pblImagePath = string.Empty;
                return;
            }

            //画像ファイルがあるとき表示
            if (File.Exists(tempImgName))
            {
                lblNoImage.Visible = false;
                leadImg.Visible = true;

                // 画像操作ボタン
                btnPlus.Enabled = true;
                btnMinus.Enabled = true;

                //画像ロード
                Leadtools.Codecs.RasterCodecs.Startup();
                Leadtools.Codecs.RasterCodecs cs = new Leadtools.Codecs.RasterCodecs();

                // 描画時に使用される速度、品質、およびスタイルを制御します。 
                Leadtools.RasterPaintProperties prop = new Leadtools.RasterPaintProperties();
                prop = Leadtools.RasterPaintProperties.Default;
                prop.PaintDisplayMode = Leadtools.RasterPaintDisplayModeFlags.Resample;
                leadImg.PaintProperties = prop;

                leadImg.Image = cs.Load(tempImgName, 0, Leadtools.Codecs.CodecsLoadByteOrder.BgrOrGray, 1, 1);

                //画像表示倍率設定
                if (global.miMdlZoomRate == 0f)
                {
                    leadImg.ScaleFactor *= global.ZOOM_RATE;
                }
                else
                {
                    leadImg.ScaleFactor *= global.miMdlZoomRate;
                }

                //画像のマウスによる移動を可能とする
                leadImg.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.Pan;

                // グレースケールに変換
                Leadtools.ImageProcessing.GrayscaleCommand grayScaleCommand = new Leadtools.ImageProcessing.GrayscaleCommand();
                grayScaleCommand.BitsPerPixel = 8;
                grayScaleCommand.Run(leadImg.Image);
                leadImg.Refresh();

                cs.Dispose();
                Leadtools.Codecs.RasterCodecs.Shutdown();
                //global.pblImagePath = tempImgName;
            }
            else
            {
                //画像ファイルがないとき
                lblNoImage.Visible = true;

                // 画像操作ボタン
                btnPlus.Enabled = false;
                btnMinus.Enabled = false;

                leadImg.Visible = false;
                //global.pblImagePath = string.Empty;
            }
        }

        private void leadImg_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void leadImg_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        /// -------------------------------------------------------------------------
        /// <summary>
        ///     基準年月以前の勤務チェックリストデータを削除します</summary>
        /// <param name="sYYMM">
        ///     基準年月</param>
        /// -------------------------------------------------------------------------
        private void deleteCheckArchived(int sYYMM)
        {
            // 勤怠チェックリストデータセットを読み込む
            MTYSDataSetTableAdapters.勤怠チェックリストTableAdapter adp = new MTYSDataSetTableAdapters.勤怠チェックリストTableAdapter();
            adp.Fill(dts.勤怠チェックリスト);
            
            // 基準年月以前の勤務チェックリストデータを取得します
            var h = dts.勤怠チェックリスト
                    .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                a.年 * 100 + a.月 <= sYYMM);

            // foreach用の配列を作成
            //var hLst = h.ToList();

            // データを削除します
            foreach (var lh in h)
            {
                lh.Delete();
            }

            // データベース更新
            adp.Update(dts.勤怠チェックリスト);
        }
        
        /// -------------------------------------------------------------------------
        /// <summary>
        ///     基準年月以前の過去勤務票ヘッダデータとその明細データを削除します</summary>
        /// <param name="sYYMM">
        ///     基準年月</param>     
        /// -------------------------------------------------------------------------
        private void deleteLastDataArchived(int sYYMM)
        {
            // 基準年月以前の過去勤務票ヘッダデータを取得します
            var h = dts.過去勤務票ヘッダ
                    .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                a.年 * 100 + a.月 <= sYYMM);

            // foreach用の配列を作成
            var hLst = h.ToList();

            foreach (var lh in hLst)
            {
                // ヘッダIDが一致する過去勤務票明細を取得します
                var m = dts.過去勤務票明細
                    .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                a.ヘッダID == lh.ID);

                // foreach用の配列を作成
                var list = m.ToList();

                // 該当過去勤務票明細を削除します
                foreach (var lm in list)
                {
                    MTYSDataSet.過去勤務票明細Row lRow = (MTYSDataSet.過去勤務票明細Row)dts.過去勤務票明細.Rows.Find(lm.ID);
                    lRow.Delete();
                }

                // 過去勤務票ヘッダを削除します
                lh.Delete();
            }
            
            // データベース更新
            adpMn.UpdateAll(dts);
        }
        
        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     設定月数分経過した過去画像を削除する</summary>
        /// <param name="_dYYMM">
        ///     基準年月 (例：201401)</param>
        /// -----------------------------------------------------------------------------
        private void deleteImageArchived(int _dYYMM)
        {
            int _DataYYMM;
            string fileYYMM;

            // 設定月数分経過した過去画像を削除する            
            foreach (string files in System.IO.Directory.GetFiles(Properties.Settings.Default.tifPath, "*.tif"))
            {
                // ファイル名が規定外のファイルは読み飛ばします
                if (System.IO.Path.GetFileName(files).Length < 20) continue;

                //ファイル名より年月を取得する
                fileYYMM = System.IO.Path.GetFileName(files).Substring(5, 6);

                if (Utility.NumericCheck(fileYYMM))
                {
                    _DataYYMM = int.Parse(fileYYMM);

                    //基準年月以前なら削除する
                    if (_DataYYMM <= _dYYMM) File.Delete(files);
                }
            }
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     勤務票ヘッダデータと勤務票明細データを全件削除します</summary>
        /// -------------------------------------------------------------------
        private void deleteDataAll()
        {
            Cursor = Cursors.WaitCursor;

            // 勤務票明細全行削除
            var m = dts.勤務票明細.Where(a => a.RowState != DataRowState.Deleted);
            foreach (var t in m)
            {
                t.Delete();
            }

            // 勤務票ヘッダ全行削除
            var h = dts.勤務票ヘッダ.Where(a => a.RowState != DataRowState.Deleted);
            foreach (var t in h)
            {
                t.Delete();
            }

            // データベース更新
            adpMn.UpdateAll(dts);

            // 後片付け
            dts.勤務票明細.Dispose();
            dts.勤務票ヘッダ.Dispose();

            Cursor = Cursors.Default;

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
