using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();
        }
        private void overviewLoaded(object sender, RoutedEventArgs e)
        {
            datagridOldPeriods.ItemsSource = UserService.Instance.getPastPeriods().DefaultView;
            datagridOldPeriods.Columns[0].Visibility = Visibility.Hidden;
            datagridOldPeriods.Columns[1].Visibility = Visibility.Hidden;
        }
        private void ClickNewPeriod(object sender, RoutedEventArgs e)
        {
            NewPeriod Np = new NewPeriod();
            Np.Show();
            Close();
        }
        private void clickViewOld(object sender, RoutedEventArgs e)
        {
            if (datagridOldPeriods.SelectedItem != null)
            {
                DataRowView row = datagridOldPeriods.SelectedItem as DataRowView;

                DataRowView dataRow = (DataRowView)datagridOldPeriods.SelectedItem;
                int periodId = (int)dataRow.Row.ItemArray[0];
                UserService.Instance.SelectedPeriod = periodId;

                Showroom show = new Showroom();
                show.Show();
                Close();
            }
            else
            {
                MessageBox.Show("choose the periode you want specified");
            }

        }
        private void clickLogout(object sender, RoutedEventArgs e)
        {
            UserService.Instance.logUserOut();

            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
        private void datagridOldPeriods_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
                if (dataGridTextColumn != null)
                {
                    dataGridTextColumn.Binding.StringFormat = "{0:d}";
                }
            }
            if (e.PropertyType == typeof(Double))
            {
                DataGridTextColumn datagridTextColumn = e.Column as DataGridTextColumn;
                if(datagridTextColumn != null)
                {
                    datagridTextColumn.Binding.StringFormat = "{0:F2}";
                }
            }
        }
        private void buttonChangesClicked(object sender, RoutedEventArgs e)
        {
            Window up = new UpdatePeriod();
            up.Show();
            Close();
        }
    }
}
