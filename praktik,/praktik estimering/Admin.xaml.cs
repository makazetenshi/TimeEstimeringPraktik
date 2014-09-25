using System;
using System.Collections.Generic;
using System.Data;
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

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {

        public Admin()
        {
            InitializeComponent();
            AdminService.Instance.createAllAdapterCommands();
        }

        private void updateTables()
        {

            AdminService.Instance.updateTables();

            dayDataGrid.ItemsSource = AdminService.Instance.day.DefaultView;
            estimateDataGrid.ItemsSource = AdminService.Instance.estimate.DefaultView;
            formulaDataGrid.ItemsSource = AdminService.Instance.formula.DefaultView;
            examDataGrid.ItemsSource = AdminService.Instance.exam.DefaultView;
            userDataGrid.ItemsSource = AdminService.Instance.users.DefaultView;
            periodsDataGrid.ItemsSource = AdminService.Instance.period.DefaultView;
            otherDataGrid.ItemsSource = AdminService.Instance.other.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateTables();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save all changes made? Note: You can't undo these changes once you press Accept", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AdminService.Instance.updateDatabase();
                updateTables();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("No changes you have made will be saved. Do you want to exit and return to login?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                UserService.Instance.logUserOut();
                Window login = new MainWindow();
                login.Show();
                this.Close();
            }
        }

        private void User_GotFocus(object sender, RoutedEventArgs e)
        {
            userDataGrid.CanUserResizeColumns = false;
            userDataGrid.CanUserResizeRows = false;
            userDataGrid.CanUserDeleteRows = true;
        }

        private void Day_GotFocus(object sender, RoutedEventArgs e)
        {
            dayDataGrid.CanUserResizeColumns = false;
            dayDataGrid.CanUserResizeRows = false;
            dayDataGrid.CanUserDeleteRows = true;
        }

        private void Estimate_GotFocus(object sender, RoutedEventArgs e)
        {
            estimateDataGrid.CanUserResizeColumns = false;
            estimateDataGrid.CanUserResizeRows = false;
            estimateDataGrid.CanUserDeleteRows = true;
        }

        private void Formula_GotFocus(object sender, RoutedEventArgs e)
        {
            formulaDataGrid.CanUserResizeColumns = false;
            formulaDataGrid.CanUserResizeRows = false;
            formulaDataGrid.CanUserDeleteRows = true;
        }

        private void Exam_GotFocus(object sender, RoutedEventArgs e)
        {
            examDataGrid.CanUserResizeColumns = false;
            examDataGrid.CanUserResizeRows = false;
            examDataGrid.CanUserDeleteRows = true;
        }

        private void Period_GotFocus(object sender, RoutedEventArgs e)
        {
            if(periodsDataGrid.Columns.Count > 0)
            periodsDataGrid.Columns[0].Visibility = Visibility.Hidden;
            periodsDataGrid.IsReadOnly = true;
            periodsDataGrid.CanUserResizeColumns = false;
            periodsDataGrid.CanUserResizeRows = false;
        }

        private void Other_GotFocus(object sender, RoutedEventArgs e)
        {
            otherDataGrid.CanUserResizeColumns = false;
            otherDataGrid.CanUserResizeRows = false;
            otherDataGrid.CanUserDeleteRows = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to restore local changes made?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                updateTables();
            }
        }
    }
}
