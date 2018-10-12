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
    public partial class frmShain : Form
    {
        // マスター名
        string msName = "社員";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 社員マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 勤怠記号マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public frmShain()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);
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

            Utility.cmbKbnLoad(cmbChohyo);
            cmbTaishokuLoad(cmbTaishoku);
            cmbOosakaLoad(cmbOosaka);
            Utility.ComboBumon.load(cmbSz);

            // 画面初期化
            DispInitial();
        }

        //カラム定義
        string C_Code = "col1";
        string C_Name = "col2";
        string C_ShukkinKbn = "col3";
        string C_ShainKbn = "col4";
        string C_PartKbn = "col5";
        string C_Memo = "col6";
        string C_Date = "col7";

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
                tempDGV.Height = 258;

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
                //tempGrid.DataSource = dt;
                tempGrid.DataSource = dts.社員所属;

                tempGrid.CurrentCell = null;

                // ヘッダテキスト
                tempGrid.Columns[9].HeaderText = "大阪G";

                // 列幅調整
                tempGrid.Columns[0].Width = 100;
                tempGrid.Columns[1].Width = 120;
                tempGrid.Columns[2].Width = 100;
                tempGrid.Columns[3].Width = 80;
                tempGrid.Columns[4].Width = 140;
                tempGrid.Columns[5].Width = 60;
                tempGrid.Columns[6].Width = 60;
                tempGrid.Columns[7].Width = 80;
                tempGrid.Columns[8].Width = 80;
                tempGrid.Columns[9].Width = 60;
                tempGrid.Columns[10].Width = 100;
                tempGrid.Columns[11].Width = 130;

                // 
                tempGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempGrid.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 1列目・社員番号で昇順ソート
                tempGrid.Sort(tempGrid.Columns[0], ListSortDirection.Ascending);

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
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFuri.Text = string.Empty;
            txtShokukyu.Text = string.Empty;
            txtShikaku.Text = string.Empty;

            cmbSz.SelectedIndex = -1;
            cmbTaishoku.SelectedIndex = -1;
            cmbChohyo.SelectedIndex = -1;
            cmbOosaka.SelectedIndex = 0;
            txtMemo.Text = string.Empty;

            btnUpdate.Enabled = true;
            btnDel.Enabled = false;
            btnClear.Enabled = false;
            txtID.Enabled = true;
            txtID.Focus();

            cmbTmData.SelectedIndex = 0;    // 2018/10/12

            dg.CurrentCell = null;
        }

        /// -------------------------------------------------------------
        /// <summary>
        ///     退職区分コンボボックスデータソース取得 </summary>
        /// <param name="cmbKbn">
        ///     コンボボックスオブジェクト</param>
        /// -------------------------------------------------------------
        private void cmbTaishokuLoad(ComboBox cmb)
        {
            cmb.DisplayMember = "名称";
            cmb.ValueMember = "ID";

            string [] sArray = {"在職","退職"};
            cmb.DataSource = sArray;

            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// -------------------------------------------------------------
        /// <summary>
        ///     大阪製造部コンボボックスデータソース取得 </summary>
        /// <param name="cmbKbn">
        ///     コンボボックスオブジェクト</param>
        /// -------------------------------------------------------------
        private void cmbOosakaLoad(ComboBox cmb)
        {
            cmb.DisplayMember = "名称";
            cmb.ValueMember = "ID";

            string[] sArray = { "", "A", "B", "C", "D" };
            cmb.DataSource = sArray;

            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
        /// -------------------------------------------------------------
        /// <summary>
        ///     帳票区分コンボボックスデータソース取得 </summary>
        /// <param name="cmbKbn">
        ///     コンボボックスオブジェクト</param>
        /// <param name="dts">
        ///     データセット</param>
        /// -------------------------------------------------------------
        private void cmbSzLoad(ComboBox cmbKbn)
        {
            cmbKbn.DisplayMember = "名称";
            cmbKbn.ValueMember = "ID";

            var s = dts.社員所属
                .Where(a => a.RowState != System.Data.DataRowState.Deleted && a.RowState != System.Data.DataRowState.Detached)
                .OrderBy(a => a.所属コード)
                .Select(a => new
                {
                    ID = a.所属コード,
                    名称 = a.所属コード.ToString() + " " + a.所属名称
                }).Distinct();

            cmbKbn.DataSource = s.ToList();

            cmbKbn.DropDownStyle = ComboBoxStyle.DropDownList;
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
                    if (MessageBox.Show(txtName.Text + "を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセットにデータを追加します
                    Utility.ComboBumon cmb = (Utility.ComboBumon)cmbSz.SelectedItem;
                    dts.社員所属.Add社員所属Row(Utility.StrtoInt(txtID.Text), txtName.Text, txtFuri.Text, Utility.StrtoInt(cmb.ID), cmb.DisplayName, Utility.StrtoInt(txtShokukyu.Text), Utility.StrtoInt(txtShikaku.Text), cmbTaishoku.SelectedIndex, Utility.StrtoInt(cmbChohyo.SelectedValue.ToString()), cmbOosaka.SelectedItem.ToString(), txtMemo.Text, DateTime.Now, cmbTmData.SelectedIndex);
                    
                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(txtName.Text + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    var r = dts.社員所属.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.社員番号 == Utility.StrtoInt(fMode.ID));
                    
                    if (!r.HasErrors)
                    {
                        r.氏名 = txtName.Text;
                        r.フリガナ = txtFuri.Text; 

                        cmb = (Utility.ComboBumon)cmbSz.SelectedItem;
                        r.所属コード = Utility.StrtoInt(cmb.ID);
                        r.所属名称 = cmb.DisplayName; 

                        r.職級 = Utility.StrtoInt(txtShokukyu.Text);
                        r.資格 = Utility.StrtoInt(txtShikaku.Text);
                        r.退職区分 = cmbTaishoku.SelectedIndex;
                        r.帳票区分 = Utility.StrtoInt(cmbChohyo.SelectedValue.ToString());
                        r.大阪製造部勤務グループ = cmbOosaka.SelectedItem.ToString();
                        r.備考 = txtMemo.Text;
                        r.更新年月日 = DateTime.Now;
                        r.出勤簿タイムカード印字 = cmbTmData.SelectedIndex;    // 2018/10/12
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
                    string str = this.txtID.Text;

                    // 未入力またはスペースのみは不可
                    if ((this.txtID.Text).Trim().Length < 1)
                    {
                        this.txtID.Focus();
                        throw new Exception("コードを入力してください");
                    }

                    //ゼロは不可 : 2014/06/23 ０：半休あり
                    //if (this.txtID.Text.ToString() == "0")
                    //{
                    //    this.txtID.Focus();
                    //    throw new Exception("ゼロは登録できません");
                    //}

                    //登録済みコードか調べる
                    MTYSDataSet.社員所属Row r = dts.社員所属.FindBy社員番号(Utility.StrtoInt(txtID.Text));
                    if (r != null)
                    {
                        txtID.Focus();
                        throw new Exception("既に登録済みのコードです");
                    }
                }

                //名称チェック
                if (txtName.Text.Trim().Length < 1)
                {
                    txtName.Focus();
                    throw new Exception(msName + "氏名を入力してください");
                }

                // 所属コンボボックス
                if (cmbSz.SelectedIndex == -1)
                {
                    cmbSz.Focus();
                    throw new Exception("所属を選択してください");
                }

                // 在職区分
                if (cmbTaishoku.SelectedIndex == -1)
                {
                    cmbTaishoku.Focus();
                    throw new Exception("在職区分を選択してください");
                }

                // 帳票区分
                if (cmbChohyo.SelectedIndex == -1)
                {
                    cmbChohyo.Focus();
                    throw new Exception("帳票区分を選択してください");
                }

                // 所属・帳票区分対応テーブルより該当する帳票区分を取得して同期をとる
                if (cmbSz.SelectedItem != null)
                {
                    Utility.ComboBumon cmb = (Utility.ComboBumon)cmbSz.SelectedItem;
                    if (cmbChohyo.SelectedValue.ToString() != Utility.getChohyoKbn(Utility.StrtoInt(cmb.ID)).ToString())
                    {
                        cmbChohyo.Focus();
                        throw new Exception("所属に該当しない帳票区分です");
                    }
                }
                
                // 大阪製造部グループ
                if (cmbOosaka.SelectedIndex == -1)
                {
                    cmbOosaka.Focus();
                    throw new Exception("大阪製造部グループを選択してください");
                }

                if (cmbChohyo.SelectedValue.ToString() == "3" && cmbOosaka.SelectedIndex == 0)
                {
                    if (MessageBox.Show("大阪製造部グループが未選択状態です" + Environment.NewLine + "グループを選択しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmbOosaka.Focus();
                        return false;
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
            msgStr += dg[0, fMode.rowIndex].Value.ToString() + "：" + dg[1, fMode.rowIndex].Value.ToString() + Environment.NewLine + Environment.NewLine;
            msgStr += "上記の" + msName + "が選択されました。よろしいですか？";

            if (MessageBox.Show(msgStr, "選択", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No) 
                return;

            // 対象となるデータテーブルROWを取得します
            MTYSDataSet.社員所属Row sQuery = dts.社員所属.FindBy社員番号(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

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
        private void ShowData(MTYSDataSet.社員所属Row s)
        {
            fMode.ID = s.社員番号.ToString();
            txtID.Text = s.社員番号.ToString();
            txtID.Enabled = false;
            txtName.Text = s.氏名;
            txtFuri.Text = s.フリガナ;
            Utility.ComboBumon.selectedIndex(cmbSz, s.所属コード);
            txtShokukyu.Text = s.職級.ToString();
            txtShikaku.Text = s.資格.ToString();
            //cmbChohyo.SelectedValue = s.帳票区分;
            cmbTaishoku.SelectedIndex = s.退職区分;
            cmbOosaka.SelectedItem = s.大阪製造部勤務グループ;
            txtMemo.Text = s.備考;

            // 出勤簿タイムカード打刻データの印字の有無 : 2018/10/12
            if (s.Is出勤簿タイムカード印字Null() || s.出勤簿タイムカード印字 == global.flgOff)
            {
                cmbTmData.SelectedIndex = 0;
            }
            else
            {
                cmbTmData.SelectedIndex = 1;
            }

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
                var d = dts.社員所属.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && a.社員番号 == Utility.StrtoInt(fMode.ID));

                // foreach用の配列を作成する
                var list = d.ToList();

                // 削除
                foreach (var it in list)
                {
                    MTYSDataSet.社員所属Row dl = (MTYSDataSet.社員所属Row)dts.社員所属.Rows.Find(it.社員番号);
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
            txtID.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // データベース更新後、データテーブルにデータを再読み込み
            adpMn.UpdateAll(dts);
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);

            this.Hide();
            frmXlsToShain frm = new frmXlsToShain();
            frm.ShowDialog();
            this.Close();
        }

        private void cmbChohyo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChohyo.SelectedValue != null)
            {
                if (cmbChohyo.SelectedValue.ToString() == "3")
                {
                    cmbOosaka.Enabled = true;
                }
                else
                {
                    cmbOosaka.Enabled = false;
                }
            }
        }

        private void cmbSz_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 所属・帳票区分対応テーブルより該当する帳票区分を取得して同期をとる
            if (cmbSz.SelectedItem != null)
            {
                Utility.ComboBumon cmb = (Utility.ComboBumon)cmbSz.SelectedItem;
                cmbChohyo.SelectedValue = Utility.getChohyoKbn(Utility.StrtoInt(cmb.ID));
            }
        }
    }
}
