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
    /// <summary>
    /// Interaction logic for StaffsWindow.xaml
    /// </summary>
    public partial class StaffsWindow : Window
    {
        public Account? Account { get; set; }
        private readonly StaffInfoService _staffService;

        public StaffsWindow()
        {
            InitializeComponent();
            _staffService = new StaffInfoService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                this.Close();
                return;
            }

            dgStaffs.ItemsSource = _staffService.GetAllStaff();
        }
        private void BtnBookDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                MessageBox.Show("Bạn cần đăng nhập để đặt lịch!", "Thông báo");
                return;
            }

            int doctorId = (int)((Button)sender).Tag;

            // Mở cửa sổ đặt lịch chi tiết
            var bookingWindow = new DoctorBookingWindow
            {
                CustomerId = Account.Id,
                DoctorId = doctorId
            };

            bookingWindow.ShowDialog();
        }
    }
}
