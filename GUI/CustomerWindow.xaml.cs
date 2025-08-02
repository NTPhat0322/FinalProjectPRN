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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Account? Account { get; set; }
        public CustomerWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                this.Close();
                return;
            }
            txtWelcome.Text = $"Welcome {Account.Name}";
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null) return;
            ServiceWindow serviceWindow = new ServiceWindow();
            serviceWindow.Account = Account;
            serviceWindow.ShowDialog();
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null) return;
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Account = Account;
            scheduleWindow.ShowDialog();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null) return;
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Account = Account;
            profileWindow.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Confirmation",
                                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
