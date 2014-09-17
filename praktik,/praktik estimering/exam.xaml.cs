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
    /// Interaction logic for exam.xaml
    /// </summary>
    public partial class exam : Window
    {
        public exam()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datagridExam.ItemsSource = PeriodService.Instance.FormulaList().DefaultView;
            datagridExam.Columns[0].Visibility = Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = (DataView)datagridExam.ItemsSource;
            DataTable dt = cloneTable(dv);

            if (PeriodService.Instance.InsertFormulaActivities(dt))
            {
                Showroom sr = new Showroom();
                sr.Show();
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
    }
}
