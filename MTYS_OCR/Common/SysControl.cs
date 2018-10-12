﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.SqlClient;
using System.Data.OleDb;

namespace MTYS_OCR.Common
{
    class SysControl
    {
        //設定データベース接続
        public class SetDBConnect
        {
            public OleDbConnection cnOpen()
            {
                // データベース接続文字列
                OleDbConnection Cn = new OleDbConnection();
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sb.Append(Properties.Settings.Default.mdbPath);
                sb.Append(global.MDBFILE);
                Cn.ConnectionString = sb.ToString();
                Cn.Open();
                return Cn;
            }
        }

    }
}
