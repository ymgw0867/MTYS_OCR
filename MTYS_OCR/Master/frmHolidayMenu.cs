using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MTYS_OCR.Master
{
    public partial class frmHolidayMenu : Form
    {
        public frmHolidayMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHolidayMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHolidayBatch frm = new frmHolidayBatch();
            frm.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 休日カレンダー表示
            this.Hide();
            frmCalender frm = new frmCalender();
            frm.ShowDialog();
            this.Show();
        }
    }
}
