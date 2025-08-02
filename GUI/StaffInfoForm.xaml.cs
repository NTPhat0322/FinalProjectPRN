using System;
using System.Collections.Generic;
using System.Linq;
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
using DAL.DTOs;
using DAL.Entities;

namespace GUI
{
    /// <summary>
    /// Interaction logic for StaffInfoForm.xaml
    /// </summary>
    public partial class StaffInfoForm : Window
    {
        private AccountService _accountService = new();
        private DepartmentService _departmentService = new();
        private StaffInfoService _staffService = new();
        public StaffInformationDTO? CurrentStaffInformation { get; set; }
        public StaffInfoForm()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //hỏi có muốn hủy không
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close(); //đóng cửa sổ
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInputs())
            {
                return; // Nếu không hợp lệ, dừng lại
            }
            //lấy thông tin từ các trường nhập liệu
            var selectedAccount = cmbAccount.SelectedItem as Account;
            var selectedDepartment = cmbDepartment.SelectedItem as Department;
            var degree = txtDegree.Text.Trim();
            int experience = 0; // Mặc định là 0 nếu không nhập
            if (!string.IsNullOrWhiteSpace(txtExperience.Text))
            {
                int.TryParse(txtExperience.Text, out experience);
            }
            //kiểm tra xem là update hay thêm mới
            if (CurrentStaffInformation != null)
            {
                //nếu có staff thì cập nhật
                CurrentStaffInformation.DepartmentId = selectedDepartment.Id;
                CurrentStaffInformation.Degree = degree;
                CurrentStaffInformation.YearOfExperience = experience;
                ////cập nhật vào db
                _staffService.Update(CurrentStaffInformation);
                MessageBox.Show("Thông tin nhân viên đã được cập nhật thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //đóng cửa sổ
            }
            else
            {
                //xóa staff info của đối tượng account nếu có
                _staffService.DeleteByAccountId(selectedAccount.Id);
                //tạo đối tượng nhân viên mới
                StaffInfo newStaffInfo = new StaffInfo
                {
                    AccountId = selectedAccount.Id,
                    DepartmentId = selectedDepartment.Id,
                    Degree = degree,
                    YearOfExperience = experience
                };
                //thêm vào db
                _staffService.Add(newStaffInfo);
                MessageBox.Show("Thông tin nhân viên đã được lưu thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //đóng cửa sổ
            }
        }

        private bool ValidateInputs()
        {
            if (cmbAccount.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbAccount.Focus();
                return false;
            }
            // Kiểm tra lựa chọn Khoa/Phòng ban
            if (cmbDepartment.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn khoa/phòng ban.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbDepartment.Focus();
                return false;
            }

            // Kiểm tra Số năm kinh nghiệm (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtExperience.Text))
            {
                int experience;
                if (!int.TryParse(txtExperience.Text, out experience) || experience < 0)
                {
                    MessageBox.Show("Vui lòng nhập số năm kinh nghiệm hợp lệ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtExperience.Focus();
                    return false;
                }
            }

            return true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataToAccountBox();
            FillDataToDepartmentBox();
            if (CurrentStaffInformation != null)
            {
                FillData(); //nếu có staff thì đổ dữ liệu vào các trường nhập liệu
                //đặt tiêu đề cửa sổ là "Cập nhật thông tin nhân viên"
                this.Title = "Cập nhật thông tin nhân viên";
                //không cho phép thay đổi tài khoản
                cmbAccount.IsEnabled = false; // không cho phép thay đổi tài khoản
            }
        }

        private void FillData()
        {
            if (CurrentStaffInformation != null)
            {
                //đổ dữ liệu vào các trường nhập liệu
                cmbAccount.SelectedValue = CurrentStaffInformation.AccountId;
                cmbDepartment.SelectedValue = CurrentStaffInformation.DepartmentId;
                txtDegree.Text = CurrentStaffInformation.Degree;
                txtExperience.Text = CurrentStaffInformation.YearOfExperience.ToString();
            }
        }

        private void FillDataToAccountBox()
        {
            //đổ dữ liệu account vào account combo box
            var accounts = _accountService.GetAll();
            if (accounts != null && accounts.Count > 0)
            {
                cmbAccount.ItemsSource = accounts;
                cmbAccount.DisplayMemberPath = "Name"; // Hiển thị tên trong ComboBox
                cmbAccount.SelectedValuePath = "Id"; // Lưu Id của tài khoản
            }
        }
        private void FillDataToDepartmentBox()
        {
            //đổ dữ liệu department vào department combo box
            var departments = _departmentService.GetAll();
            if (departments != null && departments.Count > 0)
            {
                cmbDepartment.ItemsSource = departments;
                cmbDepartment.DisplayMemberPath = "Name"; // Hiển thị tên trong ComboBox
                cmbDepartment.SelectedValuePath = "Id"; // Lưu Id của phòng ban
            }
        }
    }
}
