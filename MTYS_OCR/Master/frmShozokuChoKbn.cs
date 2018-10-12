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
    public partial class frmShozokuChoKbn : Form
    {
        // マスター名
        string msName = "所属・帳票区分対応";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 所属帳票区分対応マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 所属帳票区分対応マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.所属帳票区分対応TableAdapter adp = new MTYSDataSetTableAdapters.所属帳票区分対応TableAdapter();

        // 社員所属マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adpS = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public frmShozokuChoKbn()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.所属帳票区分対応TableAdapter = adp;
            adpMn.所属帳票区分対応TableAdapter.Fill(dts.所属帳票区分対応);

            adpS.Fill(dts.社員所属);
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
                tempGrid.DataSource = dts.所属帳票区分対応;

                tempGrid.CurrentCell = null;

                // 列幅調整
                tempGrid.Columns[0].Visible = false;    // IDコード列は非表示とする
                tempGrid.Columns[1].Width = 100;
                tempGrid.Columns[2].Width = 100;
                tempGrid.Columns[3].Width = 200;
                tempGrid.Columns[4].Width = 120;

                tempGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                    dts.所属帳票区分対応.Add所属帳票区分対応Row(int.Parse(txtCode.Text), int.Parse(cmbKbn.SelectedValue.ToString()), txtMemo.Text, DateTime.Now);
                    
                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtCode.Text + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    var r = dts.所属帳票区分対応.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && 
                                               a.ID == int.Parse(fMode.ID));
                    
                    if (!r.HasErrors)
                    {
                        r.所属コード = int.Parse(txtCode.Text);
                        r.帳票区分 = int.Parse(cmbKbn.SelectedValue.ToString());
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
                    var s = dts.所属帳票区分対応.Where(a => a.RowState != DataRowState.Deleted && 
                                                           a.RowState != DataRowState.Detached &&  
                                                           a.所属コード.ToString() == txtCode.Text);

                    if (s.Count() > 0)
                    {
                        txtCode.Focus();
                        throw new Exception("既に登録済みの所属コードです");
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
            msgStr += "所属コード:" + dg[1, fMode.rowIndex].Value.ToString() + "が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                return;

            // 対象となるデータテーブルROWを取得します
            MTYSDataSet.所属帳票区分対応Row sQuery = dts.所属帳票区分対応.FindByID(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

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
        private void ShowData(MTYSDataSet.所属帳票区分対応Row s)
        {
            fMode.ID = s.ID.ToString();
            txtCode.Enabled = false;
            txtCode.Text = s.所属コード.ToString();
            cmbKbn.SelectedValue = s.帳票区分;
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
                var d = dts.所属帳票区分対応.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.ID == int.Parse(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.所属帳票区分対応Row dl = (MTYSDataSet.所属帳票区分対応Row)dts.所属帳票区分対応.Rows.Find(it.ID);
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
            cmbKbn.Focus();
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

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            string szName = string.Empty;

            var s = dts.社員所属.Where(a => a.RowState != DataRowState.Detached && a.RowState != DataRowState.Deleted &&
                                           a.所属コード == Utility.StrtoInt(txtCode.Text));

            foreach (var t in s)
            {
                szName = t.所属名称;
            }

            txtName.Text = szName;
        }
    }
}
