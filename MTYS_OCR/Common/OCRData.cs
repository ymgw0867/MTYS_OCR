using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace MTYS_OCR.Common
{
    class OCRData
    {
        #region エラー項目番号プロパティ
        //---------------------------------------------------
        //          エラー情報
        //---------------------------------------------------
        /// <summary>
        ///     エラーヘッダ行RowIndex</summary>
        public int _errHeaderIndex { get; set; }

        /// <summary>
        ///     エラー項目番号</summary>
        public int _errNumber { get; set; }

        /// <summary>
        ///     エラー明細行RowIndex </summary>
        public int _errRow { get; set; }

        /// <summary> 
        ///     エラーメッセージ </summary>
        public string _errMsg { get; set; }

        /// <summary> 
        ///     エラーなし </summary>
        public int eNothing = 0;

        /// <summary> 
        ///     エラー項目 = 対象年月 </summary>
        public int eYearMonth = 1;

        /// <summary> 
        ///     エラー項目 = 対象月 </summary>
        public int eMonth = 2;

        /// <summary> 
        ///     エラー項目 = 個人番号 </summary>
        public int eShainNo = 3;

        /// <summary> 
        ///     エラー項目 = 所属コード </summary>
        public int eShozoku = 4;

        /// <summary> 
        ///     エラー項目 = 日 </summary>
        public int eDay = 5;

        /// <summary> 
        ///     エラー項目 = 勤怠区分1 </summary>
        public int eKintaiKigou1 = 6;

        /// <summary> 
        ///     エラー項目 = 勤怠区分2 </summary>
        public int eKintaiKigou2 = 7;

        /// <summary> 
        ///     エラー項目 = 開始時 </summary>
        public int eSH = 8;

        /// <summary> 
        ///     エラー項目 = 開始分 </summary>
        public int eSM = 9;

        /// <summary> 
        ///     エラー項目 = 終了時 </summary>
        public int eEH = 10;

        /// <summary> 
        ///     エラー項目 = 終了分 </summary>
        public int eEM = 11;

        /// <summary> 
        ///     エラー項目 = 時間外時 </summary>
        public int eZH = 12;

        /// <summary> 
        ///     エラー項目 = 時間外分 </summary>
        public int eZM = 13;

        /// <summary> 
        ///     エラー項目 = 休日出勤時 </summary>
        public int eKSH = 14;

        /// <summary> 
        ///     エラー項目 = 休日出勤分 </summary>
        public int eKSM = 15;

        /// <summary> 
        ///     エラー項目 = 深夜勤務時 </summary>
        public int eSIH = 16;

        /// <summary> 
        ///     エラー項目 = 深夜勤務分 </summary>
        public int eSIM = 17;

        /// <summary> 
        ///     エラー項目 = 出勤形態 </summary>
        public int eShukeitai = 18;

        /// <summary> 
        ///     エラー項目 = 普通残業合計 </summary>
        public int eZanTL = 19;

        /// <summary> 
        ///     エラー項目 = 深夜残業合計 </summary>
        public int eShinyaTL = 20;

        /// <summary> 
        ///     エラー項目 = 休日勤務合計 </summary>
        public int eKyujitsuTL = 21;

        /// <summary> 
        ///     エラー項目 = 遅刻合計 </summary>
        public int eChikokuTL = 22;

        /// <summary> 
        ///     エラー項目 = 早退時間合計 </summary>
        public int eSoutaiTL = 23;

        /// <summary> 
        ///     エラー項目 = 出勤日数 </summary>
        public int eShukkin = 24;

        /// <summary> 
        ///     エラー項目 = 有給休暇 </summary>
        public int eYukyu  = 25;

        /// <summary> 
        ///     エラー項目 = 特別休暇 </summary>
        public int eTokukyu = 26;

        /// <summary> 
        ///     エラー項目 = 欠勤日数 </summary>
        public int eKekkin = 27;

        /// <summary> 
        ///     エラー項目 = 生理分娩 </summary>
        public int eSeiri = 28;

        /// <summary> 
        ///     エラー項目 = 時差出勤 </summary>
        public int eJisa = 29;

        /// <summary> 
        ///     エラー項目 = 保安回数・平日 </summary>
        public int eHoanH = 30;

        /// <summary> 
        ///     エラー項目 = 保安回数・休日 </summary>
        public int eHoanK = 31;

        /// <summary> 
        ///     エラー項目 = 宿日直回数・平日 </summary>
        public int eShukuH = 32;

        /// <summary> 
        ///     エラー項目 = 宿日直回数・休日 </summary>
        public int eShukuK = 33;

        /// <summary> 
        ///     エラー項目 = 1L勤回数 </summary>
        public int e1Lkin = 34;

        /// <summary> 
        ///     エラー項目 = 2勤回数 </summary>
        public int e2kin = 35;

        /// <summary> 
        ///     エラー項目 = ③勤回数 </summary>
        public int eMaru3kin = 36;
        
        /// <summary> 
        ///     エラー項目 = ３勤回数 </summary>
        public int e3kin = 37;

        /// <summary> 
        ///     エラー項目 = 日祝日勤務回数・平日 </summary>
        public int eShukujitsu = 38;

        /// <summary> 
        ///     エラー項目 = 控除日数 </summary>
        public int eKoujo = 39;
        #endregion


        #region 警告項目
        ///     <!--警告項目配列 -->
        public int[] warArray = new int[6];

        /// <summary>
        ///     警告項目番号</summary>
        public int _warNumber { get; set; }

        /// <summary>
        ///     警告明細行RowIndex </summary>
        public int _warRow { get; set; }

        /// <summary> 
        ///     警告項目 = 勤怠記号1&2 </summary>
        public int wKintaiKigou = 0;

        /// <summary> 
        ///     警告項目 = 開始終了時分 </summary>
        public int wSEHM = 1;

        /// <summary> 
        ///     警告項目 = 時間外時分 </summary>
        public int wZHM = 2;

        /// <summary> 
        ///     警告項目 = 深夜勤務時分 </summary>
        public int wSIHM = 3;

        /// <summary> 
        ///     警告項目 = 休日出勤時分 </summary>
        public int wKSHM = 4;

        /// <summary> 
        ///     警告項目 = 出勤形態 </summary>
        public int wShukeitai = 5;

        #endregion

        #region フィールド定義
        /// <summary> 
        ///     警告項目 = 時間外1.25時 </summary>
        public int [] wZ125HM = new int[global.MAX_GYO];

        /// <summary> 
        ///     実働時間 </summary>
        public double _workTime;

        /// <summary> 
        ///     深夜稼働時間 </summary>
        public double _workShinyaTime;
        #endregion

        #region 単位時間フィールド
        /// <summary> 
        ///     ３０分単位 </summary>
        private int tanMin30 = 30;

        /// <summary> 
        ///     １５分単位 </summary> 
        private int tanMin15 = 15;

        /// <summary> 
        ///     １分単位 </summary>
        private int tanMin1 = 1;
        #endregion

        #region 時間チェック記号定数
        private const string cHOUR = "H";           // 時間をチェック
        private const string cMINUTE = "M";         // 分をチェック
        private const string cTIME = "HM";          // 時間・分をチェック
        #endregion

        #region 勤怠記号
        private const string KINTAIKIGOU_1 = "1";   // １：定時勤務
        private const string KINTAIKIGOU_2 = "2";   // ２：休日勤務
        private const string KINTAIKIGOU_3 = "3";   // ３：早出
        private const string KINTAIKIGOU_4 = "4";   // ４：残業
        private const string KINTAIKIGOU_5 = "5";   // ５：欠勤
        private const string KINTAIKIGOU_6 = "6";   // ６：出張
        private const string KINTAIKIGOU_7 = "7";   // ７：遅刻
        private const string KINTAIKIGOU_8 = "8";   // ８：早退
        private const string KINTAIKIGOU_9 = "9";   // ９：有給休暇
        private const string KINTAIKIGOU_0 = "0";   // ０：半休
        private const string KINTAIKIGOU_A = "A";   // A：特別休暇
        private const string KINTAIKIGOU_B = "B";   // B：宿日直
        private const string KINTAIKIGOU_C = "C";   // C：保安
        #endregion

        #region 出勤形態
        private const string KEITAI_1 = "1";        // 本社：時差A、静岡：１直、大阪：①勤
        private const string KEITAI_2 = "2";        // 本社：時差B、静岡：２直、大阪：１勤
        private const string KEITAI_3 = "3";        // 本社：１直、大阪：１L勤
        private const string KEITAI_4 = "4";        // 本社：２直、大阪：２勤
        private const string KEITAI_5 = "5";        // 本社：３直、大阪：③勤
        private const string KEITAI_6 = "6";        // 大阪：３勤
        #endregion

        private const int SHOKUKYU_KEIYAKU71 = 71;  // 資格71：契約社員

        // テーブルアダプターマネージャーインスタンス
        MTYSDataSetTableAdapters.TableAdapterManager adpMn = new MTYSDataSetTableAdapters.TableAdapterManager();

        ///-------------------------------------------------------------------------------
        /// <param name="p">
        ///     戻り値指定パラメータ：0:出勤すべき日数, 1:計算用日数</param>
        /// <summary>
        ///     出勤すべき日数と計算用出勤日数を取得する </summary>
        /// <param name="sYear">
        ///     年</param>
        /// <param name="sMonth">
        ///     月</param>
        /// <param name="cKbn">
        ///     帳票区分</param>
        /// <param name="oskg">
        ///     大阪製造部グループ</param>
        ///-------------------------------------------------------------------------------
        public int getWorkDays(int p, int sYear, int sMonth, int cKbn, string oskg)
        {
            int rVal = 0;
            
            int wrkDays = 0;    // 出勤すべき日数
            int calDays = 0;    // 計算用日数

            MTYSDataSetTableAdapters.出勤日数TableAdapter adp = new MTYSDataSetTableAdapters.出勤日数TableAdapter();
            MTYSDataSet dts = new MTYSDataSet();
            adp.Fill(dts.出勤日数);

            var s = dts.出勤日数.Where(a => a.年 == sYear && a.月 == sMonth);
            foreach (var t in s)
            {
                if (cKbn.ToString() == global.C_HONSHA || cKbn.ToString() == global.C_SHIZUOKA)
                {
                    wrkDays = t.本社静岡印字用;
                    calDays = t.本社静岡計算用;
                }
                else if (cKbn.ToString() == global.C_OOSAKA)
                {
                    if (oskg == global.OOSAKAG_A)
                    {
                        wrkDays = t.大阪A印字用;
                        calDays = t.大阪A計算用;
                    }
                    else if (oskg == global.OOSAKAG_B)
                    {
                        wrkDays = t.大阪B印字用;
                        calDays = t.大阪B計算用;
                    }
                    else if (oskg == global.OOSAKAG_C)
                    {
                        wrkDays = t.大阪C印字用;
                        calDays = t.大阪C計算用;
                    }
                    else if (oskg == global.OOSAKAG_D)
                    {
                        wrkDays = t.大阪D印字用;
                        calDays = t.大阪D計算用;
                    }
                    else
                    {
                        wrkDays = t.大阪印字用;
                        calDays = t.大阪計算用;
                    }
                }
            }

            // 日数を返す
            if (p == 0) // 出勤すべき日数
            {
                rVal = wrkDays;
            }
            else if (p == 1)    // 計算用日数
            {
                rVal = calDays;
            }

            return rVal;
        }
        
        ///-----------------------------------------------------------------------
        /// <summary>
        ///     ＣＳＶデータをＭＤＢに登録する：DataSet Version </summary>
        /// <param name="_InPath">
        ///     CSVデータパス</param>
        /// <param name="frmP">
        ///     プログレスバーフォームオブジェクト</param>
        /// <param name="dts">
        ///     データセット</param>
        ///-----------------------------------------------------------------------
        public void CsvToMdb(string _InPath, frmPrg frmP, MTYSDataSet dts)
        {
            string headerKey = string.Empty;    // ヘッダキー
            string prnKBN = string.Empty;       // 申請書ID
            string sName = string.Empty;        // 社員名
            string sFuri = string.Empty;        // フリガナ
            string sShozoku = string.Empty;     // 所属名
            string sShozokuCode = string.Empty; // 所属コード
            string sOskG = string.Empty;        // 大阪製造部グループ

            SqlDataReader dr = null;

            // テーブルセットオブジェクト
            MTYSDataSet tblSt = new MTYSDataSet();
            try
            {
                // 勤務表ヘッダデータセット読み込み
                MTYSDataSetTableAdapters.勤務票ヘッダTableAdapter hAdp = new MTYSDataSetTableAdapters.勤務票ヘッダTableAdapter();
                adpMn.勤務票ヘッダTableAdapter = hAdp;
                adpMn.勤務票ヘッダTableAdapter.Fill(tblSt.勤務票ヘッダ);

                // 勤務表明細データセット読み込み
                MTYSDataSetTableAdapters.勤務票明細TableAdapter iAdp = new MTYSDataSetTableAdapters.勤務票明細TableAdapter();
                adpMn.勤務票明細TableAdapter = iAdp;
                adpMn.勤務票明細TableAdapter.Fill(tblSt.勤務票明細);

                // 対象CSVファイル数を取得
                string [] t = System.IO.Directory.GetFiles(_InPath, "*.csv");
                int cLen = t.Length;

                //CSVデータをMDBへ取込
                int cCnt = 0;
                foreach (string files in System.IO.Directory.GetFiles(_InPath, "*.csv"))
                {
                    //件数カウント
                    cCnt++;

                    //プログレスバー表示
                    frmP.Text = "OCR変換CSVデータロード中　" + cCnt.ToString() + "/" + cLen.ToString();
                    frmP.progressValue = cCnt * 100 / cLen;
                    frmP.ProgressStep();

                    ////////OCR処理対象のCSVファイルかファイル名の文字数を検証する
                    //////string fn = Path.GetFileName(files);

                    int sDays = 0;

                    // CSVファイルインポート
                    var s = System.IO.File.ReadAllLines(files, Encoding.Default);
                    foreach (var stBuffer in s)
                    {
                        // カンマ区切りで分割して配列に格納する
                        string[] stCSV = stBuffer.Split(',');

                        // ヘッダ行
                        if (stCSV[0] == "*")
                        {
                            headerKey = Utility.GetStringSubMax(stCSV[1].Trim(), 17);   // ヘッダーキー取得
                            prnKBN = Utility.GetStringSubMax(stCSV[2].Trim(), 1);       // ID取得
                            string sNo = Utility.GetStringSubMax(stCSV[5].Trim(), 4);   // 個人番号

                            sName = string.Empty;        // 社員名
                            sShozoku = string.Empty;     // 所属名
                            MTYSDataSet.社員所属Row sr = null;

                            // 社員情報を取得します
                            if (errCheckShainCode(dts, Utility.StrtoInt(sNo), out sr))
                            {
                                sShozokuCode = sr.所属コード.ToString();
                                sShozoku = sr.所属名称;
                                sName = sr.氏名;
                                sFuri = sr.フリガナ;
                                sOskG = sr.大阪製造部勤務グループ;
                            }
                            else
                            {
                                sShozokuCode = string.Empty;
                                sShozoku = string.Empty;
                                sName = string.Empty;
                                sFuri = string.Empty;
                                sOskG = string.Empty;
                            }

                            // 計算用日数取得
                            int cDays = getWorkDays(1, 2000 + Utility.StrtoInt(Utility.GetStringSubMax(stCSV[3].Trim(), 2)), Utility.StrtoInt(Utility.GetStringSubMax(stCSV[4].Trim(), 2)), Utility.StrtoInt(Utility.GetStringSubMax(stCSV[2].Trim(), 1)), sOskG);
                            
                            // 出勤すべき日数取得
                            int hDays = getWorkDays(1, 2000 + Utility.StrtoInt(Utility.GetStringSubMax(stCSV[3].Trim(), 2)), Utility.StrtoInt(Utility.GetStringSubMax(stCSV[4].Trim(), 2)), Utility.StrtoInt(Utility.GetStringSubMax(stCSV[2].Trim(), 1)), sOskG);

                            // MDBへ登録する：勤務票ヘッダテーブル
                            tblSt.勤務票ヘッダ.Add勤務票ヘッダRow(setNewHeadRecRow(tblSt, stCSV, sName, sFuri, sOskG, cDays, hDays, sShozokuCode, sShozoku));
                        }
                        else
                        {
                            // 勤務票明細テーブル
                            DateTime dt;

                            sDays++;

                            // 存在する日付のときにMDBへ登録する
                            string tempDt = global.cnfYear.ToString() + "/" + global.cnfMonth.ToString() + "/" + sDays.ToString();

                            if (DateTime.TryParse(tempDt, out dt))
                            {
                                // データセットに勤務報告書明細データを追加する
                                tblSt.勤務票明細.Add勤務票明細Row(setNewItemRecRow(tblSt, headerKey, stCSV, sDays));
                            }
                        }
                    }
                }

                // データベースへ反映
                adpMn.UpdateAll(tblSt);

                //CSVファイルを削除する
                foreach (string files in System.IO.Directory.GetFiles(_InPath, "*.csv"))
                {
                    System.IO.File.Delete(files);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "勤務票CSVインポート処理", MessageBoxButtons.OK);
            }
            finally
            {
            }
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     追加用MTYSDataSet.勤務票ヘッダRowオブジェクトを作成する（社員・出向社員用）</summary>
        /// <param name="tblSt">
        ///     テーブルセット</param>
        /// <param name="stCSV">
        ///     CSV配列</param>
        /// <param name="sName">
        ///     社員名</param>
        /// <param name="sFuri">
        ///     フリガナ</param>    
        /// <param name="sOskG">
        ///     大阪製造部グループ</param>
        /// <param name="cDays">
        ///     計算用出勤日数</param>
        /// <param name="sDays">
        ///     出勤すべき日数</param>
        /// <param name="sShozokuCode">
        ///     所属コード</param>
        /// <param name="sShozoku">
        ///     所属名</param>
        /// <returns>
        ///     追加するMTYSDataSet.勤務票ヘッダRowオブジェクト</returns>
        ///---------------------------------------------------------------------------------
        private MTYSDataSet.勤務票ヘッダRow setNewHeadRecRow(MTYSDataSet tblSt, string[] stCSV, string sName, string sFuri, string sOskG, int cDays, int sDays, string sShozokuCode, string sShozoku)
        {
            MTYSDataSet.勤務票ヘッダRow r = tblSt.勤務票ヘッダ.New勤務票ヘッダRow();
            r.ID = Utility.GetStringSubMax(stCSV[1].Trim(), 17);
            r.帳票番号 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[2].Trim(), 1));
            r.個人番号 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[5].Trim(), 4));
            r.氏名 = sName;
            r.フリガナ = sFuri;
            r.年 = 2000 + Utility.StrtoInt(Utility.GetStringSubMax(stCSV[3].Trim().Replace("-", ""), 2));
            r.月 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[4].Trim().Replace("-", ""), 2));
            r.所属コード = sShozokuCode;
            r.所属名 = sShozoku;
            r.給与区分 = string.Empty;
            r.画像名 = Utility.GetStringSubMax(stCSV[1].Trim(), 17) + ".tif";
            r.出勤すべき日数 = sDays;
            r.計算用出勤日数 = cDays;
            r.出勤日数合計 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[16].Trim(), 2));
            r.出勤日数2 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[17].Trim(), 1));
            r.有休日数合計 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[18].Trim(), 2));
            r.有休日数2 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[19].Trim(), 1));
            r.有休時間合計 = 0;
            r.特休日数合計 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[20].Trim(), 2));
            r.振休日数合計 = 0;
            r.振出日数合計 = 0;
            r.遅刻早退回数 = 0;
            r.欠勤日数合計 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[21].Trim(), 2));
            r.生理分娩日数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[22].Trim(), 2));
            r.時差出勤日数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[23].Trim(), 2));
            r.平日保安回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[24].Trim(), 1));
            r.休日保安回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[25].Trim(), 1));
            r.平日宿日直回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[26].Trim(), 1));
            r.休日宿日直回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[27].Trim(), 1));
            r._1L勤回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[28].Trim(), 2));
            r._2勤回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[29].Trim(), 2));
            r.丸3勤回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[30].Trim(), 2));
            r._3勤回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[31].Trim(), 2));
            r.日祝日勤務回数 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[32].Trim(), 1));
            r.控除日数 = 0;
            r.実稼動日数合計 = 0;
            r.総労働 = 0;
            r.総労働分 = 0;
            r.残業時 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[6].Trim(), 3));
            r.残業分 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[7].Trim(), 2));
            r.深夜時 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[8].Trim(), 3));
            r.深夜分 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[9].Trim(), 2));
            r.休日勤務時 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[10].Trim(), 3));
            r.休日勤務分 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[11].Trim(), 2));
            r.遅刻時間時 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[12].Trim(), 2));
            r.遅刻時間分 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[13].Trim(), 2));
            r.早退時間時 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[14].Trim(), 2));
            r.早退時間分 = Utility.StrtoInt(Utility.GetStringSubMax(stCSV[15].Trim(), 2));
            r.月間規定勤務時間 = 0;
            r.パート労働時間総枠 = 0;
            r.確認 = 0;
            r.データ領域名 = string.Empty;
            r.更新年月日 = DateTime.Now;

            return r;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     追加用MTYSDataSet.勤務票明細Rowオブジェクトを作成する（社員・出向社員用）</summary>
        /// <param name="stCSV">
        ///     CSV配列</param>
        /// <param name="i">
        ///     日付</param>
        /// <returns>
        ///     追加するMTYSDataSet.勤務票明細Rowオブジェクト</returns>
        ///---------------------------------------------------------------------------------
        private MTYSDataSet.勤務票明細Row setNewItemRecRow(MTYSDataSet tblSt, string headerKey, string[] stCSV, int i)
        {
            MTYSDataSet.勤務票明細Row r = tblSt.勤務票明細.New勤務票明細Row();

            r.ヘッダID = headerKey;
            r.日付 = i.ToString();
            r.休日マーク = 0;
            r.勤怠記号1 = Utility.GetStringSubMax(stCSV[0].Trim().Replace("-", ""), 1);
            r.勤怠記号2 = Utility.GetStringSubMax(stCSV[1].Trim().Replace("-", ""), 1);
            r.開始時 = Utility.GetStringSubMax(stCSV[2].Trim().Replace("-", ""), 2);
            r.開始分 = Utility.GetStringSubMax(stCSV[3].Trim().Replace("-", ""), 2);
            r.終了時 = Utility.GetStringSubMax(stCSV[4].Trim().Replace("-", ""), 2);
            r.終了分 = Utility.GetStringSubMax(stCSV[5].Trim().Replace("-", ""), 2);
            r.時間外時 = Utility.GetStringSubMax(stCSV[6].Trim().Replace("-", ""), 2);      // 普通残業・時2桁 2015/04/21
            r.時間外分 = Utility.GetStringSubMax(stCSV[7].Trim().Replace("-", ""), 2);
            r.休憩時 = string.Empty;
            r.休憩分 = string.Empty;
            r.休日出勤時 = Utility.GetStringSubMax(stCSV[10].Trim().Replace("-", ""), 2);
            r.休日出勤分 = Utility.GetStringSubMax(stCSV[11].Trim().Replace("-", ""), 2);
            r.所定内休日時 = string.Empty;
            r.所定内休日分 = string.Empty;
            r.深夜時 = Utility.GetStringSubMax(stCSV[8].Trim().Replace("-", ""), 1);         // 深夜残業・時1桁 2015/04/21
            r.深夜分 = Utility.GetStringSubMax(stCSV[9].Trim().Replace("-", ""), 2);
            r.実働時 = string.Empty;
            r.実働分 = string.Empty;
            r.交通費 = 0;
            r.訂正 = 0;
            r.実働編集 = 0;
            r.遅早退時 = string.Empty;
            r.遅早退分 = string.Empty;
            r.出勤形態 = Utility.GetStringSubMax(stCSV[12].Trim().Replace("-", ""), 1);
            r.更新年月日 = DateTime.Now;

            // 時分どちらかに「値あり」のときEmptyは０にする　2014/03/24
            r.時間外時 = hmStrToZero(r.時間外時, r.時間外分);
            r.時間外分 = hmStrToZero(r.時間外分, r.時間外時);
            r.休日出勤時 = hmStrToZero(r.休日出勤時, r.休日出勤分);
            r.休日出勤分 = hmStrToZero(r.休日出勤分, r.休日出勤時);
            r.深夜時 = hmStrToZero(r.深夜時, r.深夜分);
            r.深夜分 = hmStrToZero(r.深夜分, r.深夜時);

            return r;
        }

        ///----------------------------------------------------------------------------------------
        /// <summary>
        ///     値1がemptyで値2がNot string.Empty のとき "0"を返す。そうではないとき値1をそのまま返す</summary>
        /// <param name="str1">
        ///     値1：文字列</param>
        /// <param name="str2">
        ///     値2：文字列</param>
        /// <returns>
        ///     文字列</returns>
        ///----------------------------------------------------------------------------------------
        private string hmStrToZero(string str1, string str2)
        {
            string rVal = str1;
            if (str1 == string.Empty && str2 != string.Empty)
                rVal = "0";

            return rVal;
        }

        ///--------------------------------------------------------------------------------------------------
        /// <summary>
        ///     エラーチェックメイン処理。
        ///     エラーのときOCRDataクラスのヘッダ行インデックス、フィールド番号、明細行インデックス、
        ///     エラーメッセージが記録される </summary>
        /// <param name="sIx">
        ///     開始ヘッダ行インデックス</param>
        /// <param name="eIx">
        ///     終了ヘッダ行インデックス</param>
        /// <param name="frm">
        ///     親フォーム</param>
        /// <param name="dts">
        ///     データセット</param>
        /// <returns>
        ///     True:エラーなし、false:エラーあり</returns>
        ///-----------------------------------------------------------------------------------------------
        public Boolean errCheckMain(int sIx, int eIx, Form frm, MTYSDataSet dts, string [] sKey)
        {
            int rCnt = 0;

            // オーナーフォームを無効にする
            frm.Enabled = false;

            // プログレスバーを表示する
            frmPrg frmP = new frmPrg();
            frmP.Owner = frm;
            frmP.Show();

            // レコード件数取得
            //int cTotal = dts.勤務票ヘッダ.Rows.Count;

            // 出勤簿データ読み出し
            Boolean eCheck = true;

            for (int i = 0; i < sKey.Length; i++)
            {
                //データ件数加算
                rCnt++;

                //プログレスバー表示
                frmP.Text = "エラーチェック実行中　" + rCnt.ToString() + "/" + sKey.Length.ToString();
                frmP.progressValue = rCnt * 100 / sKey.Length;
                frmP.ProgressStep();

                //指定範囲ならエラーチェックを実施する：（i:行index）
                if (i >= sIx && i <= eIx)
                {
                    // 勤務票ヘッダ行のコレクションを取得します
                    //MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Rows[i];
                    MTYSDataSet.勤務票ヘッダRow r = (MTYSDataSet.勤務票ヘッダRow)dts.勤務票ヘッダ.Single(a => a.ID == sKey[i]);

                    // エラーチェック実施
                    eCheck = errCheckData(dts, r);

                    if (!eCheck)　//エラーがあったとき
                    {
                        _errHeaderIndex = i;     // エラーとなったヘッダRowIndex
                        break;
                    }
                }
            }

            // いったんオーナーをアクティブにする
            frm.Activate();

            // 進行状況ダイアログを閉じる
            frmP.Close();

            // オーナーのフォームを有効に戻す
            frm.Enabled = true;

            return eCheck;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        ///     エラー情報を取得します </summary>
        /// <param name="eID">
        ///     エラーデータのID</param>
        /// <param name="eNo">
        ///     エラー項目番号</param>
        /// <param name="eRow">
        ///     エラー明細行</param>
        /// <param name="eMsg">
        ///     表示メッセージ</param>
        ///---------------------------------------------------------------------------------
        private void setErrStatus(int eNo, int eRow, string eMsg)
        {
            //errHeaderIndex = eHRow;
            _errNumber = eNo;
            _errRow = eRow;
            _errMsg = eMsg;
        }

        ///-----------------------------------------------------------------------------------------------
        /// <summary>
        ///     項目別エラーチェック。
        ///     エラーのときヘッダ行インデックス、フィールド番号、明細行インデックス、エラーメッセージが記録される </summary>
        /// <param name="dts">
        ///     データセット</param>
        /// <param name="r">
        ///     勤務票ヘッダ行コレクション</param>
        /// <returns>
        ///     エラーなし：true, エラー有り：false</returns>
        ///-----------------------------------------------------------------------------------------------
        /// 
        public Boolean errCheckData(MTYSDataSet dts, MTYSDataSet.勤務票ヘッダRow r)
        {
            string sDate;
            DateTime eDate;

            // 勤怠記号データテーブルを取得します
            MTYSDataSetTableAdapters.勤怠記号TableAdapter adp_k = new MTYSDataSetTableAdapters.勤怠記号TableAdapter();
            adpMn.勤怠記号TableAdapter = adp_k;
            adpMn.勤怠記号TableAdapter.Fill(dts.勤怠記号);

            // 出勤形態データテーブルを取得します
            MTYSDataSetTableAdapters.出勤形態TableAdapter adp_T = new MTYSDataSetTableAdapters.出勤形態TableAdapter();
            adpMn.出勤形態TableAdapter = adp_T;
            adpMn.出勤形態TableAdapter.Fill(dts.出勤形態);

            // 帳票種別を取得
            string shain_Type = r.帳票番号.ToString();

            // 対象年
            if (Utility.NumericCheck(r.年.ToString()) == false)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 < 1)
            {
                setErrStatus(eYearMonth, 0, "年が正しくありません");
                return false;
            }

            if (r.年 != global.cnfYear)
            {
                setErrStatus(eYearMonth, 0, "対象年（" + global.cnfYear + "年）と一致していません");
                return false;
            }

            // 対象月
            if (!Utility.NumericCheck(r.月.ToString()))
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) < 1 || int.Parse(r.月.ToString()) > 12)
            {
                setErrStatus(eMonth, 0, "月が正しくありません");
                return false;
            }

            if (int.Parse(r.月.ToString()) != global.cnfMonth)
            {
                setErrStatus(eMonth, 0, "対象月（" + global.cnfMonth + "月）と一致していません");
                return false;
            }

            //--------------------------------------------------------------
            //      対象年月    
            //--------------------------------------------------------------
            sDate = r.年.ToString() + "/" + r.月.ToString() + "/01";
            if (DateTime.TryParse(sDate, out eDate) == false)
            {
                setErrStatus(eYearMonth, 0, "年月が正しくありません");
                return false;
            }

            //--------------------------------------------------------------
            //      社員番号
            //--------------------------------------------------------------
            // 数字以外のとき
            if (!Utility.NumericCheck(Utility.NulltoStr(r.個人番号)))
            {
                setErrStatus(eShainNo, 0, "社員番号が入力されていません");
                return false;
            }

            // 社員情報を参照します
            MTYSDataSet.社員所属Row sr;
            if (!errCheckShainCode(dts, Utility.StrtoInt(r.個人番号.ToString()), out sr))
            {
                setErrStatus(eShainNo, 0, "マスター未登録の社員番号です");
                return false;
            }
             
            // 同じ社員番号の勤務票データが複数存在しているか
            if (!getSameNumber(dts.勤務票ヘッダ, r.帳票番号, r.個人番号, r.ID))
            {
                setErrStatus(eShainNo, 0, "同じ帳票ID、社員番号のデータが複数あります");
                return false;
            }

            //--------------------------------------------------------------
            //      時差出勤記入チェック
            //--------------------------------------------------------------
            if (!checkJisaShukkin(r, sr))
            {
                setErrStatus(eJisa, 0, "時差出勤は静岡のみ記入です");
                return false;
            }

            //--------------------------------------------------------------
            //      宿日直記入チェック
            //--------------------------------------------------------------
            if (!checkShukuNicchoku(r, "宿日直回数", sr)) return false;

            //--------------------------------------------------------------
            //      １L勤記入チェック
            //--------------------------------------------------------------
            if (!check1LKin(r, "１L勤回数", sr)) return false;

            //--------------------------------------------------------------
            //      ２勤記入チェック
            //--------------------------------------------------------------
            if (!check2Kin(r, "２勤回数", sr)) return false;

            //--------------------------------------------------------------
            //      丸３勤記入チェック
            //--------------------------------------------------------------
            if (!checkMaru3Kin(r, "③勤回数", sr)) return false;

            //--------------------------------------------------------------
            //      ３勤記入チェック
            //--------------------------------------------------------------
            if (!check3Kin(r, "３勤回数", sr)) return false;

            //--------------------------------------------------------------
            //      日祝日勤務記入チェック
            //--------------------------------------------------------------
            if (!checkShukuNissu(r, "日祝日勤務回数", sr)) return false;

            //
            // 日付別データ
            //

            int iX = 0;
            string k = string.Empty;    // 特別休暇記号
            string yk = string.Empty;   // 有給記号
            int stKbn = 0;              // 勤怠区分の出勤退勤区分
            bool cHoliday = false;      // 休日の有無

            // 合計エリア
            double tZan = 0;            // 普通残業合計
            double tShinyaZan = 0;      // 深夜残業合計
            double tKyujitsu = 0;       // 休日勤務合計

            double tKekkin = 0;         // 欠勤日数
            double tYukyu = 0;          // 有給日数
            double tHankyu = 0;         // 半休日数
            double tTokukyu = 0;        // 特別休暇日数
            double t2Choku = 0;         // 静岡・２直日数
            double tSoutaiKaisu = 0;    // 早退記入数
            double tChikokuKaisu = 0;   // 遅刻記入数
            double tHoanH = 0;          // 保安平日
            double tHoanK = 0;          // 保安休日
            double tShukuH = 0;         // 宿日直・平日回数
            double tShukuK = 0;         // 宿日直・休日回数
            double t1L = 0;             // 大阪１L勤回数
            double t2Kin = 0;           // 大阪２勤回数
            double tMaru3kin = 0;       // 大阪③勤回数
            double t3kin = 0;           // 大阪３勤回数
            
            // 勤務票明細データ行を取得
            var mData = dts.勤務票明細.Where(a => a.ヘッダID == r.ID).OrderBy(a => a.ID);

            foreach (var m in mData)
            {
                // 日付インデックス加算
                iX++;

                // 勤怠区分初期化
                stKbn = 0;

                //--------------------------------------------------------------
                //      日付
                //--------------------------------------------------------------
                // 日付は数字か
                if (!Utility.NumericCheck(m.日付))
                {
                    setErrStatus(eDay, iX - 1, "日が正しくありません");
                    return false;
                }

                sDate = r.年.ToString() + "/" + r.月.ToString() + "/" + m.日付.ToString();

                // 存在しない日付に記入があるとき
                if (!DateTime.TryParse(sDate, out eDate))
                {
                    if (Utility.NulltoStr(m.勤怠記号1) != string.Empty || Utility.NulltoStr(m.勤怠記号2) != string.Empty ||
                    Utility.NulltoStr(m.開始時) != string.Empty || Utility.NulltoStr(m.開始分) != string.Empty || 
                    Utility.NulltoStr(m.終了時) != string.Empty || Utility.NulltoStr(m.終了分) != string.Empty || 
                    Utility.NulltoStr(m.時間外時) != string.Empty || Utility.NulltoStr(m.時間外分) != string.Empty || 
                    Utility.NulltoStr(m.休日出勤時) != string.Empty || Utility.NulltoStr(m.休日出勤分) != string.Empty || 
                    Utility.NulltoStr(m.深夜時) != string.Empty || Utility.NulltoStr(m.深夜分) != string.Empty || 
                    Utility.NulltoStr(m.出勤形態) != string.Empty)
                    {
                        setErrStatus(eDay, iX - 1, "この行には記入できません");
                        return false;
                    }
                }

                // 無記入の行はチェック対象外とする
                if (Utility.NulltoStr(m.勤怠記号1) == string.Empty && Utility.NulltoStr(m.勤怠記号2) == string.Empty &&
                    Utility.NulltoStr(m.開始時) == string.Empty && Utility.NulltoStr(m.開始分) == string.Empty &&
                    Utility.NulltoStr(m.終了時) == string.Empty && Utility.NulltoStr(m.終了分) == string.Empty &&
                    Utility.NulltoStr(m.時間外時) == string.Empty && Utility.NulltoStr(m.時間外分) == string.Empty &&
                    Utility.NulltoStr(m.休日出勤時) == string.Empty && Utility.NulltoStr(m.休日出勤分) == string.Empty &&
                    Utility.NulltoStr(m.深夜時) == string.Empty && Utility.NulltoStr(m.深夜分) == string.Empty &&
                    Utility.NulltoStr(m.出勤形態) == string.Empty)
                {
                    continue;
                }

                // 休日情報を取得
                cHoliday = getHolidayStatus(dts, eDate, sr.帳票区分.ToString(), sr.大阪製造部勤務グループ);

                // 勤怠記号_1
                if (m.勤怠記号1 != string.Empty)
                {
                    if (!errCheckKintaiCode(dts, iX, m.勤怠記号1, eKintaiKigou1, cHoliday, sr)) return false;

                    //// 勤怠記号1スターの出勤区分を取得
                    //MTYSDataSet.勤怠記号Row kR = dts.勤怠記号.FindByID(m.勤怠記号1);
                    //if (kR != null) stKbn = kR.出勤区分;
                    
                }
                else
                {
                    stKbn = global.flgOff;
                }

                //// 勤怠区分の出退勤区分が「0」で勤怠区分以外が無記入の行はチェック対象外とする
                //if (stKbn == global.flgOff &&
                //    Utility.NulltoStr(m.開始時) == string.Empty && Utility.NulltoStr(m.開始分) == string.Empty &&
                //    Utility.NulltoStr(m.終了時) == string.Empty && Utility.NulltoStr(m.終了分) == string.Empty &&
                //    Utility.NulltoStr(m.時間外時) == string.Empty && Utility.NulltoStr(m.時間外分) == string.Empty &&
                //    Utility.NulltoStr(m.休日出勤時) == string.Empty && Utility.NulltoStr(m.休日出勤分) == string.Empty &&
                //    Utility.NulltoStr(m.深夜時) == string.Empty && Utility.NulltoStr(m.深夜分) == string.Empty)
                //{
                //    continue;
                //}

                
                // 勤怠記号_2
                if (m.勤怠記号2 != string.Empty)
                {
                    if (!errCheckKintaiCode(dts, iX, m.勤怠記号2, eKintaiKigou2, cHoliday, sr)) return false;
                }
                
                // 勤怠記号の併記
                if (!errCheckKintai2(iX, m.勤怠記号1, m.勤怠記号2, eKintaiKigou1)) return false;
                
                // 始業時刻・終業時刻チェック
                if (!errCheckTime(m, "出退時間", tanMin1, iX)) return false;
                
                // 出勤形態チェック
                if (!errCheckSKeitai(dts,m,"出勤形態",iX,sr,cHoliday)) return false;
                
                // 普通残業チェック
                if (!errCheckZan(m, "普通残業", tanMin30, iX)) return false;
                
                // 深夜勤務チェック
                if (!errCheckShinya(m, "深夜残業", tanMin30, iX, sr)) return false;

                // 休日勤務チェック
                if (!errCheckKyujitsuShukkin(m, "休日出勤", tanMin30, iX, cHoliday)) return false;

                ///---------------------------------------------------------------------------------
                ///     月間合計時間、回数を取得
                ///---------------------------------------------------------------------------------

                // 普通残業合計
                tZan += (Utility.StrtoDouble(m.時間外時) * 60 + Utility.StrtoDouble(m.時間外分));

                // 深夜残業
                tShinyaZan += (Utility.StrtoDouble(m.深夜時) * 60 + Utility.StrtoDouble(m.深夜分));

                // 休日勤務
                tKyujitsu += (Utility.StrtoDouble(m.休日出勤時) * 60 + Utility.StrtoDouble(m.休日出勤分));

                // 欠勤日数
                if (m.勤怠記号1 == KINTAIKIGOU_5 || m.勤怠記号2 == KINTAIKIGOU_5) 
                    tKekkin++;

                // 有給日数
                if (m.勤怠記号1 == KINTAIKIGOU_9 || m.勤怠記号2 == KINTAIKIGOU_9)
                    tYukyu++;

                // 半休日数
                if (m.勤怠記号1 == KINTAIKIGOU_0 || m.勤怠記号2 == KINTAIKIGOU_0)
                    tHankyu += 0.5;

                // 特別休暇日数
                if (m.勤怠記号1 == KINTAIKIGOU_A || m.勤怠記号2 == KINTAIKIGOU_A)
                    tTokukyu++;

                // 静岡の２直回数
                if (sr.帳票区分.ToString() == global.C_SHIZUOKA && m.出勤形態 == KEITAI_2)
                    t2Choku++;

                // 早退回数
                if (m.勤怠記号1 == KINTAIKIGOU_8 || m.勤怠記号2 == KINTAIKIGOU_8)
                    tSoutaiKaisu++;

                // 遅刻回数
                if (m.勤怠記号1 == KINTAIKIGOU_7 || m.勤怠記号2 == KINTAIKIGOU_7)
                    tChikokuKaisu++;

                /* 保安回数
                 * 1日に2回記入ありのため、記号欄各々カウントする 2014/11/10
                 */
                if (m.勤怠記号1 == KINTAIKIGOU_C)
                {
                    if (cHoliday)
                    {
                        tHoanK++;   // 休日保安回数
                    }
                    else
                    {
                        tHoanH++;   // 平日保安回数
                    }
                }

                if (m.勤怠記号2 == KINTAIKIGOU_C)
                {
                    if (cHoliday)
                    {
                        tHoanK++;   // 休日保安回数
                    }
                    else
                    {
                        tHoanH++;   // 平日保安回数
                    }
                }

                // 宿日直回数（本社、静岡を対象）
                if (sr.帳票区分.ToString() == global.C_HONSHA || sr.帳票区分.ToString() == global.C_SHIZUOKA)
                {
                    // 1日に2回記入ありのため、記号欄各々カウントする 2014/11/10
                    if (m.勤怠記号1 == KINTAIKIGOU_B)
                    {
                        if (cHoliday)
                        {
                            tShukuK++;  // 休日宿日直回数
                        }
                        else
                        {
                            tShukuH++;  // 平日宿日直回数
                        }
                    }

                    if (m.勤怠記号2 == KINTAIKIGOU_B)
                    {
                        if (cHoliday)
                        {
                            tShukuK++;  // 休日宿日直回数
                        }
                        else
                        {
                            tShukuH++;  // 平日宿日直回数
                        }
                    }
                }

                // １L勤回数
                if (sr.帳票区分.ToString() == global.C_OOSAKA && m.出勤形態 == KEITAI_3)
                {
                    t1L++;
                }

                // 2勤回数
                if (sr.帳票区分.ToString() == global.C_OOSAKA && m.出勤形態 == KEITAI_4)
                {
                    t2Kin++;
                }

                // ③勤回数
                if (sr.帳票区分.ToString() == global.C_OOSAKA && m.出勤形態 == KEITAI_5)
                {
                    tMaru3kin++;
                }

                // 3勤回数
                if (sr.帳票区分.ToString() == global.C_OOSAKA && m.出勤形態 == KEITAI_6)
                {
                    t3kin++;
                }
            }

            // 普通残業合計
            if (!checkZangyoTL(r, "普通残業合計", tZan)) return false;

            // 深夜残業
            if (!checkShinyaTL(r, "深夜残業合計", tShinyaZan)) return false;

            // 休日勤務
            if (!checkKyujitsuTL(r, "休日勤務時間合計", tKyujitsu)) return false;

            // 早退時間
            if (!checkSoutai(r, "早退時間合計", tSoutaiKaisu)) return false;

            // 遅刻時間合計
            if (!checkChikoku(r, "遅刻時間合計", tChikokuKaisu)) return false;

            // 出勤日数 2014/10/10 計算用日数 → 印字用日数
            double cDays = (double)getWorkDays(0, r.年, r.月, sr.帳票区分, sr.大阪製造部勤務グループ);
            if (!checkShukkinDays(r, "出勤日数合計", cDays, tKekkin, tYukyu, tHankyu, tTokukyu)) return false;

            // 有給日数
            if (!checkYukyuDays(r, "有給日数合計", tYukyu, tHankyu)) return false;

            // 特別休暇
            if (!checkTokuDays(r, "特別休暇合計", tTokukyu)) return false;

            // 欠勤日数
            if (!checkKekkinDays(r, "欠勤日数合計", tKekkin)) return false;

            // 生理分娩
            cDays = (double)getWorkDays(0, r.年, r.月, sr.帳票区分, sr.大阪製造部勤務グループ);  // 出勤すべき日数を取得
            if (!checkSeiriDays(r, "生理分娩日数", (int)cDays)) return false;

            // 時差出勤
            if (!checkJisaDays(r, "２直日数", t2Choku, sr)) return false;

            // 保安回数
            if (!checkHoan(r, "保安記入数", tHoanH, tHoanK)) return false;

            // 宿日直回数
            if (!checkShuku(r, "宿日直回数", tShukuH, tShukuK)) return false;

            // １L勤回数
            if (!check1LKin(r,"１L勤回数", t1L, sr)) return false;

            // ２勤回数
            if (!check2Kin(r, "２勤回数",t2Kin, sr)) return false;

            // ③勤回数
            if (!checkMaru3Kin(r, "③勤回数",tMaru3kin, sr)) return false;

            // ３勤回数
            if (!check3Kin(r, "３勤回数", t3kin, sr)) return false;

            // 控除日数
            cDays = (double)getWorkDays(0, r.年, r.月, sr.帳票区分, sr.大阪製造部勤務グループ);  // 出勤すべき日数を取得
            if (!checkKoujyoDays(r, "控除日数", (int)cDays)) return false;

            return true;
        }
        
        /// -----------------------------------------------------------------------
        /// <summary>
        ///     自分以外で帳票番号と個人番号が同じ勤務票データが存在するか調べる </summary>
        /// <param name="dTbl">
        ///     MTYSDataSet.勤務票ヘッダDataTable</param>
        /// <param name="sChoID">
        ///     帳票ID</param>
        /// <param name="sNumber">
        ///     個人番号</param>
        /// <param name="sID">
        ///     勤務票ヘッダID</param>
        /// <returns>
        ///     同番号あり：true, 同番号なし：false</returns>
        /// -----------------------------------------------------------------------
        private bool getSameNumber(MTYSDataSet.勤務票ヘッダDataTable dTbl, int sChoID, int sNumber, string sID)
        {
            var s = dTbl.Where(a => a.帳票番号 == sChoID && a.個人番号 == sNumber && a.ID != sID);
            if (s.Count() > 0) return false;
            else return true;
        }

        /// --------------------------------------------------------------------------
        /// <summary>
        ///     勤怠記号チェック </summary>
        /// <param name="dts">
        ///     データセットオブジェクト</param>
        /// <param name="iX">
        ///     明細インデックス</param>
        /// <param name="kCode">
        ///     勤怠記号</param>
        /// <param name="errNum">
        ///     エラー番号</param>
        /// <param name="cHoliday">
        ///     休日ステータス</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        /// --------------------------------------------------------------------------
        private bool errCheckKintaiCode(MTYSDataSet dts, int iX, string kCode, int errNum,bool cHoliday, MTYSDataSet.社員所属Row sr)
        {
            // 無記入は戻す
            if (Utility.NulltoStr(kCode) == string.Empty) return true;

            // 勤怠記号のチェック
            MTYSDataSet.勤怠記号Row kR = dts.勤怠記号.FindByID(kCode);

            if (kR == null) // マスター登録区分か検証する
            {
                setErrStatus(errNum, iX - 1, "勤怠記号が正しくありません");
                return false;
            }
            else
            {
                if (sr.帳票区分 == int.Parse(global.C_HONSHA) && kR.本社 == global.flgOff)
                {
                    setErrStatus(errNum, iX - 1, "本社が使用しない勤怠記号です");
                    return false;
                }

                if (sr.帳票区分 == int.Parse(global.C_SHIZUOKA) && kR.静岡 == global.flgOff)
                {
                    setErrStatus(errNum, iX - 1, "静岡が使用しない勤怠記号です");
                    return false;
                }

                if (sr.帳票区分 == int.Parse(global.C_OOSAKA) && kR.大阪製造部 == global.flgOff)
                {
                    setErrStatus(errNum, iX - 1, "大阪製造部が使用しない勤怠記号です");
                    return false;
                }
            }

            // 休日関連
            if (cHoliday && 
                kCode != KINTAIKIGOU_2 && kCode != KINTAIKIGOU_6 && 
                kCode != KINTAIKIGOU_B && kCode != KINTAIKIGOU_C)
            {
                setErrStatus(errNum, iX - 1, "休日の勤怠記号が正しくありません");
                return false;
            }

            if (!cHoliday && kCode == KINTAIKIGOU_2)
            {
                setErrStatus(errNum, iX - 1, "平日に「２：休日勤務」が記入されています");
                return false;
            }

            // 2019/06/24 契約社員以外も半休使用可能となったため、コメント化
            //// 契約社員以外で半休記入
            //if (kCode == KINTAIKIGOU_0 && sr.資格 != SHOKUKYU_KEIYAKU71)
            //{
            //    setErrStatus(errNum, iX - 1, "契約社員以外に「０：半休」が記入されています");
            //    return false;
            //}

            return true;
        }

        ///------------------------------------------------------------------------------
        /// <summary>
        ///     勤怠記号の併記チェック </summary>
        /// <param name="iX">
        ///     明細インデックス</param>
        /// <param name="kCode1">
        ///     勤怠記号１</param>
        /// <param name="kCode2">
        ///     勤怠記号２</param>
        /// <param name="errNum">
        ///     エラー番号</param>
        /// <returns>
        ///     true:エラーなし、false:エラーあり</returns>
        ///------------------------------------------------------------------------------
        private bool errCheckKintai2(int iX, string kCode1, string kCode2, int errNum)
        {
            // どちらかが無記入のときはチェックしない
            if (kCode1.Trim() == string.Empty || kCode2.Trim() == string.Empty)
                return true;

            // 同じ勤怠記号が併記されている
            if (kCode1.Equals(kCode2))
            {
                /* 勤怠記号「B:宿日直」「C:保安」は併記されていてもエラーとしない
                 * ※警告扱いとする 2014/11/10
                 */
                if (kCode1 != KINTAIKIGOU_B && kCode1 != KINTAIKIGOU_C)
                {
                    setErrStatus(errNum, iX - 1, "同じ勤怠記号が記入されています");
                    return false;
                }
            }

            // 「5.欠勤」とその他の記号が併記されている
            if (kCode1 == KINTAIKIGOU_5 || kCode2 == KINTAIKIGOU_5)
            {
                setErrStatus(errNum, iX - 1, "「5.欠勤」は他の記号と併記出来ません");
                return false;
            }

            // 「9.有給休暇」とその他の記号が併記されている
            if (kCode1 == KINTAIKIGOU_9 || kCode2 == KINTAIKIGOU_9)
            {
                setErrStatus(errNum, iX - 1, "「9.有給休暇」は他の記号と併記出来ません");
                return false;
            }

            // 「A.特別休暇」とその他の記号が併記されている
            if (kCode1 == KINTAIKIGOU_A || kCode2 == KINTAIKIGOU_A)
            {
                setErrStatus(errNum, iX - 1, "「A.特別休暇」は他の記号と併記出来ません");
                return false;
            }

            // 「3.早出」と「7.遅刻」が併記されている
            if ((kCode1 == KINTAIKIGOU_3 && kCode2 == KINTAIKIGOU_7) || 
                (kCode1 == KINTAIKIGOU_7 && kCode2 == KINTAIKIGOU_3))
            {
                setErrStatus(errNum, iX - 1, "「3.早出」と「7.遅刻」は併記出来ません");
                return false;
            }

            // 「4.残業」と「8.早退」が併記されている
            if ((kCode1 == KINTAIKIGOU_4 && kCode2 == KINTAIKIGOU_8) ||
                (kCode1 == KINTAIKIGOU_8 && kCode2 == KINTAIKIGOU_4))
            {
                setErrStatus(errNum, iX - 1, "「4.残業」と「8.早退」は併記出来ません");
                return false;
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="stKbn">
        ///     勤怠記号の出勤怠区分</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckTime(MTYSDataSet.勤務票明細Row m, string tittle, int Tani, int iX)
        {
            /* 勤怠記号が「2:休日勤務」「3:早出」「4:残業」「0:半休」「7:遅刻」「8:早退」「B:宿日直」「C:保安」で
               時刻が無記入のときNGとする
               2014/10/10 条件より「B:宿日直」「C:保安」を撤廃（始業、終業時刻の記入は必要なし）*/

            string kigou = m.勤怠記号1.Trim() + m.勤怠記号2.Trim();

            if (kigou.Contains(KINTAIKIGOU_0) || kigou.Contains(KINTAIKIGOU_2) || kigou.Contains(KINTAIKIGOU_3) ||
                kigou.Contains(KINTAIKIGOU_4) || kigou.Contains(KINTAIKIGOU_7) || kigou.Contains(KINTAIKIGOU_8)
                //kigou.Contains(KINTAIKIGOU_B) || kigou.Contains(KINTAIKIGOU_C))
                )
            {
                if (m.開始時 == string.Empty)
                {
                    setErrStatus(eSH, iX - 1, tittle + "が未入力です");
                    return false;
                }

                if (m.開始分 == string.Empty)
                {
                    setErrStatus(eSM, iX - 1, tittle + "が未入力です");
                    return false;
                }

                if (m.終了時 == string.Empty)
                {
                    setErrStatus(eEH, iX - 1, tittle + "が未入力です");
                    return false;
                }

                if (m.終了分 == string.Empty)
                {
                    setErrStatus(eEM, iX - 1, tittle + "が未入力です");
                    return false;
                }
            }

            // 勤怠記号が「5:欠勤」「9:有休」「A:特別休暇」で時刻が記入されているときNGとする
            if (kigou.Contains(KINTAIKIGOU_5) || kigou.Contains(KINTAIKIGOU_9) || kigou.Contains(KINTAIKIGOU_A))
            {
                string kigouMsg = string.Empty;

                if (kigou == KINTAIKIGOU_5) kigouMsg = "5.欠勤";
                else if (kigou == KINTAIKIGOU_9) kigouMsg = "9.有休";
                else if (kigou == KINTAIKIGOU_A) kigouMsg = "A.特別休暇";

                if (m.開始時 != string.Empty)
                {
                    setErrStatus(eSH, iX - 1, "勤怠区分が「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }

                if (m.開始分 != string.Empty)
                {
                    setErrStatus(eSM, iX - 1, "勤怠区分が「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }

                if (m.終了時 != string.Empty)
                {
                    setErrStatus(eEH, iX - 1, "勤怠区分が「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }

                if (m.終了分 != string.Empty)
                {
                    setErrStatus(eEM, iX - 1, "勤怠区分が「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }
            }
            
            // 開始時間と終了時間
            string sTimeW = m.開始時.Trim() + m.開始分.Trim();
            string eTimeW = m.終了時.Trim() + m.終了分.Trim();

            if (sTimeW != string.Empty && eTimeW == string.Empty)
            {
                setErrStatus(eEH, iX - 1, tittle + "終業時刻が未入力です");
                return false;
            }

            if (sTimeW == string.Empty && eTimeW != string.Empty)
            {
                setErrStatus(eSH, iX - 1, tittle + "始業時刻が未入力です");
                return false;
            }

            // 記入のとき
            if (m.開始時 != string.Empty || m.開始分 != string.Empty ||
                m.終了時 != string.Empty || m.終了分 != string.Empty)
            {
                // 数字範囲、単位チェック
                if (!checkHourSpan(m.開始時))
                {
                    setErrStatus(eSH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.開始分, Tani))
                {
                    setErrStatus(eSM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkHourSpan(m.終了時))
                {
                    setErrStatus(eEH, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                if (!checkMinSpan(m.終了分, Tani))
                {
                    setErrStatus(eEM, iX - 1, tittle + "が正しくありません");
                    return false;
                }

                //// 終了時刻範囲
                //if (Utility.StrtoInt(Utility.NulltoStr(m.終了時)) == 24 &&
                //    Utility.StrtoInt(Utility.NulltoStr(m.終了分)) > 0)
                //{
                //    setErrStatus(eEM, iX - 1, tittle + "終了時刻範囲を超えています（～２４：００）");
                //    return false;
                //}
            }

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間記入範囲チェック 0～23の数値 </summary>
        /// <param name="h">
        ///     記入値</param>
        /// <returns>
        ///     正常:true, エラー:false</returns>
        ///------------------------------------------------------------------------------------
        private bool checkHourSpan(string h)
        {
            if (!Utility.NumericCheck(h)) return false;
            else if (int.Parse(h) < 0 || int.Parse(h) > 23) return false;
            else return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     分記入範囲チェック：0～59の数値及び記入単位 </summary>
        /// <param name="h">
        ///     記入値</param>
        /// <param name="tani">
        ///     記入単位分</param>
        /// <returns>
        ///     正常:true, エラー:false</returns>
        ///------------------------------------------------------------------------------------
        private bool checkMinSpan(string m, int tani)
        {
            if (!Utility.NumericCheck(m)) return false;
            else if (int.Parse(m) < 0 || int.Parse(m) > 59) return false;
            else if (int.Parse(m) % tani != 0) return false;
            else return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     時間外記入チェック </summary>
        /// <param name="obj">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckZan(MTYSDataSet.勤務票明細Row m, string tittle, int Tani, int iX)
        {
            // 無記入なら終了
            if (m.時間外時 == string.Empty && m.時間外分 == string.Empty) return true;

            // 勤怠記号が「2:休日勤務」「5:欠勤」「9:有休」「A:特別休暇」で記入されているとき
            string kigou = m.勤怠記号1.Trim() + m.勤怠記号2.Trim();

            if (kigou.Contains(KINTAIKIGOU_2) || kigou.Contains(KINTAIKIGOU_5) || kigou.Contains(KINTAIKIGOU_9) ||
                kigou.Contains(KINTAIKIGOU_A))
            {
                string kigouMsg = string.Empty;

                if (kigou.Contains(KINTAIKIGOU_2)) kigouMsg = "2.休日勤務";
                else if (kigou.Contains(KINTAIKIGOU_5)) kigouMsg = "5.欠勤";
                else if (kigou.Contains(KINTAIKIGOU_9)) kigouMsg = "9.有休";
                else if (kigou.Contains(KINTAIKIGOU_A)) kigouMsg = "A.特別休暇";

                if (m.時間外時 != string.Empty)
                {
                    setErrStatus(eZH, iX - 1, "「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }

                if (m.時間外分 != string.Empty)
                {
                    setErrStatus(eZM, iX - 1, "「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }
            }

            //  始業、終業時刻が無記入で普通残業が記入されているときエラー
            if (m.開始時 == string.Empty && m.開始分 == string.Empty &&
                 m.終了時 == string.Empty && m.終了分 == string.Empty)
            {
                if (m.時間外時 != string.Empty)
                {
                    setErrStatus(eZH, iX - 1, "始業、終業時刻が無記入で" + tittle + "が入力されています");
                    return false;
                }

                if (m.時間外分 != string.Empty)
                {
                    setErrStatus(eZM, iX - 1, "始業、終業時刻が無記入で" + tittle + "が入力されています");
                    return false;
                }
            }

            //// 記入のとき
            //if (m.時間外時 != string.Empty || m.時間外分 != string.Empty)
            //{
            //    // 時間と分のチェック
            //    if (!checkHourSpan(m.時間外時))
            //    {
            //        setErrStatus(eZH, iX - 1, tittle + "が正しくありません");
            //        return false;
            //    }

            //    if (!checkMinSpan(m.時間外分, Tani))
            //    {
            //        setErrStatus(eZM, iX - 1, tittle + "が正しくありません。（" + Tani.ToString() + "分単位）" );
            //        return false;
            //    }
            //}

            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     休日出勤記入チェック </summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckKyujitsuShukkin(MTYSDataSet.勤務票明細Row m, string tittle, int Tani, int iX, bool cHoliday)
        {
            // 勤怠記号が「5:欠勤」「9:有休」「0:半休」「A:特別休暇」で記入されているとき
            string kigou = m.勤怠記号1.Trim() + m.勤怠記号2.Trim();

            if (kigou.Contains(KINTAIKIGOU_5) || kigou.Contains(KINTAIKIGOU_9) || kigou.Contains(KINTAIKIGOU_0) || 
                kigou.Contains(KINTAIKIGOU_A))
            {
                string kigouMsg = string.Empty;

                if (kigou == KINTAIKIGOU_5) kigouMsg = "5.欠勤";
                else if (kigou == KINTAIKIGOU_9) kigouMsg = "9.有休";
                else if (kigou == KINTAIKIGOU_0) kigouMsg = "0.半休";
                else if (kigou == KINTAIKIGOU_A) kigouMsg = "A.特別休暇";

                if (m.休日出勤時 != string.Empty)
                {
                    setErrStatus(eKSH, iX - 1, "「" + kigouMsg + "」で" + tittle + "が記入されています");
                    return false;
                }

                if (m.休日出勤分 != string.Empty)
                {
                    setErrStatus(eKSM, iX - 1, "「" + kigouMsg + "」で" + tittle + "が記入されています");
                    return false;
                }
            }

            // 出退勤時刻が無記入で記入されているときエラー
            if (m.開始時 == string.Empty && m.開始分 == string.Empty && m.終了時 == string.Empty && 
                m.終了分 == string.Empty)
            {
                if (m.休日出勤時 != string.Empty)
                {
                    setErrStatus(eKSH, iX - 1, "始業・就業時刻が無記入で" + tittle + "が入力されています");
                    return false;
                }

                if (m.休日出勤分 != string.Empty)
                {
                    setErrStatus(eKSM, iX - 1, "始業・就業時刻が無記入で" + tittle + "が入力されています");
                    return false;
                }
            }

            // 休日以外に記入があるとき
            if (!cHoliday && (m.休日出勤時 != string.Empty || m.休日出勤分 != string.Empty))
            {
                setErrStatus(eKSH, iX - 1, "休日以外で" + tittle + "が入力されています");
                return false;
            }
                        
            return true;
        }

        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     深夜勤務記入チェック </summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="Tani">
        ///     分記入単位</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///------------------------------------------------------------------------------------
        private bool errCheckShinya(MTYSDataSet.勤務票明細Row m, string tittle, int Tani, int iX, MTYSDataSet.社員所属Row sr)
        {
            // 無記入なら終了
            if (m.深夜時 == string.Empty && m.深夜分 == string.Empty) return true;

            // 勤怠記号が「5:欠勤」「9:有休」「A:特別休暇」で記入されているとき
            string kigou = m.勤怠記号1.Trim() + m.勤怠記号2.Trim();

            if (kigou.Contains(KINTAIKIGOU_5) || kigou.Contains(KINTAIKIGOU_9) || kigou.Contains(KINTAIKIGOU_A))
            {
                string kigouMsg = string.Empty;

                if (kigou == KINTAIKIGOU_5) kigouMsg = "5.欠勤";
                else if (kigou == KINTAIKIGOU_9) kigouMsg = "9.有休";
                else if (kigou == KINTAIKIGOU_A) kigouMsg = "A.特別休暇";

                if (m.深夜時 != string.Empty)
                {
                    setErrStatus(eSIH, iX - 1, "「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }

                if (m.深夜分 != string.Empty)
                {
                    setErrStatus(eSIM, iX - 1, "「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }
            }

            // 深夜稼働時間取得
            if (m.開始時 != string.Empty || m.開始分 != string.Empty ||
                m.終了時 != string.Empty || m.終了分 != string.Empty)
            {
                _workShinyaTime = getShinyaWorkTime(m.開始時, m.開始分, m.終了時, m.終了分);

                // 始業時刻から終業時刻の範囲が22時～5時に該当しない場合で記入されているとき → 警告扱い 2014/10/10
                // 開始終了時間無記入の場合は警告扱い
                // ※但し、静岡：出勤形態「2:2直」、大阪製造部：出勤形態「5:③勤」「6:3勤」が記入されている場合は除く
                if (_workShinyaTime == 0 && (m.深夜時 != string.Empty || m.深夜分 != string.Empty))
                {
                    if ((sr.帳票区分 == int.Parse(global.C_SHIZUOKA) && m.出勤形態 == KEITAI_2) ||
                        ((sr.帳票区分 == int.Parse(global.C_OOSAKA)) && (m.出勤形態 == KEITAI_5 ||
                        m.出勤形態 == KEITAI_6)))
                    {
                    }
                    else
                    {
                        // 全て警告扱いに変更　2014/10/10
                        //setErrStatus(eSIM, iX - 1, "勤務時間の範囲が22時～5時に非該当で" + tittle + "が入力されています");
                        //return false;
                    }
                }
            }
            
            return true;
        }

        ///-------------------------------------------------------------------------------
        /// <summary>
        ///     出勤形態チェック </summary>
        /// <param name="dts">
        ///     データセット</param>
        /// <param name="m">
        ///     MTYSDataSet.勤務票明細Row</param>
        /// <param name="tittle">
        ///     チェック項目名称</param>
        /// <param name="iX">
        ///     日付を表すインデックス</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <param name="cHoliday">
        ///     休日ステータス</param>
        /// <returns>
        ///     エラーなし：true, エラーあり：false</returns>
        ///-------------------------------------------------------------------------------
        private bool errCheckSKeitai(MTYSDataSet dts, MTYSDataSet.勤務票明細Row m, string tittle, int iX, MTYSDataSet.社員所属Row sr, bool cHoliday)
        {
            // 無記入なら終了
            if (m.出勤形態 == string.Empty) return true;

            // 出勤形態マスターに登録されているか
            var s = dts.出勤形態.Where(a => a.コード == Utility.StrtoInt(m.出勤形態) && a.帳票区分 == sr.帳票区分);
            if (s.Count() == 0)
            {
                setErrStatus(eShukeitai, iX - 1, "マスター未登録の" + tittle + "です");
                return false;
            }

            // 休日に記入されているとき
            if (cHoliday)
            {
                setErrStatus(eShukeitai, iX - 1, "休日に" + tittle + "が入力されています");
                return false;
            }

            // 勤怠記号が「5:欠勤」「6:出張」「9:有休」「A:特別休暇」で記入されているとき
            string kigou = m.勤怠記号1.Trim() + m.勤怠記号2.Trim();

            if (kigou.Contains(KINTAIKIGOU_5) || kigou.Contains(KINTAIKIGOU_6) || kigou.Contains(KINTAIKIGOU_9) ||
                kigou.Contains(KINTAIKIGOU_A))
            {
                string kigouMsg = string.Empty;

                if (kigou == KINTAIKIGOU_5) kigouMsg = "5.欠勤";
                else if (kigou == KINTAIKIGOU_6) kigouMsg = "6.出張";
                else if (kigou == KINTAIKIGOU_9) kigouMsg = "9.有休";
                else if (kigou == KINTAIKIGOU_A) kigouMsg = "A.特別休暇";

                if (m.出勤形態 != string.Empty)
                {
                    setErrStatus(eShukeitai, iX - 1, "「" + kigouMsg + "」で" + tittle + "が入力されています");
                    return false;
                }
            }

            return true;
        }


        ///------------------------------------------------------------------------------------
        /// <summary>
        ///     実働時間を取得する</summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="sH">
        ///     開始時</param>
        /// <param name="sM">
        ///     開始分</param>
        /// <param name="eH">
        ///     終了時</param>
        /// <param name="eM">
        ///     終了分</param>
        /// <param name="rH">
        ///     休憩時間・分</param>
        /// <returns>
        ///     実働時間</returns>
        ///------------------------------------------------------------------------------------
        public double getWorkTime(string sH, string sM, string eH, string eM, int rH)
        {
            DateTime sTm;
            DateTime eTm;
            DateTime cTm;
            double w = 0;   // 稼働時間

            // 時刻情報に不備がある場合は０を返す
            if (!Utility.NumericCheck(sH) || !Utility.NumericCheck(sM) || 
                !Utility.NumericCheck(eH) || !Utility.NumericCheck(eM))
                return 0;

            // 開始時刻取得
            if (Utility.StrtoInt(sH) == 24)
            {
                if (DateTime.TryParse("0:" + Utility.StrtoInt(sM).ToString(), out cTm))
                {
                    sTm = cTm;
                }
                else return 0;
            }
            else
            {
                if (DateTime.TryParse(Utility.StrtoInt(sH).ToString() + ":" + Utility.StrtoInt(sM).ToString(), out cTm))
                {
                    sTm = cTm;
                }
                else return 0;
            }

            // 終了時刻取得
            if (Utility.StrtoInt(eH) == 24)
                eTm = DateTime.Parse("23:59");
            else
            {
                if (DateTime.TryParse(Utility.StrtoInt(eH).ToString() + ":" + Utility.StrtoInt(eM).ToString(), out cTm))
                {
                    eTm = cTm;
                }
                else return 0;
            }

            // 終了時間が24:00記入のときは23:59までの計算なので稼働時間1分加算する
            if (Utility.StrtoInt(eH) == 24 && Utility.StrtoInt(eM) == 0)
            {
                w = Utility.GetTimeSpan(sTm, eTm).TotalMinutes + 1;
            }
            else if (sTm == eTm)    // 同時刻の場合は翌日の同時刻とみなす 2014/10/10
            {
                w = Utility.GetTimeSpan(sTm, eTm.AddDays(1)).TotalMinutes;  // 稼働時間
            }
            else
            {
                w = Utility.GetTimeSpan(sTm, eTm).TotalMinutes;  // 稼働時間
            }

            // 休憩時間を差し引く
            if (w >= rH) w = w - rH;
            else w = 0;

            // 値を返す
            return w;
        }

        ///--------------------------------------------------------------
        /// <summary>
        ///     深夜勤務時間を取得する</summary>
        /// <param name="m">
        ///     勤務票明細Rowコレクション</param>
        /// <param name="sH">
        ///     開始時</param>
        /// <param name="sM">
        ///     開始分</param>
        /// <param name="eH">
        ///     終了時</param>
        /// <param name="eM">
        ///     終了分</param>
        /// <returns>
        ///     深夜勤務時間</returns>
        /// ------------------------------------------------------------
        private double getShinyaWorkTime(string sH, string sM, string eH, string eM)
        {
            DateTime sTime;
            DateTime eTime;
            DateTime cTm;

            double wkShinya = 0;    // 深夜稼働時間

            // 時刻情報に不備がある場合は０を返す
            if (!Utility.NumericCheck(sH) || !Utility.NumericCheck(sM) ||
                !Utility.NumericCheck(eH) || !Utility.NumericCheck(eM))
                return 0;

            // 開始時間を取得
            if (DateTime.TryParse(Utility.StrtoInt(sH).ToString() + ":" + Utility.StrtoInt(sM).ToString(), out cTm))
            {
                sTime = cTm;
            }
            else return 0;

            // 終了時間を取得
            if (Utility.StrtoInt(eH) == 24 && Utility.StrtoInt(eM) == 0)
            {
                eTime = global.dt2359;
            }
            else if (DateTime.TryParse(Utility.StrtoInt(eH).ToString() + ":" + Utility.StrtoInt(eM).ToString(), out cTm))
            {
                eTime = cTm;
            }
            else return 0;


            // 当日内の勤務のとき
            if (sTime.TimeOfDay < eTime.TimeOfDay)
            {
                // 早出残業時間を求める
                if (sTime < global.dt0500)  // 開始時刻が午前5時前のとき
                {
                    // 早朝時間帯稼働時間
                    if (eTime >= global.dt0500)
                    {
                        wkShinya += Utility.GetTimeSpan(sTime, global.dt0500).TotalMinutes;
                    }
                    else
                    {
                        wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                    }
                }

                // 終了時刻が22:00以降のとき
                if (eTime >= global.dt2200)
                {
                    // 当日分の深夜帯稼働時間を求める
                    if (sTime <= global.dt2200)
                    {
                        // 出勤時刻が22:00以前のとき深夜開始時刻は22:00とする
                        wkShinya += Utility.GetTimeSpan(global.dt2200, eTime).TotalMinutes;
                    }
                    else
                    {
                        // 出勤時刻が22:00以降のとき深夜開始時刻は出勤時刻とする
                        wkShinya += Utility.GetTimeSpan(sTime, eTime).TotalMinutes;
                    }

                    // 終了時間が24:00記入のときは23:59までの計算なので稼働時間1分加算する
                    if (Utility.StrtoInt(eH) == 24 && Utility.StrtoInt(eM) == 0)
                        wkShinya += 1;
                }
            }
            else
            {
                // 日付を超えて終了したとき（開始時刻 >= 終了時刻）※2014/10/10 同時刻は翌日の同時刻とみなす

                // 早出残業時間を求める
                if (sTime < global.dt0500)  // 開始時刻が午前5時前のとき
                {
                    wkShinya += Utility.GetTimeSpan(sTime, global.dt0500).TotalMinutes;
                }

                // 当日分の深夜勤務時間（～０：００まで）
                if (sTime <= global.dt2200)
                {
                    // 出勤時刻が22:00以前のとき無条件に120分
                    wkShinya += global.TOUJITSU_SINYATIME;
                }
                else
                {
                    // 出勤時刻が22:00以降のとき出勤時刻から24:00までを求める
                    wkShinya += Utility.GetTimeSpan(sTime, global.dt2359).TotalMinutes + 1;
                }

                // 0:00以降の深夜勤務時間を加算（０：００～終了時刻）
                if (eTime.TimeOfDay > global.dt0500.TimeOfDay)
                {
                    wkShinya += Utility.GetTimeSpan(global.dt0000, global.dt0500).TotalMinutes;
                }
                else
                {
                    wkShinya += Utility.GetTimeSpan(global.dt0000, eTime).TotalMinutes;
                }
            }

            return wkShinya;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     時差出勤記入チェック</summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkJisaShukkin(MTYSDataSet.勤務票ヘッダRow r, MTYSDataSet.社員所属Row sr)
        {
            // 静岡はチェック対象外
            if (sr.帳票区分.ToString() != global.C_SHIZUOKA && r.時差出勤日数 > 0) return false;

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     宿日直記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkShukuNicchoku(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_HONSHA && sr.帳票区分.ToString() != global.C_SHIZUOKA)
            {
                if (r.平日宿日直回数 > 0)
                {
                    setErrStatus(eShukuH, 0, tittle + "は本社、静岡のみ記入です");
                    return false;
                }

                if (r.休日宿日直回数 > 0)
                {
                    setErrStatus(eShukuK, 0, tittle + "は本社、静岡のみ記入です");
                    return false;
                }
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     1L勤記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check1LKin(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA && r._1L勤回数 > 0)
            {
                setErrStatus(e1Lkin, 0, tittle + "は大阪製造部のみ記入です");
                return false;
            }

            return true;
        }
        
        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     ２勤記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check2Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA && r._2勤回数 > 0)
            {
                setErrStatus(e2kin, 0, tittle + "は大阪製造部のみ記入です");
                return false;
            }

            return true;
        }
        
        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     ③勤記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkMaru3Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA && r.丸3勤回数 > 0)
            {
                setErrStatus(eMaru3kin, 0, tittle + "は大阪製造部のみ記入です");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     ３勤記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check3Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA && r._3勤回数 > 0)
            {
                setErrStatus(e3kin, 0, tittle + "は大阪製造部のみ記入です");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     日祝日勤務記入チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkShukuNissu(MTYSDataSet.勤務票ヘッダRow r, string tittle, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA && r.日祝日勤務回数 > 0)
            {
                setErrStatus(eShukujitsu, 0, tittle + "は大阪製造部のみ記入です");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     普通残業合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tZan">
        ///     普通残業合計</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkZangyoTL(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tZan)
        {
            // 普通残業合計
            double kZan = (double)(r.残業時 * 60 + r.残業分);
            if (kZan != tZan)
            {
                setErrStatus(eZanTL, 0, tittle + "が日別の普通残業時間合計（" + Utility.dblToHHMM(tZan) + "）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     深夜残業合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tShinya">
        ///     深夜残業合計</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkShinyaTL(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tShinya)
        {
            // 深夜残業合計
            double kShinya = (double)(r.深夜時 * 60 + r.深夜分);
            if (kShinya != tShinya)
            {
                setErrStatus(eShinyaTL, 0, tittle + "が日別の深夜残業時間合計（" + Utility.dblToHHMM(tShinya) + "）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     休日勤務合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tVal">
        ///     休日勤務合計</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkKyujitsuTL(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tVal)
        {
            // 休日勤務合計
            double kVal = (double)(r.休日勤務時 * 60 + r.休日勤務分);
            if (kVal != tVal)
            {
                setErrStatus(eKyujitsuTL, 0, tittle + "が日別の" + tittle + "（" + Utility.dblToHHMM(tVal) + "）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     出勤日数合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="cDays">
        ///     印字用出勤日数</param>
        /// <param name="tSeiri">
        ///     生理分娩日数</param>
        /// <param name="tkekkin">
        ///     欠勤日数</param>
        /// <param name="tYukyu">
        ///     有給日数</param>
        /// <param name="tHankyu">
        ///     半休日数</param>
        /// <param name="tToku">
        ///     特別休暇日数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkShukkinDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, double cDays, double tkekkin, double tYukyu, double tHankyu, double tToku)
        {
            // 出勤日数計算
            double kVal = cDays - r.生理分娩日数 - tkekkin - tYukyu - tHankyu - tToku;
            double tVal = ((double)(r.出勤日数合計 * 10 + r.出勤日数2)) / 10;
            if (kVal != tVal)
            {
                setErrStatus(eShukkin, 0, tittle + "（" + kVal.ToString() + "日）が正しくありません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     有給日数合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tYukyu">
        ///     有給日数</param>
        /// <param name="tHankyu">
        ///     半休日数</param>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkYukyuDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tYukyu, double tHankyu)
        {
            // 有給日数計算
            double kVal = tYukyu + tHankyu;
            double tVal = ((double)(r.有休日数合計 * 10 + r.有休日数2)) / 10;
            if (kVal != tVal)
            {
                setErrStatus(eYukyu, 0, tittle + "が日別の有給休暇記入数（" + kVal.ToString() + "日）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     特別休暇合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tToku">
        ///     特別休暇日数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkTokuDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tToku)
        {
            if ((double)r.特休日数合計 != tToku)
            {
                setErrStatus(eTokukyu, 0, tittle + "が日別の特別休暇記入数（" + tToku.ToString() + "日）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     欠勤日数合計チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tKekkin">
        ///     欠勤日数日数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkKekkinDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tKekkin)
        {
            if ((double)r.欠勤日数合計 != tKekkin)
            {
                setErrStatus(eKekkin, 0, tittle + "が日別の欠勤記入数（" + tKekkin.ToString() + "日）と一致していません");
                return false;
            }
            return true;
        }
        
        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     生理分娩日数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="cDays">
        ///     出勤すべき日数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkSeiriDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, int cDays)
        {
            if (r.生理分娩日数 > cDays)
            {
                setErrStatus(eSeiri, 0, tittle + "が正しくありません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     静岡・２直日数(時差出勤）チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="t2Choku">
        ///     ２直日数</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkJisaDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, double t2Choku, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_SHIZUOKA) return true;

            if ((double)r.時差出勤日数 != t2Choku)
            {
                setErrStatus(eJisa, 0, tittle + "が日別の「２直」記入数（" + t2Choku.ToString() + "日）と一致していません");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     早退合計時間チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tSoutai">
        ///     早退記入数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkSoutai(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tSoutai)
        {
            if (r.早退時間時 == 0 && r.早退時間分 == 0 && tSoutai > 0)
            {
                setErrStatus(eSoutaiTL, 0, "勤怠記号「8:早退」の記入があり" + tittle + "が無記入です");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     遅刻合計時間チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tChikoku">
        ///     遅刻記入数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkChikoku(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tChikoku)
        {
            if (r.遅刻時間時 == 0 && r.遅刻時間分 == 0 && tChikoku > 0)
            {
                setErrStatus(eChikokuTL, 0, "勤怠記号「7:遅刻」の記入があり" + tittle + "が無記入です");
                return false;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     保安回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tHoanH">
        ///     平日保安記入数</param>
        /// <param name="tHoanK">
        ///     休日保安記入数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkHoan(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tHoanH, double tHoanK)
        {
            if ((double)r.平日保安回数 != tHoanH)
            {
                setErrStatus(eHoanH, 0, tittle + "が日別の「平日保安」記入数（" + tHoanH.ToString() + "回）と一致していません");
                return false;
            }

            if ((double)r.休日保安回数 != tHoanK)
            {
                setErrStatus(eHoanK, 0, tittle + "が日別の「休日保安」記入数（" + tHoanK.ToString() + "回）と一致していません");
                return false;
            }
            
            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     宿日直回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="tShukuH">
        ///     平日宿日直記入数</param>
        /// <param name="tShukuK">
        ///     休日宿日直記入数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkShuku(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tShukuH, double tShukuK)
        {
            if ((double)r.平日宿日直回数 != tShukuH)
            {
                setErrStatus(eShukuH, 0, tittle + "が日別の「平日宿日直」記入数（" + tShukuH.ToString() + "回）と一致していません");
                return false;
            }

            if ((double)r.休日宿日直回数 != tShukuK)
            {
                setErrStatus(eShukuK, 0, tittle + "が日別の「休日宿日直」記入数（" + tShukuK.ToString() + "回）と一致していません");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     大阪・１L勤回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="t1LKin">
        ///     １L勤回数</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check1LKin(MTYSDataSet.勤務票ヘッダRow r, string tittle, double t1LKin, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA) return true;

            if ((double)r._1L勤回数 != t1LKin)
            {
                setErrStatus(e1Lkin, 0, tittle + "が日別の「１L勤」記入数（" + t1LKin.ToString() + "日）と一致していません");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     大阪・2勤回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="t1LKin">
        ///     2勤回数</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check2Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, double t2Kin, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA) return true;

            if ((double)r._2勤回数 != t2Kin)
            {
                setErrStatus(e2kin, 0, tittle + "が日別の「２勤」記入数（" + t2Kin.ToString() + "日）と一致していません");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     大阪・③勤回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="t1LKin">
        ///     ③勤回数</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkMaru3Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, double tMaru3Kin, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA) return true;

            if ((double)r.丸3勤回数 != tMaru3Kin)
            {
                setErrStatus(eMaru3kin, 0, tittle + "が日別の「③勤」記入数（" + tMaru3Kin.ToString() + "日）と一致していません");
                return false;
            }

            return true;
        }

        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     大阪・３勤回数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="t1LKin">
        ///     ３勤回数</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool check3Kin(MTYSDataSet.勤務票ヘッダRow r, string tittle, double t3Kin, MTYSDataSet.社員所属Row sr)
        {
            if (sr.帳票区分.ToString() != global.C_OOSAKA) return true;

            if ((double)r._3勤回数 != t3Kin)
            {
                setErrStatus(e3kin, 0, tittle + "が日別の「３勤」記入数（" + t3Kin.ToString() + "日）と一致していません");
                return false;
            }

            return true;
        }
        
        ///-------------------------------------------------------------------------------------
        /// <summary>
        ///     控除日数チェック </summary>
        /// <param name="r">
        ///     MTYSDataSet.勤務票ヘッダRow</param>
        /// <param name="tittle">
        ///     項目名</param>
        /// <param name="cDays">
        ///     出勤すべき日数</param>
        /// <returns>
        ///     true:エラーなし, false:エラーあり</returns>
        ///-------------------------------------------------------------------------------------
        public bool checkKoujyoDays(MTYSDataSet.勤務票ヘッダRow r, string tittle, int cDays)
        {
            if (r.控除日数 > cDays)
            {
                setErrStatus(eKoujo, 0, tittle + "が正しくありません");
                return false;
            }
            return true;
        }
        ///-----------------------------------------------------------------------------
        /// <summary>
        ///     社員コードで検索して該当する社員所属データセットの行を返す </summary>
        /// <param name="dts">
        ///     データセット名</param>
        /// <param name="sCode">
        ///     検索する社員コード</param>
        /// <returns>
        ///     在籍:string.empty、退職："NonTaishoku"、休職中："NonKyushoku"、該当なし："NonMaster"
        ///     </returns>
        /// <param name="sName">
        ///     社員名</param>
        /// <param name="sOskG">
        ///     大阪製造部グループ</param>
        ///-----------------------------------------------------------------------------
        public bool errCheckShainCode(MTYSDataSet dts, int sCode, out MTYSDataSet.社員所属Row sr)
        {
            // 戻り値初期化
            //sName = string.Empty;
            //sOskG = string.Empty;
            sr = null;
            //string rtn = string.Empty;
            bool rtn = true;

            // 社員情報を取得します
            MTYSDataSet.社員所属Row r = (MTYSDataSet.社員所属Row)dts.社員所属.FindBy社員番号(sCode);

            if (r == null)
            {
                sr = null;
                rtn = false;
            }
            else
            {
                sr = r;
                rtn = true;
            }

            return rtn;
        }

        ///-----------------------------------------------------------------------
        /// <summary>
        ///     警告チェック</summary>
        /// <param name="m">
        ///     MTYSDataSet.勤務票明細Row</param>
        /// <param name="sr">
        ///     MTYSDataSet.社員所属Row</param>
        /// <param name="cHoliday">
        ///     休日ステータス</param>
        /// <param name="warArray">
        ///     警告項目配列</param>
        /// <returns>
        ///     true:警告なし、false:警告有</returns>
        ///-----------------------------------------------------------------------
        public bool warnCheck(MTYSDataSet.勤務票明細Row m, MTYSDataSet.社員所属Row sr, bool cHoliday, int [] wArray)
        {
            int st = 0;
            int et = 0;
            string kg = m.勤怠記号1 + m.勤怠記号2;

            //-------------------------------------------------------------------------
            //      勤怠記号
            //-------------------------------------------------------------------------
            // 「4:残業」が無記入で「普通残業」または「深夜残業」に時間が記入されているとき
            if (!kg.Contains(KINTAIKIGOU_4))
            {
                if (m.時間外時 != string.Empty || m.時間外分 != string.Empty)
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wZHM] = global.flgOn;            // 普通残業
                    return false;
                }

                if (m.深夜時 != string.Empty || m.深夜分 != string.Empty)
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wSIHM] = global.flgOn;           // 深夜残業
                    return false;
                }
            }

            // 「4:残業」が記入されていて「普通残業」と「深夜残業」が無記入のとき
            if (kg.Contains(KINTAIKIGOU_4))
            {
                if (m.時間外時 == string.Empty && m.時間外分 == string.Empty &&
                    m.深夜時 == string.Empty && m.深夜分 == string.Empty)
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wZHM] = global.flgOn;            // 普通残業
                    return false;
                }
            }

            // 始業時刻から終業時刻までが8時間50分未満で勤怠記号「7:遅刻」または「8:早退」の記入がないとき
            // 始業時刻から終業時刻までが8時間50分未満で勤怠記号「1:定時勤務」が記入されているとき
            // ※大阪製造部グループは除く
            if (m.開始時 != string.Empty && m.開始分 != string.Empty && m.終了時 != string.Empty && m.終了分 != string.Empty)
            {
                if (sr.大阪製造部勤務グループ == string.Empty)
                {
                    // 実働時間を取得
                    double wt = getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, 0);

                    // 8時間50分未満
                    if (wt < 530)
                    {
                        if (!kg.Contains(KINTAIKIGOU_7) && !kg.Contains(KINTAIKIGOU_8))
                        {
                            wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                            wArray[wSEHM] = global.flgOn;           // 開始終了時間
                            return false;
                        }

                        if (kg.Contains(KINTAIKIGOU_1))
                        {
                            wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                            wArray[wSEHM] = global.flgOn;           // 開始終了時間
                            return false;
                        }
                    }
                }
            }

            // 始業時刻と終業時刻が記入済みで勤怠記号が未記入のとき
            if (m.開始時 != string.Empty && m.開始分 != string.Empty && m.終了時 != string.Empty && m.終了分 != string.Empty)
            {
                if (kg == string.Empty)
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wSEHM] = global.flgOn;           // 開始終了時間
                    return false;
                }
            }

            // 一日に複数の勤怠記号が記入されている場合の警告事項
            if (kg.Length > 1)
            {
                // 「1.定時勤務」とその他の記号が併記されている
                if (kg.Contains(KINTAIKIGOU_1))
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    return false;
                }

                // 「4:残業」と「0:半休」が併記されているとき
                if (kg.Contains(KINTAIKIGOU_4) && kg.Contains(KINTAIKIGOU_0))
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    return false;
                }

                /*
                 * 「B：宿日直」または「c：保安」が1日に複数記入されているとき 2014/11/10
                 * 例）"BB" または "CC"
                 */
                if (m.勤怠記号1 == m.勤怠記号2)
                {
                    if (m.勤怠記号1.Equals(KINTAIKIGOU_B) || m.勤怠記号1.Equals(KINTAIKIGOU_C))
                    {
                        wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                        return false;
                    }
                }
            }

            //-------------------------------------------------------------------------
            //      始業時刻・終業時刻
            //-------------------------------------------------------------------------
            // 終業時刻が始業時刻以前のとき
            if (m.開始時 != string.Empty && m.開始分 != string.Empty && m.終了時 != string.Empty && m.終了分 != string.Empty)
            {
                st = Utility.StrtoInt(m.開始時) * 100 + Utility.StrtoInt(m.開始分);
                et = Utility.StrtoInt(m.終了時) * 100 + Utility.StrtoInt(m.終了分);
                if (st > et)
                {
                    wArray[wSEHM] = global.flgOn;   // 開始終了時間
                    return false;
                }
            }

            // 始業・終業時刻が無記入で以下に該当するとき
            if (m.開始時 == string.Empty && m.開始分 == string.Empty && m.終了時 == string.Empty && m.終了分 == string.Empty)
            {
                // 休日ではない
                if (!cHoliday)
                {
                    // 欠勤でも有給でも特別休暇でもない
                    if (!kg.Contains(KINTAIKIGOU_5) && !kg.Contains(KINTAIKIGOU_9) && !kg.Contains(KINTAIKIGOU_A))
                    {
                        // 勤怠記号「1:定時勤務」無記入のとき
                        if (!kg.Contains(KINTAIKIGOU_1))
                        {
                            wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                            wArray[wSEHM] = global.flgOn;           // 開始終了時間
                            return false;
                        }
                    }
                }
            }

            //-------------------------------------------------------------------------
            //      普通残業
            //-------------------------------------------------------------------------
            if (m.開始時 != string.Empty && m.開始分 != string.Empty && m.終了時 != string.Empty && m.終了分 != string.Empty)
            {                
                if (m.時間外時 != string.Empty && m.時間外分 != string.Empty)
                {
                    // 始業時刻から終業時刻までが8時間50分以下で「普通残業」欄が記入されているとき
                    // ※大阪製造部グループは除く
                    if (sr.大阪製造部勤務グループ == string.Empty)
                    {
                        double wt = getWorkTime(m.開始時, m.開始分, m.終了時, m.終了分, 0); // 実働時間を取得
                        if (wt < 530)
                        {
                            wArray[wSEHM] = global.flgOn;   // 開始終了時間
                            wArray[wZHM] = global.flgOn;    // 普通残業時間
                            return false;
                        }
                    }

                    // 勤怠記号が「6:出張」、「0:半休」、「8:早退」で当欄が記入されているとき                    ;
                    if (kg.Contains(KINTAIKIGOU_6) || kg.Contains(KINTAIKIGOU_0) || kg.Contains(KINTAIKIGOU_8))
                    {
                        wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                        wArray[wZHM] = global.flgOn;            // 普通残業時間
                        return false;
                    }
                }
            }

            //-------------------------------------------------------------------------
            //      深夜残業
            //-------------------------------------------------------------------------
            // 「普通残業」が無記入で「深夜残業」が記入済みのとき
            if (m.時間外時 == string.Empty && m.時間外分 == string.Empty)
            {
                if (m.深夜時 != string.Empty || m.深夜分 != string.Empty)
                {
                    wArray[wZHM] = global.flgOn;    // 普通残業時間
                    wArray[wSIHM] = global.flgOn;   // 深夜残業時間
                    return false;
                }
            }

            // 終業時刻が22時以降で「深夜残業」が無記入のとき
            st = Utility.StrtoInt(m.開始時) * 100 + Utility.StrtoInt(m.開始分);
            et = Utility.StrtoInt(m.終了時) * 100 + Utility.StrtoInt(m.終了分);
            if ((st > et) || (et > 2200))
            {
                if (m.深夜時 == string.Empty && m.深夜分 == string.Empty)
                {
                    wArray[wSEHM] = global.flgOn;   // 開始終了時間
                    wArray[wSIHM] = global.flgOn;   // 深夜残業時間
                    return false;
                }
            }

            // 始業時刻、終業時刻が無記入で当欄が記入されているとき
            if (m.開始時 == string.Empty && m.開始分 == string.Empty && m.終了時 == string.Empty && m.終了分 == string.Empty)
            {
                if (m.深夜時 != string.Empty || m.深夜分 != string.Empty)
                {
                    wArray[wSEHM] = global.flgOn;   // 開始終了時間
                    wArray[wSIHM] = global.flgOn;   // 深夜残業時間
                    return false;
                }
            }

            // 始業時刻から終業時刻の22時～5時に該当する時間より当欄記入時間が多いとき
            // 2014/10/10 始業時刻から終業時刻の範囲が22時～5時に該当しない場合で記入されているときを含む
            if (m.開始時 != string.Empty && m.開始分 != string.Empty && m.終了時 != string.Empty && m.終了分 != string.Empty)
            {
                // 深夜勤務時間取得
                double sw = getShinyaWorkTime(m.開始時, m.開始分, m.終了時, m.終了分);

                // 深夜残業記入時間
                double sz = Utility.StrtoDouble(m.深夜時) * 60 + Utility.StrtoDouble(m.深夜分);

                /* 22時～5時に該当する時間より当欄記入時間が多いとき
                 * 2014/10/10 始業時刻から終業時刻の範囲が22時～5時に該当しない場合で
                 * 記入されているとき(sw==0 && sz > 0)を含む
                */
                if (sw < sz)
                {
                    wArray[wSEHM] = global.flgOn;   // 開始終了時間
                    wArray[wSIHM] = global.flgOn;   // 深夜残業時間
                    return false;
                }
            }

            //-------------------------------------------------------------------------
            //      休日勤務時間
            //-------------------------------------------------------------------------
            if (m.休日出勤時 != string.Empty || m.休日出勤分 != string.Empty)
            {
                // 勤怠記号が「2:休日勤務」が無記入で当欄が記入されているとき
                if (!kg.Contains(KINTAIKIGOU_2))
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wKSHM] = global.flgOn;           // 休日勤務時間
                    return false;
                }

                // 勤怠記号が「6:出張」で当欄が記入されているとき
                if (kg.Contains(KINTAIKIGOU_6))
                {
                    wArray[wKintaiKigou] = global.flgOn;    // 勤怠記号
                    wArray[wKSHM] = global.flgOn;           // 休日勤務時間
                    return false;
                }
            }

            return true;
        }

        ///---------------------------------------------------------------------------
        /// <summary>
        ///     与えられた日付が休日か判断する </summary>
        /// <param name="dts">
        ///     MTYSDataSet</param>
        /// <param name="eDate">
        ///     任意の日付</param>
        /// <param name="cKbn">
        ///     該当社員の帳票区分</param>
        /// <param name="oskGrp">
        ///     該当社員の大阪製造部グループ</param>
        /// <returns>
        ///     休日：true, 非休日：false</returns>
        ///---------------------------------------------------------------------------
        public bool getHolidayStatus(MTYSDataSet dts, DateTime eDate, string cKbn, string oskGrp)
        {
            //--------------------------------------------------------------
            //      休日情報を取得 （cHoliday:true 休日）
            //--------------------------------------------------------------
            bool cHoliday = false;

            var s = dts.休日.Where(a => a.年月日 == eDate);

            foreach (var t in s)
            {
                // 本社で休日に該当する
                if (cKbn == global.C_HONSHA && t.本社使用 == global.flgOn)
                {
                    cHoliday = true;
                    break;
                }

                // 静岡で休日に該当する
                if (cKbn == global.C_SHIZUOKA && t.静岡使用 == global.flgOn)
                {
                    cHoliday = true;
                    break;
                }

                // 大阪製造部で休日に該当する
                if (cKbn == global.C_OOSAKA)
                {
                    // 大阪製造部Aグループ
                    if (oskGrp == global.OOSAKAG_A && t.大阪製造部A使用 == global.flgOn)
                    {
                        cHoliday = true;
                        break;
                    }

                    // 大阪製造部Bグループ
                    if (oskGrp == global.OOSAKAG_B && t.大阪製造部B使用 == global.flgOn)
                    {
                        cHoliday = true;
                        break;
                    }

                    // 大阪製造部Cグループ
                    if (oskGrp == global.OOSAKAG_C && t.大阪製造部C使用 == global.flgOn)
                    {
                        cHoliday = true;
                        break;
                    }

                    // 大阪製造部Dグループ
                    if (oskGrp == global.OOSAKAG_D && t.大阪製造部D使用 == global.flgOn)
                    {
                        cHoliday = true;
                        break;
                    }

                    // 大阪製造部Aグループ
                    if (oskGrp == string.Empty && t.大阪製造部使用 == global.flgOn)
                    {
                        cHoliday = true;
                        break;
                    }
                }
            }

            return cHoliday;
        }
    }
}
