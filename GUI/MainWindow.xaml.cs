using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Services;
using DAL.Entities;
using DAL.Utils;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountService _accountService = new();
        private RoleService _roleService = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password.Trim();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Account? account = _accountService.Login(email, password);
            if (account is null)
            {
                MessageBox.Show("Invalid Email or Password!", "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var role = _roleService.GetRoleById(account.RoleId);
            if(role!.Name == RoleName.Admin)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.ShowDialog();
            }
            else if (role.Name == RoleName.Manager)
            {
                ManagerWindow managerWindow = new ManagerWindow();
                managerWindow.ShowDialog();
            }
            else if (role.Name == RoleName.Customer)
            {
                CustomerWindow customerWindow = new CustomerWindow();
                customerWindow.Account = account;
                customerWindow.ShowDialog();
            }
            else if (role.Name == RoleName.Doctor)
            {
                DoctorWindow doctorWindow = new DoctorWindow();
                doctorWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Unknown role!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SigningUpWindow signingUpWindow = new SigningUpWindow();
            signingUpWindow.ShowDialog();
        }
    }
}