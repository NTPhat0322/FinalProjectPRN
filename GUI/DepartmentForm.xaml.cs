using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DepartmentForm.xaml
    /// </summary>
    public partial class DepartmentForm : Window
    {
        private DepartmentService _departmentService = new DepartmentService();
        public Department? CurrentDepartment { get; set; }
        public DepartmentForm()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //xác nhận 
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
            string departmentName = txtDepartmentName.Text.Trim();
            //kiểm tra xem là update hay thêm mới
            if (CurrentDepartment != null)
            {
                //cập nhật thông tin phòng ban
                CurrentDepartment.Name = departmentName;
                //thực hiện cập nhật vào cơ sở dữ liệu (giả sử có phương thức UpdateDepartment)
                _departmentService.Update(CurrentDepartment);
                MessageBox.Show("Cập nhật thông tin phòng ban thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //đóng cửa sổ
            }
            else
            {
                //tạo mới phòng ban
                Department newDepartment = new Department { Name = departmentName };
                //thực hiện thêm mới vào cơ sở dữ liệu (giả sử có phương thức AddDepartment)
                _departmentService.Add(newDepartment);
                MessageBox.Show("Thêm mới phòng ban thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //đóng cửa sổ
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khoa/phòng ban.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDepartmentName.Focus();
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentDepartment != null)
            {
                FormTitle.Text = "Cập nhật thông tin phòng ban";
                FillDepartmentDetails(); //nếu có department thì đổ dữ liệu vào các trường nhập liệu
            }
        }

        private void FillDepartmentDetails()
        {
            //nếu current derpartment khác null thì đổ dữ liệu vào các trường nhập liệu
            if (CurrentDepartment != null)
            {
                txtDepartmentName.Text = CurrentDepartment.Name;
            }

        }
    }
}
