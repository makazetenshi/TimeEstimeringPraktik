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
    /// Interaction logic for DayAktivities.xaml
    /// </summary>
    public partial class DayAktivities : Window
    {
        public DayAktivities()
        {
            InitializeComponent();
        }

        private void ButtonAddClicked(object sender, RoutedEventArgs e)
        {
            PeriodService.Instance.addToDayList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //add daylist to list box
            listboxActivities.ItemsSource= PeriodService.Instance.
        }



    }
}
