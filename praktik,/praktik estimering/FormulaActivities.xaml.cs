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
    /// Interaction logic for FormulaActivities.xaml
    /// </summary>
    public partial class FormulaActivities : Window
    {
        public FormulaActivities()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridFormula.ItemsSource = PeriodService.Instance.FormulaList().DefaultView;
            DataGridFormula.Columns[0].IsReadOnly = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = (DataView)DataGridFormula.ItemsSource;
            DataTable dt = cloneTable(dv);

            if (PeriodService.Instance.InsertFormulaActivities(dt))
            {
                exam ex = new exam();
                ex.Show();
                Close();
            }
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PeriodService.Instance.canselEverything();
        }

    }
}
