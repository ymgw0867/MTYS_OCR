using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MTYS_OCR.Common;

namespace MTYS_OCR.OCR
{
    partial class frmPastData
    {
        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     過去勤務表データを画面に表示します </summary>
        /// <param name="iX">
        ///     個人番号</param>
        /// <param name="sYY">
        ///     年</param>
        /// <param name="sMM">
        ///     月</param>
        ///------------------------------------------------------------------------------------
        private void showPastData(int iX, int sYY, int sMM)
        {
            // フォーム初期化
            formInitialize(dID, iX);

            // 過去勤務票ヘッダテーブル行を取得
            var s = dts.過去勤務票ヘッダ.Where(a => a.個人番号 == iX && a.年 == sYY && a.月 == sMM);

            foreach (var r in s)
            {
                // 社員情報を取得します
                OCRData ocr = new OCRData();
                ocr.errCheckShainCode(dts, r.個人番号, out cSR);

                // ヘッダ情報表示
                txtYear.Text = Utility.EmptytoZero(r.年.ToString());
                txtMonth.Text = Utility.EmptytoZero(r.月.ToString());

                global.ChangeValueStatus = false;   // チェンジバリューステータス
                txtNo.Text = string.Empty;
                global.ChangeValueStatus = true;    // チェンジバリューステータス

                txtNo.Text = Utility.EmptytoZero(r.個人番号.ToString());    // 社員番号
                lblName.Text = r.氏名;
                lblFuri.Text = r.フリガナ;
                lblSzCode.Text = r.所属コード;
                label1.Text = r.所属名;

                // 帳票種別表示
                global gl = new global();
                label4.Text = r.帳票番号.ToString();

                if (r.帳票番号 > 0 && r.帳票番号 <= gl.arrayChohyoID.Length)
                {
                    label27.Text = gl.arrayChohyoID[r.帳票番号 - 1];
                }

                lblNissu.Text = r.出勤すべき日数.ToString();

                // 時間合計欄
                txtZangyoTL.Text = r.残業時.ToString() + ":" + r.残業分.ToString().PadLeft(2, '0');
                txtShinyaTL.Text = r.深夜時.ToString() + ":" + r.深夜分.ToString().PadLeft(2, '0');
                txtKyujitsuTL.Text = r.休日勤務時.ToString() + ":" + r.休日勤務分.ToString().PadLeft(2, '0');
                txtChikokuTL.Text = r.遅刻時間時.ToString() + ":" + r.遅刻時間分.ToString().PadLeft(2, '0');
                txtSoutaiTL.Text = r.早退時間時.ToString() + ":" + r.早退時間分.ToString().PadLeft(2, '0');

                // 日数合計欄
                txtKeisanDays.Text = r.計算用出勤日数.ToString();
                txtShukkin.Text = r.出勤日数合計.ToString() + "." + r.出勤日数2.ToString();
                txtYukyu.Text = r.有休日数合計.ToString() + "." + r.有休日数2.ToString();
                txtTokukyu.Text = r.特休日数合計.ToString();
                txtKekkin.Text = r.欠勤日数合計.ToString();
                txtSeiri.Text = r.生理分娩日数.ToString();
                txtJisa.Text = r.時差出勤日数.ToString();
                txtHoanH.Text = r.平日保安回数.ToString();
                txtHoanK.Text = r.休日保安回数.ToString();
                txtShukuH.Text = r.平日宿日直回数.ToString();
                txtShukuK.Text = r.休日宿日直回数.ToString();
                txt1L.Text = r._1L勤回数.ToString();
                txt2Kin.Text = r._2勤回数.ToString();
                txtMaru3Kin.Text = r.丸3勤回数.ToString();
                txt3Kin.Text = r._3勤回数.ToString();
                txtshukujitsu.Text = r.日祝日勤務回数.ToString();
                txtKoujyoNissu.Text = r.控除日数.ToString();
                            
                // 日別勤怠表示
                showItem(r.帳票番号.ToString(), r.ID, dGV, r.年.ToString(), r.月.ToString());
     
                // エラー情報表示初期化
                lblErrMsg.Visible = false;
                lblErrMsg.Text = string.Empty;

                // 画像表示
                ShowImage(global.pblImagePath + r.画像名.ToString());

            }
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     勤怠明細表示 </summary>
        /// <param name="sID">
        ///     帳票ID １：本社, ２：静岡, ３：大阪製造部</param>
        /// <param name="hID">
        ///     ヘッダID</param>
        /// <param name="sYY">
        ///     年</param>
        /// <param name="sMM">
        ///     月</param>
        /// <param name="dGV">
        ///     データグリッドビューオブジェクト</param>
        ///------------------------------------------------------------------------------------
        private void showItem(string sID, string hID, DataGridView dGV, string sYY, string sMM)
        {
            // 行数を設定して表示色を初期化
            dGV.RowCount = global._MULTIGYO;
            for (int i = 0; i < global._MULTIGYO; i++)
            {
                dGV.Rows[i].DefaultCellStyle.BackColor = Color.FromName("Control");
                dGV.Rows[i].ReadOnly = true;    // 初期設定は編集不可とする
            }

            // 行インデックス初期化
            int mRow = 0;

            // 日別勤務実績表示
            var h = dts.過去勤務票明細.Where(a => a.ヘッダID == hID).OrderBy(a => a.ID);

            foreach (var t in h)
            {
                // 表示色を初期化
                dGV.Rows[mRow].DefaultCellStyle.BackColor = Color.Empty;

                // 編集を可能とする
                dGV.Rows[mRow].ReadOnly = false;

                dGV[cDay, mRow].Value = t.日付;

                global.ChangeValueStatus = false;           // これ以下ChangeValueイベントを発生させない

                dGV[cKintai1, mRow].Value = t.勤怠記号1;
                dGV[cKintai2, mRow].Value = t.勤怠記号2;
                dGV[cSH, mRow].Value = t.開始時;
                dGV[cSM, mRow].Value = t.開始分;
                dGV[cEH, mRow].Value = t.終了時;
                dGV[cEM, mRow].Value = t.終了分;
                dGV[cZH, mRow].Value = t.時間外時;
                dGV[cZM, mRow].Value = t.時間外分;
                dGV[cKSH, mRow].Value = t.休日出勤時;
                dGV[cKSM, mRow].Value = t.休日出勤分;
                dGV[cSIH, mRow].Value = t.深夜時;
                dGV[cSIM, mRow].Value = t.深夜分;
                dGV[cSKEITAI, mRow].Value = t.出勤形態;
                dGV[cID, mRow].Value = t.ID.ToString();     // 明細ＩＤ

                global.ChangeValueStatus = true;            // ChangeValueStatusをtrueに戻す

                //---------------------------------------------------------------------
                //      休日表示
                //---------------------------------------------------------------------
                OCRData ocr = new OCRData();
                string sDate = sYY + "/" + sMM + "/" + t.日付;
                DateTime eDate;
                bool cHoliday = false;

                // 存在する日付のとき休日か判断する
                if (DateTime.TryParse(sDate, out eDate))
                {
                    // 大阪製造部勤務グループ
                    string oskg = string.Empty;

                    // 帳票区分
                    string cKbn = sID;

                    // 社員情報がnullのとき
                    if (cSR != null)
                    {
                        cKbn = cSR.帳票区分.ToString();
                        oskg = cSR.大阪製造部勤務グループ;
                    }

                    cHoliday = ocr.getHolidayStatus(dts, eDate, cKbn, oskg);
                }

                // 休日のとき色表示する
                if (cHoliday)
                {
                    dGV.Rows[mRow].DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else
                {
                    dGV.Rows[mRow].DefaultCellStyle.BackColor = Color.Empty;
                }
                
                #region 警告チェック実施
                //---------------------------------------------------------------------
                //      警告チェック
                //---------------------------------------------------------------------

                //// 警告配列を初期化
                //for (int i = 0; i < ocr.warArray.Length; i++)
                //{
                //    ocr.warArray[i] = global.flgOff;
                //}

                //// 警告表示色初期化
                //dGV[cKintai1, mRow].Style.BackColor = Color.Empty;
                //dGV[cKintai2, mRow].Style.BackColor = Color.Empty;
                //dGV[cSH, mRow].Style.BackColor = Color.Empty;
                //dGV[cSE, mRow].Style.BackColor = Color.Empty;
                //dGV[cSM, mRow].Style.BackColor = Color.Empty;
                //dGV[cEH, mRow].Style.BackColor = Color.Empty;
                //dGV[cEE, mRow].Style.BackColor = Color.Empty;
                //dGV[cEM, mRow].Style.BackColor = Color.Empty;
                //dGV[cZH, mRow].Style.BackColor = Color.Empty;
                //dGV[cZE, mRow].Style.BackColor = Color.Empty;
                //dGV[cZM, mRow].Style.BackColor = Color.Empty;
                //dGV[cSIH, mRow].Style.BackColor = Color.Empty;
                //dGV[cSIE, mRow].Style.BackColor = Color.Empty;
                //dGV[cSIM, mRow].Style.BackColor = Color.Empty;
                //dGV[cKSH, mRow].Style.BackColor = Color.Empty;
                //dGV[cKSE, mRow].Style.BackColor = Color.Empty;
                //dGV[cKSM, mRow].Style.BackColor = Color.Empty;
                //dGV[cSKEITAI, mRow].Style.BackColor = Color.Empty;

                //// 警告チェック実施
                //if (cSR != null)
                //{
                //    if (!ocr.warnCheck(t, cSR, cHoliday, ocr.warArray))
                //    {
                //        // 勤怠記号
                //        if (ocr.warArray[ocr.wKintaiKigou] == global.flgOn)
                //        {
                //            dGV[cKintai1, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cKintai2, mRow].Style.BackColor = Color.LightPink;
                //        }

                //        // 開始終了時刻
                //        if (ocr.warArray[ocr.wSEHM] == global.flgOn)
                //        {
                //            dGV[cSH, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cSE, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cSM, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cEH, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cEE, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cEM, mRow].Style.BackColor = Color.LightPink;
                //        }

                //        // 普通残業時価
                //        if (ocr.warArray[ocr.wZHM] == global.flgOn)
                //        {
                //            dGV[cZH, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cZE, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cZM, mRow].Style.BackColor = Color.LightPink;
                //        }

                //        // 深夜残業時間
                //        if (ocr.warArray[ocr.wSIHM] == global.flgOn)
                //        {
                //            dGV[cSIH, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cSIE, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cSIM, mRow].Style.BackColor = Color.LightPink;
                //        }

                //        // 休日勤務時間
                //        if (ocr.warArray[ocr.wKSHM] == global.flgOn)
                //        {
                //            dGV[cKSH, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cKSE, mRow].Style.BackColor = Color.LightPink;
                //            dGV[cKSM, mRow].Style.BackColor = Color.LightPink;
                //        }

                //        // 出勤形態
                //        if (ocr.warArray[ocr.wShukeitai] == global.flgOn)
                //        {
                //            dGV[cSKEITAI, mRow].Style.BackColor = Color.LightPink;
                //        }
                //    }

                //}
                #endregion
                
                // 行インデックス加算
                mRow++;
            }

            //カレントセル選択状態としない
            dGV.CurrentCell = null;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     画像を表示する </summary>
        /// <param name="pic">
        ///     pictureBoxオブジェクト</param>
        /// <param name="imgName">
        ///     イメージファイルパス</param>
        /// <param name="fX">
        ///     X方向のスケールファクター</param>
        /// <param name="fY">
        ///     Y方向のスケールファクター</param>
        ///------------------------------------------------------------------------------------
        private void ImageGraphicsPaint(PictureBox pic, string imgName, float fX, float fY, int RectDest, int RectSrc)
        {
            Image _img = Image.FromFile(imgName);
            Graphics g = Graphics.FromImage(pic.Image);

            // 各変換設定値のリセット
            g.ResetTransform();

            // X軸とY軸の拡大率の設定
            g.ScaleTransform(fX, fY);

            // 画像を表示する
            g.DrawImage(_img, RectDest, RectSrc);

            // 現在の倍率,座標を保持する
            global.ZOOM_NOW = fX;
            global.RECTD_NOW = RectDest;
            global.RECTS_NOW = RectSrc;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     フォーム表示初期化 </summary>
        /// <param name="sID">
        ///     過去データ表示時のヘッダID</param>
        /// <param name="cIx">
        ///     勤務票ヘッダカレントレコードインデックス</param>
        ///------------------------------------------------------------------------------------
        private void formInitialize(int sID, int cIx)
        {
            // テキストボックス表示色設定
            txtYear.BackColor = Color.White;
            txtMonth.BackColor = Color.White;
            txtNo.BackColor = Color.White;

            txtYear.ForeColor = Color.Navy;
            txtMonth.ForeColor = Color.Navy;
            txtNo.ForeColor = Color.Navy;

            // 社員情報表示欄
            lblName.Text = string.Empty;
            lblSzCode.Text = string.Empty;
            label4.Text = string.Empty;
            label1.Text = string.Empty;

            // 合計欄
            txtZangyoTL.BackColor = Color.White;
            txtShinyaTL.BackColor = Color.White;
            txtKyujitsuTL.BackColor = Color.White;
            txtChikokuTL.BackColor = Color.White;
            txtSoutaiTL.BackColor = Color.White;

            txtZangyoTL.ForeColor = Color.Navy;
            txtShinyaTL.ForeColor = Color.Navy;
            txtKyujitsuTL.ForeColor = Color.Navy;
            txtChikokuTL.ForeColor = Color.Navy;
            txtSoutaiTL.ForeColor = Color.Navy;

            // 月間日数
            txtShukkin.BackColor = Color.White;
            txtYukyu.BackColor = Color.White;
            txtTokukyu.BackColor = Color.White;
            txtKekkin.BackColor = Color.White;
            txtSeiri.BackColor = Color.White;
            txtJisa.BackColor = Color.White;
            txtHoanH.BackColor = Color.White;
            txtHoanK.BackColor = Color.White;
            txtShukuH.BackColor = Color.White;
            txtShukuK.BackColor = Color.White;
            txt1L.BackColor = Color.White;
            txt2Kin.BackColor = Color.White;
            txt3Kin.BackColor = Color.White;
            txtMaru3Kin.BackColor = Color.White;
            txtshukujitsu.BackColor = Color.White;
            txtKoujyoNissu.BackColor = Color.White;

            txtShukkin.ForeColor = Color.Navy;
            txtYukyu.ForeColor = Color.Navy;
            txtTokukyu.ForeColor = Color.Navy;
            txtKekkin.ForeColor = Color.Navy;
            txtSeiri.ForeColor = Color.Navy;
            txtJisa.ForeColor = Color.Navy;
            txtHoanH.ForeColor = Color.Navy;
            txtHoanK.ForeColor = Color.Navy;
            txtShukuH.ForeColor = Color.Navy;
            txtShukuK.ForeColor = Color.Navy;
            txt1L.ForeColor = Color.Navy;
            txt2Kin.ForeColor = Color.Navy;
            txt3Kin.ForeColor = Color.Navy;
            txtMaru3Kin.ForeColor = Color.Navy;
            txtshukujitsu.ForeColor = Color.Navy;
            txtKoujyoNissu.ForeColor = Color.Navy;

            lblNoImage.Visible = false;

            // ヘッダ情報
            txtYear.ReadOnly = true;
            txtMonth.ReadOnly = true;
            txtNo.ReadOnly = true;

            // 合計欄
            txtZangyoTL.ReadOnly = true;
            txtShinyaTL.ReadOnly = true;
            txtKyujitsuTL.ReadOnly = true;
            txtChikokuTL.ReadOnly = true;
            txtSoutaiTL.ReadOnly = true;

            // 月間日数
            txtShukkin.ReadOnly = true;
            txtYukyu.ReadOnly = true;
            txtTokukyu.ReadOnly = true;
            txtKekkin.ReadOnly = true;
            txtSeiri.ReadOnly = true;
            txtJisa.ReadOnly = true;
            txtHoanH.ReadOnly = true;
            txtHoanK.ReadOnly = true;
            txtShukuH.ReadOnly = true;
            txtShukuK.ReadOnly = true;
            txt1L.ReadOnly = true;
            txt2Kin.ReadOnly = true;
            txt3Kin.ReadOnly = true;
            txtMaru3Kin.ReadOnly = true;
            txtshukujitsu.ReadOnly = true;
            txtKoujyoNissu.ReadOnly = true;
                
            //データ数表示
            lblPage.Text = string.Empty;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     エラー表示 </summary>
        /// <param name="ocr">
        ///     OCRDATAクラス</param>
        ///------------------------------------------------------------------------------------
        private void ErrShow(OCRData ocr)
        {
            if (ocr._errNumber != ocr.eNothing)
            {
                // グリッドビューCellEnterイベント処理は実行しない
                gridViewCellEnterStatus = false;

                lblErrMsg.Visible = true;
                lblErrMsg.Text = ocr._errMsg;

                // 対象年月
                if (ocr._errNumber == ocr.eYearMonth)
                {
                    txtYear.BackColor = Color.Yellow;
                    txtMonth.BackColor = Color.Yellow;
                    txtYear.Focus();
                }

                // 対象月
                if (ocr._errNumber == ocr.eMonth)
                {
                    txtMonth.BackColor = Color.Yellow;
                    txtMonth.Focus();
                }

                // 個人番号
                if (ocr._errNumber == ocr.eShainNo)
                {
                    txtNo.BackColor = Color.Yellow;
                    txtNo.Focus();
                }

                // 日
                if (ocr._errNumber == ocr.eDay)
                {
                    dGV[cDay, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cDay, ocr._errRow];
                }

                // 勤怠記号1
                if (ocr._errNumber == ocr.eKintaiKigou1)
                {
                    dGV[cKintai1, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKintai1, ocr._errRow];
                }

                // 勤怠記号2
                if (ocr._errNumber == ocr.eKintaiKigou2)
                {
                    dGV[cKintai2, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKintai2, ocr._errRow];
                }

                // 開始時
                if (ocr._errNumber == ocr.eSH)
                {
                    dGV[cSH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSH, ocr._errRow];
                }

                // 開始分
                if (ocr._errNumber == ocr.eSM)
                {
                    dGV[cSM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSM, ocr._errRow];
                }

                // 終了時
                if (ocr._errNumber == ocr.eEH)
                {
                    dGV[cEH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cEH, ocr._errRow];
                }

                // 終了分
                if (ocr._errNumber == ocr.eEM)
                {
                    dGV[cEM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cEM, ocr._errRow];
                }

                // 時間外・時
                if (ocr._errNumber == ocr.eZH)
                {
                    dGV[cZH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cZH, ocr._errRow];
                }

                // 時間外・分
                if (ocr._errNumber == ocr.eZM)
                {
                    dGV[cZM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cZM, ocr._errRow];
                }

                // 深夜・時
                if (ocr._errNumber == ocr.eSIH)
                {
                    dGV[cSIH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSIH, ocr._errRow];
                }

                // 深夜・分
                if (ocr._errNumber == ocr.eSIM)
                {
                    dGV[cSIM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSIM, ocr._errRow];
                }
                
                // 休日出勤・時
                if (ocr._errNumber == ocr.eKSH)
                {
                    dGV[cKSH, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKSH, ocr._errRow];
                }

                // 休日出勤・分
                if (ocr._errNumber == ocr.eKSM)
                {
                    dGV[cKSM, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cKSM, ocr._errRow];
                }

                // 出勤形態
                if (ocr._errNumber == ocr.eShukeitai)
                {
                    dGV[cSKEITAI, ocr._errRow].Style.BackColor = Color.Yellow;
                    dGV.Focus();
                    dGV.CurrentCell = dGV[cSKEITAI, ocr._errRow];
                }

                // 普通残業合計
                if (ocr._errNumber == ocr.eZanTL)
                {
                    txtZangyoTL.BackColor = Color.Yellow;
                    txtZangyoTL.Focus();
                }

                // 深夜残業合計
                if (ocr._errNumber == ocr.eShinyaTL)
                {
                    txtShinyaTL.BackColor = Color.Yellow;
                    txtShinyaTL.Focus();
                }
                
                // 休日勤務合計
                if (ocr._errNumber == ocr.eKyujitsuTL)
                {
                    txtKyujitsuTL.BackColor = Color.Yellow;
                    txtKyujitsuTL.Focus();
                }

                // 遅刻時間合計
                if (ocr._errNumber == ocr.eChikokuTL)
                {
                    txtChikokuTL.BackColor = Color.Yellow;
                    txtChikokuTL.Focus();
                }

                // 早退時間合計
                if (ocr._errNumber == ocr.eSoutaiTL)
                {
                    txtSoutaiTL.BackColor = Color.Yellow;
                    txtSoutaiTL.Focus();
                }

                // 出勤日数
                if (ocr._errNumber == ocr.eShukkin)
                {
                    txtShukkin.BackColor = Color.Yellow;
                    txtShukkin.Focus();
                }

                // 有給日数
                if (ocr._errNumber == ocr.eYukyu)
                {
                    txtYukyu.BackColor = Color.Yellow;
                    txtYukyu.Focus();
                }
                
                // 特別休暇
                if (ocr._errNumber == ocr.eTokukyu)
                {
                    txtTokukyu.BackColor = Color.Yellow;
                    txtTokukyu.Focus();
                }

                // 欠勤日数
                if (ocr._errNumber == ocr.eKekkin)
                {
                    txtKekkin.BackColor = Color.Yellow;
                    txtKekkin.Focus();
                }

                // 生理分娩
                if (ocr._errNumber == ocr.eSeiri)
                {
                    txtSeiri.BackColor = Color.Yellow;
                    txtSeiri.Focus();
                }
                
                // 時差出勤（静岡２直）
                if (ocr._errNumber == ocr.eJisa)
                {
                    txtJisa.BackColor = Color.Yellow;
                    txtJisa.Focus();
                }
                
                // 保安回数・平日
                if (ocr._errNumber == ocr.eHoanH)
                {
                    txtHoanH.BackColor = Color.Yellow;
                    txtHoanH.Focus();
                }

                // 保安回数・休日
                if (ocr._errNumber == ocr.eHoanK)
                {
                    txtHoanK.BackColor = Color.Yellow;
                    txtHoanK.Focus();
                }

                // 宿日直回数・平日
                if (ocr._errNumber == ocr.eShukuH)
                {
                    txtShukuH.BackColor = Color.Yellow;
                    txtShukuH.Focus();
                }

                // 宿日直回数・休日
                if (ocr._errNumber == ocr.eShukuK)
                {
                    txtShukuK.BackColor = Color.Yellow;
                    txtShukuK.Focus();
                }

                // 大阪・１L勤回数
                if (ocr._errNumber == ocr.e1Lkin)
                {
                    txt1L.BackColor = Color.Yellow;
                    txt1L.Focus();
                }

                // 大阪・2勤回数
                if (ocr._errNumber == ocr.e2kin)
                {
                    txt2Kin.BackColor = Color.Yellow;
                    txt2Kin.Focus();
                }

                // 大阪・丸３勤回数
                if (ocr._errNumber == ocr.eMaru3kin)
                {
                    txtMaru3Kin.BackColor = Color.Yellow;
                    txtMaru3Kin.Focus();
                }

                // 大阪・3勤回数
                if (ocr._errNumber == ocr.e3kin)
                {
                    txt3Kin.BackColor = Color.Yellow;
                    txt3Kin.Focus();
                }

                // 大阪・日祝日勤務回数
                if (ocr._errNumber == ocr.eShukujitsu)
                {
                    txtshukujitsu.BackColor = Color.Yellow;
                    txtshukujitsu.Focus();
                }

                // 控除日数
                if (ocr._errNumber == ocr.eKoujo)
                {
                    txtKoujyoNissu.BackColor = Color.Yellow;
                    txtKoujyoNissu.Focus();
                }

                // グリッドビューCellEnterイベントステータスを戻す
                gridViewCellEnterStatus = true;

            }
        }
    }
}
