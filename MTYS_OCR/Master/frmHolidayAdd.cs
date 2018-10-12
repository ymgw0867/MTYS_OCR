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
    public partial class frmHolidayAdd : Form
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

        public frmHolidayAdd(DateTime dt)
        {
            InitializeComponent();

            // データテーブルにデータを読み込む
            adpMn.休日TableAdapter = adp;
            adpMn.休日TableAdapter.Fill(dts.休日);

            // 日付
            _dt = dt;
            txtDt.Text = _dt.ToShortDateString();
        }

        DateTime _dt = DateTime.Today;

        private void frm_Load(object sender, EventArgs e)
        {
            // フォーム最大サイズ
            Utility.WindowsMaxSize(this, this.Width, this.Height);

            // フォーム最小サイズ
            Utility.WindowsMinSize(this, this.Width, this.Height);

            // 画面初期化
            DispInitial();

            // データ表示
            GridEnter(_dt);
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
        /// 画面の初期化
        /// </summary>
        private void DispInitial()
        {
            //dtHoliday.Value = DateTime.Today;
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

            //dtHoliday.Enabled = true;
            //dtHoliday.Focus();
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
                    if (MessageBox.Show(_dt.ToShortDateString() + "を登録します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセットにデータを追加します
                    dts.休日.Add休日Row(_dt, Utility.checkToInt(chkHon), Utility.checkToInt(chkShiz),
                                       Utility.checkToInt(chkOsk), Utility.checkToInt(chkOskA), Utility.checkToInt(chkOskB),
                                       Utility.checkToInt(chkOskC), Utility.checkToInt(chkOskD), Utility.checkToInt(chkAmiH),
                                       Utility.checkToInt(chkAmiS), Utility.checkToInt(chkAmiO), Utility.checkToInt(chkAmiA),
                                       Utility.checkToInt(chkAmiB), Utility.checkToInt(chkAmiC), Utility.checkToInt(chkAmiD),
                                       cmbTekiyou.Text, txtMemo.Text, DateTime.Now);
                    
                    break;

                // 更新処理
                case global.FORM_EDITMODE:

                    // 確認
                    if (MessageBox.Show(_dt.ToShortDateString() + "を更新します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) 
                        return;

                    // データセット更新
                    var r = dts.休日.Single(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached && 
                                               a.年月日 == _dt);
                    
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
                        MessageBox.Show("データの更新に失敗しました","更新エラー",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                    break;

                default:
                    break;
            }

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
                    var s = dts.休日.Where(a => a.年月日 == _dt);
                    if (s.Count() > 0)
                    {
                        txtDt.Focus();
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

        /// ---------------------------------------------------------------
        /// <summary>
        ///     休日データ表示処理 </summary>
        /// <param name="dt">
        ///     年月日</param>
        /// ---------------------------------------------------------------
        private void GridEnter(DateTime dt)
        {
            //// 対象となるデータテーブルROWを取得します
            //MTYSDataSet.休日Row sQuery = dts.休日.FindByID(int.Parse(dg[0, fMode.rowIndex].Value.ToString()));

            // 対象となる休日データを取得します
            var s = dts.休日.Where(a => a.RowState != DataRowState.Deleted && a.RowState != DataRowState.Detached &&
                                        a.年月日 == dt);

            // 登録済みのデータ
            if (s.Count() != 0)
            {
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

                    btnDel.Enabled = true;
                    btnClear.Enabled = true;
                }

                // モードステータスを「編集モード」にします
                fMode.Mode = global.FORM_EDITMODE;
            }
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
                // 画面データ消去
                DispInitial();
            }
        }

        private void frmKintaiKbn_Shown(object sender, EventArgs e)
        {
            
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

        private void chkHon_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
