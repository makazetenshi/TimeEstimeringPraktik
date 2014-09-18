using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            
            DatagridDay.ItemsSource = UpdateService.Instance.Day.DefaultView;
            DatagridDay.Columns[0].Visibility = Visibility.Hidden;
            DatagridDay.Columns[1].IsReadOnly = true;

            DatagridEstimate.ItemsSource = UpdateService.Instance.Esti.DefaultView;
            Debug.WriteLine(DatagridEstimate.Columns.Count());
            //DatagridEstimate.Columns[0].Visibility = Visibility.Hidden;
            
            /*DatagridEstimate.Columns[0].Visibility = Visibility.Hidden;
            DatagridEstimate.Columns[1].IsReadOnly = true;*/

            DatagridFormula.ItemsSource = UpdateService.Instance.Form.DefaultView;
            /*DatagridFormula.Columns[0].Visibility = Visibility.Hidden;
            DatagridFormula.Columns[1].IsReadOnly = true;*/

            DatagridExamn.ItemsSource = UpdateService.Instance.Exam.DefaultView;
            /*DatagridExamn.Columns[0].Visibility = Visibility.Hidden;
            DatagridExamn.Columns[1].IsReadOnly = true;*/

            

        }
        private void ButtonDoneClicked(object sender, RoutedEventArgs e)
        {
            Window over = new Overview();
            over.Show();
            Close();
        }
        private void ButtonUpdateClicked(object sender, RoutedEventArgs e)
        {

        }

        private void comboboxPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService.Instance.getDataSet((int)comboboxPeriod.SelectedValue);
        }

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
