using BLL.Services;
using DAL.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    public partial class ScheduleWindow : Window
    {
        private readonly ScheduleService _scheduleService;
        private readonly ServiceService _serviceService;

        // Nhận thông tin Account từ CustomerWindow
        public Account? Account { get; set; }

        public ScheduleWindow()
        {
            InitializeComponent();
            _scheduleService = new ScheduleService();
            _serviceService = new ServiceService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                MessageBox.Show("Account not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            
            cbService.ItemsSource = _serviceService.GetAll();

            
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            if (Account == null) return;
            dgSchedule.ItemsSource = _scheduleService.GetByAccount(Account.Id);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                MessageBox.Show("Account not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbService.SelectedItem == null || dpDate.SelectedDate == null || string.IsNullOrWhiteSpace(txtTime.Text))
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy service
            var service = cbService.SelectedItem as Service;
            if (service == null)
            {
                MessageBox.Show("Invalid service!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra giờ nhập vào
            if (!TimeSpan.TryParse(txtTime.Text, out TimeSpan time))
            {
                MessageBox.Show("Invalid time format! Use HH:mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo DateTime cho ScheduleTime
            var scheduleDateTime = dpDate.SelectedDate.Value.Date + time;

            // Tạo schedule mới
            var schedule = new Schedule
            {
                AccountId = Account.Id,
                ServiceId = service.Id,
                ScheduleTime = scheduleDateTime,
                IsDeleted = false
            };

            // Lưu vào DB
            bool added = _scheduleService.Add(schedule);
            if (added)
            {
                MessageBox.Show("Schedule added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadSchedule();
            }
            else
            {
                MessageBox.Show("Failed to add schedule!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int scheduleId)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this schedule?",
                                              "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool deleted = _scheduleService.Delete(scheduleId);
                    if (deleted)
                    {
                        MessageBox.Show("Schedule deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadSchedule();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete schedule!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
