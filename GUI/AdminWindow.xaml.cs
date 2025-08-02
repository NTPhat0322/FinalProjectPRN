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
using DAL.DTOs;
using DAL.Entities;

namespace GUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private DepartmentService _departmentService = new();
        private StaffInfoService _staffInfoService = new();
        private AccountService _accountService = new();
        private ServiceService _serviceService = new();

        private List<StaffInformationDTO> _currentStaffInfoDTOs = null!;
        private List<Department> _currentDepartments = null!;
        private List<Service> _currentServices = null!;

        public AdminWindow()
        {
            InitializeComponent();
            LoadAllData();
        }
        private void LoadAllData()
        {
            LoadServices();
            LoadDepartments();
            LoadStaffInfo();
        }
        private void LoadDepartments()
        {
            _currentDepartments = _departmentService.GetAll();
            dgDepartments.ItemsSource = _currentDepartments;
        }
        private void LoadStaffInfo()
        {
            _currentStaffInfoDTOs = _staffInfoService.CreateStaffInfoDTO();
            dgStaffInfo.ItemsSource = _currentStaffInfoDTOs;
        }
        private void LoadServices()
        {
            _currentServices = _serviceService.GetAll();
            dgServices.ItemsSource = _currentServices;
        }

        private void ClearStaffSearchButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearchStaff.Clear();
            LoadStaffInfo();
        }

        private void SearchDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            //lấy giá trị tìm kiếm từ ô nhập liệu
            string searchText = txtSearchDepartment.Text.Trim();

            //kiểm tra xem ô nhập liệu có rỗng không
            if (string.IsNullOrEmpty(searchText))
            {
                //nếu rỗng thì hiển thị tất cả
                LoadDepartments();
            }
            else
            {
                //nếu không rỗng thì lọc danh sách theo tên
                LoadDepartments();
                var filteredDepartments = _currentDepartments
                    .Where(d => d.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                //cập nhật nguồn dữ liệu của DataGrid
                dgDepartments.ItemsSource = filteredDepartments;
            }
        }

        private void CancelStaffButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            //hỏi xem người dùng có chắc chắn muốn đăng xuất không
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //nếu có thì đăng xuất
                this.Close();
            }
        }

        private void SearchServiceButton_Click(object sender, RoutedEventArgs e)
        {
            //lấy giá trị tìm kiếm từ ô nhập liệu
            string searchText = txtSearchService.Text.Trim();
            //kiểm tra xem ô nhập liệu có rỗng không
            if (string.IsNullOrEmpty(searchText))
            {
                //nếu rỗng thì hiển thị tất cả dịch vụ
                LoadServices();
            }
            else
            {
                //nếu không rỗng thì lọc danh sách dịch vụ theo tên hoặc mô tả
                LoadServices();
                var filteredServices = _currentServices
                    .Where(s => s.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                s.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                //cập nhật nguồn dữ liệu của DataGrid
                dgServices.ItemsSource = filteredServices;
            }
        }
        private void ClearServiceSearchButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearchService.Clear();
            LoadServices();
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm serviceForm = new ServiceForm();
            serviceForm.CurrentService = null;
            serviceForm.ShowDialog();
            this.LoadServices();
        }

        private void TxtSearchService_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var service = button.DataContext as Service;
            
            if (service != null)
            {
                if (service.IsDeleted)
                {
                    MessageBox.Show("Dịch vụ này đã bị xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                ServiceForm serviceForm = new ServiceForm();
                serviceForm.CurrentService = service;
                serviceForm.ShowDialog();
                this.LoadServices();
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ để chỉnh sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var service = button.DataContext as Service;
            
            if(service != null)
            {
                if (service.IsDeleted)
                {
                    MessageBox.Show("Dịch vụ này đã bị xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ '{service.Name}' không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _serviceService.Delete(service);
                    LoadServices();
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveServiceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelServiceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSearchDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearDepartmentSearchButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearchDepartment.Clear();
            LoadDepartments();
        }

        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            DepartmentForm departmentForm = new DepartmentForm();
            departmentForm.CurrentDepartment = null;
            departmentForm.ShowDialog();
            LoadDepartments();
        }

        private void DgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var department = button.DataContext as Department;

            if (department != null)
            {
                //confirm
                MessageBoxResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng ban '{department.Name}' không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    //kiểm tra xem phòng ban có đang được sử dụng không
                    if (_staffInfoService.IsDepartmentInUse(department.Id))
                    {
                        MessageBox.Show("Phòng ban này đang được sử dụng bởi nhân viên, không thể xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _departmentService.Delete(department);
                    LoadDepartments();
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ để xóa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveDepartmentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelDepartmentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSearchStaff_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EditDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var department = button.DataContext as Department;

            if (department != null)
            {
                DepartmentForm departmentForm = new DepartmentForm();
                departmentForm.CurrentDepartment = department;
                departmentForm.ShowDialog();
                this.LoadDepartments();
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ để chỉnh sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchStaffButton_Click(object sender, RoutedEventArgs e)
        {
            //lấy giá trị tìm kiếm từ ô nhập liệu
            string searchText = txtSearchStaff.Text.Trim();
            //kiểm tra xem ô nhập liệu có rỗng không
            if (string.IsNullOrEmpty(searchText))
            {
                //nếu rỗng thì hiển thị tất cả
                LoadStaffInfo();
            }
            else
            {
                //nếu không rỗng thì lọc danh sách theo tên hoặc phòng ban
                LoadStaffInfo();
                var filteredStaff = _currentStaffInfoDTOs
                    .Where(s => s.StaffName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                s.DepartmentName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                //cập nhật nguồn dữ liệu của DataGrid
                dgStaffInfo.ItemsSource = filteredStaff;
            }
        }

        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            StaffInfoForm staffInfoForm = new StaffInfoForm();
            staffInfoForm.CurrentStaffInformation = null;
            staffInfoForm.ShowDialog();
            LoadStaffInfo();
        }

        private void DgStaffInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditStaffButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var staffInfo = button.DataContext as StaffInformationDTO;

            if (staffInfo != null)
            {
                StaffInfoForm staffInfoForm = new StaffInfoForm();
                staffInfoForm.CurrentStaffInformation = staffInfo;
                staffInfoForm.ShowDialog();
                this.LoadStaffInfo();
            }
            else
            {
                MessageBox.Show("Không tìm thấy dịch vụ để chỉnh sửa.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteStaffButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveStaffButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
