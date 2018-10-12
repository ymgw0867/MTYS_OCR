using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using MTYS_OCR.Common;

namespace MTYS_OCR.Master
{
    public partial class frmXlsToShain : Form
    {
        // テーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 社員所属マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.社員所属TableAdapter adp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        public frmXlsToShain()
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.社員所属TableAdapter = adp;
            adpMn.社員所属TableAdapter.Fill(dts.社員所属);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblFileName.Text = string.Empty;

            // ダイアログボックスの表示
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "インポートするExcelファイルの選択";
            openFileDialog1.Filter = "xlsファイル|*.xls;*xlsx";
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblFileName.Text = openFileDialog1.FileName;
                this.button1.Enabled = true;
            }
            else return;
        }

        const int XLSSTROW = 2;         // エクセルシート開始行

        #region // エクセルシートデータ列定義
        const int COL_CODE = 1;             // 社員コード
        const int COL_FURI = 2;             // フリガナ
        const int COL_NAME = 3;             // 氏名
        const int COL_SCODE = 4;            // 所属コード
        const int COL_SNAME = 5;            // 所属名
        const int COL_SHOKUKYU = 6;         // 職級
        const int COL_DAMMY = 7;            // 空白列
        const int COL_SHIKAKU = 8;          // 資格
        #endregion

        #region // エクセル取得項目
        string colName = string.Empty;      // 氏名
        string colFuri = string.Empty;      // フリガナ
        string colsCode = string.Empty;     // 所属コード
        string colsName = string.Empty;     // 所属名
        string colShokukyu = string.Empty;  // 職級
        string colShikaku = string.Empty;   // 資格
        #endregion

        /// <summary>
        /// 社員マスターインポート
        /// </summary>
        private void import_msEmployee(string xlsFileName)
        {
            Excel.Application oXls = new Excel.Application();

            Excel.Workbook oXlsBook = (Excel.Workbook)(oXls.Workbooks.Open(xlsFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                               Type.Missing, Type.Missing));

            Excel.Worksheet oxlsSheet = (Excel.Worksheet)oXlsBook.Sheets[1];

            Excel.Range dRng;
            Excel.Range[] rng = new Microsoft.Office.Interop.Excel.Range[2];

            Excel.Range stRange = (Excel.Range)oxlsSheet.Cells[1, 1];
            //Excel.Range edRange = (Excel.Range)oxlsSheet.Cells[26, 12];

            // 読み込み開始行
            int fromRow = XLSSTROW;

            // 利用領域行数を取得
            int toRow = oxlsSheet.UsedRange.Rows.Count;

            //// ステータスラベル表示
            this.toolStripStatusLabel1.Text = "社員データインポート中...";

            // プログレスバー設定
            toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Minimum = 0;
            this.toolStripProgressBar1.Maximum = toRow;
            this.toolStripProgressBar1.Visible = true;

            try
            {
                // マウスポインタを待機状態する
                this.Cursor = Cursors.WaitCursor;

                //// 社員マスターインポート開始を通知
                //listBox1.Items.Add("社員マスターのインポートを開始しました。");

                string msgType = string.Empty;
                int impCnt = 0;     // 新規追加件数
                int upCnt = 0;      // 更新件数

                //// 社員マスターID最大値を取得します
                //int maxID = getMaxEmployeeID();

                // エクセルシートの社員名の列を順次読み込む
                for (int i = fromRow; i <= toRow; i++)
                {
                    // 社員コードセルの値を取得します
                    dRng = (Excel.Range)oxlsSheet.Cells[i, COL_CODE];
                    string hCode = dRng.Text.ToString().Trim();

                    // セルに有効値があること
                    if (Utility.StrtoInt(hCode) != 0)
                    {
                        // セルの値を取得
                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_NAME];   // 氏名
                        colName = dRng.Text.ToString().Trim();

                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_FURI];   // フリガナ
                        colFuri = dRng.Text.ToString().Trim();

                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SCODE];  // 所属コード
                        colsCode = dRng.Text.ToString().Trim();

                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SNAME];  // 所属名
                        colsName = dRng.Text.ToString().Trim();

                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SHOKUKYU];   // 職級
                        colShokukyu = dRng.Text.ToString().Trim();

                        dRng = (Excel.Range)oxlsSheet.Cells[i, COL_SHIKAKU];    // 資格
                        colShikaku = dRng.Text.ToString().Trim();

                        // 所属コードから帳票区分を取得
                        int choHyoKbn = getChohyoKbn(Utility.StrtoInt(colsCode));

                        // 社員マスターの社員名にセルの値が未登録であれば追加登録します
                        if (!getEmployeeRec(Utility.StrtoInt(hCode)))
                        {
                            dts.社員所属.Add社員所属Row(int.Parse(hCode), colName, colFuri, Utility.StrtoInt(colsCode), 
                                                       colsName, Utility.StrtoInt(colShokukyu), Utility.StrtoInt(colShikaku), 
                                                       0, choHyoKbn, string.Empty, string.Empty, DateTime.Today, 0);

                            impCnt++;  // 追加件数
                        }
                        else
                        {
                            var r = dts.社員所属.Single(a => a.RowState != DataRowState.Deleted && 
                                a.RowState != DataRowState.Detached && a.社員番号 == Utility.StrtoInt(hCode));

                            r.氏名 = colName;
                            r.フリガナ = colFuri;
                            r.所属コード = Utility.StrtoInt(colsCode);
                            r.所属名称 = colsName;
                            r.職級 = Utility.StrtoInt(colShokukyu);
                            r.資格 = Utility.StrtoInt(colShikaku);
                            r.帳票区分 = choHyoKbn;
                            r.更新年月日 = DateTime.Now;

                            upCnt++;  // 更新件数
                        }

                        // プログレスバー表示
                        this.toolStripProgressBar1.Value = i;
                    }
                }

                // インポート件数結果を表示する
                string msg = "インポートが終了しました。" + Environment.NewLine + Environment.NewLine + 
                             "追加件数：" + impCnt.ToString() + Environment.NewLine + 
                             "更新件数：" + upCnt.ToString();

                MessageBox.Show(msg,"",MessageBoxButtons.OK,MessageBoxIcon.Information);

                //保存処理
                oXls.DisplayAlerts = false;

                //Bookをクローズ
                oXlsBook.Close(Type.Missing, Type.Missing, Type.Missing);

                //Excelを終了
                oXls.Quit();

                // データベースを更新
                adpMn.UpdateAll(dts);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Excelデータ取り込み", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            finally
            {
                // COM オブジェクトの参照カウントを解放する 
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oxlsSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXlsBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXls);

                //マウスポインタを元に戻す
                this.Cursor = Cursors.Default;

                // toolStrip表示終了
                toolStripProgressBar1.Visible = false;
                toolStripStatusLabel1.Text = string.Empty;

                // 画面クローズ
                this.Close();
            }
        }

        ///-----------------------------------------------------------------
        /// <summary>
        ///     社員マスター登録済みのコードか調べる　</summary>
        /// <param name="hCode">
        ///     社員コード</param>
        /// <returns>
        ///     登録済み:true, 未登録:false</returns>
        ///-----------------------------------------------------------------
        private bool getEmployeeRec(int hCode)
        {
            bool rtn = false;

            var s = dts.社員所属.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                           a.社員番号 == hCode);
            if (s.Count() > 0) rtn = true;

            return rtn;
        }

        /// -------------------------------------------------------------------
        /// <summary>
        ///     所属・帳票区分対応テーブルから帳票区分を取得する </summary>
        /// <param name="szCode">
        ///     所属コード</param>
        /// <returns>
        ///     帳票区分</returns>
        /// -------------------------------------------------------------------
        private int getChohyoKbn(int szCode)
        {
            int rtn = 1;    // 該当所属コードが登録されていないときは本社扱い

            // データテーブルにデータを読み込む
            MTYSDataSetTableAdapters.所属帳票区分対応TableAdapter adp = new MTYSDataSetTableAdapters.所属帳票区分対応TableAdapter();
            adpMn.所属帳票区分対応TableAdapter = adp;
            adpMn.所属帳票区分対応TableAdapter.Fill(dts.所属帳票区分対応);

            var s = dts.所属帳票区分対応.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                                   a.所属コード == szCode);
            // 帳票区分を取得する
            foreach (var t in s)
            {
                rtn = t.帳票区分;
            }
            
            return rtn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmXlsToShain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            import_msEmployee(this.lblFileName.Text);
        }

        private void frmXlsToShain_Load(object sender, EventArgs e)
        {
            lblFileName.Text = string.Empty;
            button1.Enabled = false;
        }
    }
}
