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
    public partial class frmShukkinType : Form
    {
        // マスター名
        string msName = "出勤形態";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 出勤形態マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 出勤形態マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.出勤形態TableAdapter adp = new MTYSDataSetTableAdapters.出勤形態TableAdapter();

        public frmShukkinType()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adp.Fill(dts.出勤形態);
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

            // コンボボックススタイル
            cmbKbnLoad();

            // 画面初期化
            DispInitial();
        }

        /// <summary>
        /// 帳票区分コンボボックスデータソース取得
        /// </summary>
        private void cmbKbnLoad()
        {
            MTYSDataSetTableAdapters.帳票区分TableAdapter adp = new MTYSDataSetTableAdapters.帳票区分TableAdapter();
            adp.Fill(dts.帳票区分);

            cmbKbn.DisplayMember = "名称";
            cmbKbn.ValueMember = "ID";

            var s = dts.帳票区分
                .Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached)
                .OrderBy(a => a.ID)
                .Select(a => new
                {
                    ID = a.ID,
                    名称 = a.帳票区分名称
                });

            cmbKbn.DataSource = s.ToList();

            cmbKbn.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        ////カラム定義
        //string C_Code = "col1";
        //string C_Name = "col2";
        //string C_ShukkinKbn = "col3";
        //string C_ShainKbn = "col4";
        //string C_PartKbn = "col5";
        //string C_Memo = "col6";
        //string C_Date = "col7";

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

                ////各列幅指定
                //tempDGV.Columns.Add(C_Code, "番号");
                //tempDGV.Columns.Add(C_Name, "勤怠区分名称");
                //tempDGV.Columns.Add(C_ShukkinKbn, "出勤区分");
                //tempDGV.Columns.Add(C_ShainKbn, "社員");
                //tempDGV.Columns.Add(C_PartKbn, "パート");
                //tempDGV.Columns.Add(C_Memo, "備考");
                //tempDGV.Columns.Add(C_Date, "更新日");

                //tempDGV.Columns[C_Code].Width = 70;
                //tempDGV.Columns[C_Name].Width = 160;
                //tempDGV.Columns[C_ShukkinKbn].Width = 80;
                //tempDGV.Columns[C_ShainKbn].Width = 60;
                //tempDGV.Columns[C_PartKbn].Width = 60;
                //tempDGV.Columns[C_Memo].Width = 300;
                //tempDGV.Columns[C_Date].Width = 150;

                //tempDGV.Columns[C_Memo].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
                tempGrid.DataSource = dts.出勤形態;

                // 列幅調整
                tempGrid.Columns[0].Width = 70;
                tempGrid.Columns[1].Width = 70;
                tempGrid.Columns[2].Width = 70;
                tempGrid.Columns[3].Width = 200;
                tempGrid.Columns[4].Width = 300;
                tempGrid.Columns[5].Width = 150;

                tempGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempGrid.CurrentCell = null;

                // 帳票区分列で自動ソートする
                tempGrid.Sort(tempGrid.Columns[2], ListSortDirection.Ascending);

                // IDコード列は非表示とする
                tempGrid.Columns[0].Visible = false;
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
            fMode.Mode = global.FORM_ADDMODE;
            txtCode.Text = string.Empty;
            cmbKbn.SelectedIndex = -1;
            txtName.Text = string.Empty;
            txtMemo.Text = string.Empty;

            btnUpdate.Enabled = true;
            btnDel.Enabled = false;
            btnClear.Enabled = false;
            txtCode.Enabled = true;
            cmbKbn.Focus();
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
                    if (MessageBox.Show(txtCode.Text + "を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセットにデータを追加します
                    //dts.出勤形態.Add出勤形態Row(int.Parse(txtCode.Text), int.Parse(cmbKbn.SelectedValue.ToString()), txtName.Text, txtMemo.Text, DateTime.Now);   // 2019/12/10 コメント化
                    dts.出勤形態.Add出勤形態Row(txtCode.Text.Trim(), int.Parse(cmbKbn.SelectedValue.ToString()), txtName.Text, txtMemo.Text, DateTime.Now);

                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtCode.Text + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    var r = dts.出勤形態.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && 
                                               a.ID == int.Parse(fMode.ID));
                    
                    if (!r.HasErrors)
                    {
                        //r.コード = int.Parse(txtCode.Text);  // 2019/12/10 コメント化
                        r.コード = txtCode.Text.Trim();
                        r.帳票区分 = Utility.StrtoInt(cmbKbn.SelectedValue.ToString());
                        r.名称 = txtName.Text;
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
                //ゼロは不可
                if (this.txtCode.Text.ToString() == "0")
                {
                    this.txtCode.Focus();
                    throw new Exception("ゼロは登録できません");
                }

                // 帳票区分
                if (cmbKbn.SelectedIndex == -1)
                {
                    cmbKbn.Focus();
                    throw new Exception("帳票区分を選択してください");
                }

                //登録モードのとき番号をチェック
                if (fMode.Mode == global.FORM_ADDMODE)
                {
                    //登録済みコードか調べる
                    var s = dts.出勤形態.Where(a => a.コード.ToString() == txtCode.Text && a.帳票区分.ToString() == cmbKbn.SelectedValue.ToString());
                    if (s.Count() > 0)
                    {
                        txtCode.Focus();
                        throw new Exception("既に登録済みのコードです");
                    }
                }

                //名称チェック
                if (txtCode.Text.Trim().Length < 1)
                {
                    txtCode.Focus();
                    throw new Exception(msName + "名を入力してください");
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
            msgStr += dg[1, fMode.rowIndex].Value.ToString() + "：" + dg[3, fMode.rowIndex].Value.ToString() + Environment.NewLine + Environment.NewLine;
            msgStr += "上記の" + msName + "が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                return;

            // 対象となるデータテーブルROWを取得します
            MTYSDataSet.出勤形態Row sQuery = dts.出勤形態.FindByID(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

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
        private void ShowData(MTYSDataSet.出勤形態Row s)
        {
            fMode.ID = s.ID.ToString();
            txtCode.Enabled = false;
            txtCode.Text = s.コード.ToString();
            cmbKbn.SelectedValue = s.帳票区分;
            txtName.Text = s.名称;
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
            // データセットの内容をデータベースへ反映させます
            adp.Update(dts.出勤形態);

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
                var d = dts.出勤形態.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.ID == int.Parse(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.出勤形態Row dl = (MTYSDataSet.出勤形態Row)dts.出勤形態.Rows.Find(it.ID);
                    dl.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの削除に失敗しました" + Environment.NewLine + ex.Message);
            }
            finally
            {
                // 削除をコミット
                adp.Update(dts.出勤形態);

                // データテーブルにデータを読み込む
                adp.Fill(dts.出勤形態);

                // 画面データ消去
                DispInitial();
            }
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            cmbKbn.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
