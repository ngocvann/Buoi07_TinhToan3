using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtSo1.Enter += TextBox_SelectAll;
            txtSo2.Enter += TextBox_SelectAll;
            txtSo1.GotFocus += TextBox_SelectAll;
            txtSo2.GotFocus += TextBox_SelectAll;
            txtSo1.MouseUp += TextBox_KeepSelectAll;
            txtSo2.MouseUp += TextBox_KeepSelectAll;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";
            radCong.Checked = true;             //đầu tiên chọn phép cộng
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void TextBox_SelectAll(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            BeginInvoke((Action)(() => tb.SelectAll()));
        }

        private void TextBox_KeepSelectAll(object sender, MouseEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.SelectionLength == 0) tb.SelectAll();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            string strSo1 = txtSo1.Text.Trim();
            string strSo2 = txtSo2.Text.Trim();

            if (strSo1.Replace("-", "").Replace(".", "").Length > 30 ||
        strSo2.Replace("-", "").Replace(".", "").Length > 30)
            {
                MessageBox.Show("Giá trị nhập vào lớn hơn 30 chữ số. Vui lòng nhập lại",
                                "Lỗi nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //lấy giá trị của 2 ô số
            double so1, so2, kq = 0;

            // Check input data
            bool isNumber1 = double.TryParse(txtSo1.Text, NumberStyles.AllowLeadingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out so1);
            bool isNumber2 = double.TryParse(txtSo2.Text, NumberStyles.AllowLeadingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out so2);

            if(!isNumber1 || !isNumber2){
                MessageBox.Show("Giá trị nhập vào không hợp lệ", "Lỗi nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //Thực hiện phép tính dựa vào phép toán được chọn
            if (radCong.Checked) kq = so1 + so2;
            else if (radTru.Checked) kq = so1 - so2;
            else if (radNhan.Checked) kq = so1 * so2;
            else if (radChia.Checked)
            {
                if (so2 == 0)
                {
                    MessageBox.Show("Số bị chia của phép chia phải khác 0", "Lỗi phép tính", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    kq = so1 / so2;
                }
            }
            //Hiển thị kết quả lên trên ô kết quả
            txtKq.Text = kq.ToString();
        }
    }
}
