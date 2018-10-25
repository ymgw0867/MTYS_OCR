using System;
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
    public partial class frmPastData : Form
    {
        /// ------------------------------------------------------------
        /// <summary>
        ///     コンストラクタ </summary>
        /// <param name="sID">
        ///     処理モード</param>
        /// ------------------------------------------------------------
        public frmPastData(int sID, int syy, int sMM)
        {
            InitializeComponent();

            dID = sID;              // 処理モード
            _sYY = syy;
            _sMM = sMM;

            // 画像パス取得
            global.pblImagePath = Properties.Settings.Default.tifPath;

        }

        // データアダプターオブジェクト
        MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter adp = new MTYSDataSetTableAdapters.過去勤務票ヘッダTableAdapter();            
        MTYSDataSetTableAdapters.過去勤務票明細TableAdapter mAdp = new MTYSDataSetTableAdapters.過去勤務票明細TableAdapter();
        MTYSDataSetTableAdapters.社員所属TableAdapter sAdp = new MTYSDataSetTableAdapters.社員所属TableAdapter();
        MTYSDataSetTableAdapters.休日TableAdapter yAdp = new MTYSDataSetTableAdapters.休日TableAdapter();
        //MTYSDataSetTableAdapters.勤怠記号TableAdapter kAdp = new MTYSDataSetTableAdapters.勤怠記号TableAdapter();
        //MTYSDataSetTableAdapters.出勤形態TableAdapter eAdp = new MTYSDataSetTableAdapters.出勤形態TableAdapter();
        //MTYSDataSetTableAdapters.出勤日数TableAdapter nAdp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();

        // データセットオブジェクト
        MTYSDataSet dts = new MTYSDataSet();

        // カレント社員情報
        MTYSDataSet.社員所属Row cSR = null;
        
        /// <summary>
        ///     カレントデータRowsインデックス</summary>
        int cI = 0;

        // 社員マスターより取得した所属コード
        string mSzCode = string.Empty;

        #region 終了ステータス定数
        const string END_BUTTON = "btn";
        const string END_MAKEDATA = "data";
        const string END_CONTOROL = "close";
        const string END_NODATA = "non Data";
        #endregion
        
        int dID = 0;                                // 表示する過去データのID
        int _sYY = 0;                               // 指定年
        int _sMM = 0;                               // 指定月
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
            
            // キャプション
            this.Text = "過去勤務報告書データ表示";

            // 参照テーブルデータセット読込み
            //adp.Fill(dts.過去勤務票ヘッダ);       // 2018/10/25 コメント化
            //mAdp.Fill(dts.過去勤務票明細);       // 2018/10/25 コメント化

            sAdp.Fill(dts.社員所属);
            yAdp.Fill(dts.休日);

            //kAdp.Fill(dts.勤怠記号);
            //eAdp.Fill(dts.出勤形態);
            //nAdp.Fill(dts.出勤日数);

            // グリッドビュー定義
            GridviewSet gs = new GridviewSet();
            gs.Setting_Shain(dGV);

            // 編集作業、過去データ表示の判断
            // 渡されたヘッダIDの過去データを表示します
            showPastData(dID, _sYY, _sMM);

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

                        //// 編集可否
                        //if (c.Index < 2 || c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE ||
                        //    c.Name == cSIE)
                        //    c.ReadOnly = true;
                        //else c.ReadOnly = false;

                        // 区切り文字
                        if (c.Name == cSE || c.Name == cEE || c.Name == cZE || c.Name == cKSE || c.Name == cSIE)
                            c.DefaultCellStyle.Font = new Font("ＭＳＰゴシック", 8, FontStyle.Regular);

                        // 入力可能桁数
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)c;
                        if (c.Name == cKintai1 || c.Name == cKintai2 || c.Name == cZH || c.Name == cSKEITAI) 
                            col.MaxInputLength = 1;
                        else col.MaxInputLength = 2;

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
                gv.ReadOnly = true;

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
            btnRtn.Focus();
        }

        //private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (e.Control is DataGridViewTextBoxEditingControl)
        //    {
        //        //イベントハンドラが複数回追加されてしまうので最初に削除する
        //        e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
        //        //イベントハンドラを追加する
        //        e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        //    }
        //}

        //private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (e.Control is DataGridViewTextBoxEditingControl)
        //    {
        //        //イベントハンドラが複数回追加されてしまうので最初に削除する
        //        e.Control.KeyPress -= new KeyPressEventHandler(Control_KeyPress);
        //        //イベントハンドラを追加する
        //        e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
        //    }
        //}

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

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じる
            this.Tag = END_BUTTON;
            this.Close();
        }

        private void frmCorrect_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 解放する
            this.Dispose();
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
    }
}
