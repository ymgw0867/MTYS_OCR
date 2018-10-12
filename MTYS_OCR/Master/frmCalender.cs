using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTYS_OCR.Common;

namespace MTYS_OCR.Master
{
    public partial class frmCalender : Form
    {
        // マスター名
        string msName = "休日カレンダー";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.休日TableAdapter adp = new MTYSDataSetTableAdapters.休日TableAdapter();

        // 初期日付1日
        DateTime DtF = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        // 月末日
        DateTime DtL = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
        
        // 指定日付
        DateTime selDT = DateTime.Today; 

        public frmCalender()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.休日TableAdapter = adp;
            adpMn.休日TableAdapter.Fill(dts.休日);
        }

        private void frmCalender_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最少サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // 初期画面は休日表示
            radioButton1.Checked = true;

            // データグリッド定義
            GridViewSetting(dg);

            // データグリッドビューにデータを表示します
            GridViewShow(dg, DtF, DtL);

            // データ画面初期化
            DispInitial();
        }

        // データグリッドビューカラム定義
        string colDay = "col1";
        string colWeek = "col2";
        string colTekiyo = "col3";
        string colHonsha = "colHonsha";
        string colShizuoka = "colShizuoka";
        string colOosaka = "colOosaka";
        string colOosakaA = "colOosakaA";
        string colOosakaB = "colOosakaB";
        string colOosakaC = "colOosakaC";
        string colOosakaD = "colOosakaD";

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        /// <param name="tempDGV">データグリッドビューオブジェクト</param>
        private void GridViewSetting(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更する

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", 9, FontStyle.Regular);

                // カラム定義
                tempDGV.Columns.Add(colDay, "日");
                tempDGV.Columns.Add(colWeek, "曜");
                tempDGV.Columns.Add(colHonsha, "本社");
                tempDGV.Columns.Add(colShizuoka, "静岡");
                tempDGV.Columns.Add(colOosaka, "大製");
                tempDGV.Columns.Add(colOosakaA, "大Ａ");
                tempDGV.Columns.Add(colOosakaB, "大Ｂ");
                tempDGV.Columns.Add(colOosakaC, "大Ｃ");
                tempDGV.Columns.Add(colOosakaD, "大Ｄ");
                tempDGV.Columns.Add(colTekiyo, "摘要");

                // 列幅調整
                tempDGV.Columns[colDay].Width = 30;        // 日
                tempDGV.Columns[colWeek].Width = 30;       // 曜日
                tempDGV.Columns[colHonsha].Width = 38;     // 本社
                tempDGV.Columns[colShizuoka].Width = 38;   // 静岡
                tempDGV.Columns[colOosaka].Width = 38;     // 大阪製造部
                tempDGV.Columns[colOosakaA].Width = 38;    // 大阪製造Ｇ・Ａ班
                tempDGV.Columns[colOosakaB].Width = 38;    // 大阪製造Ｇ・Ｂ班
                tempDGV.Columns[colOosakaC].Width = 38;    // 大阪製造Ｇ・Ｃ班
                tempDGV.Columns[colOosakaD].Width = 38;    // 大阪製造Ｇ・Ｄ班
                tempDGV.Columns[colTekiyo].Width = 160;    // 摘要

                tempDGV.Columns[colTekiyo].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempDGV.Columns[colDay].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                tempDGV.Columns[colWeek].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                //tempDGV.Height = 341;

                // 奇数行の色
                //tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tempDGV.MultiSelect = false;

                // 編集不可とする
                tempDGV.ReadOnly = true;

                // 追加行表示しない
                tempDGV.AllowUserToAddRows = false;

                // データグリッドビューから行削除を禁止する
                tempDGV.AllowUserToDeleteRows = false;

                // 手動による列移動の禁止
                tempDGV.AllowUserToOrderColumns = false;

                // 列サイズ変更可
                tempDGV.AllowUserToResizeColumns = true;

                // 行サイズ変更禁止
                tempDGV.AllowUserToResizeRows = false;

                // 行ヘッダーの自動調節
                //tempDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                //TAB動作
                tempDGV.StandardTab = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// データグリッドビューにデータを表示します
        /// </summary>
        /// <param name="tempGrid">データグリッドビューオブジェクト名</param>
        private void GridViewShow(DataGridView tempGrid, DateTime dtF, DateTime dtL)
        {
            try
            {
                // 対象年月
                label1.Text = dtF.Year.ToString() + "（" + Properties.Settings.Default.gengou + (dtF.Year - Properties.Settings.Default.RekiHosei).ToString() + "）年 " + dtF.Month + "月";

                // 休日表示
                tempGrid.RowCount = 0;
                tempGrid.RowCount = DtL.Day;

                for (DateTime dt = DtF; dt <= DtL; dt = dt.AddDays(1))
                {
                    tempGrid[colDay, dt.Day - 1].Value = dt.Day.ToString();
                    tempGrid[colWeek, dt.Day - 1].Value = dt.ToString("ddd");

                    // 土日ならば背景色表示
                    if (tempGrid[colWeek, dt.Day - 1].Value.ToString() == "土" || tempGrid[colWeek, dt.Day - 1].Value.ToString() == "日")
                    {
                        tempGrid[colDay, dt.Day - 1].Style.BackColor = Color.MistyRose;
                        tempGrid[colWeek, dt.Day - 1].Style.BackColor = Color.MistyRose;
                    }

                    // 休日マスター読み込み
                    var s = dts.休日.Where(a => a.RowState != DataRowState.Deleted && 
                                                a.RowState != DataRowState.Detached && a.年月日 == dt);
                    if (s.Count() != 0)
                    {
                        foreach (var t in s)
                        {
                            //tempGrid[colTekiyo, dt.Day - 1].Style.BackColor = Color.MistyRose;
                            tempGrid[colTekiyo, dt.Day - 1].Value = t.摘要;

                            // 休日が設定されているか調べる
                            if (radioButton1.Checked)
                            {
                                if (t.本社使用 == global.flgOn) tempGrid[colHonsha, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.静岡使用 == global.flgOn) tempGrid[colShizuoka, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.大阪製造部使用 == global.flgOn) tempGrid[colOosaka, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.大阪製造部A使用 == global.flgOn) tempGrid[colOosakaA, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.大阪製造部B使用 == global.flgOn) tempGrid[colOosakaB, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.大阪製造部C使用 == global.flgOn) tempGrid[colOosakaC, dt.Day - 1].Style.BackColor = Color.MistyRose;
                                if (t.大阪製造部D使用 == global.flgOn) tempGrid[colOosakaD, dt.Day - 1].Style.BackColor = Color.MistyRose;
                            }

                            // 網掛け設定がされているか調べる
                            if (radioButton2.Checked)
                            {
                                if (t.本社網掛け == global.flgOn) tempGrid[colHonsha, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.静岡網掛け == global.flgOn) tempGrid[colShizuoka, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.大阪製造部網掛け == global.flgOn) tempGrid[colOosaka, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.大阪A網掛け == global.flgOn) tempGrid[colOosakaA, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.大阪B網掛け == global.flgOn) tempGrid[colOosakaB, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.大阪C網掛け == global.flgOn) tempGrid[colOosakaC, dt.Day - 1].Style.BackColor = Color.LightGray;
                                if (t.大阪D網掛け == global.flgOn) tempGrid[colOosakaD, dt.Day - 1].Style.BackColor = Color.LightGray;
                            }

                        }
                    }

                    tempGrid.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            // 現在の年月1日の前日を前月月末日に設定
            DtL = DtF.AddDays(-1);

            // 前月1日を設定
            DtF = new DateTime(DtL.Year, DtL.Month, 1);

            // カレンダー表示
            GridViewShow(dg, DtF, DtL);

            // データ表示初期化
            DispInitial();
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            // 現在の月末日の翌日を翌月1日に設定
            DtF = DtL.AddDays(1);

            // 翌月末日を設定
            DtL = new DateTime(DtF.Year, DtF.Month, DateTime.DaysInMonth(DtF.Year,DtF.Month));

            // カレンダー表示
            GridViewShow(dg, DtF, DtL);

            // データ表示初期化
            DispInitial();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (dg.RowCount == 0) return;

            // データ表示
            GridViewShow(dg, DtF, DtL);
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // データ画面初期化
                DispInitial();

                // 対象行の日付を取得
                selDT = DateTime.Parse(DtF.Year.ToString() + "/" + DtF.Month.ToString() + "/" + dg[colDay, dg.SelectedRows[0].Index].Value.ToString());

                // 日付表示
                txtDt.Text = selDT.ToLongDateString() + selDT.ToString(" (" + "ddd" + ")");

                // データ表示
                GridEnter(selDT);
            }

        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            fMode.Mode = global.FORM_ADDMODE;

            txtDt.Text = string.Empty;

            cmbTekiyouLoad();
            cmbTekiyou.Text = string.Empty;
            cmbTekiyou.SelectedIndex = -1;

            chkHon.CheckState = CheckState.Unchecked;
            chkShiz.CheckState = CheckState.Unchecked;
            chkOsk.CheckState = CheckState.Unchecked;
            chkOskA.CheckState = CheckState.Unchecked;
            chkOskB.CheckState = CheckState.Unchecked;
            chkOskC.CheckState = CheckState.Unchecked;
            chkOskD.CheckState = CheckState.Unchecked;

            chkHon.ForeColor = Color.Gray;
            chkShiz.ForeColor = Color.Gray;
            chkOsk.ForeColor = Color.Gray;
            chkOskA.ForeColor = Color.Gray;
            chkOskB.ForeColor = Color.Gray;
            chkOskC.ForeColor = Color.Gray;
            chkOskD.ForeColor = Color.Gray;

            chkAmiH.CheckState = CheckState.Unchecked;
            chkAmiS.CheckState = CheckState.Unchecked;
            chkAmiO.CheckState = CheckState.Unchecked;
            chkAmiA.CheckState = CheckState.Unchecked;
            chkAmiB.CheckState = CheckState.Unchecked;
            chkAmiC.CheckState = CheckState.Unchecked;
            chkAmiD.CheckState = CheckState.Unchecked;

            chkAmiH.ForeColor = Color.Gray;
            chkAmiS.ForeColor = Color.Gray;
            chkAmiO.ForeColor = Color.Gray;
            chkAmiA.ForeColor = Color.Gray;
            chkAmiB.ForeColor = Color.Gray;
            chkAmiC.ForeColor = Color.Gray;
            chkAmiD.ForeColor = Color.Gray;

            txtMemo.Text = string.Empty;

            btnUpdate.Enabled = false;
            btnDel.Enabled = false;
            btnClear.Enabled = false;

            label3.Text = string.Empty;
        }

        /// ---------------------------------------------------------------
        /// <summary>
        ///     休日データ表示処理 </summary>
        /// <param name="dt">
        ///     年月日</param>
        /// ---------------------------------------------------------------
        private void GridEnter(DateTime dt)
        {
            // 対象となる休日データを取得します
            var s = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                        a.年月日 == dt);

            // 登録済みのデータ
            if (s.Count() != 0)
            {
                label3.Text = "※以下の内容で登録済みです";

                // 編集画面に表示
                foreach (var t in s)
                {
                    chkHon.Checked = Utility.intToCheck(t.本社使用);
                    chkShiz.Checked = Utility.intToCheck(t.静岡使用);
                    chkOsk.Checked = Utility.intToCheck(t.大阪製造部使用);
                    chkOskA.Checked = Utility.intToCheck(t.大阪製造部A使用);
                    chkOskB.Checked = Utility.intToCheck(t.大阪製造部B使用);
                    chkOskC.Checked = Utility.intToCheck(t.大阪製造部C使用);
                    chkOskD.Checked = Utility.intToCheck(t.大阪製造部D使用);

                    chkAmiH.Checked = Utility.intToCheck(t.本社網掛け);
                    chkAmiS.Checked = Utility.intToCheck(t.静岡網掛け);
                    chkAmiO.Checked = Utility.intToCheck(t.大阪製造部網掛け);
                    chkAmiA.Checked = Utility.intToCheck(t.大阪A網掛け);
                    chkAmiB.Checked = Utility.intToCheck(t.大阪B網掛け);
                    chkAmiC.Checked = Utility.intToCheck(t.大阪C網掛け);
                    chkAmiD.Checked = Utility.intToCheck(t.大阪D網掛け);

                    cmbTekiyou.Text = t.摘要;
                    txtMemo.Text = t.備考;

                    btnUpdate.Enabled = true;
                    btnDel.Enabled = true;
                    btnClear.Enabled = true;
                }

                // モードステータスを「編集モード」にします
                fMode.Mode = global.FORM_EDITMODE;
            }
            else
            {
                label3.Text = string.Empty; ;
                btnUpdate.Enabled = true;
            }
        }
        
        // 摘要コンボボックス値ロード
        private void cmbTekiyouLoad()
        {
            cmbTekiyou.DisplayMember = "name";
            var s = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached)
                .Select(a => new
                {
                    name = a.摘要
                }).Distinct();

            cmbTekiyou.DataSource = s.ToList();
        }

        private void chkHon_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                chk.ForeColor = Color.Black;
            }
            else
            {
                chk.ForeColor = Color.Gray;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //エラーチェック
            if (!fDataCheck()) return;

            switch (fMode.Mode)
            {
                // 新規登録
                case global.FORM_ADDMODE:

                    // 確認
                    if (MessageBox.Show(txtDt.Text + "を休日登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    // データセットにデータを追加します
                    dts.休日.Add休日Row(selDT, Utility.checkToInt(chkHon), Utility.checkToInt(chkShiz),
                                       Utility.checkToInt(chkOsk), Utility.checkToInt(chkOskA), Utility.checkToInt(chkOskB),
                                       Utility.checkToInt(chkOskC), Utility.checkToInt(chkOskD), Utility.checkToInt(chkAmiH),
                                       Utility.checkToInt(chkAmiS), Utility.checkToInt(chkAmiO), Utility.checkToInt(chkAmiA),
                                       Utility.checkToInt(chkAmiB), Utility.checkToInt(chkAmiC), Utility.checkToInt(chkAmiD),
                                       cmbTekiyou.Text, txtMemo.Text, DateTime.Now);

                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtDt.Text + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    // データセット更新
                    var r = dts.休日.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                               a.年月日 == selDT);

                    if (!r.HasErrors)
                    {
                        r.本社使用 = Utility.checkToInt(chkHon);
                        r.静岡使用 = Utility.checkToInt(chkShiz);
                        r.大阪製造部使用 = Utility.checkToInt(chkOsk);
                        r.大阪製造部A使用 = Utility.checkToInt(chkOskA);
                        r.大阪製造部B使用 = Utility.checkToInt(chkOskB);
                        r.大阪製造部C使用 = Utility.checkToInt(chkOskC);
                        r.大阪製造部D使用 = Utility.checkToInt(chkOskD);

                        r.本社網掛け = Utility.checkToInt(chkAmiH);
                        r.静岡網掛け = Utility.checkToInt(chkAmiS);
                        r.大阪製造部網掛け = Utility.checkToInt(chkAmiO);
                        r.大阪A網掛け = Utility.checkToInt(chkAmiA);
                        r.大阪B網掛け = Utility.checkToInt(chkAmiB);
                        r.大阪C網掛け = Utility.checkToInt(chkAmiC);
                        r.大阪D網掛け = Utility.checkToInt(chkAmiD);

                        r.摘要 = cmbTekiyou.Text;
                        r.備考 = txtMemo.Text;
                        r.更新年月日 = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show("データの更新に失敗しました", "更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;

                default:
                    break;
            }

            // データグリッドビューにデータを表示します
            GridViewShow(dg, DtF, DtL);

            // 画面データ消去
            DispInitial();      
        }
    
        //登録データチェック
        private Boolean fDataCheck()
        {
            try
            {
                //　登録モードのとき番号をチェック
                if (fMode.Mode == global.FORM_ADDMODE)
                {
                    //登録済みコードか調べる
                    var s = dts.休日.Where(a => a.年月日 == selDT);
                    if (s.Count() > 0)
                    {
                        throw new Exception("この日付は既に登録済みです");
                    }
                }
                
                // 休日、網掛け共にチェックがないとき
                if (!chkHon.Checked && !chkShiz.Checked && !chkOsk.Checked && !chkOskA.Checked && !chkOskB.Checked &&
                    !chkOskC.Checked && !chkOskD.Checked &&
                    !chkAmiH.Checked && !chkAmiS.Checked && !chkAmiO.Checked && !chkAmiA.Checked &&
                    !chkAmiB.Checked && !chkAmiC.Checked && !chkAmiD.Checked)
                {
                    throw new Exception("休日の事業所、網掛け設定にチェックがありません");
                }

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, msName + "保守", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じます
            this.Close();
        }

        private void frmCalender_FormClosing(object sender, FormClosingEventArgs e)
        {
            // データセットの内容をデータベースへ反映させます。
            adpMn.UpdateAll(dts);

            this.Dispose();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 確認
                if (MessageBox.Show("削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                // 削除データ取得（エラー回避のためDataRowState.Deleted と DataRowState.Detachedは除外して抽出する）
                var d = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.年月日 == selDT);

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.休日Row dl = (MTYSDataSet.休日Row)dts.休日.Rows.Find(it.ID);
                    dl.Delete();
                }

                // データベースコミット
                adpMn.UpdateAll(dts);
                adpMn.休日TableAdapter.Fill(dts.休日);
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの削除に失敗しました" + Environment.NewLine + ex.Message);
            }
            finally
            {
                // データグリッドビューにデータを表示します
                GridViewShow(dg, DtF, DtL);

                // 画面データ消去
                DispInitial();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // 画面データ消去
            DispInitial();

            dg.CurrentCell = null;
        }
    }
}
