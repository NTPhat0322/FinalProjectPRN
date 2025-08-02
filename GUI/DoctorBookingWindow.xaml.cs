using BLL.Services;
using DAL.Entities;
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

namespace GUI
{
    public partial class DoctorBookingWindow : Window
    {
        public int CustomerId { get; set; }
        public int DoctorId { get; set; } // Truyền từ StaffsWindow

        private readonly AccountService _accountService;
        private readonly ServiceService _serviceService;
        private readonly ScheduleService _scheduleService;

        public DoctorBookingWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
            _serviceService = new ServiceService();
            _scheduleService = new ScheduleService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Customer Info
            var customer = _accountService.GetById(CustomerId);
            if (customer != null)
            {
                txtCustomerName.Text = customer.Name;
                txtGender.Text = string.IsNullOrEmpty(customer.Gender) ? "Chưa rõ" : customer.Gender;
            }

            // Load Doctor Info
            var doctor = _accountService.GetById(DoctorId);
            if (doctor != null)
            {
                txtDoctorName.Text = doctor.Name;
            }

            // Load danh sách dịch vụ
            cbService.ItemsSource = _serviceService.GetAll();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (cbService.SelectedItem == null || dpDate.SelectedDate == null || string.IsNullOrWhiteSpace(txtTime.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi");
                return;
            }

            // Kiểm tra định dạng giờ
            if (!TimeSpan.TryParse(txtTime.Text, out var time))
            {
                MessageBox.Show("Giờ không hợp lệ! Định dạng: HH:mm", "Lỗi");
                return;
            }

            var scheduleTime = dpDate.SelectedDate.Value.Date + time;
            int serviceId = (int)cbService.SelectedValue;

            // Tạo lịch hẹn
            var schedule = new Schedule
            {
                AccountId = CustomerId,
                ServiceId = serviceId,
                DoctorId = DoctorId, 
                ScheduleTime = scheduleTime,
                IsDeleted = false
            };

            bool success = _scheduleService.Add(schedule);
            if (success)
            {
                MessageBox.Show("Đặt lịch thành công!", "Thông báo");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đặt lịch thất bại!", "Lỗi");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
