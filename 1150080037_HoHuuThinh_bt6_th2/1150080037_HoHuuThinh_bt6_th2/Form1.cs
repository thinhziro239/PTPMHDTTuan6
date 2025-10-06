using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThucHanh2
{
    public class Form1 : Form
    {
        Label lblTitle, lblHoTen, lblLop, lblNgaySinh, lblDiaChi;
        TextBox txtHoTen, txtLop, txtDiaChi;
        DateTimePicker dtpNgaySinh;
        Button btnThem, btnXoa, btnSua, btnThoat;
        ListView lvSinhVien;
        GroupBox gbThongTin, gbChucNang;

        public Form1()
        {
            // === FORM ===
            this.Text = "Thực hành 2 - Quản lý Sinh viên";
            this.Size = new Size(750, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // === TIÊU ĐỀ ===
            lblTitle = new Label()
            {
                Text = "THÔNG TIN SINH VIÊN",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(230, 20)
            };
            this.Controls.Add(lblTitle);

            // === GROUPBOX THÔNG TIN ===
            gbThongTin = new GroupBox()
            {
                Text = "Thông tin chung sinh viên",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(690, 180),
                Location = new Point(25, 70)
            };
            this.Controls.Add(gbThongTin);

            // --- Họ tên ---
            lblHoTen = new Label() { Text = "Họ tên:", Location = new Point(40, 40), AutoSize = true };
            txtHoTen = new TextBox() { Location = new Point(120, 38), Width = 200 };

            // --- Lớp ---
            lblLop = new Label() { Text = "Lớp:", Location = new Point(370, 40), AutoSize = true };
            txtLop = new TextBox() { Location = new Point(430, 38), Width = 200 };

            // --- Ngày sinh ---
            lblNgaySinh = new Label() { Text = "Ngày sinh:", Location = new Point(40, 90), AutoSize = true };
            dtpNgaySinh = new DateTimePicker() { Location = new Point(120, 88), Width = 200 };

            // --- Địa chỉ ---
            lblDiaChi = new Label() { Text = "Địa chỉ:", Location = new Point(370, 90), AutoSize = true };
            txtDiaChi = new TextBox() { Location = new Point(430, 88), Width = 200 };

            gbThongTin.Controls.AddRange(new Control[]
            {
                lblHoTen, txtHoTen, lblLop, txtLop, lblNgaySinh, dtpNgaySinh, lblDiaChi, txtDiaChi
            });

            // === GROUPBOX CHỨC NĂNG ===
            gbChucNang = new GroupBox()
            {
                Text = "Chức năng",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Size = new Size(690, 70),
                Location = new Point(25, 260)
            };
            this.Controls.Add(gbChucNang);

            btnThem = new Button() { Text = "Thêm", Location = new Point(80, 25), Width = 100 };
            btnSua = new Button() { Text = "Sửa", Location = new Point(220, 25), Width = 100 };
            btnXoa = new Button() { Text = "Xóa", Location = new Point(360, 25), Width = 100 };
            btnThoat = new Button() { Text = "Thoát", Location = new Point(500, 25), Width = 100 };

            gbChucNang.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnThoat });

            // === LISTVIEW ===
            lvSinhVien = new ListView()
            {
                Location = new Point(25, 350),
                Size = new Size(690, 150),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };

            lvSinhVien.Columns.Add("Họ tên", 180);
            lvSinhVien.Columns.Add("Lớp", 120);
            lvSinhVien.Columns.Add("Ngày sinh", 150);
            lvSinhVien.Columns.Add("Địa chỉ", 200);

            this.Controls.Add(lvSinhVien);

            // === GÁN SỰ KIỆN ===
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
            btnThoat.Click += (s, e) => this.Close();
            lvSinhVien.SelectedIndexChanged += LvSinhVien_SelectedIndexChanged;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = new ListViewItem(txtHoTen.Text);
            item.SubItems.Add(txtLop.Text);
            item.SubItems.Add(dtpNgaySinh.Value.ToShortDateString());
            item.SubItems.Add(txtDiaChi.Text);
            lvSinhVien.Items.Add(item);

            ClearInput();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn 1 dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lvSinhVien.Items.Remove(lvSinhVien.SelectedItems[0]);
            ClearInput();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn 1 dòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var item = lvSinhVien.SelectedItems[0];
            item.SubItems[0].Text = txtHoTen.Text;
            item.SubItems[1].Text = txtLop.Text;
            item.SubItems[2].Text = dtpNgaySinh.Value.ToShortDateString();
            item.SubItems[3].Text = txtDiaChi.Text;
        }

        private void LvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedItems.Count == 0) return;
            var item = lvSinhVien.SelectedItems[0];

            txtHoTen.Text = item.SubItems[0].Text;
            txtLop.Text = item.SubItems[1].Text;
            dtpNgaySinh.Value = DateTime.Parse(item.SubItems[2].Text);
            txtDiaChi.Text = item.SubItems[3].Text;
        }

        private void ClearInput()
        {
            txtHoTen.Clear();
            txtLop.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Now;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
