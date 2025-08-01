using DAL.Entities;
using BLL.Services;
using System;
using System.Linq;
using System.Windows;

namespace GUI
{
    public partial class ServiceWindow : Window
    {
        public Account? Account { get; set; }
        private readonly ServiceService _serviceService;
        private int _editId = 0;

        public ServiceWindow()
        {
            InitializeComponent();
            _serviceService = new ServiceService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadServices();
        }

        private void LoadServices()
        {
            // Load tất cả Service chưa bị xóa
            dgService.ItemsSource = _serviceService.GetAll()
                                                  .Where(s => !s.IsDeleted)
                                                  .ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please enter Name and Price!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Price must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_editId == 0)
            {
                // Add service
                var service = new Service
                {
                    Name = txtName.Text.Trim(),
                    Price = price,
                    Description = txtDescription.Text.Trim(),
                    IsDeleted = false
                };

                try
                {
                    _serviceService.Add(service);
                    MessageBox.Show("Service added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearForm();
                    LoadServices();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to add service! \n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Update service
                var service = _serviceService.GetAll().FirstOrDefault(s => s.Id == _editId);
                if (service != null)
                {
                    service.Name = txtName.Text.Trim();
                    service.Price = price;
                    service.Description = txtDescription.Text.Trim();

                    if (_serviceService.Update(service))
                    {
                        MessageBox.Show("Service updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearForm();
                        LoadServices();
                        _editId = 0;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update service!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.Tag is int id)
            {
                var service = _serviceService.GetAll().FirstOrDefault(s => s.Id == id);
                if (service != null)
                {
                    _editId = service.Id;
                    txtName.Text = service.Name;
                    txtPrice.Text = service.Price.ToString();
                    txtDescription.Text = service.Description;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.Tag is int id)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this service?",
                                              "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirm == MessageBoxResult.Yes)
                {
                    var service = _serviceService.GetAll().FirstOrDefault(s => s.Id == id);
                    if (service != null && _serviceService.Delete(service))
                    {
                        MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadServices();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete service!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
        }
    }
}
