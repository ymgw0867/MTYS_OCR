using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTYS_OCR.Common;
using System.Data.OleDb;

namespace MTYS_OCR.Master
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();

            adp.Fill(dt);

            MTYSDataSet.環境設定Row r = (MTYSDataSet.環境設定Row)dt.FindByID(global.configKEY);

            txtYear.Text = r.年.ToString();
            txtMonth.Text = r.月.ToString();
            lblPath.Text = r.受け渡しデータ作成パス;
            txtArchive.Text = r.データ保存月数.ToString();
        }

        MTYSDataSetTableAdapters.環境設定TableAdapter adp = new MTYSDataSetTableAdapters.環境設定TableAdapter();
        MTYSDataSet.環境設定DataTable dt = new MTYSDataSet.環境設定DataTable();

        private void frmConfig_Load(object sender, EventArgs e)
        {
            Utility.WindowsMaxSize(this, this.Width, this.Height);
            Utility.WindowsMinSize(this, this.Width, this.Height);
        }


        /// <summary>
        /// フォルダダイアログ選択
        /// </summary>
        /// <returns>フォルダー名</returns>
        private string userFolderSelect()
        {
            string fName = string.Empty;

            //出力フォルダの選択ダイアログの表示
            // FolderBrowserDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            // ダイアログの説明を設定する
            folderBrowserDialog1.Description = "フォルダを選択してください";

            // ルートになる特殊フォルダを設定する (初期値 SpecialFolder.Desktop)
            folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.Desktop;

            // 初期選択するパスを設定する
            folderBrowserDialog1.SelectedPath = @"C:\";

            // [新しいフォルダ] ボタンを表示する (初期値 true)
            folderBrowserDialog1.ShowNewFolderButton = true;

            // ダイアログを表示し、戻り値が [OK] の場合は、選択したディレクトリを表示する
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fName = folderBrowserDialog1.SelectedPath + @"\";
            }
            else
            {
                // 不要になった時点で破棄する
                folderBrowserDialog1.Dispose();
                return fName;
            }

            // 不要になった時点で破棄する
            folderBrowserDialog1.Dispose();

            return fName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //フォルダーを選択する
            lblPath.Text = userFolderSelect();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // データ更新
            DataUpdate();
        }

        private void DataUpdate()
        {
            if (MessageBox.Show("データを更新してよろしいですか","確認",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            // エラーチェック
            if (!errCheck()) return;

            // データ更新
            MTYSDataSet.環境設定Row r = (MTYSDataSet.環境設定Row)dt.FindByID(global.configKEY);

            if (r != null)
            {
                r.年 = int.Parse(txtYear.Text);
                r.月 = int.Parse(txtMonth.Text);
                r.受け渡しデータ作成パス = lblPath.Text;
                r.データ保存月数 = int.Parse(txtArchive.Text);
                r.更新年月日 = DateTime.Now;

                adp.Update(dt);

                global.cnfYear = int.Parse(txtYear.Text);
                global.cnfMonth = int.Parse(txtMonth.Text);
                global.cnfPath = lblPath.Text;
            }
            else
            {
                MessageBox.Show("データの読み込みに失敗しました","更新エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
 
            // 終了
            this.Close();
        }

        private bool errCheck()
        {
            // 処理年
            if (!Utility.NumericCheck(txtYear.Text))
            {
                MessageBox.Show("処理年が正しくありません","エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                txtYear.Focus();
                return false;
            }

            // 処理月
            if (!Utility.NumericCheck(txtMonth.Text))
            {
                MessageBox.Show("処理月が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            if (Utility.StrtoInt(txtMonth.Text) < 1 || Utility.StrtoInt(txtMonth.Text) > 12)
            {
                MessageBox.Show("処理月が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            // パス
            if (lblPath.Text.Trim() == string.Empty)
            {
                MessageBox.Show("受け渡しデータ作成パスが指定されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblPath.Focus();
                return false;
            }

            // データ保存期間
            if (!Utility.NumericCheck(txtArchive.Text))
            {
                MessageBox.Show("履歴データ保存期間が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMonth.Focus();
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 後片付け
            this.Dispose();
        }

    }
}
