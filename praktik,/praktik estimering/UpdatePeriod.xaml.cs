using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace praktik_estimering
{
    /// <summary>
    /// Interaction logic for UpdatePeriod.xaml
    /// </summary>
    public partial class UpdatePeriod : Window
    {
        public UpdatePeriod()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxPeriod.ItemsSource = UpdateService.Instance.getPeriods().DefaultView;
            comboboxPeriod.SelectedValuePath = "Id";
            comboboxPeriod.DisplayMemberPath = "Show";
            comboboxPeriod.SelectedIndex = 0;

            DatagridDay.ItemsSource = UpdateService.Instance.allTables.Tables[0].DefaultView;
            DatagridEstimate.ItemsSource = UpdateService.Instance.allTables.Tables[1].DefaultView;
            DatagridFormula.ItemsSource = UpdateService.Instance.allTables.Tables[2].DefaultView;
            DatagridExamn.ItemsSource = UpdateService.Instance.allTables.Tables[3].DefaultView;

            DatagridDay.Columns[0].Visibility = Visibility.Hidden;
            DatagridDay.Columns[1].IsReadOnly = true;
        }
        private void ButtonDoneClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("If you do not press update the new data will not be saved. " +
                                                   "Do you wish to continue?", "Confirmation", MessageBoxButton.YesNo);
            if (mbr == MessageBoxResult.Yes)
            {
                Window over = new Overview();
                over.Show();
                Close();
            }
        }
        private void ButtonUpdateClicked(object sender, RoutedEventArgs e)
        {
            UpdateService.Instance.updatePeriod();
        }
        private void comboboxPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService.Instance.getDataSet((int)comboboxPeriod.SelectedValue);
            UpdateService.Instance.selectedPeriod = (int) comboboxPeriod.SelectedValue;
        }
        private void dayGotFocus(object sender, RoutedEventArgs e)
        {
            if (DatagridDay.Columns.Count > 0)
            {
                DatagridDay.Columns[0].Visibility = Visibility.Hidden;
                DatagridDay.Columns[1].IsReadOnly = true;
            }
        }
        private void estimateGotFocus(object sender, RoutedEventArgs e)
        {
            if (DatagridEstimate.Columns.Count > 0)
            {
                DatagridEstimate.Columns[0].Visibility = Visibility.Hidden;
                DatagridEstimate.Columns[1].IsReadOnly = true;
            }
        }
        private void PrakticalGotFocus(object sender, RoutedEventArgs e)
        {
            if (DatagridFormula.Columns.Count > 0)
            {
                DatagridFormula.Columns[0].Visibility = Visibility.Hidden;
                DatagridFormula.Columns[1].IsReadOnly = true;
            }
        }
        private void examGotFocus(object sender, RoutedEventArgs e)
        {
            if (DatagridExamn.Columns.Count > 0) { 
                DatagridExamn.Columns[0].Visibility = Visibility.Hidden;
                DatagridExamn.Columns[1].IsReadOnly = true;
            }
        }
    }
}
