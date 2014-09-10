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
    /// Interaction logic for EstimateActivities.xaml
    /// </summary>
    public partial class EstimateActivities : Window
    {
        public EstimateActivities()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridEstimateActivity.ItemsSource = PeriodService.Instance.EstimateactivityList().DefaultView;
            DataGridEstimateActivity.Columns[0].Visibility = Visibility.Hidden;
            DataGridEstimateActivity.Columns[1].IsReadOnly = true;


        }

        private DataTable cloneTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataView dv = (DataView)DataGridEstimateActivity.ItemsSource;
            DataTable dt = cloneTable(dv);

            if (PeriodService.Instance.InsertestimationActivities(dt))
            {
                FormulaActivities fa = new FormulaActivities();
                fa.Show();
                Close();
            }
        }
    }
}
