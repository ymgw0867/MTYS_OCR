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
    public partial class frmUnSubmit : Form
    {
        public frmUnSubmit(string ComName, string ComNo)
        {
            InitializeComponent();

            //_DBName = DBName;
            _ComNo = ComNo;
            _ComName = ComName;
        }

        string _DBName = string.Empty;
        string _ComName = string.Empty;
        string _ComNo = string.Empty;

        // 社員区分配列
        string[] shainArray = { "", "社員", "パートタイマー", "出向社員"};

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void frmUnSubmit_Load(object sender, EventArgs e)
        {
            //ウィンドウズ最小サイズ
            Utility.WindowsMinSize(this, this.Size.Width, this.Size.Height);

            //// データ領域名コンボボックスのデータソースをセットする
            //dataComboSet();

            // データグリッド定義
            GridviewSet(dataGridView1);

            // 画面初期化
            DispClear();

            //// 会社領域名表示
            this.Text += "【" + _ComName + "】";

            // 年月表示
            txtYear.Text = global.cnfYear.ToString();
            txtMonth.Text = global.cnfMonth.ToString();
        }


        // カラム定義
        private string ColData = "c0";
        private string ColSz = "c1";
        private string ColSznm = "c2";
        private string ColCode = "c3";
        private string ColName = "c4";
        private string ColStatus = "c5";
        private string ColYear = "c6";
        private string ColMonth = "c7";
        private string ColID = "c8";
        private string ColType = "c9";

        /// <summary>
        /// データグリッドビューの定義を行います
        /// </summary>
        private void GridviewSet(DataGridView tempDGV)
        {
            try
            {
                //フォームサイズ定義

                // 列スタイルを変更する

                tempDGV.EnableHeadersVisualStyles = false;

                // 列ヘッダー表示位置指定
                tempDGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                // 列ヘッダーフォント指定
                tempDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", 10, FontStyle.Regular);

                // データフォント指定
                tempDGV.DefaultCellStyle.Font = new Font("Meiryo UI", 10, FontStyle.Regular);

                // 行の高さ
                tempDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                tempDGV.ColumnHeadersHeight = 22;
                tempDGV.RowTemplate.Height = 22;

                // 全体の高さ
                tempDGV.Height = 643;

                // 奇数行の色
                tempDGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

                // 各列幅指定
                tempDGV.Columns.Add(ColYear, "年");
                tempDGV.Columns.Add(ColMonth, "月");
                tempDGV.Columns.Add(ColSznm, "区分");
                tempDGV.Columns.Add(ColCode, "個人番号");
                tempDGV.Columns.Add(ColName, "氏名");
                tempDGV.Columns.Add(ColID, "ID");

                tempDGV.Columns[ColYear].Width = 60;
                tempDGV.Columns[ColMonth].Width = 40;
                tempDGV.Columns[ColSznm].Width = 100;
                tempDGV.Columns[ColCode].Width = 100;
                tempDGV.Columns[ColName].Width = 200;

                tempDGV.Columns[ColID].Visible = false;

                tempDGV.Columns[ColName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                tempDGV.Columns[ColYear].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[ColMonth].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                tempDGV.Columns[ColCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 編集可否
                tempDGV.ReadOnly = true;

                // 行ヘッダを表示しない
                tempDGV.RowHeadersVisible = false;

                // 選択モード
                tempDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "エラーメッセージ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void DispClear()
        {
            txtYear.Text = string.Empty;
            txtMonth.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox2.SelectedIndex = -1;
            txtCode.Text = string.Empty;
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            if (errCheck()) DataSelect();
        }

        ///------------------------------------------------------------------
        /// <summary>
        ///     指定項目エラーチェック</summary>
        /// <returns>
        ///     エラーなし:true, エラーあり:false</returns>
        ///------------------------------------------------------------------
        private bool errCheck()
        {
            try 
	        {
                if (txtYear.Text != string.Empty && !Utility.NumericCheck(txtYear.Text))
                {
                    txtYear.Focus();
                    throw new Exception("年が正しくありません");
                }

                if (txtMonth.Text != string.Empty && !Utility.NumericCheck(txtMonth.Text))
                {
                    txtMonth.Focus();
                    throw new Exception("月が正しくありません");
                }

                if (txtMonth.Text != string.Empty)
                {
                    if (int.Parse(txtMonth.Text) < 1 || int.Parse(txtMonth.Text) > 12)
                    {
                        txtMonth.Focus();
                        throw new Exception("月が正しくありません");
                    }
                }
            }
	        catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
	        }
            return true;
        }

        ///--------------------------------------------------------
        /// <summary>
        ///     データ検索</summary>
        ///--------------------------------------------------------
        private void DataSelect()
        {
            // 指定社員区分
            int sShain = comboBox2.SelectedIndex + 1;

            // 指定社員番号
            int sCode = Utility.StrtoInt(txtCode.Text);
            
            // データグリッドビューの表示を初期化する
            dataGridView1.RowCount = 0;

            // 過去出勤簿ヘッダデータリーダーを取得する
            MTYSDataSet.過去勤務票ヘッダTableAdapter hAdp = new MTYSDataSet.過去勤務票ヘッダTableAdapter();
            MTYSDataSet.過去勤務票ヘッダDataTable hd = new MTYSDataSet.過去勤務票ヘッダDataTable();
            hAdp.FillByYM(hd, int.Parse(txtYear.Text), int.Parse(txtMonth.Text));

            for (int i = 0; i < hd.Rows.Count; i++)
            {
                MTYSDataSet.過去勤務票ヘッダRow r = (MTYSDataSet.過去勤務票ヘッダRow)hd.Rows[i];

                // データ領域
                if (_ComNo.PadLeft(4, '0') != r.データ領域名) continue;

                // 社員区分判定
                if (sShain != 0)
                {
                    // 指定以外の社員区分（帳票番号は読み飛ばし）
                    if (sShain != r.帳票番号) continue;
                }

                // 社員番号指定
                if (sCode != 0)
                {
                    // 指定以外の社員番号は読み飛ばし
                    if (sCode != r.個人番号) continue;
                }

                // グリッドへ表示する
                gridShow(r, dataGridView1);
            }

            dataGridView1.CurrentCell = null;

            // 終了
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("該当するデータはありませんでした", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ///---------------------------------------------------------------------
        /// <summary>
        ///     データグリッドへ表示する</summary>
        /// <param name="r">
        ///     JSDataSet.過去勤務票ヘッダRow</param>
        /// <param name="g">
        ///     datagridviewオブジェクト</param>
        ///---------------------------------------------------------------------
        private void gridShow(MTYSDataSet.過去勤務票ヘッダRow r, DataGridView g)
        {
            g.Rows.Add();
            g[ColYear, g.Rows.Count - 1].Value = r.年.ToString();
            g[ColMonth, g.Rows.Count - 1].Value = r.月.ToString();
            g[ColSznm, g.Rows.Count - 1].Value = shainArray[r.帳票番号];
            g[ColCode, g.Rows.Count - 1].Value = r.個人番号.ToString().PadLeft(4, '0');
            g[ColName, g.Rows.Count - 1].Value = r.氏名.ToString();

            g[ColID, g.Rows.Count - 1].Value = r.ID.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUnSubmit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string rID = string.Empty;

            rID = dataGridView1[ColID, dataGridView1.SelectedRows[0].Index].Value.ToString();

            if (rID != string.Empty)
            {
                this.Hide();
                OCR.frmPastData frm = new OCR.frmPastData(rID);
                frm.ShowDialog();
                this.Show();
            }
        }

        private void frmUnSubmit_Shown(object sender, EventArgs e)
        {
            txtYear.Focus();
        }
    }
}
