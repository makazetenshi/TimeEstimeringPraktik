using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for NewDayActivity.xaml
    /// </summary>
    public partial class DayActivity : Window
    {
        public DayActivity()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridDayActivity.ItemsSource = PeriodService.Instance.DayactivityList().DefaultView;
            DataGridDayActivity.Columns[0].IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DataView dv = (DataView) DataGridDayActivity.ItemsSource;
            DataTable dt = cloneDataTable(dv);

            if (PeriodService.Instance.InsertDayActivities(dt))
            {
                Window EstimateActivities = new EstimateActivities();
                EstimateActivities.Show();
                this.Close();
            }
        }

        private DataTable cloneDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PeriodService.Instance.canselEverything();
        }
    }
}
