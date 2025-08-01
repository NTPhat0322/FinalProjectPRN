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
using DAL.Entities;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        private ServiceService _serviceService = new ServiceService();

        public Service? CurrentService { get; set; }
        public ServiceForm()
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
            if (!ValidateInputs())
            {
                return; // Nếu không hợp lệ, dừng lại
            }
            //lấy thông tin từ các trường nhập liệu
            string serviceName = txtServiceName.Text.Trim();
            string serviceDescription = txtServiceDescription.Text.Trim();
            decimal servicePrice;
            if (!decimal.TryParse(txtServicePrice.Text, out servicePrice))
            {
                MessageBox.Show("Giá dịch vụ không hợp lệ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtServicePrice.Focus();
                return;
            }
            //kiểm tra xem là update hay thêm mới
            if (CurrentService != null)
            {
                //nếu có service thì cập nhật
                CurrentService.Name = serviceName;
                CurrentService.Description = serviceDescription;
                CurrentService.Price = servicePrice;
                //cập nhật vào db
                _serviceService.Update(CurrentService);
                MessageBox.Show("Dịch vụ đã được cập nhật thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); //đóng cửa sổ
                return;
            }

            //tạo đối tượng dịch vụ mới
            Service service = new()
            {
                Name = serviceName,
                Description = serviceDescription,
                Price = servicePrice
            };
            //them vao db
            _serviceService.Add(service);
            MessageBox.Show("Dịch vụ đã được lưu thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); //đóng cửa sổ
        }

        private bool ValidateInputs()
        {
            // Kiểm tra Tên Dịch Vụ
            if (string.IsNullOrWhiteSpace(txtServiceName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtServiceName.Focus();
                return false;
            }

            // Kiểm tra Giá
            if (string.IsNullOrWhiteSpace(txtServicePrice.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập giá dịch vụ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtServicePrice.Focus();
                return false;
            }

            decimal price;
            if (!decimal.TryParse(txtServicePrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtServicePrice.Focus();
                return false;
            }

            return true;

        }

        // Allow only numbers and decimal point in price field
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,.]"); // Chỉ cho phép nhập chữ số và dấu thập phân
            e.Handled = regex.IsMatch(e.Text);

            // Kiểm tra nếu người dùng nhập thêm dấu chấm thập phân khi đã có sẵn một dấu chấm
            if (e.Text == "." && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void FillServiceDetails()
        {
            //nếu current service khác null thì đổ dữ liệu vào các trường nhập liệu
            if (CurrentService != null)
            {
                txtServiceName.Text = CurrentService.Name;
                txtServiceDescription.Text = CurrentService.Description;
                txtServicePrice.Text = CurrentService.Price.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentService != null)
            {
                this.Title = "Cập nhật dịch vụ"; //nếu có service thì đổi tiêu đề thành cập nhật dịch vụ
                FillServiceDetails(); //nếu có service thì đổ dữ liệu vào các trường nhập liệu
            }
        }
    }
}
