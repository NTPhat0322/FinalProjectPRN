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
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    /// 
    public partial class DoctorWindow : Window
    {
        public Account? DoctorAccount { get; set; }
        private readonly ScheduleService _scheduleService;

        public DoctorWindow()
        {
            InitializeComponent();
            _scheduleService = new ScheduleService();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DoctorAccount == null)
            {
                MessageBox.Show("Không tìm thấy thông tin bác sĩ!", "Lỗi");
                this.Close();
                return;
            }

            // Lấy danh sách lịch hẹn của Doctor
            dgAppointments.ItemsSource = _scheduleService.GetByDoctor(DoctorAccount.Id);
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
