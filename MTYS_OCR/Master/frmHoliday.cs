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
    public partial class frmHoliday : Form
    {
        // マスター名
        string msName = "休日";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.休日TableAdapter adp = new MTYSDataSetTableAdapters.休日TableAdapter();

        public frmHoliday()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.休日TableAdapter = adp;
            adpMn.休日TableAdapter.Fill(dts.休日);
        }

        private void frm_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // データグリッド定義
            GridViewSetting(dg);

            // データグリッドビューにデータを表示します
            GridViewShow(dg);

            // 画面初期化
            DispInitial();
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

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 20;
                tempDGV.RowTemplate.Height = 20;

                // 全体の高さ
                tempDGV.Height = 341;

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
        private void GridViewShow(DataGridView tempGrid)
        {
            try
            {
                // データソースにデータテーブルをバインド
                tempGrid.DataSource = dts.休日;

                // 列幅調整
                tempGrid.Columns[1].Width = 100;    // 年月日
                tempGrid.Columns[16].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempGrid.CurrentCell = null;

                // IDコード列は非表示とする
                tempGrid.Columns[0].Visible = false;
                tempGrid.Columns[2].Visible = false;
                tempGrid.Columns[3].Visible = false;
                tempGrid.Columns[4].Visible = false;
                tempGrid.Columns[5].Visible = false;
                tempGrid.Columns[6].Visible = false;
                tempGrid.Columns[7].Visible = false;
                tempGrid.Columns[8].Visible = false;
                tempGrid.Columns[9].Visible = false;
                tempGrid.Columns[10].Visible = false;
                tempGrid.Columns[11].Visible = false;
                tempGrid.Columns[12].Visible = false;
                tempGrid.Columns[13].Visible = false;
                tempGrid.Columns[14].Visible = false;
                tempGrid.Columns[15].Visible = false;
                tempGrid.Columns[17].Visible = false;
                tempGrid.Columns[18].Visible = false;

                //tempGrid.Columns[13].Visible = false;
                //tempGrid.Columns[14].Visible = false;
                //tempGrid.Columns[15].Visible = false;
                //tempGrid.Columns[16].Visible = false;

                tempGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                //年月日列でソートする
                tempGrid.Sort(tempGrid.Columns[1], ListSortDirection.Descending);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            dtHoliday.Value = DateTime.Today;
            fMode.Mode = global.FORM_ADDMODE;

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

            chkAmiH.CheckState = CheckState.Unchecked;
            chkAmiS.CheckState = CheckState.Unchecked;
            chkAmiO.CheckState = CheckState.Unchecked;
            chkAmiA.CheckState = CheckState.Unchecked;
            chkAmiB.CheckState = CheckState.Unchecked;
            chkAmiC.CheckState = CheckState.Unchecked;
            chkAmiD.CheckState = CheckState.Unchecked;

            //chkAmiH.Enabled = false;
            //chkAmiS.Enabled = false;
            //chkAmiO.Enabled = false;
            //chkAmiA.Enabled = false;
            //chkAmiB.Enabled = false;
            //chkAmiC.Enabled = false;
            //chkAmiD.Enabled = false;

            txtMemo.Text = string.Empty;

            btnUpdate.Enabled = true;
            btnDel.Enabled = false;
            btnClear.Enabled = false;
            dtHoliday.Enabled = true;
            dtHoliday.Focus();
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
                    if (MessageBox.Show(dtHoliday.Value.ToShortDateString() + "を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセットにデータを追加します
                    dts.休日.Add休日Row(dtHoliday.Value, Utility.checkToInt(chkHon), Utility.checkToInt(chkShiz),
                                       Utility.checkToInt(chkOsk), Utility.checkToInt(chkOskA), Utility.checkToInt(chkOskB),
                                       Utility.checkToInt(chkOskC), Utility.checkToInt(chkOskD), Utility.checkToInt(chkAmiH),
                                       Utility.checkToInt(chkAmiS), Utility.checkToInt(chkAmiO), Utility.checkToInt(chkAmiA),
                                       Utility.checkToInt(chkAmiB), Utility.checkToInt(chkAmiC), Utility.checkToInt(chkAmiD),
                                       cmbTekiyou.Text, txtMemo.Text, DateTime.Now);
                    
                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(dtHoliday.Value.ToShortDateString() + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    var r = dts.休日.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && 
                                               a.ID == int.Parse(fMode.ID));
                    
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
                        r.大阪A網掛け= Utility.checkToInt(chkAmiA);
                        r.大阪B網掛け = Utility.checkToInt(chkAmiB);
                        r.大阪C網掛け = Utility.checkToInt(chkAmiC);
                        r.大阪D網掛け = Utility.checkToInt(chkAmiD);

                        r.摘要 = cmbTekiyou.Text;
                        r.備考 = txtMemo.Text;
                        r.更新年月日 = DateTime.Now;
                    }
                    else
                    {
                        MessageBox.Show(fMode.ID + "がキー不在です：データの更新に失敗しました","更新エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                    break;

                default:
                    break;
            }

            // グリッドデータ再表示
            GridViewShow(dg);

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
                    //登録済みコードか調べる
                    var s = dts.休日.Where(a => a.年月日 == dtHoliday.Value);
                    if (s.Count() > 0)
                    {
                        dtHoliday.Focus();
                        throw new Exception("この日付は既に登録済みです");
                    }
                }
                
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, msName + "保守", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
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
            msgStr += dg[1, fMode.rowIndex].Value.ToString() + " が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                return;

            // 対象となるデータテーブルROWを取得します
            MTYSDataSet.休日Row sQuery = dts.休日.FindByID(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

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
        private void ShowData(MTYSDataSet.休日Row s)
        {
            fMode.ID = s.ID.ToString();
            dtHoliday.Enabled = false;
            dtHoliday.Value = s.年月日;
            chkHon.Checked = Utility.intToCheck(s.本社使用);
            chkShiz.Checked = Utility.intToCheck(s.静岡使用);
            chkOsk.Checked = Utility.intToCheck(s.大阪製造部使用);
            chkOskA.Checked = Utility.intToCheck(s.大阪製造部A使用);
            chkOskB.Checked = Utility.intToCheck(s.大阪製造部B使用);
            chkOskC.Checked = Utility.intToCheck(s.大阪製造部C使用);
            chkOskD.Checked = Utility.intToCheck(s.大阪製造部D使用);

            chkAmiH.Checked = Utility.intToCheck(s.本社網掛け);
            chkAmiS.Checked = Utility.intToCheck(s.静岡網掛け);
            chkAmiO.Checked = Utility.intToCheck(s.大阪製造部網掛け);
            chkAmiA.Checked = Utility.intToCheck(s.大阪A網掛け);
            chkAmiB.Checked = Utility.intToCheck(s.大阪B網掛け);
            chkAmiC.Checked = Utility.intToCheck(s.大阪C網掛け);
            chkAmiD.Checked = Utility.intToCheck(s.大阪D網掛け);

            cmbTekiyou.Text = s.摘要;
            txtMemo.Text = s.備考;

            btnDel.Enabled = true;
            btnClear.Enabled = true;
        }

        private void dg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GridEnter();
        }

        private void btnRtn_Click(object sender, EventArgs e)
        {
            // フォームを閉じます
            this.Close();
        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // データセットの内容をデータベースへ反映させます。
            //adp.Update(dt);

            adpMn.UpdateAll(dts);

            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DispInitial();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 確認
                if (MessageBox.Show("削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

                // 削除データ取得（エラー回避のためDataRowState.Deleted と DataRowState.Detachedは除外して抽出する）
                var d = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.ID == int.Parse(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.休日Row dl = (MTYSDataSet.休日Row)dts.休日.Rows.Find(it.ID);
                    dl.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの削除に失敗しました" + Environment.NewLine + ex.Message);
            }
            finally
            {
                // グリッドデータ再表示
                GridViewShow(dg);

                // 画面データ消去
                DispInitial();
            }
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            dtHoliday.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            // データベース更新後、データテーブルにデータを読み込む
            adpMn.UpdateAll(dts);
            adpMn.休日TableAdapter = adp;
            adpMn.休日TableAdapter.Fill(dts.休日);

            // 休日カレンダー表示
            this.Hide();
            frmCalender frm = new frmCalender();
            frm.ShowDialog();
            this.Show();
        }

        private void chkHon_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
