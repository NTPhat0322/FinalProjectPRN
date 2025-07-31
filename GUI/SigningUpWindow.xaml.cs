using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Services;
using DAL.Entities;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SigningUpWindow.xaml
    /// </summary>
    public partial class SigningUpWindow : Window
    {
        private AccountService _accountService = new AccountService();
        public SigningUpWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return; // Nếu không hợp lệ, dừng lại
            }

            //lấy thông tin từ các trường nhập liệu
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();
            DateTime? dateOfBirth = dpDateOfBirth.SelectedDate;
            bool isMale = rbMale.IsChecked ?? true;


            //tạo đối tượng tài khoản mới
            Account account = new()
            {
                RoleId = 4,
                Email = email,
                Password = password,
                Name = name,
                DateOfBirth = DateOnly.FromDateTime(dateOfBirth!.Value),
                Gender = isMale ? "Nam" : "Nữ",
            };

            //add db
            if (_accountService.Add(account))
            {
                MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Đóng cửa sổ đăng ký
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            // Kiểm tra email đã tồn tại chưa
            if (_accountService.GetByEmail(txtEmail.Text.Trim()) != null)
            {
                MessageBox.Show("Email đã tồn tại. Vui lòng sử dụng một email khác.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            // Kiểm tra định dạng email
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Vui lòng nhập một địa chỉ email hợp lệ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }

            // Kiểm tra độ dài mật khẩu
            if (txtPassword.Password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            // Ngày sinh phải trước ngày hiện tại
            if (dpDateOfBirth.SelectedDate.HasValue && dpDateOfBirth.SelectedDate.Value >= DateTime.Today)
            {
                MessageBox.Show("Ngày sinh phải trước ngày hiện tại.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDateOfBirth.Focus();
                return false;
            }

            // Ngày sinh là bắt buộc
            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng nhập ngày sinh.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDateOfBirth.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtName.Focus();
                return false;
            }

            return true;
        }

    }
}
