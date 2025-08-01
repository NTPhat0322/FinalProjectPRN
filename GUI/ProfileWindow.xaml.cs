using System;
using System.Windows;
using System.Windows.Controls;
using DAL.Entities;
using BLL.Services;

namespace GUI
{
    public partial class ProfileWindow : Window
    {
        public Account? Account { get; set; }
        private readonly AccountService _accountService;

        public ProfileWindow()
        {
            InitializeComponent();
            _accountService = new AccountService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Account == null)
            {
                MessageBox.Show("No account data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            txtName.Text = Account.Name;
            txtEmail.Text = Account.Email;
            txtRole.Text = Account.Role?.Name ?? "N/A";

            foreach (ComboBoxItem item in cbGender.Items)
            {
                if (item.Content.ToString() == Account.Gender)
                {
                    cbGender.SelectedItem = item;
                    break;
                }
            }

            if (Account.DateOfBirth.HasValue)
            {
                dpDOB.SelectedDate = Account.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Account == null) return;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Full Name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            Account.Name = txtName.Text.Trim();
            Account.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
            Account.DateOfBirth = dpDOB.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dpDOB.SelectedDate.Value)
                : null;

            bool updated = _accountService.Update(Account);
            if (updated)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update profile!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
