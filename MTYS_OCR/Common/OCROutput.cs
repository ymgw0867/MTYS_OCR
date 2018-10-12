using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MTYS_OCR.Common
{
    ///------------------------------------------------------------------
    /// <summary>
    ///     給与計算受け渡しデータクラス </summary>
    ///     
    ///------------------------------------------------------------------
    class OCROutput
    {
        #region フィールド定義
        // 親フォーム
        Form _preForm;

        // 休日出勤の7.5時間勤務（分）
        const int work75 = 450;
        #endregion

        #region データテーブルインスタンス
        MTYSDataSet.勤務票ヘッダDataTable _hTbl;
        MTYSDataSet.勤務票明細DataTable _mTbl;
        #endregion

        private const string TXTFILE_SHAIN = "社員勤怠";

        MTYSDataSet _dts = new MTYSDataSet();
        MTYSDataSetTableAdapters.勤怠チェックリストTableAdapter cAdp = new MTYSDataSetTableAdapters.勤怠チェックリストTableAdapter();
        MTYSDataSetTableAdapters.社員所属TableAdapter sAdp = new MTYSDataSetTableAdapters.社員所属TableAdapter();

        #region 給与計算用受入データプロパティ
        //---------------------------------------------------
        //      給与計算用受入データプロパティ
        //---------------------------------------------------
        /// <summary>汎用データ：社員番号</summary>
        public string sShainNo { get; private set; }

        /// <summary>氏名</summary>
        public string sName { get; private set; }

        /// <summary>フリガナ</summary>
        public string sFuri { get; private set; }

        /// <summary>出勤すべき日数</summary>
        public string sShukkinSubekiDays { get; private set; }

        /// <summary>出勤日数</summary>
        public string sShukkinDays { get; private set; }

        /// <summary>欠勤日数</summary>
        public string sKekkinDays { get; private set; }

        /// <summary>有休日数</summary>
        public string sYuKyuDays { get; private set; }

        /// <summary>特別休暇日数</summary>
        public string sTokuKyuDays { get; private set; }

        /// <summary>生理分娩日数</summary>
        public string sSeiriDays { get; private set; }

        /// <summary>遅刻時間</summary>
        public string sChikokuH { get; private set; }

        /// <summary>遅刻分</summary>
        public string sChikokuM { get; private set; }

        /// <summary>早退時間</summary>
        public string sSoutaiH { get; private set; }

        /// <summary>早退分</summary>
        public string sSoutaiM { get; private set; }

        /// <summary>普通残業時間</summary>
        public string sZanH { get; private set; }

        /// <summary>普通残業分</summary>
        public string sZanM { get; private set; }

        /// <summary>深夜時間</summary>
        public string sShinyaH { get; private set; }

        /// <summary>深夜分</summary>
        public string sShinyaM { get; private set; }

        /// <summary>休日出勤時間</summary>
        public string sKyujitsuH { get; private set; }

        /// <summary>休日出勤分</summary>
        public string sKyujitsuM { get; private set; }

        /// <summary>宿日直日数・平日 ※本社・静岡</summary>
        public string sShukuH { get; private set; }

        /// <summary>宿日直日数・休日 ※本社・静岡</summary>
        public string sShukuK { get; private set; }

        /// <summary>保安日数・平日</summary>
        public string sHoanH { get; private set; }

        /// <summary>保安日数・休日</summary>
        public string sHoanK { get; private set; }

        /// <summary>２直日数(時差出勤）※静岡のみ</summary>
        public string sJisaDays { get; private set; }
        
        /// <summary>1L勤日数 ※大阪製造部のみ</summary>
        public string s1LKinDays { get; private set; }

        /// <summary>2勤日数 ※大阪製造部のみ</summary>
        public string s2KinDays { get; private set; }

        /// <summary>丸３勤日数 ※大阪製造部のみ</summary>
        public string sMaru3KinDays { get; private set; }

        /// <summary>3勤日数 ※大阪製造部のみ</summary>
        public string s3KinDays { get; private set; }

        /// <summary>日祝日勤務日数 ※大阪製造部のみ</summary>
        public string sShukujitsuDays { get; private set; }

        /// <summary>控除日数</summary>
        public string sKoujyoDays { get; private set; }

        /// <summary>残業60H超時間・平日</summary>
        public string sZan60HH { get; private set; }

        /// <summary>残業60H超分・平日</summary>
        public string sZan60MH { get; private set; }

        /// <summary>残業60H超時間・休日</summary>
        public string sZan60HK { get; private set; }

        /// <summary>残業60H超分・休日</summary>
        public string sZan60MK { get; private set; }        
        #endregion

        ///--------------------------------------------------------------------------
        /// <summary>
        ///     給与計算用計算用受入データ作成クラスコンストラクタ</summary>
        /// <param name="preFrm">
        ///     親フォーム</param>
        /// <param name="hTbl">
        ///     勤務票ヘッダDataTable</param>
        /// <param name="mTbl">
        ///     勤務票明細DataTable</param>
        ///--------------------------------------------------------------------------
        public OCROutput(Form preFrm, MTYSDataSet dts)
        {
            _preForm = preFrm;
            _dts = dts;
            _hTbl = dts.勤務票ヘッダ;
            _mTbl = dts.勤務票明細;
            cAdp.Fill(_dts.勤怠チェックリスト);
        }

        ///--------------------------------------------------------------------------------------
        /// <summary>
        ///     給与計算用受入データ作成</summary>
        ///--------------------------------------------------------------------------------------     
        public void SaveData()
        {
            #region 出力配列
            string[] arrayShain = null;     // 出力配列
            #endregion

            #region 出力件数変数
            int sCnt = 0;   // 社員出力件数
            #endregion

            Boolean pblFirstGyouFlg = true;
            string wID = string.Empty;

            // 出力先フォルダがあるか？なければ作成する
            string cPath = global.cnfPath;
            if (!System.IO.Directory.Exists(cPath)) System.IO.Directory.CreateDirectory(cPath);

            try
            {
                //オーナーフォームを無効にする
                _preForm.Enabled = false;

                //プログレスバーを表示する
                frmPrg frmP = new frmPrg();
                frmP.Owner = _preForm;
                frmP.Show();

                // 勤務票ヘッダレコード件数取得
                int cTotal = _hTbl.Count();
                int rCnt = 1;

                // 伝票最初行フラグ
                pblFirstGyouFlg = true;

                // 勤務票ヘッダデータ取得
                var s = _hTbl.OrderBy(a => a.ID);

                foreach (var r in s)
                {
                    // プログレスバー表示
                    frmP.Text = "給与計算用受入データ作成中です・・・" + rCnt.ToString() + "/" + cTotal.ToString();
                    frmP.progressValue = rCnt * 100 / cTotal;
                    frmP.ProgressStep();

                    // 出力データ初期化
                    InitOutRec();

                    // 社員番号
                    sShainNo = r.個人番号.ToString().PadLeft(global.ShainMaxLength, '0');

                    // 集計処理
                    setDataShain(r);

                    // 配列にデータを出力
                    sCnt++;
                    Array.Resize(ref arrayShain, sCnt);
                    arrayShain[sCnt - 1] = getTextShainData();

                    // データ件数加算
                    rCnt++;

                    pblFirstGyouFlg = false;
                }

                // 勤怠テキストファイル出力
                if (arrayShain != null) txtFileWrite(cPath + TXTFILE_SHAIN + ".csv", arrayShain);

                // いったんオーナーをアクティブにする
                _preForm.Activate();

                // 進行状況ダイアログを閉じる
                frmP.Close();

                // オーナーのフォームを有効に戻す
                _preForm.Enabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("給与計算用受入データ作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
            finally
            {
                //if (OutData.sCom.Connection.State == ConnectionState.Open) OutData.sCom.Connection.Close();
            }
        }
        
        ///----------------------------------------------------------------------------
        /// <summary>
        ///     配列にテキストデータをセットする </summary>
        /// <param name="array">
        ///     社員、パート、出向社員の各配列</param>
        /// <param name="cnt">
        ///     拡張する配列サイズ</param>
        /// <param name="txtData">
        ///     セットする文字列</param>
        ///----------------------------------------------------------------------------
        private void txtArraySet(string [] array, int cnt, string txtData)
        {
            Array.Resize(ref array, cnt);   // 配列のサイズ拡張
            array[cnt - 1] = txtData;       // 文字列のセット
        }


        ///----------------------------------------------------------------------------
        /// <summary>
        ///     テキストファイルを出力する</summary>
        /// <param name="outFilePath">
        ///     パスを含む出力ファイル名</param>
        /// <param name="arrayData">
        ///     書き込む配列データ</param>
        ///----------------------------------------------------------------------------
        private void txtFileWrite(string outFilePath, string [] arrayData)
        {
            // 出力ファイルが存在するとき
            if (System.IO.File.Exists(outFilePath))
            {
                // リネーム付加文字列（タイムスタンプ）
                string newFileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                     DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                                     DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0'); 

                // リネーム後ファイル名
                string reFileName = Path.GetDirectoryName(outFilePath) + @"\" + Path.GetFileNameWithoutExtension(outFilePath) + newFileName + ".csv";

                // 確認表示
                MessageBox.Show(outFilePath + "は既に存在しています。" + Environment.NewLine + Environment.NewLine + 
                    "登録済みファイルは名前を以下のように変更して保存します。" + Environment.NewLine + Environment.NewLine +
                "現）" + outFilePath + Environment.NewLine + 
                "新）" + reFileName, "既存ファイルの名前変更", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 既存のファイルをリネーム
                File.Move(outFilePath, reFileName);
            }

            // テキストファイル出力
            File.WriteAllLines(outFilePath, arrayData, System.Text.Encoding.GetEncoding(932));
        }
        
        ///----------------------------------------------------------------------------------
        /// <summary>
        ///     給与計算用受入データ集計処理</summary>
        /// <param name="m">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        ///---------------------------------------------------------------------------------- 
        private void setDataShain(MTYSDataSet.勤務票ヘッダRow r)
        {
            try
            {
                sShainNo = r.個人番号.ToString();
                sName = r.氏名;
                sFuri = Utility.NulltoStr(r.フリガナ);
                sShukkinSubekiDays = (r.計算用出勤日数 - r.生理分娩日数).ToString();

                // 2014/10/10 計算用日数で出勤日数を再計算する
                double sDays = getShukkinDays(r);
                int d1 = (int)System.Math.Floor(sDays);
                int d2 = (int)((sDays * 10) % 10);
                sShukkinDays = d1.ToString() + "." + d2.ToString();
                
                sKekkinDays = r.欠勤日数合計.ToString() + ".0";
                sYuKyuDays = r.有休日数合計.ToString() + "." + r.有休日数2.ToString();
                sTokuKyuDays = r.特休日数合計.ToString() + ".0";
                sSeiriDays = r.生理分娩日数.ToString() + ".0";
                sChikokuH = hhmmTo30min(r.遅刻時間時, r.遅刻時間分);
                sSoutaiH = hhmmTo30min(r.早退時間時, r.早退時間分);
                sZanH = hhmmTo30min(r.残業時, r.残業分);
                sShinyaH = hhmmTo30min(r.深夜時, r.深夜分);
                sKyujitsuH = hhmmTo30min(r.休日勤務時, r.休日勤務分);
                sShukuH = r.平日宿日直回数.ToString();
                sShukuK = r.休日宿日直回数.ToString();
                sHoanH = r.平日保安回数.ToString();
                sHoanK = r.休日保安回数.ToString();
                sJisaDays = r.時差出勤日数.ToString();
                s1LKinDays = r._1L勤回数.ToString();
                s2KinDays = r._2勤回数.ToString();
                sMaru3KinDays = r.丸3勤回数.ToString();
                s3KinDays = r._3勤回数.ToString();
                sShukujitsuDays = r.日祝日勤務回数.ToString();
                sKoujyoDays = r.控除日数.ToString();

                // 60時間超残業・日曜以外休日勤務時間を取得
                int zan60 = 0;
                int kyujitsu60 = 0;
                getOver60ZanTime(r, out zan60, out kyujitsu60);

                // 残業60H超・平日
                if (zan60 > 0)
                {
                    sZan60HH = (zan60 / 60).ToString();
                    sZan60MH = (zan60 - (Utility.StrtoInt(sZan60HH) * 60)).ToString();
                    sZan60HH = sZan60HH + "." + sZan60MH.PadLeft(2, '0');
                }
                else
                {
                    sZan60HH = "0.00";
                }

                // 残業60H超・休日
                if (kyujitsu60 > 0)
                {
                    sZan60HK = (kyujitsu60 / 60).ToString();
                    sZan60MK = (kyujitsu60 - (Utility.StrtoInt(sZan60HK) * 60)).ToString();
                    sZan60HK = sZan60HK + "." + sZan60MK.PadLeft(2, '0');
                }
                else
                {
                    sZan60HK = "0.00";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("給与計算用受入データ作成中" + Environment.NewLine + e.Message, "エラー", MessageBoxButtons.OK);
            }
        }

        ///----------------------------------------------------------------------
        /// <summary>
        ///     計算用出勤日数で出勤日数を算出する </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <returns>
        ///     出勤日数</returns>
        ///----------------------------------------------------------------------
        private double getShukkinDays(MTYSDataSet.勤務票ヘッダRow r)
        {
            double sVal = 0;
            double sShDays = r.計算用出勤日数;
            double sSeDays = r.生理分娩日数;
            double sKeDays = r.欠勤日数合計;
            double sYukyu = 0;
            double sYukyu1 = r.有休日数合計;
            double sYukyu2 = r.有休日数2;
            double sTokDays = r.特休日数合計;

            // 小数点以下あり有給日数
            sYukyu = (sYukyu1 * 10 + sYukyu2) / 10;

            // 出勤日数算出
            sVal = sShDays - sSeDays - sKeDays - sYukyu - sTokDays;

            return sVal;
        }

        ///------------------------------------------------------------------------
        /// <summary>
        ///     時分を３０分単位で変換して文字列で返す </summary>
        /// <param name="hh">
        ///     時間</param>
        /// <param name="mm">
        ///     分</param>
        /// <returns>
        ///     0.00（時.分）※30分は0.5</returns>
        ///------------------------------------------------------------------------
        private string hhmmTo30min(int hh, int mm)
        {
            if (mm < 15) mm = 0;
            else if (mm < 45) mm = 5;
            else
            {
                hh++;
                mm = 0;
            }

            return  hh.ToString() + "." + mm.ToString().PadRight(2, '0');
        }

        //-------------------------------------------------------------------------
        /// <summary>
        ///     社員勤怠.TXT レコード文字列作成</summary>
        /// <returns>
        ///     社員勤怠.TXT レコード文字列</returns>
        ///------------------------------------------------------------------------
        private string getTextShainData()
        {
            return getShainShukkoText();
        }

        //-------------------------------------------------------------------------
        /// <summary>
        ///     社員勤怠.TXT・出向社員.TXT レコード文字列作成</summary>
        /// <returns>
        ///     社員勤怠.TXT, 出向社員.TXT レコード文字列</returns>
        ///------------------------------------------------------------------------
        private string getShainShukkoText()
        {
            //出力文字列作成
            StringBuilder sb = new StringBuilder();
            sb.Append(sShainNo).Append(",");
            sb.Append(sName).Append(",");
            sb.Append(sFuri).Append(",");
            sb.Append(sShukkinSubekiDays).Append(",");
            sb.Append(sShukkinDays).Append(",");
            sb.Append(sKekkinDays).Append(",");
            sb.Append(sYuKyuDays).Append(",");
            sb.Append(sTokuKyuDays).Append(",");
            sb.Append(sSeiriDays).Append(",");
            sb.Append(sChikokuH).Append(",");
            sb.Append(sSoutaiH).Append(",");
            sb.Append(sZanH).Append(",");
            sb.Append(sShinyaH).Append(",");
            sb.Append(sKyujitsuH).Append(",");
            sb.Append(sShukuH).Append(",");
            sb.Append(sShukuK).Append(",");
            sb.Append(sHoanH).Append(",");
            sb.Append(sHoanK).Append(",");
            sb.Append(sJisaDays).Append(",");
            sb.Append(s1LKinDays).Append(",");
            sb.Append(s2KinDays).Append(",");
            sb.Append(sMaru3KinDays).Append(",");
            sb.Append(s3KinDays).Append(",");
            sb.Append(sShukujitsuDays).Append(",");
            sb.Append(sKoujyoDays).Append(",");
            sb.Append(sZan60HH).Append(",");
            sb.Append(sZan60HK);

            return sb.ToString();
        }

        ///--------------------------------------------------------------
        /// <summary>
        ///     給与計算用受入データ初期化</summary>
        ///--------------------------------------------------------------
        private void InitOutRec()
        {
            sShainNo = string.Empty;            // 社員番号
            sName = string.Empty;               // 氏名
            sShainNo = string.Empty;            // フリガナ
            sShukkinSubekiDays = global.FLGOFF; // 出勤すべき日数
            sShukkinDays = global.FLGOFF;       // 出勤日数
            sKekkinDays = global.FLGOFF;        // 欠勤日数
            sYuKyuDays = global.FLGOFF;         // 有休日数
            sTokuKyuDays = global.FLGOFF;       // 特別休暇日数
            sSeiriDays = global.FLGOFF;         // 生理分娩日数
            sChikokuH = global.FLGOFF;          // 遅刻時間
            sChikokuM = global.FLGOFF;          // 遅刻分
            sSoutaiH = global.FLGOFF;           // 早退時間
            sSoutaiM = global.FLGOFF;           // 早退分
            sZanH = global.FLGOFF;              // 遅刻時間
            sZanM = global.FLGOFF;              // 遅刻分
            sShinyaH = global.FLGOFF;           // 深夜時間
            sShinyaM = global.FLGOFF;           // 深夜分
            sKyujitsuH = global.FLGOFF;         // 休日勤務時間
            sKyujitsuM = global.FLGOFF;         // 休日勤務分
            sShukuH = global.FLGOFF;            // 宿日直日数・平日 ※本社・静岡
            sShukuK = global.FLGOFF;            // 宿日直日数・平日 ※本社・静岡
            sHoanH = global.FLGOFF;             // 保安日数・平日
            sHoanK = global.FLGOFF;             // 保安日数・休日
            sJisaDays = global.FLGOFF;          // ２直日数(時差出勤）※静岡のみ
            s1LKinDays = global.FLGOFF;         // 1L勤日数 ※大阪製造部のみ
            s2KinDays = global.FLGOFF;          // 2勤日数 ※大阪製造部のみ
            sMaru3KinDays = global.FLGOFF;      // 丸３勤日数 ※大阪製造部のみ
            s3KinDays = global.FLGOFF;          // ３勤日数 ※大阪製造部のみ
            sShukujitsuDays = global.FLGOFF;    // 日祝日勤務日数 ※大阪製造部のみ
            sKoujyoDays = global.FLGOFF;        // 控除日数
            sZan60HH = global.FLGOFF;           // 残業60H超時間・平日
            sZan60MH = global.FLGOFF;           // 残業60H超分・平日
            sZan60HK = global.FLGOFF;           // 残業60H超時間・休日
            sZan60MK = global.FLGOFF;           // 残業60H超分・休日
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        ///     該当月1日からの「累計残業時間及び日曜を除く休日勤務時間」が60Ｈを超えた日以降の平日の
        ///     残業時間数および日曜を除く休日勤務時間数を算出する　</summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="zan60">
        ///     60Ｈを超えた日以降の平日の残業時間数</param>
        /// <param name="kyujitsu60">
        ///     60Ｈを超えた日以降の日曜を除く休日勤務時間数</param>
        /// <returns>
        ///     true:終了</returns>
        /// ------------------------------------------------------------------------------------------
        private bool getOver60ZanTime(MTYSDataSet.勤務票ヘッダRow r, out int zan60, out int kyujitsu60)
        {
            int zanKyu = 0;
            int zan = 0;
            int kyujitsu = 0;
            bool over60 = false;
            DateTime eDate;
            string w = string.Empty;

            zan60 = 0;      // 残業60H超・平日
            kyujitsu60 = 0; // 残業60H超・休日

            var s = _mTbl.Where(a => a.ヘッダID == r.ID).OrderBy(a => a.ID);

            foreach (var t in s)
            {
                string sDate = r.年.ToString() + "/" + r.月.ToString() + "/" + t.日付;

                // 存在する日付と認識された場合
                if (DateTime.TryParse(sDate, out eDate))
                {
                    // 曜日を取得する
                    w = ("日月火水木金土").Substring(int.Parse(eDate.DayOfWeek.ToString("d")), 1);

                    zan = Utility.StrtoInt(t.時間外時) * 60 + Utility.StrtoInt(t.時間外分); 

                    // 残業時間有り
                    if (zan > 0)
                    {
                        // 残業時間加算
                        zanKyu += zan;

                        // 60H超チェック
                        if (zanKyu > 3600)
                        {
                            // 2015/10/29 土曜日が平日扱いの日を条件に追加
                            if (w == "月" || w == "火" || w == "水" || w == "木" || w == "金" || w == "土")
                            {
                                if (!over60)
                                {
                                    // 今、60Hを超えたので60Hを超えた分をセット
                                    zan60 = zanKyu - 3600;

                                    // ステータスを60H超えにする
                                    over60 = true;
                                }
                                else
                                {
                                    // 既に60Hを超えているのでそのまま60H超平日残業時間に加算
                                    zan60 += zan;
                                }
                            }
                        }
                    }

                    // 日曜日以外の休日勤務時間数
                    if (w != "日")
                    {
                        kyujitsu = Utility.StrtoInt(t.休日出勤時) * 60 + Utility.StrtoInt(t.休日出勤分);

                        if (kyujitsu > 0)
                        {
                            // 休日勤務時間加算
                            zanKyu += kyujitsu;

                            // 60H超チェック
                            if (zanKyu > 3600)
                            {
                                if (!over60)
                                {
                                    // 今、60Hを超えたので60Hを超えた分をセット
                                    kyujitsu60 = zanKyu - 3600;

                                    // ステータスを60H超えにする
                                    over60 = true;
                                }
                                else
                                {
                                    // 既に60Hを超えているのでそのまま60H超休日勤務時間に加算
                                    kyujitsu60 += kyujitsu;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        ///------------------------------------------------------------------------------
        /// <summary>
        ///     CSVデータを勤怠チェックリストテーブルへ取込 </summary>
        ///------------------------------------------------------------------------------
        public void writeChecklist()
        {
            //CSVデータを勤怠チェックリストテーブルへ取込
            var s = System.IO.File.ReadAllLines(global.cnfPath + TXTFILE_SHAIN + ".csv", Encoding.Default);
            foreach (var stBuffer in s)
            {
                // カンマ区切りで分割して配列に格納する
                string[] stCSV = stBuffer.Split(',');

                // 登録済みなら削除する
                MTYSDataSet.勤怠チェックリストRow rr = _dts.勤怠チェックリスト.FindBy年月社員番号(global.cnfYear, global.cnfMonth, Utility.StrtoInt(stCSV[0]));
                if (rr != null)
                {
                    rr.Delete();
                    cAdp.Update(_dts.勤怠チェックリスト);
                }

                // レコード追加処理
                _dts.勤怠チェックリスト.Add勤怠チェックリストRow(setNewListRecRow(stCSV));
            }

            // データベース更新
            cAdp.Update(_dts.勤怠チェックリスト);
        }

        /// --------------------------------------------------------------------------
        /// <summary>
        ///     勤怠チェックリストテーブルにレコードを追加する </summary>
        /// <param name="stCSV">
        ///     給与計算用CSVデータ配列</param>
        /// <returns>
        ///     MTYSDataSet.勤怠チェックリストRow</returns>
        /// --------------------------------------------------------------------------
        private MTYSDataSet.勤怠チェックリストRow setNewListRecRow(string[] stCSV)
        {
            MTYSDataSet.勤怠チェックリストRow r = _dts.勤怠チェックリスト.New勤怠チェックリストRow();
            r.年 = global.cnfYear;
            r.月 = global.cnfMonth;
            r.社員番号 = Utility.StrtoInt(stCSV[0]);
            r.氏名 = stCSV[1];
            r.フリガナ = stCSV[2];

            // 社員情報を取得
            MTYSDataSet.社員所属Row sr = _dts.社員所属.FindBy社員番号(Utility.StrtoInt(stCSV[0]));

            if (!sr.IsNull(0))
            {
                r.帳票区分 = sr.帳票区分;
                r.所属コード = sr.所属コード;
                r.所属名 = sr.所属名称;
            }
            else
            {
                r.帳票区分 = 0;
                r.所属コード = 0;
                r.所属名 = string.Empty;
            }
            
            r.出勤すべき日数 = stCSV[3];
            r.出勤日数 = stCSV[4];
            r.欠勤日数 = stCSV[5];
            r.有給休暇 = stCSV[6];
            r.特別休暇 = stCSV[7];
            r.生理分娩休暇 = stCSV[8];
            r.遅刻時間 = stCSV[9];
            r.早退時間 = stCSV[10];
            r.普通残業 = stCSV[11];
            r.深夜残業 = stCSV[12];
            r.休日勤務 = stCSV[13];
            r.宿日直平日 = stCSV[14];
            r.宿日直休日 = stCSV[15];
            r.保安平日 = stCSV[16];
            r.保安休日 = stCSV[17];
            r.時差出勤 = stCSV[18];
            r._1L勤 = stCSV[19];
            r._2勤 = stCSV[20];
            r.丸3勤 = stCSV[21];
            r._3勤 = stCSV[22];
            r.日祝日勤務 = stCSV[23];
            r.控除日数 = stCSV[24];
            r.残業60H超平日 = stCSV[25];
            r.残業60H超休日 = stCSV[26];
            r.更新年月日 = DateTime.Now;

            return r;
        }

    }
}
