using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace MTYS_OCR.Common
{
    class clsLinqCsv
    {
        [CsvColumn(Name = "カード番号")]
        public string sCardNum { get; set; }

        [CsvColumn(Name = "従業員番号")]
        public int sNum { get; set; }

        [CsvColumn(Name = "従業員氏名")]
        public string sName { get; set; }

        [CsvColumn(Name = "所属番号")]
        public int shozoku { get; set; }

        [CsvColumn(Name = "年/月/日")]
        public DateTime sDate { get; set; }

        [CsvColumn(Name = "シフト番号")]
        public string shiftNum { get; set; }

        [CsvColumn(Name = "平日/休日区分")]
        public string sDummy1 { get; set; }

        [CsvColumn(Name = "不在理由")]
        public string sDummy2 { get; set; }

        [CsvColumn(Name = "出勤打刻")]
        public string sShukinTime { get; set; }

        [CsvColumn(Name = "出勤マーク")]
        public string sDummy3 { get; set; }

        [CsvColumn(Name = "外出打刻")]
        public string sDummy4 { get; set; }

        [CsvColumn(Name = "外出マーク")]
        public string sDummy5 { get; set; }

        [CsvColumn(Name = "戻打刻")]
        public string sDummy6 { get; set; }

        [CsvColumn(Name = "戻マーク")]
        public string sDummy7 { get; set; }

        [CsvColumn(Name = "退勤打刻")]
        public string sTaikinTime { get; set; }

        [CsvColumn(Name = "退勤マーク")]
        public string sDummy8 { get; set; }

        [CsvColumn(Name = "例外１")]
        public string sDummy9 { get; set; }

        [CsvColumn(Name = "例外マーク")]
        public string sDummy10 { get; set; }

        [CsvColumn(Name = "例外２")]
        public string sDummy11 { get; set; }

        [CsvColumn(Name = "例外２マーク")]
        public string sDummy12 { get; set; }

        [CsvColumn(Name = "コメント")]
        public string sDummy13 { get; set; }
    }
}
