using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace MTYS_OCR.Common
{
    class clsLinqCsv
    {
        [CsvColumn(FieldIndex = 1)]
        public string sCardNum { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public int sNum { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public string sName { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public int shozoku { get; set; }

        [CsvColumn(FieldIndex = 5)]
        public DateTime sDate { get; set; }

        [CsvColumn(FieldIndex = 6)]
        public string shiftNum { get; set; }

        [CsvColumn(FieldIndex = 7)]
        public string sDummy1 { get; set; }

        [CsvColumn(FieldIndex = 8)]
        public string sDummy2 { get; set; }

        [CsvColumn(FieldIndex = 9)]
        public string sShukinTime { get; set; }

        [CsvColumn(FieldIndex = 10)]
        public string sDummy3 { get; set; }

        [CsvColumn(FieldIndex = 11)]
        public string sDummy4 { get; set; }

        [CsvColumn(FieldIndex = 12)]
        public string sDummy5 { get; set; }

        [CsvColumn(FieldIndex = 13)]
        public string sDummy6 { get; set; }

        [CsvColumn(FieldIndex = 14)]
        public string sDummy7 { get; set; }

        [CsvColumn(FieldIndex = 15)]
        public string sTaikinTime { get; set; }

        [CsvColumn(FieldIndex = 16)]
        public string sDummy8 { get; set; }

        [CsvColumn(FieldIndex = 17)]
        public string sDummy9 { get; set; }

        [CsvColumn(FieldIndex = 18)]
        public string sDummy10 { get; set; }

        [CsvColumn(FieldIndex = 19)]
        public string sDummy11 { get; set; }

        [CsvColumn(FieldIndex = 20)]
        public string sDummy12 { get; set; }

        [CsvColumn(FieldIndex = 21)]
        public string sDummy13 { get; set; }
    }
}
