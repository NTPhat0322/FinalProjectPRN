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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AccountService _accountService = new();
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

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.CurrentUser = userAccount;
            //this.Hide();
            //mainWindow.ShowDialog();
            //this.Show();

        }
    }
}