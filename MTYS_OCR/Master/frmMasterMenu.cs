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
    public partial class frmMasterMenu : Form
    {
        public frmMasterMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMasterMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void frmMasterMenu_Load(object sender, EventArgs e)
        {
            // ウィンドウ最大サイズ
            Common.Utility.WindowsMaxSize(this, this.Width, this.Height);

            // ウィンドウ最小サイズ
            Common.Utility.WindowsMinSize(this, this.Width, this.Height); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmKintaiKbn frm = new frmKintaiKbn();
            frm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            //frmTokubetsuTeate frm = new frmTokubetsuTeate();
            //frm.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShukkinType frm = new frmShukkinType();
            frm.ShowDialog();
            this.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmHolidayMenu frm = new frmHolidayMenu();
            frm.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShukinDays frm = new frmShukinDays();
            frm.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShozokuChoKbn frm = new frmShozokuChoKbn();
            frm.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmShain frm = new frmShain();
            frm.ShowDialog();
            this.Show();
        }
    }
}
