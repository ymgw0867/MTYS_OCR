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
    public partial class frmShukinDays : Form
    {
        // マスター名
        string msName = "出勤日数";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 出勤日数マスターテーブルアダプター生成
        //MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 出勤日数マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.出勤日数TableAdapter adp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();

        public frmShukinDays()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adp.Fill(dts.出勤日数);

            // 画面初期化
            DispInitial();
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            fMode.Mode = global.FORM_ADDMODE;
            txtYear.Text = string.Empty;
            txtMonth.Text = string.Empty;
            txtHSi.Text = string.Empty;
            txtHSc.Text = string.Empty;
            txtOi.Text = string.Empty;
            txtOc.Text = string.Empty;
            txtOAi.Text = string.Empty;
            txtOAc.Text = string.Empty;
            txtOBi.Text = string.Empty;
            txtOBc.Text = string.Empty;
            txtOCi.Text = string.Empty;
            txtOCc.Text = string.Empty;
            txtODi.Text = string.Empty;
            txtODc.Text = string.Empty;
            txtMemo.Text = string.Empty;

            btnUpdate.Enabled = true;
            btnDel.Enabled = false;
            btnClear.Enabled = false;
            txtYear.Enabled = true;
            txtMonth.Enabled = true;
            txtYear.Focus();
        }

        private void frmShukinDays_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // データグリッド定義
            GridViewSetting(dg);

            // データグリッドビューにデータを表示します
            GridViewShow(dg);
        }
        
        // データグリッドビューカラム定義
        string colYear = "coly";
        string colMonth = "colm";
        string colHSi = "colHSi";
        string colHSc = "colHSc";
        string colOAi = "colOAi";
        string colOAc = "colOAc";
        string colOBi = "colOBi";
        string colOBc = "colOBc";
        string colOCi = "colOCi";
        string colOCc = "colOCc";
        string colODi = "colODi";
        string colODc = "colODc";
        string colMemo = "colMemo";

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

                //// カラム定義
                //tempDGV.Columns.Add(colYear, "年");
                //tempDGV.Columns.Add(colMonth, "月");
                //tempDGV.Columns.Add(colHSi, "本社・静岡印字");
                //tempDGV.Columns.Add(colHSc, "本社・静岡計算");
                //tempDGV.Columns.Add(colOAi, "大阪Ａ印字");
                //tempDGV.Columns.Add(colOAc, "大阪Ａ計算");
                //tempDGV.Columns.Add(colOBi, "大阪Ｂ印字");
                //tempDGV.Columns.Add(colOBc, "大阪Ｂ計算");
                //tempDGV.Columns.Add(colOCi, "大阪Ｃ印字");
                //tempDGV.Columns.Add(colOCc, "大阪Ｃ計算");
                //tempDGV.Columns.Add(colODi, "大阪Ｄ印字");
                //tempDGV.Columns.Add(colODc, "大阪Ｄ計算");
                //tempDGV.Columns.Add(colMemo, "備考");

                //// 列幅調整
                //tempDGV.Columns[colYear].Width = 60;    // 年
                //tempDGV.Columns[colMonth].Width = 60;   // 月
                //tempDGV.Columns[colHSi].Width = 80;     // 本社静岡印字用
                //tempDGV.Columns[colHSc].Width = 80;     // 本社静岡計算用
                //tempDGV.Columns[colOAi].Width = 80;     // 大阪製造部Ａ印字用
                //tempDGV.Columns[colOAc].Width = 80;     // 大阪製造部Ａ計算用
                //tempDGV.Columns[colOBi].Width = 80;     // 大阪製造部Ｂ印字用
                //tempDGV.Columns[colOBc].Width = 80;     // 大阪製造部Ｂ計算用
                //tempDGV.Columns[colOCi].Width = 80;     // 大阪製造部Ｃ印字用
                //tempDGV.Columns[colOCc].Width = 80;     // 大阪製造部Ｃ計算用
                //tempDGV.Columns[colODi].Width = 80;     // 大阪製造部Ｄ印字用
                //tempDGV.Columns[colODc].Width = 80;     // 大阪製造部Ｄ計算用
                //tempDGV.Columns[colMemo].Width = 120;   // 備考

                //tempDGV.Columns[colMemo].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //tempDGV.Columns[colYear].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                //tempDGV.Columns[colMonth].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                //tempDGV.Height = 341;

                // 奇数行の色
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

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
        private void GridViewShow(DataGridView dg)
        {
            try
            {
                dg.DataSource = dts.出勤日数;

                //adp.Fill(dts.出勤日数);
                ////var dt = dts.出勤日数.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached);
                //var s = dts.出勤日数.OrderByDescending(a => a.年 * 100 + a.月);
                //dg.DataSource = s.ToList();

                dg.CurrentCell = null;

                // 列幅調整                
                dg.Columns[0].Visible = false;
                dg.Columns[1].Width = 60;       // 年
                dg.Columns[2].Width = 40;       // 月
                dg.Columns[3].Width = 80;       // 本社静岡印字用
                dg.Columns[4].Width = 80;       // 本社静岡計算用
                dg.Columns[5].Width = 80;       // 大阪製造部印字用
                dg.Columns[6].Width = 80;       // 大阪製造部計算用
                dg.Columns[7].Width = 80;       // 大阪製造部Ａ印字用
                dg.Columns[8].Width = 80;       // 大阪製造部Ａ計算用
                dg.Columns[9].Width = 80;       // 大阪製造部Ｂ印字用
                dg.Columns[10].Width = 80;      // 大阪製造部Ｂ計算用
                dg.Columns[11].Width = 80;      // 大阪製造部Ｃ印字用
                dg.Columns[12].Width = 80;      // 大阪製造部Ｃ計算用
                dg.Columns[13].Width = 80;      // 大阪製造部Ｄ印字用
                dg.Columns[14].Width = 80;      // 大阪製造部Ｄ計算用
                dg.Columns[15].Width = 120;     // 備考

                //dg.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //dg.Columns[16].Visible = false;
                //dg.Columns[17].Visible = false;
                //dg.Columns[18].Visible = false;
                //dg.Columns[19].Visible = false;
                //dg.Columns[20].Visible = false;

                for (int i = 1; i < 15; i++)
                {
                    dg.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// グリッドビュー行選択時処理
        /// </summary>
        private void GridEnter()
        {
            string msgStr;
            fMode.rowIndex = dg.SelectedRows[0].Index;

            // 選択確認
            msgStr = "";
            msgStr += dg[1, fMode.rowIndex].Value.ToString() + "年" + dg[2, fMode.rowIndex].Value.ToString() + "月が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            // 対象となるデータテーブルROWを取得します
            MTYSDataSet.出勤日数Row sQuery = dts.出勤日数.FindByID(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

            if (sQuery != null)
            {
                // 編集画面に表示
                ShowData(sQuery);

                // モードステータスを「編集モード」にします
                fMode.Mode = global.FORM_EDITMODE;
            }
            else
            {
                MessageBox.Show(dg[0, fMode.rowIndex].Value.ToString() + "がキー不在です：データの読み込みに失敗しました", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// -------------------------------------------------------
        /// <summary>
        ///     マスターの内容を画面に表示する </summary>
        /// <param name="sTemp">
        ///     マスターインスタンス</param>
        /// -------------------------------------------------------
        private void ShowData(MTYSDataSet.出勤日数Row s)
        {
            fMode.ID = s.ID.ToString();
            txtYear.Text = s.年.ToString();
            txtYear.Enabled = false;
            txtMonth.Text = s.月.ToString();
            txtMonth.Enabled = false;

            txtHSi.Text = s.本社静岡印字用.ToString();
            txtHSc.Text = s.本社静岡計算用.ToString();
            txtOi.Text = s.大阪印字用.ToString();
            txtOc.Text = s.大阪計算用.ToString();
            txtOAi.Text = s.大阪A印字用.ToString();
            txtOAc.Text = s.大阪A計算用.ToString();
            txtOBi.Text = s.大阪B印字用.ToString();
            txtOBc.Text = s.大阪B計算用.ToString();
            txtOCi.Text = s.大阪C印字用.ToString();
            txtOCc.Text = s.大阪C計算用.ToString();
            txtODi.Text = s.大阪D印字用.ToString();
            txtODc.Text = s.大阪D計算用.ToString();
            txtMemo.Text = s.備考;

            btnDel.Enabled = true;
            btnClear.Enabled = true;
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
                    if (MessageBox.Show(txtYear.Text + "年" + txtMonth.Text + "月を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    // データセットにデータを追加します
                    dts.出勤日数.Add出勤日数Row(Utility.StrtoInt(txtYear.Text), Utility.StrtoInt(txtMonth.Text),
                        Utility.StrtoInt(txtHSi.Text), Utility.StrtoInt(txtHSc.Text), Utility.StrtoInt(txtOi.Text), Utility.StrtoInt(txtOc.Text), 
                        Utility.StrtoInt(txtOAi.Text),Utility.StrtoInt(txtOAc.Text), Utility.StrtoInt(txtOBi.Text), Utility.StrtoInt(txtOBc.Text), 
                        Utility.StrtoInt(txtOCi.Text), Utility.StrtoInt(txtOCc.Text), Utility.StrtoInt(txtODi.Text), 
                        Utility.StrtoInt(txtODc.Text), txtMemo.Text, DateTime.Now);

                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtYear.Text + "年" + txtMonth.Text + "月を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;

                    // データセット更新
                    var r = dts.出勤日数.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.ID == Utility.StrtoInt(fMode.ID));

                    if (!r.HasErrors)
                    {
                        r.年 = int.Parse(txtYear.Text);
                        r.月 = int.Parse(txtMonth.Text);
                        r.本社静岡印字用 = Utility.StrtoInt(txtHSi.Text);
                        r.本社静岡計算用 = Utility.StrtoInt(txtHSc.Text);
                        r.大阪印字用 = Utility.StrtoInt(txtOi.Text);
                        r.大阪計算用 = Utility.StrtoInt(txtOc.Text);
                        r.大阪A印字用 = Utility.StrtoInt(txtOAi.Text);
                        r.大阪A計算用 = Utility.StrtoInt(txtOAc.Text);
                        r.大阪B印字用 = Utility.StrtoInt(txtOBi.Text);
                        r.大阪B計算用 = Utility.StrtoInt(txtOBc.Text);
                        r.大阪C印字用 = Utility.StrtoInt(txtOCi.Text);
                        r.大阪C計算用 = Utility.StrtoInt(txtOCc.Text);
                        r.大阪D印字用 = Utility.StrtoInt(txtODi.Text);
                        r.大阪D計算用 = Utility.StrtoInt(txtODc.Text);
                        r.備考 = txtMemo.Text;
                        r.更新年月日 = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show(fMode.ID + "がキー不在です：データの更新に失敗しました", "更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;

                default:
                    break;
            }

            // グリッドデータ再表示
            //GridViewShow(dg);

            // 画面データ消去
            DispInitial();      
        }

        //登録データチェック
        private Boolean fDataCheck()
        {
            try
            {
                //登録モードのとき番号をチェック
                if (fMode.Mode == global.FORM_ADDMODE)
                {
                    //登録済み年月か調べる
                    var s = dts.出勤日数.Where(a => 
                        a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && 
                        a.年 == Utility.StrtoInt(txtYear.Text) && a.月 == Utility.StrtoInt(txtMonth.Text));

                    if (s.Count() > 0)
                    {
                        txtYear.Focus();
                        throw new Exception(txtYear.Text + "年" + txtMonth.Text + "月は既に登録済みです");
                    }
                }

                // 年チェック
                if (txtYear.Text.Trim().Length < 1)
                {
                    txtYear.Focus();
                    throw new Exception("年を入力してください");
                }

                if (int.Parse(txtYear.Text) < 2014)
                {
                    txtYear.Focus();
                    throw new Exception("2014年以降で入力してください");
                }

                if (txtMonth.Text.Trim().Length < 1)
                {
                    txtMonth.Focus();
                    throw new Exception("月を入力してください");
                }

                if (Utility.StrtoInt(txtMonth.Text) < 1 || Utility.StrtoInt(txtMonth.Text) > 12)
                {
                    txtMonth.Focus();
                    throw new Exception("月を正しく入力してください");
                }

                // 日数が1以上で月日数の範囲内かチェック
                if (!NisuuCheck(txtHSi)) return false;
                if (!NisuuCheck(txtHSc)) return false;
                if (!NisuuCheck(txtOi)) return false;
                if (!NisuuCheck(txtOc)) return false;
                if (!NisuuCheck(txtOAi)) return false;
                if (!NisuuCheck(txtOAc)) return false;
                if (!NisuuCheck(txtOBi)) return false;
                if (!NisuuCheck(txtOBc)) return false;
                if (!NisuuCheck(txtOCi)) return false;
                if (!NisuuCheck(txtOCc)) return false;
                if (!NisuuCheck(txtODi)) return false;
                if (!NisuuCheck(txtODc)) return false;

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, msName + "保守", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        ///---------------------------------------------------------
        /// <summary>
        ///     日数が1以上で月日数の範囲内かチェックする</summary>
        /// <param name="tx">
        ///     テキストボックスオブジェクト</param>
        /// <returns>
        ///     範囲内：true, 範囲外：false</returns>
        ///---------------------------------------------------------
        private bool NisuuCheck(TextBox tx)
        {
            bool rtn = true;

            // 日数が月日数の範囲内か
            if (Utility.StrtoInt(tx.Text) < 1)
            {
                tx.Focus();
                MessageBox.Show("日数ゼロは登録できません");
                return false;
            }

            // 日数が月日数の範囲内か
            if (Utility.StrtoInt(tx.Text) > DateTime.DaysInMonth(int.Parse(txtYear.Text) + Properties.Settings.Default.RekiHosei, int.Parse(txtMonth.Text)))
            {
                tx.Focus();
                MessageBox.Show("月の日数より多い日数は登録できません");
                return false;
            }

            return rtn;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 確認
                if (MessageBox.Show("削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                // 削除データ取得（エラー回避のためDataRowState.Deleted と DataRowState.Detachedは除外して抽出する）
                var d = dts.出勤日数.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.ID == int.Parse(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.出勤日数Row dl = (MTYSDataSet.出勤日数Row)dts.出勤日数.Rows.Find(it.ID);
                    dl.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの削除に失敗しました" + Environment.NewLine + ex.Message);
            }
            finally
            {
                //adp.Update(dts.出勤日数);
                //adp.Fill(dts.出勤日数);

                // グリッドデータ再表示
                GridViewShow(dg);

                // 画面データ消去
                DispInitial();
            }
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShukinDays_FormClosing(object sender, FormClosingEventArgs e)
        {
            // データセットの内容をデータベースへ反映させます。
            adp.Update(dts);

            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DispInitial();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
        }

        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GridEnter();
        }

        private void frmShukinDays_Shown(object sender, EventArgs e)
        {
            txtYear.Focus();
        }
    }
}
