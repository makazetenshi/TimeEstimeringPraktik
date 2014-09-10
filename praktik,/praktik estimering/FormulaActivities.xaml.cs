using System;
using System.Collections.Generic;
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
            DataGridFormula.Columns[0].Visibility = Visibility.Hidden;

        }

    }
}
