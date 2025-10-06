using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4_TH1_ConnectSQL_OneFile
{
    public class Form1 : Form
    {
        // === KHAI BÁO BIẾN ===
        Label lblTitle, lblStatus;
        TextBox txtConnectionString;
        Button btnMoKetNoi, btnDongKetNoi;

        // Chuỗi kết nối (thay lại tên server của bạn ở đây)
        string strCon = @"Data Source=BIGBABY;Initial Catalog=QLDiemSV;Integrated Security=True";

        SqlConnection sqlCon = null;

        public Form1()
        {
            // === CẤU HÌNH FORM ===
            this.Text = "TH1 - Kết nối CSDL SQL Server";
            this.Size = new Size(700, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Arial", 10);

            // === TIÊU ĐỀ ===
            lblTitle = new Label()
            {
                Text = "KẾT NỐI WINFORM VỚI CƠ SỞ DỮ LIỆU SQL SERVER",
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(60, 20)
            };
            this.Controls.Add(lblTitle);

            // === TEXTBOX CHUỖI KẾT NỐI ===
            Label lblStr = new Label()
            {
                Text = "Chuỗi kết nối:",
                Location = new Point(30, 80),
                AutoSize = true
            };
            txtConnectionString = new TextBox()
            {
                Location = new Point(150, 75),
                Width = 480,
                Text = strCon
            };
            this.Controls.Add(lblStr);
            this.Controls.Add(txtConnectionString);

            // === NHÓM NÚT ===
            btnMoKetNoi = new Button()
            {
                Text = "Mở kết nối",
                Location = new Point(150, 130),
                Size = new Size(150, 40)
            };
            btnDongKetNoi = new Button()
            {
                Text = "Đóng kết nối",
                Location = new Point(350, 130),
                Size = new Size(150, 40)
            };
            this.Controls.Add(btnMoKetNoi);
            this.Controls.Add(btnDongKetNoi);

            // === TRẠNG THÁI ===
            lblStatus = new Label()
            {
                Text = "Trạng thái: Chưa kết nối",
                Location = new Point(150, 200),
                AutoSize = true,
                ForeColor = Color.Red
            };
            this.Controls.Add(lblStatus);

            // === GÁN SỰ KIỆN ===
            btnMoKetNoi.Click += BtnMoKetNoi_Click;
            btnDongKetNoi.Click += BtnDongKetNoi_Click;
        }

        // === SỰ KIỆN MỞ KẾT NỐI ===
        private void BtnMoKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(txtConnectionString.Text);
                }

                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MessageBox.Show("✅ Kết nối thành công!", "Thông báo");
                    lblStatus.Text = "Trạng thái: Đã kết nối";
                    lblStatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối: " + ex.Message, "Lỗi");
            }
        }

        // === SỰ KIỆN ĐÓNG KẾT NỐI ===
        private void BtnDongKetNoi_Click(object sender, EventArgs e)
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
                MessageBox.Show("🔒 Đã đóng kết nối.", "Thông báo");
                lblStatus.Text = "Trạng thái: Đã đóng kết nối";
                lblStatus.ForeColor = Color.Red;
            }
        }

        // === HÀM MAIN ===
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
