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
    public partial class frmHolidayBatch : Form
    {
        // マスター名
        string msName = "休日一括登録";

        // フォームモードインスタンス
        Utility.frmMode fMode = new Utility.frmMode();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        // データテーブル生成
        MTYSDataSet dts = new MTYSDataSet();

        // 休日マスターテーブルアダプター生成
        MTYSDataSetTableAdapters.休日TableAdapter adp = new MTYSDataSetTableAdapters.休日TableAdapter();

        int C_SUNDAY = 0;   // 日曜日
        int C_SATURDAY = 6; // 土曜日

        public frmHolidayBatch()
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

            // 画面初期化
            DispInitial();
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            dt1.Value = DateTime.Today;
            dt2.Value = DateTime.Today;
            chkSun.CheckState = CheckState.Unchecked;
            chkSat.CheckState = CheckState.Unchecked;
            chkShuku.CheckState = CheckState.Unchecked;

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

            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //エラーチェック
            if (!fDataCheck()) return;

            // 確認
            if (MessageBox.Show("休日一括登録を行います。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                return;

            // データセットにデータを追加します
            int n = addHolidayBatch();
                    
            // 確認表示
            MessageBox.Show(n.ToString() + "件登録しました","休日一括登録",MessageBoxButtons.OK,MessageBoxIcon.Information);

            // データセットの内容をデータベースへ反映させます。
            if (n > 0) adpMn.UpdateAll(dts);

            // 閉じる
            this.Close();
        }

        ///-------------------------------------------------------------------
        /// <summary>
        ///     休日マスター追加 </summary>
        /// <returns>
        ///     登録件数</returns>
        /// ------------------------------------------------------------------
        private int addHolidayBatch()
        {
            int cnt = 0;

            for (DateTime dt = dt1.Value; dt <= dt2.Value; dt = dt.AddDays(1))
            {
                // 休日摘要初期化
                string teki = string.Empty;

                // 祝日の設定
                if (chkShuku.Checked)
                {
                    string mmdd = dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

                    foreach (var item in global.sDay)
                    {
                        // 祝日の配列に一致する日付があった場合
                        if (item.Substring(0, 5) == mmdd)
                        {
                            teki = item.Substring(5, item.Length - 5);  // 祝日名を取得
                            break;
                        }
                    }
                }

                // 祝日以外の日曜日または土曜日のとき
                if (teki == string.Empty)
                {
                    if ((chkSun.Checked && (int)dt.DayOfWeek == 0) || (chkSat.Checked && (int)dt.DayOfWeek == 6))
                    {
                        // 摘要の設定
                        if ((int)dt.DayOfWeek == C_SUNDAY) teki = "日曜";
                        if ((int)dt.DayOfWeek == C_SATURDAY) teki = "土曜";
                    }
                }

                // 未登録の日付か
                var s = dts.休日.Where(a => a.年月日 == dt && a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached);
                if (s.Count() == 0)
                {
                    // データセットにデータを追加します
                    if (teki != string.Empty)
                    {
                        dts.休日.Add休日Row(dt, Utility.checkToInt(chkHon), Utility.checkToInt(chkShiz), Utility.checkToInt(chkOsk),
                                            Utility.checkToInt(chkOskA), Utility.checkToInt(chkOskB), Utility.checkToInt(chkOskC),
                                            Utility.checkToInt(chkOskD), Utility.checkToInt(chkAmiH), Utility.checkToInt(chkAmiS),
                                            Utility.checkToInt(chkAmiO), Utility.checkToInt(chkAmiA), Utility.checkToInt(chkAmiB),
                                            Utility.checkToInt(chkAmiC), Utility.checkToInt(chkAmiD), teki, string.Empty,
                                            DateTime.Now);
                        cnt++;
                    }
                }
            }

            return cnt;
        }


        //登録データチェック
        private Boolean fDataCheck()
        {
            try
            {
                // 期間
                if (dt1.Value > dt2.Value)
                {
                    dt1.Focus();
                    throw new Exception("設定期間が正しくありません");
                }

                // 曜日選択
                if (chkSun.CheckState == CheckState.Unchecked && chkSat.CheckState == CheckState.Unchecked && chkShuku.CheckState == CheckState.Unchecked)
                {
                    chkSun.Focus();
                    throw new Exception("曜日を選択してください");
                }

                // 事業所選択
                if (chkHon.CheckState == CheckState.Unchecked && chkShiz.CheckState == CheckState.Unchecked && 
                    chkOsk.CheckState == CheckState.Unchecked && chkOskA.CheckState == CheckState.Unchecked && 
                    chkOskB.CheckState == CheckState.Unchecked && chkOskC.CheckState == CheckState.Unchecked && 
                    chkOskD.CheckState == CheckState.Unchecked)
                {
                    chkHon.Focus();
                    throw new Exception("事業所を選択してください");
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

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            dt1.Focus();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShukujitsu frm = new frmShukujitsu();
            frm.ShowDialog();
        }
    }
}
