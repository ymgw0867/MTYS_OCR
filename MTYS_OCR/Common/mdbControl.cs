using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace MTYS_OCR.Common
{
    class mdbControl
    {
        protected StringBuilder sb = new StringBuilder();

        public void dbConnect(OleDbCommand cm)
        {
            // データベース接続文字列
            OleDbConnection Cn = new OleDbConnection();
            sb.Clear();
            
            //sb.Append("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
            //sb.Append(Properties.Settings.Default.mdbOlePath);

            sb.Append(Properties.Settings.Default.MTYSConnectionString);
            Cn.ConnectionString = sb.ToString();
            Cn.Open();

            cm.Connection = Cn;
            //sCom.Connection = Cn;
        }
    }
}
