using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public EstimateActivities()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridEstimateActivity.ItemsSource = PeriodService.Instance.EstimateactivityList().DefaultView;
            DataGridEstimateActivity.Columns[0].IsReadOnly = true;

            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to cancel?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                PeriodService.Instance.canselEverything();
                Window ov = new Overview();
                ov.Show();
                Close();
            }
        }
    }
}
